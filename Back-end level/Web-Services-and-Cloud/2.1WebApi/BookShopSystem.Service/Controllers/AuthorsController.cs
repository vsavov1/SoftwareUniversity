namespace BookShopSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Data;
    using Data.Models;
    using ModelsDTO;

    public class AuthorsController : ApiController
    {
        private BookShopContext _db = new BookShopContext();

        public IHttpActionResult GetAuthor(int id)
        {
            Author author = _db.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }

            AuthorDto authorDto = new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            foreach (var book in author.Books)
            {
                authorDto.Books.Add(new BookDto()
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
                });
            }

            return Ok(authorDto);
        }

        // POST: api/Authors
        //NOTE add headers to request
        //{
        //  firstName: "peshoooo",
        //  lastName: "gooshoooo"
        //}

        [ResponseType(typeof(AuthorBindingModel))]
        public IHttpActionResult PostAuthor(AuthorBindingModel author)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Authors.Add(new Author()
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            });

            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = author.LastName }, author);
        }

        // POST api/authors/{id}/books
        [Route("api/authors/{id}/books")]
        public IHttpActionResult GetAuthorBooks(int id)
        {
            Author author = _db.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }

            List<BookDto> bookDto = author.Books.Select(book => new BookDto()
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
            }).ToList();

            return Ok(bookDto);
        }
    }
}