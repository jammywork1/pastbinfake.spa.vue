using System;
using PastBinFake.SPA.DataAccessLayer.Entities;

namespace PastBinFake.SPA.DomainLogic.Models
{
    public class NoteCreateDomainModel : INoteEntity
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ExpiredDateTime { get; set; }
    }
}