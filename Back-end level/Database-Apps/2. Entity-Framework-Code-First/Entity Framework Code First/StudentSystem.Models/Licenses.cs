using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSystem.Models
{
    public class Licenses
    {

        public Licenses()
        {
            this.Resource = new HashSet<Resource>();
        }
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Resource> Resource { get; set; }
    }
}
