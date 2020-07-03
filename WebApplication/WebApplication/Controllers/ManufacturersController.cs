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
    public class ManufacturersController : ControllerBase
    {

        private readonly IRepository<Manufacturer> repository;

        private readonly IValidator<RequestManufacturerModel> validator;
        private readonly IMapper mapper;

        ItemService itemService;

        public ManufacturersController(IRepository<Manufacturer> repository, IValidator<RequestManufacturerModel> validator, IMapper mapper, ItemService itemService)
        {
            this.repository = repository;
            this.validator = validator;
            this.mapper = mapper;
            this.itemService = itemService;
        }

        // GET api/manufacturers
        [HttpGet]
        public ActionResult<IEnumerable<ResponseManufacturerModel>> Get()
        {
            return Ok(mapper.Map<List<ResponseManufacturerModel>>(repository.GetAllElements().ToList()));
        }

        // GET api/manufacturers/1
        [HttpGet("{id}")]
        public ActionResult<ResponseManufacturerModel> Get(int id)
        {
            var manufacturer = repository.GetById(id);

            if (manufacturer == null)
                return NotFound();

            return Ok(mapper.Map<ResponseManufacturerModel>(manufacturer));
        }

        // GET api/manufacturers/3
        [HttpPut("{id}")]
        public ActionResult<IEnumerable<ResponseManufacturerModel>> Put(int id, RequestManufacturerModel manufacturer)
        {
            Manufacturer old;
            if ((old = repository.GetById(id)) == null)
                return NotFound();

            var result = validator.Validate(manufacturer);
            if (!result.IsValid)
                return BadRequest(result.ToString());

            var updated = mapper.Map<Manufacturer>(manufacturer);
            updated.Id = id;

            if (!repository.Update(updated))
                return BadRequest("Update error");

            itemService.UpdateManufacturers(updated);

            return Ok(mapper.Map<ResponseManufacturerModel>(updated));
        }

        // POST api/manufacturers
        [HttpPost]
        public ActionResult<ResponseManufacturerModel> Post([FromBody]RequestManufacturerModel manufacturer)
        {
            var result = validator.Validate(manufacturer);
            if (!result.IsValid)
                return BadRequest(result.ToString());

            var newManufacturer = mapper.Map<Manufacturer>(manufacturer);

            repository.Insert(newManufacturer);
            return Ok(mapper.Map<ResponseManufacturerModel>(newManufacturer));
        }

        // DELETE api/manufacturers/5
        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            if (!repository.Delete(id))
                return NotFound();

            itemService.OnManufacturerDelete(id);
            return Ok("Item was deleted");
        }
    }
}