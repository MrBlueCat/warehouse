using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> repository;

        private readonly IValidator<RequestCustomerModel> validator;
        private readonly IMapper mapper;

        ItemService itemService;

        public CustomersController(IRepository<Customer> repository, IValidator<RequestCustomerModel> validator, IMapper mapper, ItemService itemService)
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
            this.itemService = itemService;
        }

        // GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<ResponseCustomerModel>> Get()
        {
            return Ok(mapper.Map<List<ResponseCustomerModel>>(repository.GetAllElements().ToList()));
        }

        // GET api/customers/1
        [HttpGet("{id}")]
        public ActionResult<ResponseCustomerModel> Get(int id)
        {
            var customer = repository.GetById(id);

            if (customer == null)
                return NotFound();

            return Ok(mapper.Map<ResponseCustomerModel>(customer));
        }

        // GET api/customers/3
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<ResponseCustomerModel>> Put(int id, RequestCustomerModel customer)
        {
            Customer old;
            if ((old = repository.GetById(id)) == null)
                return NotFound();

            var result = validator.Validate(customer);
            if (!result.IsValid)
                return BadRequest(result.ToString());

            var updated = mapper.Map<Customer>(customer);
            updated.Id = id;

            if (!repository.Update(updated))
                return BadRequest("Update error");

            itemService.UpdateCutomers(updated);

            return Ok(mapper.Map<ResponseCustomerModel>(updated));
        }

        // POST api/customers
        [HttpPost]
        public ActionResult<ResponseCustomerModel> Post([FromBody]RequestCustomerModel customer)
        {
            var result = validator.Validate(customer);
            if (!result.IsValid)
                return BadRequest(result.ToString());

            var newCustomer = mapper.Map<Customer>(customer);

            repository.Insert(newCustomer);
            return Ok(mapper.Map<ResponseCustomerModel>(newCustomer));
        }


        // DELETE api/customers/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            if (!repository.Delete(id))
                return NotFound();

            itemService.DeleteCutomers(id);
            return Ok("Item was deleted");
        }
    }
}