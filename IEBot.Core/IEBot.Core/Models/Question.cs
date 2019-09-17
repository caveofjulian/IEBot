using System;
using System.Collections.Generic;
using System.Text;

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

        public Guid GUID { get; }
        public string Description { get; set; }

        public DateTime Time { get; set; }

        public ulong UserId { get; set; }

        public bool Answered { get; set; } = false;
    }
}
