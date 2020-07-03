using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class ManufacturerProfile : Profile
    {
        public ManufacturerProfile()
        {
            CreateMap<Manufacturer, ResponseManufacturerModel>();
            CreateMap<RequestManufacturerModel, Manufacturer>();
        }
    }
}
