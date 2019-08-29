using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetNote.Models;
using NetNote.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetNote
{
    [Route("api/[controller]/{action}")]
    public class NoteController : Controller
    {
        private INoteRepository noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<List<Note>> GetAsync()
        {
            return await noteRepository.ListAsync();
        }

    }
}
