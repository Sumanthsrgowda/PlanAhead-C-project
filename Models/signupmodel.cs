using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class signupmodel
    {
        [Display(Name ="RegisterNo")]
        public int regno { get; set; }
        [Display(Name = "Name")]
        public string stdname { get; set; }
        [Display(Name = "Branch")]

        public string stdbranch { get; set; }

        public string stdyear { get; set; }

        public int stdvalid { get; set; }
        [EmailAddress]
        public string email { get; set; }

        public string password { get; set; }
    }
}
