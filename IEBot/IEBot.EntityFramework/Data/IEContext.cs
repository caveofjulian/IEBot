using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace IEBot.EntityFramework.Data
{
    public class IEContext : DbContext
    {
        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<Answer> Answers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseMySQL(@"Server=localhost;Database=IEBot;Uid=julian;Pwd=hoihoi123");
        }
    }
}
