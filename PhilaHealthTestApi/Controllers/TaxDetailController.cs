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
			try
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
					(@"//table[@id = 'ctl00_BodyContentPlaceHolder_GetTaxInfoControl_grdPaymentsHistory']");

				var paymentHistoryRows = paymentHistory.SelectNodes("//tr");

				List<PaymentHistory> payments = new List<PaymentHistory>();

				foreach (var p in paymentHistoryRows)
				{
					var paymentHistoryCols = p.SelectNodes("//td");

					payments.Add(new PaymentHistory()
					{
						BRTNo = propertyTaxAccountNo,
						Year = int.Parse(paymentHistoryCols[0].InnerText),
						Principal = double.Parse(paymentHistoryCols[1].InnerText),
						Interest = double.Parse(paymentHistoryCols[2].InnerText),
						Penalty = double.Parse(paymentHistoryCols[3].InnerText),
						Other = double.Parse(paymentHistoryCols[4].InnerText),
						Total = double.Parse(paymentHistoryCols[5].InnerText),
						LienNo = paymentHistoryCols[6].InnerText,
						Solicitor = paymentHistoryCols[7].InnerText,
						Status = paymentHistoryCols[8].InnerText
					});
				}


				return Ok(paymentHistory);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}
	}

}