using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceManagement.Models
{
    public class AddresseeConfig : IEntityTypeConfiguration<Addressee>
    {
        public void Configure(EntityTypeBuilder<Addressee> builder)
        {
            builder.HasKey(c => new { c.AddresseeId });
        }
    }
}
