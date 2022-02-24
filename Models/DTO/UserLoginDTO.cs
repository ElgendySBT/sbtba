using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Models.DTO
{
	public class UserLoginDTO
	{

		[Required]
		[Display(Name = "User Name")]
		public string userName { get; set; }
		[StringLength(8, ErrorMessage = "Name length can't be more than 8.", MinimumLength = 6)]
		public string password { get; set; }
	}
}
