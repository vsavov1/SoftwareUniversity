namespace NewsDB.Models
{
    using System.ComponentModel.DataAnnotations;

    public class News
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(1000)]
        [Required(ErrorMessage = "Content is requeid field")]
        public string Content { get; set; }
    }
}
