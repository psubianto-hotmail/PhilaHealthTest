using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhilaHealthTestApi.Models;

namespace PhilaHealthTestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxDetailController : ControllerBase
    {

		[HttpGet("{id}", Name = "GetTaxDetail")]
		public async Task<ActionResult<QueryResult>> GetByBrtNo(string brtno)
		{
			var detail = new QueryResult();

			HttpClient client = new HttpClient();
			var response = await client.GetAsync("http://legacy.phila.gov/revenue/realestatetax/?txtBRTNo=161113200");
			var content = await response.Content.ReadAsStringAsync();

			HtmlDocument page = new HtmlDocument();
			page.LoadHtml(content);

			// Get Customer Information
			String propertyTaxAccountNo = page.DocumentNode.SelectSingleNode
				(@"//span[@id = 'ctl00_BodyContentPlaceHolder_GetTaxInfoControl_frm_lblPropertyTaxAccountNo']").InnerHtml;
			String propertyAddress = page.DocumentNode.SelectSingleNode
				(@"//span[@id = 'ctl00_BodyContentPlaceHolder_GetTaxInfoControl_frm_lblPropertyAddress']").InnerHtml;
			String propertyPostalCode = page.DocumentNode.SelectSingleNode
				(@"//span[@id = 'ctl00_BodyContentPlaceHolder_GetTaxInfoControl_frm_Label1']").InnerHtml;
			String propertyOwnerName = page.DocumentNode.SelectSingleNode
				(@"//span[@id = 'ctl00_BodyContentPlaceHolder_GetTaxInfoControl_frm_lblOwnerName']").InnerHtml;
			String propertyLienSaleAccount = page.DocumentNode.SelectSingleNode
				(@"//span[@id = 'ctl00_BodyContentPlaceHolder_GetTaxInfoControl_frm_lblLienSaleAccount']").InnerHtml;
			String propertyEndPaymentDate = page.DocumentNode.SelectSingleNode
				(@"//span[@id = 'ctl00_BodyContentPlaceHolder_GetTaxInfoControl_frm_lblPaymentPostDate']").InnerHtml;

			detail.BRTNo = propertyTaxAccountNo;
			detail.Address = propertyAddress;
			detail.PostalCode = propertyPostalCode;
			detail.OwnerName = propertyOwnerName;
			detail.LienSaleAccount = propertyLienSaleAccount;
			detail.EndPaymentDate = propertyEndPaymentDate;
			
			// Get Payment History
			var paymentHistory = page.DocumentNode.SelectSingleNode
				(@"//table[@id = 'ctl00_BodyContentPlaceHolder_GetTaxInfoControl_grdPaymentsHistory']").Descendants();

			List<PaymentHistory> payments = new List<PaymentHistory>();

			foreach (var p in paymentHistory)
			{
				payments.Add(new PaymentHistory()
				{
					BRTNo = propertyTaxAccountNo,
					Year = 0,
					Principal = 0,
					Interest = 0,
					Penalty = 0,
					Other = 0,
					Total = 0,
					LienNo = p.InnerHtml,
					Solicitor = p.InnerHtml,
					Status = p.InnerHtml
				});
			}


			return Ok(paymentHistory);
		}
	}

}