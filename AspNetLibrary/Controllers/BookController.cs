using Microsoft.AspNetCore.Mvc;
using AspNetLibrary.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace AspNetLibrary.Controllers
{
	public class BookController : Controller
	{
		private BookDbContext bookDBContext;
		private BookSuggestionsDbContext bkSuggestionsDbContext;

		public BookController(BookDbContext bookContext, BookSuggestionsDbContext bookSuggestionsContext)
		{
			bookDBContext = bookContext;
			bkSuggestionsDbContext = bookSuggestionsContext;
		}

		public IActionResult Index(string name, string combo)
		{
			//doing this prevent any direct entering without loginning to the library page
			if (HttpContext.Session.GetString("user") == "userlogin")
			{
				if (String.IsNullOrEmpty(name))
				{
					var books = bookDBContext.Books.ToList();
					return View(books);
				}

				else
				{
					var book = new List<Books>();
					switch (combo)
					{
						case "Name":
							book = bookDBContext.Books.Where(pr => pr.Name.Contains(name)).ToList();
							break;
						case "Genre":
							book = bookDBContext.Books.Where(pr => pr.Genre.Contains(name)).ToList();
							break;
						case "Author":
							book = bookDBContext.Books.Where(pr => pr.Author.Contains(name)).ToList();
							break;
						default:
							break;
					}
					return View(book);
				}
			}

			else
			{
				return RedirectToAction("Login", "User");
			}



		}

		[HttpGet]
		public IActionResult BkSuggestion()
		{
			if (HttpContext.Session.GetString("user") == "userlogin")
			{
				return View();
			}

			else
			{
				return RedirectToAction("Login", "User");
			}

		}

		[HttpPost]
		public IActionResult BkSuggestion(BookSuggestions suggestedBook)
		{
			bkSuggestionsDbContext.BookSuggestions.Add(suggestedBook);
			bkSuggestionsDbContext.SaveChanges();
			TempData["status"] = "Book has been suggested successfully!";
			return View();
		}

	}
}

