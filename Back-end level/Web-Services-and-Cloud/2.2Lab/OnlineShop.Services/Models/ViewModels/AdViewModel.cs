using System.Linq;
using System.Linq.Expressions;
using Microsoft.Ajax.Utilities;

namespace OnlineShop.Services.Models.ViewModels
{
    using System;
    using System.Collections.Generic;
    using OnlineShop.Models;

    public class AdViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime PostedOn { get; set; }

        public AdStatus Status { get; set; }

        public  OwnerViewModel Owner { get; set; }

        public string Type { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public static Expression<Func<Ad, AdViewModel>> Create
        {
            get
            {
                return a => new AdViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Status = a.Status,
                    PostedOn = a.PostedOn,
                    Type = a.Type.Name,
                    Owner = new OwnerViewModel()
                    {
                        Id = a.Owner.Id,
                        Name = a.Owner.UserName
                    },
                    Price = a.Price,
                    Categories = a.Categories
                        .Select(c => new CategoryViewModel()
                        {
                            Id = c.Id,
                            Name = c.Name
                        })
                };
            }
        }
    }
}