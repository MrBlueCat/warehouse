using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<RequestItemModel, Item>();
            CreateMap<Item, ResponseItemModel>();
            CreateMap<UpdateItemModel, Item>();
        }
    }
}
