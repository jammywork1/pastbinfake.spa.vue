using Microsoft.Extensions.DependencyInjection;
using PastBinFake.SPA.DataAccessLayer;
using PastBinFake.SPA.DataAccessLayer.Memory;
using PastBinFake.SPA.DomainLogic.UrlGenerators;

namespace PastBinFake.SPA.DomainLogic
{
    public class DependencyInjectionConfigure
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IShortUrlGenerator, GuidShortUrlGenerator>();
            services.AddTransient<MemoryContext>();
            services.AddTransient<IUnitOfWork, MemoryUnitOfWork>();
            services.AddTransient(typeof(NotesService));
            services.AddTransient(typeof(SystemFacade));
        }
    }
}