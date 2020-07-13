using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DCSCoreMvc.Models
{
  public class BabyListViewModel
  {
    [Required]
    [EmailAddress]
    public string Email
    {
      get; set;
    }

    [Required]
    public string Name
    {
      get; set;
    }


    [Required]
    public string Address { get; set; }
    [Required]
    public string Nr { get; set; }
    [Required]
    public string PostalCode { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string Phone { get; set; }
  }

}
