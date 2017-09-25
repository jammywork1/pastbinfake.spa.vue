using System.Collections.Generic;
using System.Collections.Concurrent;
using PastBinFake.SPA.DataAccessLayer.Entities;

namespace PastBinFake.SPA.DataAccessLayer.Memory
{
    public class MemoryContext
    {
//        private static readonly HashSet<INoteEntity> _noteEntities =
//            new HashSet<INoteEntity>(new NoteEqualityComparer());
//
//        public HashSet<INoteEntity> Notes => _noteEntities;

        private static readonly ConcurrentDictionary<string, INoteEntity> _noteEntities = new ConcurrentDictionary<string, INoteEntity>();
        public ConcurrentDictionary<string, INoteEntity> Notes => _noteEntities;
    }
}