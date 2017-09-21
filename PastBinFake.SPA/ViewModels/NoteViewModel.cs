using System;

namespace PastBinFake.SPA.ViewModels
{
    public class NoteViewModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ExpiredDateTime { get; set; }
    }
}