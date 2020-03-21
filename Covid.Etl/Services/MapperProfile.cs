using AutoMapper;
using Covid.Data.Models;

namespace Covid.Etl.Services
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<RawData, DeadCount>();
            CreateMap<RawData, ConfirmedCount>();
            CreateMap<RawData, RecoveredCount>();
        }
    }
}