namespace BookShopSystem.Service.Controllers
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Data;
    using Data.Models;
    using ModelsDTO;

    public class CategoriesController : ApiController
    {
        private BookShopContext _db = new BookShopContext();

        // GET: api/Categories
        public IQueryable<CategoryDto> GetCategories()
        {
            return _db.Categories
                .Select(c => new CategoryDto()
                {
                    Id = c.Id,
                    Name = c.Name
                });
        }

        // GET: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id)
        {
            Category category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            return Ok(new CategoryDto()
            {
               Id = category.Id,
               Name = category.Name
            });
        }

        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, CategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string oldName = _db.Categories
                .FirstOrDefault(c => c.Id == id)
                .Name;

            _db.Categories
                .FirstOrDefault(c => c.Id == id)
                .Name = category.Name;

            _db.SaveChanges();

            return this.Ok(string.Format("Category: \"{0}\" was renamed to \"{1}\"", oldName, category.Name));
        }

        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(CategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Categories.Any(c => c.Name == category.Name))
            {
                return BadRequest("Duplicated category");
            }

            _db.Categories.AddOrUpdate(new Category()
            {
                Name = category.Name
            });
            _db.SaveChanges();

            return this.Ok(string.Format("Category: {0} was succefully added", category.Name));
        }

        // DELETE: api/Categories/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id)
        {
            Category category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            _db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}