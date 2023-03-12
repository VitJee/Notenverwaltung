using Microsoft.IdentityModel.Tokens;

namespace Notenverwaltung.Models
{
    public class AnmeldenViewModel
    {
        public string? benutzerName { get; set; }
        public string? benutzerPasswort { get; set; }

        public bool sindAnmeldeDatenRichtig(NotenverwaltungDB context)
        {
            List<Benutzer> alleBenutzer = context.Benutzer.ToList();
            bool anmeldeDatenSindRichtig = false;
            alleBenutzer.ForEach(benutzer =>
            {
                if (benutzer.benutzerName == benutzerName && benutzer.benutzerPasswort == benutzerPasswort)
                {
                    anmeldeDatenSindRichtig = true;
                }
            });
            return anmeldeDatenSindRichtig;
        }

        public int getBenutzerId(NotenverwaltungDB context)
        {
            int idWert = -1;
            context.Benutzer.ToList().ForEach(benutzer =>
            {
                if (benutzer.benutzerName == benutzerName && benutzer.benutzerPasswort == benutzerPasswort)
                {
                    idWert = benutzer.id;
                }
            });
            return idWert;
        }
    }
}
