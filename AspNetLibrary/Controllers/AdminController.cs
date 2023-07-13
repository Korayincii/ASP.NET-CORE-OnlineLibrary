using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AspNetLibrary.Models;
using AspNetLibrary.ViewModels;
using static System.Reflection.Metadata.BlobBuilder;

namespace AspNetLibrary.Controllers
{
	public class AdminController : Controller
	{
		private BookSuggestionsDbContext bkSuggestionsDbContext;
		private BookDbContext bookDBContext;

		public AdminController(BookDbContext bookContext, BookSuggestionsDbContext bkSuggestionsContext)
		{
			bookDBContext = bookContext;
			bkSuggestionsDbContext = bkSuggestionsContext;
		}

		public IActionResult Index()
		{

			if (HttpContext.Session.GetString("admin") == "adminlogin")
			{
				var bookList = bookDBContext.Books.ToList();
				return View(bookList);
			}

			else
			{
				return RedirectToAction("Login", "User");
			}

		}

		[HttpGet]
		public IActionResult AddBook()
		{
			if (HttpContext.Session.GetString("admin") == "adminlogin")
			{
				return View();
			}

			else
			{
				return RedirectToAction("Login", "User");
			}

		}

		[HttpPost]
		public IActionResult AddBook(Books newBook)
		{
			if (ModelState.IsValid)
			{
				bookDBContext.Books.Add(newBook);
				bookDBContext.SaveChanges();
				TempData["status"] = "Book has been added successfully!";
				return RedirectToAction("AddBook");
			}

			else
			{
				TempData["status"] = "Book could not added!";
				return RedirectToAction("AddBook");
			}
		}


		public IActionResult RemoveBook()
		{
			if (HttpContext.Session.GetString("admin") == "adminlogin")
			{
				return View();
			}

			else
			{
				return RedirectToAction("Login", "User");
			}
		}

        public IActionResult DeleteBook(int id)
        {
            var book = bookDBContext.Books.Find(id);

            bookDBContext.Books.Remove(book);

            bookDBContext.SaveChanges();

            TempData["status"] = "Selected book is deleted!";

            return RedirectToAction("Index");
        }

        [HttpGet]
		public IActionResult UpdateBook(int id)
		{
			if (HttpContext.Session.GetString("admin") == "adminlogin")
			{
				var book = bookDBContext.Books.Find(id);

				return View(book);
			}

			else
			{
				return RedirectToAction("Login", "User");
			}

		}

		[HttpPost]
		public IActionResult UpdateBook(Books updateBook)
		{
			if (ModelState.IsValid)
			{
				bookDBContext.Books.Update(updateBook);
				bookDBContext.SaveChanges();

				TempData["status"] = "Book has been updated!";

				return RedirectToAction("Index");
			}

			else
			{
				TempData["status"] = "Book could not updated!";

				return RedirectToAction("Index");
			}

		}

		

		public IActionResult ShowSuggestions()
		{
			var bookList = bkSuggestionsDbContext.BookSuggestions.ToList();
			return View(bookList);
		}

		public IActionResult DeleteSuggestions(int id)
		{
			var book = bkSuggestionsDbContext.BookSuggestions.Find(id);

			bkSuggestionsDbContext.BookSuggestions.Remove(book);

			bkSuggestionsDbContext.SaveChanges();

			TempData["status"] = "Selected book is deleted!";

			return RedirectToAction("Index");
		}
	}
}
