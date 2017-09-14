using System;
using PastBinFake.SPA.DataLayer.Entities;

namespace PastBinFake.SPA.DomainLogic.Models
{
    public class NoteDomainModel : INoteEntity
    {
        public string Url { get; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDateTime { get; }
        public DateTime? ExpiredDateTime { get; }
    }
}