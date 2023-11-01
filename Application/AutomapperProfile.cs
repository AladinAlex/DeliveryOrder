using Application.Dto;
using AutoMapper;
using Domain.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dto => dto.Truck, opt => opt.MapFrom(x => x.Truck.Name))
                .ForMember(dto => dto.AddressSender, opt => opt.MapFrom(x => x.AddressSender.сity.Name+ " " + x.AddressSender.address))
                .ForMember(dto => dto.AddressRecipient, opt => opt.MapFrom(x => x.AddressRecipient.сity.Name+ " " + x.AddressSender.address));

            CreateMap<City, CityDto>();
            CreateMap<Address, AddressDto>()
                .ForMember(dto => dto.сity, opt => opt.MapFrom(x => x.сity.Name));

            CreateMap<PriceList, PriceListDto>()
                .ForMember(dto => dto.CityStart, opt => opt.MapFrom(x => x.CityStart.Name))
                .ForMember(dto => dto.CityFinish, opt => opt.MapFrom(x => x.CityFinish.Name));

            CreateMap<Truck, TruckDto>();
        }
    }
}
