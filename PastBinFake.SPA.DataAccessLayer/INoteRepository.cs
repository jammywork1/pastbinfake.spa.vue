using System.Collections.Generic;
using System.Threading.Tasks;
using PastBinFake.SPA.DataLayer.Entities;

namespace PastBinFake.SPA.DataLayer
{
    public interface INoteRepository
    {
        Task<INoteEntity> GetNoteByUrl(string url);
        Task<IEnumerable<INoteEntity>> GetActualNotesSlice(int take, int skip);
        Task Create(INoteEntity noteEntity);
    }
}