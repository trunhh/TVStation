﻿
namespace TVStation.Data.DTO.Plans
{
    public class ProductionRegistrationDTO
    {
        public string Sector { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool IsPersonal { get; set; }
        public DateTime Airdate { get; set; }
        public string Genre { get; set; } = string.Empty;
    }
}
