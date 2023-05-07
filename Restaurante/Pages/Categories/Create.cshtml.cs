
using Aula03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace Aula03.pages.Categories
{
	public class Create : PageModel
	{
		[BindProperty]
		public Category Category { get; set; } = new();

		public Create()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
				return Page();
			try
			{
				var httpClient = new HttpClient();
				var url = $"https://localhost:7048/Categories/";

				var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
				var jsonProduct = JsonConvert.SerializeObject(Category);
				requestMessage.Content = new StringContent(jsonProduct, Encoding.UTF8, "application/json");

				var response = await httpClient.SendAsync(requestMessage);

				return RedirectToPage("/Categories/Index");
			}
			catch (DbUpdateException)
			{
				return Page();
			}
		}
	}
}