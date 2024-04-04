using BooksManager.Interfaces;
using CurrenciesManager.Infra;
using System.Xml;
using System.Xml.Linq;

namespace BooksManager.Infra
{
    public class Library : IBookManager
    {
        IConfiguration _config { get; set; }
        private List<BookModel> _booksData = new List<BookModel>();
        public Library(IConfiguration config)
        {
                _config = config;
        }

        public async Task<List<BookModel>> GetAll()
        {
            
            var urlBooks = _config["BooksUrl"];

            var data = XDocument.Load(urlBooks);

            var rootCatalog = data?.Element("catalog");
            foreach (var item in rootCatalog.Elements("book"))
            {
                var currency = new BookModel()
                {
                    Price = item.Element("price").Value,
                    Id = item.Attribute("id").Value,
                    Genre = item.Element("genre")?.Value,
                    Author = item.Element("author")?.Value,
                    Description = item.Element("description")?.Value,
                    Title = item.Element("title")?.Value
                };
                _booksData.Add(currency);
            }
            return _booksData;
        }

        public async Task<BookModel> GetBookById(string id)
        {
            await GetAll();
            return _booksData.SingleOrDefault(b => b.Id == id);
        }


        public async Task<bool> AddBook(string bookXmlElem)
        {
            List<string> UpdatedData = new List<string>();
            try
            {
                //Valide it is a correct format
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(bookXmlElem);
                string[] lines = await System.IO.File.ReadAllLinesAsync(_config["BooksUrl"]);
                int index = Array.IndexOf(lines, "<catalog>");
                if (index == -1)
                {
                    return false;
                }
                UpdatedData.AddRange(lines.ToList());
                UpdatedData.Insert(index + 1, xmlDocument.OuterXml);
                await System.IO.File.WriteAllLinesAsync(_config["BooksUrl"], UpdatedData);
            }
            catch 
            {
                return false;
            }

            return true;
        }
    }
}
