﻿namespace OutboxPatternAPI.Helper
{
    public class EmailSettings
    {
        public const string SectionName = "EmailSettings";
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
