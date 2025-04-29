using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace WebApplication1.Models
{
    public class eventmodel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventDept { get; set; }
        public string Description { get; set; }
        public string? Otherdept { get; set; }
        public string Date { get; set; }
        public string Response { get; set; }
        [Display(Name = "Poster")]
        public IFormFile coverphoto { get; set; }
        
        public string Poster { get; set; }
        public string Report { get; set; }
        public string Status { get; set; }


    }
}
