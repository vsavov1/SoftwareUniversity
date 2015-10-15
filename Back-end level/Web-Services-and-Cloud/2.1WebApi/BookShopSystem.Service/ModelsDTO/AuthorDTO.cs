namespace BookShopSystem.Service.ModelsDTO
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AuthorDto
    {
        public AuthorDto()
        {
            this.Books = new HashSet<BookDto>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string LastName { get; set; }
        public ICollection<BookDto> Books { get; set; }
    }
}