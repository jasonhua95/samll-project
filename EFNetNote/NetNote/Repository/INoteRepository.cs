using Microsoft.EntityFrameworkCore;
using NetNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNote.Repository
{
    /// <summary>
    /// 仓储操作数据库
    /// </summary>
    public interface INoteRepository
    {
        Task<Note> GetByIdAsync(int id);
        Task<List<Note>> ListAsync();
        Task AddAsync(Note note);
        Task UpdateAsync(Note note);
    }

    /// <summary>
    /// 实现笔记功能
    /// </summary>
    public class NoteRepository : INoteRepository
    {
        private NoteContext context;
        public NoteRepository(NoteContext noteContext) {
            this.context = noteContext;
        }

        public Task AddAsync(Note note)
        {
            context.Notes.Add(note);
            return context.SaveChangesAsync();
        }

        public Task<Note> GetByIdAsync(int id)
        {
            return context.Notes.FirstOrDefaultAsync(r => r.Id == id);
        }

        public Task<List<Note>> ListAsync()
        {
            return context.Notes.ToListAsync();
        }

        public Task UpdateAsync(Note note)
        {
            context.Entry(note).State = EntityState.Modified;
            return context.SaveChangesAsync();
        }
    }
}
