using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PastBinFake.SPA.DataAccessLayer.Entities;
using PastBinFake.SPA.DataAccessLayer.Exceptions;

namespace PastBinFake.SPA.DataAccessLayer.Memory
{
    public class MemoryNoteRepository : INoteRepository
    {
        private readonly MemoryContext _context;

        public MemoryNoteRepository(MemoryContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<INoteEntity>> GetActualNotesSlice(int take, int skip)
        {
            if (take <= 0) throw new ArgumentOutOfRangeException(nameof(take));
            if (skip < 0) throw new ArgumentOutOfRangeException(nameof(skip));
            return Task.FromResult(_context.Notes
                .Select(m => m.Value)
                .Where(i => !i.ExpiredDateTime.HasValue || i.ExpiredDateTime > DateTime.Now)
                .Skip(skip).Take(take));
        }

        public Task Create(INoteEntity noteEntity)
        {
            if (noteEntity == null) throw new ArgumentNullException(nameof(noteEntity));
            if (!_context.Notes.TryAdd(noteEntity.Url, noteEntity))
                return Task.FromException(new CreateEntityException());
            return Task.CompletedTask;
        }

        public Task Update(INoteEntity noteEntity)
        {
            if (!_context.Notes.ContainsKey(noteEntity.Url))
                return Task.FromException<INoteEntity>(new EntityNotFoundException());
            var entity = _context.Notes[noteEntity.Url];
            entity.Title = noteEntity.Title;
            entity.Body = noteEntity.Body;
            //_context.Notes.TryUpdate
            return Task.CompletedTask;
        }

        public Task Delete(INoteEntity noteEntity)
        {
            if (_context.Notes.TryRemove(noteEntity.Url, out noteEntity))
                return Task.CompletedTask;
            return Task.FromException<INoteEntity>(new EntityNotFoundException());
        }

        public Task<INoteEntity> GetNoteByUrl(string url)
        {
            if (!_context.Notes.ContainsKey(url))
                return Task.FromException<INoteEntity>(new EntityNotFoundException());
            var entity = _context.Notes[url];
            return Task.FromResult(entity);
        }
    }
}