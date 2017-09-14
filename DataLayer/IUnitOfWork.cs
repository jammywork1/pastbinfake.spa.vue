namespace PastBinFake.SPA.DataLayer
{
    public interface IUnitOfWork
    {
        INoteRepository NoteRepository { get; }
    }
}