using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IEBot.EntityFramework.Data
{
    public class Answer
    {
        [Key()]
        public int Id { get; set; }

        public Guid QuestionId { get; set; }

        public string Description { get; set; }

        public DateTime Time { get; set; }

        public ulong UserId { get; set; }
    }
}
