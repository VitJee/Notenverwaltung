namespace Notenverwaltung.Models
{
    public sealed class DatenViewModel
    {
        private static readonly DatenViewModel _viewModel = new DatenViewModel();
        private DatenViewModel() { }
        public static DatenViewModel instance { get { return _viewModel; } }
        private NotenverwaltungDB _context { get; set; }
        public int benutzerId { get; set; }
        public int fachId { get; set; }
        public string fachName { get; set; }

        public void initialisiereDB(NotenverwaltungDB context)
        {
            _context = context;
        }

        public Benutzer? getAngemeldeterBenutzer()
        {
            Benutzer angemeldeterBenutzer = null;
            _context.Benutzer.ToList().ForEach(benutzer =>
            {
                if (benutzer.id == benutzerId)
                {
                    angemeldeterBenutzer = benutzer;
                }
            });
            return angemeldeterBenutzer;
        }

        public double getNotenDurchschnitt(int fachId)
        {
            double notenDurchschnitt = 0.0;
            int anzahl = 0;
            _context.Note.ToList().ForEach(note =>
            {
                notenDurchschnitt += note.note * note.gewichtung;
                anzahl++;
            });
            notenDurchschnitt /= anzahl;
            return notenDurchschnitt;
        }
    }
}
