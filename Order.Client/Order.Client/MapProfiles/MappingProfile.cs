using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Newtonsoft.Json;

namespace Order.Client.MapProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderDto, Models.Order>()
                .ForMember(s => s.OrderNumber, opt => opt.MapFrom(s => s.OrderNumber))
                .ForMember(s => s.SourceOrder, opt => opt.MapFrom(s => JsonConvert.SerializeObject(s)));
        }
    }
}