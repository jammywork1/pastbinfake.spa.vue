using System.Collections.Generic;
using PastBinFake.SPA.DataLayer.Entities;

namespace PastBinFake.SPA.DataLayer.Memory
{
    public class MemoryContext
    {
        private static readonly HashSet<INoteEntity> _noteEntities = new HashSet<INoteEntity>(new NoteEqualityComparer());

        public HashSet<INoteEntity> Notes => _noteEntities;
    }
}