using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PastBinFake.SPA.DataLayer.Entities;
using PastBinFake.SPA.DataLayer.Exceptions;

namespace PastBinFake.SPA.DataLayer.Memory
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
                .Where(i => !i.ExpiredDateTime.HasValue || i.ExpiredDateTime > DateTime.Now)
                .Skip(skip).Take(take));
        }

        public Task Create(INoteEntity noteEntity)
        {
            if (noteEntity == null) throw new ArgumentNullException(nameof(noteEntity));
            if (!_context.Notes.Add(noteEntity))
                return Task.FromException(new CreateEntityException());
            return Task.CompletedTask;
        }

        public Task<INoteEntity> GetNoteByUrl(string url)
        {
            var entity = _context.Notes.FirstOrDefault(i => i.Url == url);
            if (entity == null)
                return Task.FromException<INoteEntity>(new EntityNotFoundException());
            return Task.FromResult(entity);
        }
    }
}