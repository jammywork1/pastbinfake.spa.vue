using System;

namespace PastBinFake.SPA.DomainLogic.UrlGenerators
{
    public class GuidUrlGenerator : IShortUrlGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToLower();
        }
    }
}