using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DCSCoreMvc.Data.Models
{
  [JsonObject(MemberSerialization.OptOut)]
  public class BaseModel
  {
    [JsonIgnore]
    public int Id { get; set; }

    // [Required]
    [JsonIgnore]
    public DateTimeOffset CreatedDate { get; set; }

    // [Required]
    [JsonIgnore]
    public DateTimeOffset? LastModifiedDate { get; set; }

    [JsonIgnore]
    public bool Deleted { get; set; }
  }
}
