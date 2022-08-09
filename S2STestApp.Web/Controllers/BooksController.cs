namespace S2STestApp.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using S2STestApp.Services;
    using S2STestApp.ViewModels;

    public class BooksController : Controller
    {
        private readonly BooksService _bookService;
        private readonly CartService _cartService;

        public BooksController()
        {
            _bookService = new BooksService();
            _cartService = new CartService();
        }

        [HttpGet]
        public async Task<ActionResult<PageData>> GetPageData([FromQuery] BooksRequest request)
        {
            try
            {
                var res = await _bookService.GetPageDataAsync(request);
                res.CartItemsCount = await _cartService.GetCartItemsCountAsync(request.UserData);
                res.UserData = request.UserData;
                return res;
            }   
            catch(System.Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<BookViewModel>>> GetBooks([FromQuery] BooksRequest request)
        {
            try
            {
                var userData = Extensions.GetUserData(HttpContext);
                var res = await _bookService.GetBooksAsync(request);
                return res;
            }
            catch(System.Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
