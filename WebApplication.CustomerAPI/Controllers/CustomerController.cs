using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.CustomerAPI.Data;
using WebApplication.CustomerAPI.Dto;
using WebApplication.CustomerAPI.Helpers;
using WebApplication.CustomerAPI.Model;

namespace WebApplication.CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly JwtService _jwtService;
        private readonly ICustomerRepository _repository;

        public CustomerController(ILogger<CustomerController> logger, JwtService jwtService, ICustomerRepository repository)
        {
            _logger = logger;
            _jwtService = jwtService;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            var jwt = Request.Cookies["jwt"];
            if(jwt is null) return BadRequest(new { message = "Unauthorized" });
            var token = _jwtService.Verify(jwt);
            string tokenPayloadJsonStr = token.Payload.SerializeToJson();
            JObject tokenPayloadJsonObj = JObject.Parse(tokenPayloadJsonStr);
            var customer = _repository.GetCustomerById(tokenPayloadJsonObj["Id"].Value<Int32>());
            return Ok(customer);
        }

        [HttpPost("register")]
        public IActionResult RegisterCustomer(CustomerRegisterDto dto)
        {
            var customer = new Customer
            {
                Email = dto.Email,
                Name = dto.Name,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };
            if (_repository.IsEmailExist(customer.Email)) return BadRequest("Email already exist!");
            _repository.CreateCustomer(customer);
            var jwt = _jwtService.Generate(customer.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true //cookie client side erisebilmesi icin
            });
            return Ok(customer);
        }

        [HttpPost("login")]
        public IActionResult LoginCustomer(CustomerLoginDto dto)
        {
            if (!_repository.IsEmailExist(dto.Email)) return BadRequest("Email not found!");
            var customer = _repository.GetCustomerByEmail(dto.Email);
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, customer.Password)) return BadRequest(new { message = "Wrong Password!" });

            var jwt = _jwtService.Generate(customer.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(customer);
        }

        [HttpPost("logout")]
        public IActionResult LogoutCustomer()
        {
            try
            {
                Response.Cookies.Delete("jwt");
                return Ok( new { message = "Logout success!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex });
            }
        }
    }
}
