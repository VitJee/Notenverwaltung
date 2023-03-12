using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notenverwaltung.Models;

    public class NotenverwaltungDB : DbContext
    {
        public NotenverwaltungDB (DbContextOptions<NotenverwaltungDB> options)
            : base(options)
        {
        }

        public DbSet<Notenverwaltung.Models.Benutzer> Benutzer { get; set; } = default!;

        public DbSet<Notenverwaltung.Models.Fach> Fach { get; set; } = default!;

        public DbSet<Notenverwaltung.Models.Note> Note { get; set; } = default!;
    }
