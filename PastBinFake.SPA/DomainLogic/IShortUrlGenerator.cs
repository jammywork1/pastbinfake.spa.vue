namespace PastBinFake.SPA.DomainLogic
{
    /// <summary>
    /// Generate url for new notes
    /// </summary>
    public interface IShortUrlGenerator
    {
        string Generate();
    }
}