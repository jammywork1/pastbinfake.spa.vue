using System;

namespace PastBinFake.SPA.DataLayer.Entities
{
    public interface INoteEntity
    {
        string Url { get; }
        string Title { get; set; }
        string Body { get; set; }
        DateTime CreatedDateTime { get; }
        DateTime? ExpiredDateTime { get; }

    }
}