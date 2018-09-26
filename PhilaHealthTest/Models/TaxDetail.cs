using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhilaHealthTest.Models
{
	public class TaxDetail
	{
		[Display(Name = "BRT #")]
		public string BRTNo { get; set; }

		[Display(Name = "Year")]
		public int Year { get; set; }

		[Display(Name = "Principal")]
		public double Principal { get; set; }

		[Display(Name = "Interest")]
		public double Interest { get; set; }

		[Display(Name = "Penalty")]
		public double Penalty { get; set; }

		[Display(Name = "Other")]
		public double Other { get; set; }

		[Display(Name = "Total")]
		public double Total { get; set; }

		[Display(Name = "Lien #")]
		public string LienNo { get; set; }

		[Display(Name = "City Solicitor")]
		public string Solicitor { get; set; }

		[Display(Name = "Status")]
		public string Status { get; set; }
	}
}
