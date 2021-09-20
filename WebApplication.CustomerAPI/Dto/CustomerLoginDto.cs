using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.CustomerAPI.Dto
{
    public class CustomerLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
