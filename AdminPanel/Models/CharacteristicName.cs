using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class CharacteristicName : IEntityTypeConfiguration<CharacteristicName>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Characteristic>? Characteristics { get; set; }


        public void Configure(EntityTypeBuilder<CharacteristicName> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
