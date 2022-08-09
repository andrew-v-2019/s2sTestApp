namespace S2STestApp.ViewModels
{
    public class CartItemViewModel
    {
        public CartItemViewModel(int cartItemId, int bookId, string bookName)
        {
            CartItemId = cartItemId;
            BookId = bookId;
            BookName = bookName;
        }

        public int CartItemId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
    }
}
