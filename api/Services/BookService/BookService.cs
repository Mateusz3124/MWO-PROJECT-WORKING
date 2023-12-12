using P06.Shared;
using P06.Shared.Services.BookService;
using P06.Shared.Books;
using P07Book.DataSeeder;
using Bogus.Bson;

namespace P05Shop.API.Services.BookService
{
    public class BookService : IBookService
    {
        public List<Book> books;
        public BookService() { 
        books = BookSeeder.GenerateBookData();
        }
        public async Task<ServiceResponse<string>> CreateBookAsync(Book book)
        {
            try
            {
                Book result = new Book();
                foreach (Book checkedBook in books)
                {
                    if (checkedBook.Id == book.Id)
                    {
                        return new ServiceResponse<string>()
                        {
                            Data = null,
                            Message = "Book with this id already exists",
                            Success = false,
                            CodeError = 400
                        };
                    }
                }
                books.Add(book);
                var response = new ServiceResponse<string>()
                {
                    Data = "Book added",
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<string>()
                {
                    Data = null,
                    Message = "Problem with Database",
                    Success = false,
                    CodeError = 500
                };
            }
        }


        public async Task<ServiceResponse<string>> DeleteBookAsync(int id)
        {
            try
            {
                Book result = new Book();
                foreach (Book checkedBook in books)
                {
                    if (checkedBook.Id == id)
                    {
                        result = checkedBook;
                    }
                }
                if (result.Author == null)
                {
                    return new ServiceResponse<string>()
                    {
                        Data = null,
                        Message = "Book with this id doesn't exist",
                        Success = false,
                        CodeError = 400
                    };
                }
                books.Remove(result);
                var response = new ServiceResponse<string>()
                {
                    Data = "Deleted Book",
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<string>()
                {
                    Data = null,
                    Message = "Problem with Database",
                    Success = false,
                    CodeError = 500
                };
            }
        }

        public async Task<ServiceResponse<List<Book>>> ReadBooksAsync()
        {
            try
            {
                var response = new ServiceResponse<List<Book>>()
                {
                    Data = books,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<Book>>()
                {
                    Data = null,
                    Message = "Problem with dataseeder library",
                    Success = false,
                    CodeError = 500
                };
            }

        }

        public async Task<ServiceResponse<string>> UpdateBookAsync(int id, Book book)
        {
            try
            {
                int index = 0;
                Book result = new Book();
                foreach (Book checkedBook in books)
                {
                    if (checkedBook.Id == book.Id)
                    {
                        if (checkedBook.Id != id) 
                        {
                            return new ServiceResponse<string>()
                            {
                                Data = null,
                                Message = "Book with this id already exists",
                                Success = false,
                                CodeError = 400
                            };
                        }
                    }
                    if (checkedBook.Id == id)
                    {
                        result = checkedBook;
                        break;
                    }
                    index++;
                }
                if (result.Author == null)
                {
                    return new ServiceResponse<string>()
                    {
                        Data = null,
                        Message = "Book with this id doesn't exist",
                        Success = false,
                        CodeError = 400
                    };
                }
                books[index] = book;
                var response = new ServiceResponse<string>()
                {
                    Data = "Updated info",
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<string>()
                {
                    Data = null,
                    Message = "Problem with Database",
                    Success = false,
                    CodeError = 500
                };
            }
        }
    }
}
