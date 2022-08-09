namespace S2STestApp.Services
{
    using Microsoft.EntityFrameworkCore;
    using S2STestApp.Data;
    using S2STestApp.Data.Models;
    using S2STestApp.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class BooksService
    {

        private const int itemsOnPage = 20;

        public IQueryable<int> GetCxcludeBookIds(BooksContext dbContext, string userData)
        {
            return dbContext.CartItems.Where(x => x.UserData.Equals(userData)).Select(x => x.BookId).Distinct();
        }


        public IQueryable<Book> GetQuery(BooksContext dbContext, BooksRequest request)
        {
            var excludeBookIds = GetCxcludeBookIds(dbContext, request.UserData);

            var query = dbContext.Books.AsQueryable();
            if(request.SortField.Trim().ToLower().Equals("name"))
            {
                if(request.SortDirection == SortDirection.Asc)
                {
                    query = query.OrderBy(b => b.Title);
                }
                else if(request.SortDirection == SortDirection.Desc)
                {
                    query = query.OrderByDescending(b => b.Title);
                }
            }
            else if(request.SortField.Trim().ToLower().Equals("author"))
            {
                if(request.SortDirection == SortDirection.Asc)
                {
                    query = query.OrderBy(b => b.Author);
                }
                else if(request.SortDirection == SortDirection.Desc)
                {
                    query = query.OrderByDescending(b => b.Author);
                }
            }
            else if(request.SortField.Trim().ToLower().Equals("year"))
            {
                if(request.SortDirection == SortDirection.Asc)
                {
                    query = query.OrderBy(b => b.Year);
                }
                else if(request.SortDirection == SortDirection.Desc)
                {
                    query = query.OrderByDescending(b => b.Year);
                }
            }
            else
            {
                query = query.OrderBy(b => b.Id);
            }

            query = query.Where(x => !excludeBookIds.Contains(x.Id));

            return query;
        }

        public async Task<int> GetBooksCount(string userData)
        {
            using(var dbContext = new BooksContext())
            {
                dbContext.Database.EnsureCreated();
                var excludeBookIds = GetCxcludeBookIds(dbContext, userData);
                var booksCount = await dbContext.Books.CountAsync(x => !excludeBookIds.Contains(x.Id));
                return booksCount;
            }
        }

        public async Task<PageData> GetPageDataAsync(BooksRequest request)
        {
            var booksCount = await GetBooksCount(request.UserData);

            var pagesCount = (booksCount / itemsOnPage) + 1;
            if(booksCount % itemsOnPage == 0)
            {
                pagesCount = (booksCount / itemsOnPage);
            }
            var books = await GetBooksAsync(request);
            var pageData = new PageData(pagesCount, books);
            return pageData;
        }

        public async Task<List<BookViewModel>> GetBooksAsync(BooksRequest request)
        {
            var res = new List<BookViewModel>();
            using(var dbContext = new BooksContext())
            {
                var query = GetQuery(dbContext, request);

                var skip = request.Page * itemsOnPage;
                res = await query.Skip(skip)
                                 .Take(itemsOnPage)
                                 .Select(b => new BookViewModel(b.Id, b.Author, b.Title, b.Year))
                                 .ToListAsync();
            }

            return res;
        }
    }
}
