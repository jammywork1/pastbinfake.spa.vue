using System.Collections.Generic;
using System.Threading.Tasks;
using PastBinFake.SPA.DataAccessLayer.Entities;

namespace PastBinFake.SPA.DataAccessLayer
{
    public interface INoteRepository
    {
        Task<INoteEntity> GetNoteByUrl(string url);
        Task<IEnumerable<INoteEntity>> GetActualNotesSlice(int take, int skip);
        Task Create(INoteEntity noteEntity);
        Task Update(INoteEntity noteEntity);
        Task Delete(INoteEntity noteEntity);
    }
}