using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Entities.DTO
{
    public class UserRegisterDto
    {
		[Display(Name = "User Name")]
		public string DisplayName { get; set; }
		[Required]
		[Display(Name = "email")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[@$!%*#?&])[A-Za-z\\d@$!%*#?&]{8,}$", ErrorMessage = "Password must have 1 UpperCase,1 LowerCase,1 number,1 non alphanumeric and least 6 characters")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string confirmPassword { get; set; }

		public string mobile { get; set; }

	}
}
