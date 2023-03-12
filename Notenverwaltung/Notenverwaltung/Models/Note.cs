using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Notenverwaltung.Models
{
    public class Note
    {
        public int id { get; set; }
        [Display(Name = "Note")]
        public int note { get; set; }
        [Display(Name = "Gewichtung")]
        public int gewichtung { get; set; }
        public int fachId { get; set; }
    }
}
