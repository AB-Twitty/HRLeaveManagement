﻿using System.ComponentModel.DataAnnotations;

namespace HRLeaveManagement.MVC.Models
{
	public class LoginVM
	{
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class RegisterVM : LoginVM
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength(6)]
        public string UserName { get; set; }
    }
}
