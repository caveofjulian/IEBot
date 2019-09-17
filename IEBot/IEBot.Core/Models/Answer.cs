using System;

namespace IEBot.Models
{
    public class Answer
    {
        public Answer(Guid questionId, string description, DateTime time, ulong userId)
        {
            QuestionId = questionId;
            Description = description;
            Time = time;
            UserId = userId;
        }

        public Guid QuestionId { get; set; }

        public string Description { get; set; }

        public DateTime Time { get; set; }

        public ulong UserId { get; set; }
    }
}
