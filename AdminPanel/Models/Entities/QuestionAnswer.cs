﻿using AdminPanel.Models.Base;
using AdminPanel.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Models.Entities;

public sealed class QuestionAnswer : Auditable
{
    public int Id { get; set; }

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;

    public string Text { get; set; } = null!;
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public bool IsActive { get; set; }

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;

    public void Configure(EntityTypeBuilder<QuestionAnswer> builder)
    {
        builder.HasOne(d => d.Question)
                .WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientCascade);
    }
}