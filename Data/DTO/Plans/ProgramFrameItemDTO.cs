﻿namespace TVStation.Data.DTO.Plans
{
    public class ProgramFrameItemDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
