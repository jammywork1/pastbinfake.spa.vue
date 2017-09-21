using System.Collections.Generic;
using PastBinFake.SPA.DataAccessLayer.Entities;

namespace PastBinFake.SPA.DataAccessLayer
{
    public class NoteEqualityComparer : IEqualityComparer<INoteEntity>
    {
        public bool Equals(INoteEntity x, INoteEntity y)
        {
            return x.Url == y.Url;
        }

        public int GetHashCode(INoteEntity obj)
        {
            return obj.Url.GetHashCode();
        }
    }
}