using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DCSCoreMvc.Models.AccountViewModels;

namespace DCSCoreMvc.Models
{
  public class RemoveFromListViewModel : LoginViewModel
  {
    [Required]
    [EmailAddress]
    public string EmailTooRemove
    {
      get; set;
    }

  }

}
