using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhilaHealthTest.Models
{
	public class Brt
	{
		[Display(Name = "BRT #")]
		public string BRTNo { get; set; }

		[Display(Name = "Property Address")]
		public string Address { get; set; }

		[Display(Name = "Postal Code")]
		public int Zip { get; set; }

		[Display(Name = "Owner Name")]
		public string Owner { get; set; }

		[Display(Name = "Lien Sales Account")]
		public string Lien { get; set; }
	}
}
	