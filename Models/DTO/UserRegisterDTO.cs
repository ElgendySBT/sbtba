using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SBTBackEnd.Models.DTO
{
	public class UserRegisterDTO
	{

		[Required]
		[Display(Name = "User Name")]
		public string email { get; set; }

		[StringLength(8, ErrorMessage = "يجب الاتزيد كلمة السر عن ثمانية احرف والاتقل عن ستة", MinimumLength = 6)]
		[DataType(DataType.Password)]
		public string password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string confirmPassword { get; set; }
		
        public string mobile { get; set; }
    }
}
