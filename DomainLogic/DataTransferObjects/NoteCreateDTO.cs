using PastBinFake.SPA.Enums;

namespace PastBinFake.SPA.DomainLogic.DataTransferObjects
{
    public class NoteCreateDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DurationEnum Duration { get; set; }
    }
}