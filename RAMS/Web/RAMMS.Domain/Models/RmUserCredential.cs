using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.Domain.Models
{
    public class RmUserCredential
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UsrUserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string UsrPassword { get; set; }
    }
}
