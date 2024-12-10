using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class MapperExtensions
{
    private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> TypePropertiesCache = new();

    public static TTarget Map<TSource, TTarget>(this TSource source)
        where TTarget : new()
    {
        if (source == null)
            return default;

        TTarget target = new();
        var sourceProperties = GetPropertiesForType(typeof(TSource));

        foreach (var targetProperty in typeof(TTarget).GetProperties())
        {
            if (!targetProperty.CanWrite || targetProperty.GetMethod.IsStatic)
                continue;

            if (sourceProperties.TryGetValue(targetProperty.Name, out PropertyInfo sourceProperty))
            {
                if (sourceProperty.GetMethod.IsStatic)
                    continue;

                if (sourceProperty.GetIndexParameters().Length == 0)
                {
                    var sourceValue = sourceProperty.GetValue(source);
                    SetTargetPropertyValue(target, targetProperty, sourceProperty, sourceValue);
                }
            }
            else
            {
                SetDefaultValue(target, targetProperty);
            }
        }

        return target;
    }

    private static void SetTargetPropertyValue(object target, PropertyInfo targetProperty, PropertyInfo sourceProperty, object sourceValue)
    {
        if (sourceValue == null)
        {
            // Preserve existing default values for non-nullable types or initialize to empty string for strings
            if (targetProperty.PropertyType == typeof(string))
            {
                if (targetProperty.GetValue(target) == null)
                    targetProperty.SetValue(target, string.Empty);
            }
            // Skip setting value if the default is already in place
            return;
        }

        if (targetProperty.PropertyType == sourceProperty.PropertyType)
        {
            targetProperty.SetValue(target, sourceValue);
        }
        else if (IsCollectionType(sourceProperty.PropertyType, targetProperty.PropertyType, out var sourceItemType, out var targetItemType))
        {
            var sourceCollection = (IEnumerable)sourceValue;
            var targetCollection = MapCollection(sourceItemType, targetItemType, sourceCollection);
            targetProperty.SetValue(target, targetCollection);
        }
        else if (IsComplexType(targetProperty.PropertyType))
        {
            var nestedTarget = MapNested(sourceProperty.PropertyType, targetProperty.PropertyType, sourceValue);
            targetProperty.SetValue(target, nestedTarget);
        }
    }

    private static void SetDefaultValue(object target, PropertyInfo targetProperty)
    {
        // Skip resetting if the property already has a non-default value
        var currentValue = targetProperty.GetValue(target);
        if (currentValue == null || Equals(currentValue, GetDefaultValue(targetProperty.PropertyType)))
        {
            targetProperty.SetValue(target, GetDefaultValue(targetProperty.PropertyType));
        }
    }


    private static object MapNested(Type sourceType, Type targetType, object sourceValue)
    {
        if (sourceValue == null)
            return null;

        var mapMethod = typeof(MapperExtensions).GetMethod(nameof(Map), BindingFlags.Static | BindingFlags.Public)
            ?.MakeGenericMethod(sourceType, targetType);

        return mapMethod?.Invoke(null, new[] { sourceValue });
    }

    private static object MapCollection(Type sourceItemType, Type targetItemType, IEnumerable sourceCollection)
    {
        var listType = typeof(List<>).MakeGenericType(targetItemType);
        var targetCollection = (IList)Activator.CreateInstance(listType);

        var mapMethod = typeof(MapperExtensions).GetMethod(nameof(Map), BindingFlags.Static | BindingFlags.Public)
            ?.MakeGenericMethod(sourceItemType, targetItemType);

        foreach (var item in sourceCollection)
        {
            var mappedItem = mapMethod?.Invoke(null, new[] { item });
            targetCollection?.Add(mappedItem);
        }

        return targetCollection;
    }

    private static object GetDefaultValue(Type type)
    {
        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }

    private static Dictionary<string, PropertyInfo> GetPropertiesForType(Type type)
    {
        if (!TypePropertiesCache.TryGetValue(type, out var properties))
        {
            properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead)
                .ToDictionary(p => p.Name, p => p);

            TypePropertiesCache[type] = properties;
        }

        return properties;
    }

    private static bool IsCollectionType(Type sourceType, Type targetType, out Type sourceItemType, out Type targetItemType)
    {
        sourceItemType = null;
        targetItemType = null;

        if (sourceType.IsGenericType && targetType.IsGenericType)
        {
            var sourceGenericType = sourceType.GetGenericTypeDefinition();
            var targetGenericType = targetType.GetGenericTypeDefinition();

            if ((sourceGenericType == typeof(IEnumerable<>) || sourceType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))) &&
                (targetGenericType == typeof(IEnumerable<>) || targetType.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))))
            {
                sourceItemType = sourceType.GetGenericArguments()[0];
                targetItemType = targetType.GetGenericArguments()[0];
                return true;
            }
        }

        return false;
    }

    private static bool IsComplexType(Type type)
    {
        return !type.IsPrimitive && type != typeof(string);
    }
}
