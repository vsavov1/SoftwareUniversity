using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{
    public class Resource
    {
        public Resource()
        {
            this.Licenseses = new HashSet<Licenses>();
        }


        [Key]
        public int ResourceId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ResourceType Type { get; set; }

        [Required]
        public string Url { get; set; }

        public virtual ICollection<Licenses> Licenseses { get; set; }
    }
}
