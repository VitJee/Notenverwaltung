using System.ComponentModel.DataAnnotations;

namespace Notenverwaltung.Models
{
    public class Benutzer
    {
        public int id { get; set; }
        [Display(Name = "Name")]
        public string? benutzerName { get; set; }
        [Display(Name = "Passwort")]
        public string? benutzerPasswort { get; set; }
    }
}
