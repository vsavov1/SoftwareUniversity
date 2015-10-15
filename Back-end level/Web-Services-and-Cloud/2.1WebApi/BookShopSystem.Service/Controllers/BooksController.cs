using System;
using Microsoft.AspNet.Identity;

namespace BookShopSystem.Service.Controllers
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Data;
    using Data.Models;
    using ModelsDTO;

    [System.Web.Http.RoutePrefix("api/books")]
    [Authorize]
    public class BooksController : ApiController
    {
        private BookShopContext _db = new BookShopContext();

        // GET: api/Books/5
        [ResponseType(typeof(BookDto))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = _db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            BookDto bookDto = new BookDto()
            {
                AgeRestriction = book.AgeRestriction,
                Copies = book.Copies,
                Description = book.Description,
                EditionType = book.EditionType,
                Price = book.Price,
                ReleaseDate = book.ReleaseDate,
                Title = book.Title,
                Id = book.Id,
                Categories = book.Categories.Select(c => c.Name).ToList()
            };

            return Ok(bookDto);
        }

        // GET: api/Books/5
        [ResponseType(typeof(BookDto))]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetSearch([FromUri]string search)
        {
            var books = _db.Books
                .Where(b => b.Title.Contains(search))
                .Take(10)
                .OrderBy(b => b.Title)
                .Select(b => new
                {
                    id = b.Id,
                    title = b.Title
                })
                .ToList();

            if (!books.Any())
            {
                return NotFound();
            }

            return Ok(books);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, [FromBody]BookBindingModel bookJson)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            Book book = _db.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return this.BadRequest(string.Format("Book with id: {0} is not found", id));
            }

            book.Title = bookJson.Title;
            book.Description = bookJson.Description;
            book.Price = bookJson.Price;
            book.Copies = bookJson.Copies;
            book.EditionType = bookJson.EditionType;
            book.AgeRestriction = bookJson.AgeRestriction;
            book.ReleaseDate = bookJson.ReleaseDate;
            book.AuthorId = bookJson.AuhtorId;

            _db.Books.AddOrUpdate(book);
            _db.SaveChanges();

            return this.Ok(new { book = book.Id, isEdited = true });
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = _db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            _db.Books.Remove(book);
            _db.SaveChanges();

            return Ok(book);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(BookPostBindingModel newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var book = new Book()
            {
                Title = newBook.Title,
                Description = newBook.Description,
                Price = newBook.Price,
                Copies = newBook.Copies,
                EditionType = newBook.EditionType,
                AgeRestriction = newBook.AgeRestriction,
                ReleaseDate = newBook.ReleaseDate,
                AuthorId = newBook.AuhtorId,
            };

            foreach (var category in newBook.Categories)
            {
                if (_db.Categories.Any(c => c.Name == category))
                {
                    book.Categories.Add(_db.Categories.FirstOrDefault(c => c.Name == category));
                }
                else
                {
                    var newCategory = new Category();
                    newCategory.Name = category;
                    book.Categories.Add(newCategory);
                }
            }

            _db.Books.Add(book);
            _db.SaveChanges();

            return this.Ok(new { id = book.Id, isCreated = true });
        }


        ///api/books/recall/{id}
        [ResponseType(typeof(Book))]
        [Route("recall/{id}")]
        public IHttpActionResult Recall(int id)
        {

            var book = _db.Books.FirstOrDefault(b => b.Id == id);
            string userId = User.Identity.GetUserId();

            if (book == null)
            {
                return NotFound();
            }

            if (userId == null)
            {
                return this.BadRequest("User not found or bad request");
            }

            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            var dateTimeNow = DateTime.Now.AddDays(30.0);
            var purchase =
                _db.Purchases.FirstOrDefault(
                    p => p.Book.Id == id 
                    && p.User.Id == user.Id 
                    && p.DateTime <= dateTimeNow);

            if (purchase == null)
            {
                return this.BadRequest("Purchase not found or bad request");
            }

            purchase.IsRecalled = true;
            book.Copies++;
            _db.SaveChanges();
            return this.Ok($"Purchase id: {purchase.Id} successfully recalled");
        }
        [ResponseType(typeof(Book))]
        [Route("buy/{id}")]
        public IHttpActionResult Buy(int id)
        {

            var book = _db.Books.FirstOrDefault(b => b.Id == id);
            string userId = User.Identity.GetUserId();

            if (book == null)
            {
                return NotFound();
            }

            if (book.Copies == 0)
            {
                return this.BadRequest("Book is out of stock or bad request");
            }

            if (userId == null)
            {
                return this.BadRequest("User not found or bad request");
            }

            book.Copies--;
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);
            var purchase = new Purchase();
            purchase.User = user;
            purchase.Amount = 1; //todo
            purchase.Book = book;
            purchase.DateTime = DateTime.Now;
            purchase.Price = (int) (book.Price * 1);//todo

            _db.Purchases.Add(purchase);
            _db.SaveChanges();
            return this.Ok($"Purchase id: {purchase.Id}");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return _db.Books.Count(e => e.Id == id) > 0;
        }
    }
}