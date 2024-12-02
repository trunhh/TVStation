﻿using System.ComponentModel.DataAnnotations;

namespace TVStation.Data.DTO
{
    public class UserDTO
    {
        public string? Email { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
