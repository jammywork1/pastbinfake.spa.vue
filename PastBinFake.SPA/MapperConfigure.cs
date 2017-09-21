using AutoMapper;
using PastBinFake.SPA.DomainLogic.DataTransferObjects;
using PastBinFake.SPA.ViewModels;

namespace PastBinFake.SPA
{
    public class MapperConfigure
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<NoteCreateViewModel, NoteCreateDTO>();
            cfg.CreateMap<NoteDTO, NoteViewModel>();
            cfg.CreateMap<NoteDTO, NoteShortViewModel>();
        }
    }
}