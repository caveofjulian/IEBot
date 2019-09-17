using System;

namespace IEBot.Core.Models
{
    public class Question
    {
        public Question(string description, DateTime time, ulong id)
        {
            Description = description;
            Time = time;
            UserId = id;

            GUID = Guid.NewGuid();
        }

        public Question(string description, DateTime time, ulong id, Guid guid, bool answered)
        {
            Description = description;
            Time = time;
            UserId = id;
            GUID = guid;
            Answered = answered;
        }

        public Guid GUID { get; }
        public string Description { get; set; }

        public DateTime Time { get; set; }

        public ulong UserId { get; set; }

        public bool Answered { get; set; } = false;
    }
}
