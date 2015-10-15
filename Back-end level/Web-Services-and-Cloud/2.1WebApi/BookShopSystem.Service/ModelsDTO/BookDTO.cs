namespace BookShopSystem.Service.ModelsDTO
{
    using System;
    using System.Collections.Generic;
    using Data.Models;

    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EditionType EditionType { get; set; }
        public decimal Price { get; set; }
        public int Copies { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public AgeRestriction AgeRestriction { get; set; }
        public  List<string> Categories { get; set; }
    }
}