using P06.Shared;
using P06.Shared.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06.Shared.Services.BookService
{
    public interface IBookService
    {
        Task<ServiceResponse<List<Book>>> ReadBooksAsync();
        Task<ServiceResponse<string>> CreateBookAsync(Book book);
        Task<ServiceResponse<string>> DeleteBookAsync(int id);
        Task<ServiceResponse<string>> UpdateBookAsync(int id, Book book);

    }
}
