using PastBinFake.SPA.Enums;

namespace PastBinFake.SPA.ViewModels
{
    public class NoteCreateViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DurationEnum Duration { get; set; }
    }
}