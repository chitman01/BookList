using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookList.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookList.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        // return list for index
        public IActionResult Index()
        {
            return View(_db.Books.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if(ModelState.IsValid)
            {
                _db.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        //Get : Book/Create
        public IActionResult Create()
        { 
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                _db.Dispose();
            }
        }
    }
}