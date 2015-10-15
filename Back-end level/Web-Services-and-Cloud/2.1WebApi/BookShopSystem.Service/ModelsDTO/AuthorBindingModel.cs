namespace BookShopSystem.Service.ModelsDTO
{
    using System.ComponentModel.DataAnnotations;

    public class AuthorBindingModel
    {
        public string FirstName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string LastName { get; set; }
    }
}