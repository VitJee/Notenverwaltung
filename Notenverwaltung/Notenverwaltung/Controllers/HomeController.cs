using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notenverwaltung.Models;
using System.Diagnostics;

namespace Notenverwaltung.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NotenverwaltungDB _context;

        public HomeController(ILogger<HomeController> logger, NotenverwaltungDB context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            DatenViewModel.instance.initialisiereDB(_context);
            return View();
        }

        [HttpPost]
        public IActionResult Anmelden(AnmeldenViewModel anmeldenViewModel)
        {
            if (anmeldenViewModel.benutzerName.IsNullOrEmpty() || anmeldenViewModel.benutzerPasswort.IsNullOrEmpty())
            {
                TempData["AnmeldenMessage"] = "Sie müssen in beiden Felder etwas eingeben!";
            }
            else
            {
                if (anmeldenViewModel.sindAnmeldeDatenRichtig(_context))
                {
                    DatenViewModel.instance.benutzerId = anmeldenViewModel.getBenutzerId(_context);
                    return RedirectToAction("Index", "Fach");
                }
                else
                {
                    TempData["AnmeldenMessage"] = "Der Benutzername oder das Passwort ist falsch!";
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Registrieren()
        {
            return RedirectToAction("Create", "Benutzer");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}