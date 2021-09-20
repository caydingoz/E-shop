using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.CustomerAPI.Dto
{
    public class CustomerRegisterDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
