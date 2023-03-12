using System.ComponentModel.DataAnnotations;

namespace Notenverwaltung.Models
{
    public class Fach
    {
        public int id { get; set; }
        [Display(Name = "Fach")]
        public string fachName { get; set; }
        public int benutzerId { get; set; }
    }
}
