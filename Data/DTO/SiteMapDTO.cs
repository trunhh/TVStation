﻿using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.DTO
{
    public class SiteMapDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
