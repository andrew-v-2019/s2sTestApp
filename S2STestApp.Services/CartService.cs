namespace S2STestApp.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using S2STestApp.Data;
    using S2STestApp.Data.Models;
    using S2STestApp.ViewModels;
    using System.Linq;
    using System.Collections.Generic;

    public class CartService
    {

        async public Task<int> BuyAsync(int bookId, string userData)
        {
            var cartItem = new CartItem()
            {
                BookId = bookId,
                Date = DateTime.UtcNow,
                UserData = userData.Trim().ToLower()
            };

            using(var dbContext = new BooksContext())
            {
                await dbContext.CartItems.AddAsync(cartItem);
                await dbContext.SaveChangesAsync();
            }

            return cartItem.BookId;
        }

        async public Task<List<CartItemViewModel>> CancelBuyAsync(int cartItemId, string userData)
        {

            using(var dbContext = new BooksContext())
            {
                var item = await dbContext.CartItems.FirstOrDefaultAsync(x => x.Id == cartItemId && x.UserData.Equals(userData));
                dbContext.CartItems.Remove(item);
                await dbContext.SaveChangesAsync();
            }

            var cartItems = await GetCartAsync(userData);
            return cartItems;
        }

        public async Task<int> GetCartItemsCountAsync(string userData)
        {
            using(var dbContext = new BooksContext())
            {
                var count = await dbContext.CartItems.CountAsync(x => x.UserData.Equals(userData));
                return count;
            }
        }


        public async Task<List<CartItemViewModel>> GetCartAsync(string userData)
        {
            using(var dbContext = new BooksContext())
            {
                var q = from ci in dbContext.CartItems
                        join b in dbContext.Books on ci.BookId equals b.Id
                        where ci.UserData == userData
                        orderby ci.Date
                        select new CartItemViewModel(ci.Id, b.Id, b.Title);

                var res = await q.ToListAsync();
                return res;
            }
        }


    }
}
