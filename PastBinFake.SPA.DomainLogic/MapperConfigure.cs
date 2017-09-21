using AutoMapper;
using PastBinFake.SPA.DomainLogic.DataTransferObjects;
using PastBinFake.SPA.DomainLogic.Models;

namespace PastBinFake.SPA.DomainLogic
{
    public class MapperConfigure
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<NoteCreateDTO, NoteCreateDomainModel>();
            cfg.CreateMap<NoteCreateDomainModel, NoteDTO>();
        }
    }
}