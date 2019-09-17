using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IEBot.EntityFramework.Data
{
    public class Question
    {
        [Key()]
        public Guid Id { get; set; }

        public string Description { get; set; }

        public DateTime Time { get; set; }

        public ulong UserId { get; set; }
    }
}
