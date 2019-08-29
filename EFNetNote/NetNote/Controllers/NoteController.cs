using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetNote.Repository;
using NetNote.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetNote.Controllers
{
    public class NoteController : Controller
    {
        private INoteRepository noteRepository;
        private INoteTypeRepository noteTypeRepository;

        public NoteController(INoteRepository noteRepository,INoteTypeRepository noteTypeRepository) {
            this.noteRepository = noteRepository;
            this.noteTypeRepository = noteTypeRepository;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var notes = await noteRepository.ListAsync();
            return View(notes);
        }

        public async Task<IActionResult> Add() {
            var types = await noteTypeRepository.ListAsync();
            ViewBag.Types = types.Select(r => new SelectListItem { Text = r.Name, Value = r.Id.ToString() });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(NoteModel model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            await noteRepository.AddAsync(new Models.Note()
            {
                Title = model.Title,
                Content = model.Content,
                Create = DateTime.Now,
                TypeId = model.Type
            });

            return RedirectToAction("Index");
        }
    }
}
