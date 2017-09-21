using System;

namespace PastBinFake.SPA.DataAccessLayer.Memory
{
    public class MemoryUnitOfWork : IUnitOfWork
    {
        private readonly MemoryContext _context;
        public MemoryUnitOfWork(MemoryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            NoteRepository = new MemoryNoteRepository(_context);
        }

        public INoteRepository NoteRepository { get; }
    }
}