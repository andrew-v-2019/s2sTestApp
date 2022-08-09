namespace S2STestApp.Web.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using S2STestApp.Services;
    using S2STestApp.ViewModels;

    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController()
        {
            _cartService = new CartService();
        }



        async public Task<ActionResult<int>> Buy(int bookId, string userData)
        {
            try
            {
                var res = await _cartService.BuyAsync(bookId, userData);
                return res;
            }
            catch(System.Exception e)
            {
                return BadRequest(e);
            }
        }

        async public Task<ActionResult<List<CartItemViewModel>>> CancelBuy(int cartItemId, string userData)
        {
            try
            {
              
                var res = await _cartService.CancelBuyAsync(cartItemId, userData);
                return res;
            }
            catch(System.Exception e)
            {
                return BadRequest(e);
            }
        }

        async public Task<ActionResult<List<CartItemViewModel>>> GetCart(string userData)
        {
            try
            {
           
                var res = await _cartService.GetCartAsync(userData);
                return res;
            }
            catch(System.Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
