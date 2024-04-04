using BooksManager.Interfaces;
using CurrenciesManager.Infra;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Nodes;
using System.Xml;
using System.Xml.Linq;

namespace CurrenciesManager.Controllers
{
    public class BookController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IBookManager _bookManage; 
        
        //private List<string> _curenciesData = new List<string>();
        public BookController(IConfiguration configuration, IBookManager bookManage)
        {
            _config = configuration;
            _bookManage = bookManage;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/getBooks")]
        public async Task<List<BookModel>> GetAll()
        {
            return await _bookManage.GetAll();
        }

        [HttpGet("api/getbookId")]
        public async Task<BookModel> GetBookById(string id)
        {
            return await _bookManage.GetBookById(id);
        }

        [HttpGet("api/addBook")]
        public async Task<bool> AddBook(string bookXmlElem)
        {
            return await _bookManage.AddBook(bookXmlElem);
        }
    }
}
