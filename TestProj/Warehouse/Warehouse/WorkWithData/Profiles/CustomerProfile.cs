using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, ResponseCustomerModel>();
            CreateMap<RequestCustomerModel, Customer>();
        }
    }
}
