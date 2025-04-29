using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApplication1.Models
{
     
        public class loginmodel
        {
            public string UserName { get; set; }
            [Display(Name = "Email")]
            [EmailAddress]
            public string email { get; set; }
            [Display(Name = "Password")]
            public string password { get; set; }
        
            public int role { get; set; }
        }
    
}