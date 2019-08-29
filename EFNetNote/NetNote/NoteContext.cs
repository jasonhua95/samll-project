using Microsoft.EntityFrameworkCore;
using NetNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote
{
    /// <summary>
    /// EF
    /// </summary>
    public class NoteContext:DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }

        public DbSet<NoteType> NoteTypes { get; set; }
    }
}
