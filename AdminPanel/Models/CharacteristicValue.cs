using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class CharacteristicValue : IEntityTypeConfiguration<CharacteristicValue>
    {
        public int Id { get; set; }
        public string Value { get; set; } = null!;

        public ICollection<Characteristic>? Characteristics { get; set; }


        public void Configure(EntityTypeBuilder<CharacteristicValue> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
