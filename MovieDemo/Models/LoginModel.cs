using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieDemo.Models
{
    public class LoginModel
    {
        [DisplayName("User name")]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}