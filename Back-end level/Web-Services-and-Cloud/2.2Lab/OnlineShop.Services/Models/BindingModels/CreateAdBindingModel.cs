using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Services.Models.BindingModels
{
    using System.Collections.Generic;

    public class CreateAdBindingModel
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int TypeId { get; set; }

        public IEnumerable<int> Categories { get; set; }
    }
}
