using System;

namespace PastBinFake.SPA.DomainLogic.UrlGenerators
{
    public class GuidShortUrlGenerator : IShortUrlGenerator
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToLower();
        }
    }
}