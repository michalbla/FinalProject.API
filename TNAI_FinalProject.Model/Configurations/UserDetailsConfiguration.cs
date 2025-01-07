using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI_FinalProject.Model.Entities;

namespace TNAI_FinalProject.Model.Configurations
{
    public class UserDetailsConfiguration : IEntityTypeConfiguration<UserDetails>
    {
        public void Configure(EntityTypeBuilder<UserDetails> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IsHandicaped).IsRequired();
            builder.Property(x => x.Position).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Payment).IsRequired().HasMaxLength(100000);
            builder.Property(x => x.HasChilldren).IsRequired();
            builder.Property(x => x.Age).HasMaxLength(100);

            builder.HasOne(x => x.User).WithMany(x => x.Details).HasForeignKey(x => x.UserId);
        }
    }
}
