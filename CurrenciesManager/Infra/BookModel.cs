using Microsoft.AspNetCore.Mvc;

namespace CurrenciesManager.Infra
{
    public class BookModel 
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}
