using Microsoft.EntityFrameworkCore;
using NetNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Repository
{
    public interface INoteTypeRepository
    {
        Task<List<NoteType>> ListAsync();
    }

    public class NoteTypeRepository : INoteTypeRepository
    {
        private NoteContext content;
        public NoteTypeRepository(NoteContext noteContext)
        {
            content = noteContext;
        }

        public Task<List<NoteType>> ListAsync() {
            return content.NoteTypes.ToListAsync();
        }
    }
}
