using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace DCSCoreMvc.Data.Models
{
  public class BabyListEntry : BaseModel
  {
    [JsonIgnore]
    public bool Client { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Nr { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Phone { get; set; }
  }

  public class BabyListEntryConfiguration : IEntityTypeConfiguration<BabyListEntry>
  {
    public void Configure(EntityTypeBuilder<BabyListEntry> builder)
    {
      builder
        .Property(u => u.CreatedDate)
        .HasDefaultValue(DateTimeOffset.Now);
      builder
        .Property(u => u.LastModifiedDate)
        .HasDefaultValue(DateTimeOffset.Now);
    }
  }
}
