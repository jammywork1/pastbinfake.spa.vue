using System.Collections.Generic;
using PastBinFake.SPA.DataAccessLayer.Entities;

namespace PastBinFake.SPA.DataAccessLayer.Memory
{
    public class MemoryContext
    {
        private static readonly HashSet<INoteEntity> _noteEntities = new HashSet<INoteEntity>(new NoteEqualityComparer());

        public HashSet<INoteEntity> Notes => _noteEntities;
    }
}