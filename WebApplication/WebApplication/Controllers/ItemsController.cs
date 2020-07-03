using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Warehouse;
using FluentValidation;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> repository;
        private readonly IService<Customer> customerService;
        private readonly IService<Manufacturer> manufacturerService;

        private readonly IValidator<RequestItemModel> validator;
        private readonly IMapper mapper;

        public ItemsController(IRepository<Item> repository, IService<Customer> customerService, IService<Manufacturer> manufacturerService, IValidator<RequestItemModel> validator, IMapper mapper)
        {
            this.repository = repository;
            this.customerService = customerService;
            this.manufacturerService = manufacturerService;
            this.validator = validator;
            this.mapper = mapper;
        }

        // GET api/items
        [HttpGet]
        public ActionResult<IEnumerable<ResponseItemModel>> Get()
        {
            return Ok(mapper.Map<List<ResponseItemModel>>(repository.GetAllElements().ToList()));
        }

        // GET api/items/5
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public ActionResult<ResponseItemModel> Get(int id)
        {
            var item = repository.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(mapper.Map<ResponseItemModel>(item));
        }

        // POST api/items
        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult<ResponseItemModel> Post([FromBody]RequestItemModel item)
        {
            var result = validator.Validate(item);
            if (!result.IsValid)
                return BadRequest(result.ToString());


            Manufacturer manufacturer = GetManufacturer(item.ManufacturerId);
            if (manufacturer == null)
                return BadRequest("Manufacturer doesn`t exists");

            Customer customer = null;
            if (item.CustomerId.HasValue)
            {
                customer = customerService.GetById(item.CustomerId.Value);
                if (customer == null)
                    return BadRequest("Customer doesn`t exists");
            }

            var newItem = mapper.Map<Item>(item);
            newItem.Manufacturer = manufacturer;
            newItem.Customer = customer;

            repository.Insert(newItem);
            return Ok(mapper.Map<ResponseItemModel>(newItem));
        }

        // PUT api/items/5
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public ActionResult<ResponseItemModel> Put(int id, UpdateItemModel item)
        {
            Item old;
            if ((old = repository.GetById(id)) == null)
                return NotFound();

            var result = validator.Validate(item);
            if (!result.IsValid)
                return BadRequest(result.ToString());

            Manufacturer manufacturer = GetManufacturer(item.ManufacturerId);
            if (manufacturer == null)
                return BadRequest("Manufacturer doesn`t exists");

            Customer customer = null;
            if (item.CustomerId.HasValue)
            {
                customer = customerService.GetById(item.CustomerId.Value);
                if (customer == null)
                    return BadRequest("Customer doesn`t exists");
            }

            var updatedItem = mapper.Map<Item>(item);
            updatedItem.Id = id;
            updatedItem.Date = old.Date;
            updatedItem.Manufacturer = manufacturer;
            updatedItem.Customer = customer;

            if (!repository.Update(updatedItem))
                return BadRequest("Update error");

            return Ok(mapper.Map<ResponseItemModel>(updatedItem));
        }

        // DELETE api/items/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!repository.Delete(id))
                return NotFound();

            return Ok();
        }

        private Manufacturer GetManufacturer(int? id)
        {
            if (!id.HasValue)
                return null;

            return manufacturerService.GetById(id.Value);
        }
    }
}
