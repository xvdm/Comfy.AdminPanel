using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Answer : IEntityTypeConfiguration<Answer>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; } = null!;
        public int UsefullAnswerCount { get; set; }
        public int NeedlesslyAnswerCount { get; set; }
        public bool IsActive { get; set; }

        public int TargetId { get; set; }
        public Question TargetQuestion { get; set; } = null!;
        public Review TargetReview { get; set; } = null!;


        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(d => d.TargetQuestion)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.TargetId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(d => d.TargetReview)
                .WithMany(p => p.Answers)
                .HasForeignKey(d => d.TargetId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
