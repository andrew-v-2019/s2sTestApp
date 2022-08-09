namespace S2STestApp.ViewModels
{
    public class BooksRequest
    {
        public int Page { get; set; }


        public string SortField { get; set; }

        public SortDirection SortDirection { get; set; }

        public string UserData {get;set;}
    }
}
