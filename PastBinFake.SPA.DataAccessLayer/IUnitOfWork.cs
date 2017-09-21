namespace PastBinFake.SPA.DataAccessLayer
{
    public interface IUnitOfWork
    {
        INoteRepository NoteRepository { get; }
    }
}