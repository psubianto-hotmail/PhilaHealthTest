using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhilaHealthTestApi.Models
{
	public class QueryResult
	{
		public string BRTNo { get; set; }
		public string Address { get; set; }
		public string PostalCode { get; set; }
		public string OwnerName { get; set; }
		public string LienSaleAccount { get; set; }
		public string EndPaymentDate { get; set; }
		public List<PaymentHistory> Payments { get; set; }
	}
	
	public class PaymentHistory
	{
		public string BRTNo { get; set; }
		public int Year { get; set; }
		public double Principal { get; set; }
		public double Interest { get; set; }
		public double Penalty { get; set; }
		public double Other { get; set; }
		public double Total { get; set; }
		public string LienNo { get; set; }
		public string Solicitor { get; set; }
		public string Status { get; set; }
	}
}
