namespace S2STestApp.ViewModels
{
    using System.Collections.Generic;

    public class PageData
    {

        public PageData(int pageCount, List<BookViewModel> books)
        {
            PageCount = pageCount;
            Books = books;
        }


        public int PageCount { get; set; }

        public List<BookViewModel> Books { get; set; } = new List<BookViewModel>();
        public int CartItemsCount { get; set; }

        public string UserData {get;set;}
    }
}
