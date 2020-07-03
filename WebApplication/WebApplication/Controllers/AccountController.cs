using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Warehouse;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IRepository<User> repository;
        private IJwtGenerator jwtGenerator;

        public AccountController(IRepository<User> repository, IJwtGenerator jwtGenerator)
        {
            this.repository = repository;
            this.jwtGenerator = jwtGenerator;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Token(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var username = model.Email;
            var password = model.Password;

            var user = repository.FindOne(x => x.Email == username && x.Password == password);
            var identity = jwtGenerator.GenerateClaimsIdentity(user);
            if (identity == null)
                return BadRequest("Invalid username or password.");

            var encodedJwt = jwtGenerator.GenerateEncodedToken(identity);
            var role = identity.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).First();
            var response = new ResponceLogin(encodedJwt, identity.Name, role, user.Avatar);

            return Ok(response);
        }

        

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            User user = repository.FindOne(u => u.Email == model.Email);
            if (user == null)
            {
                repository.Insert(new User(model.Email, model.Password, model.Avatar));

                return Ok();
            }
            else
                return BadRequest("User already exists");
        }
    }
}