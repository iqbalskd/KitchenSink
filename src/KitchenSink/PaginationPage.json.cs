using Starcounter;

namespace KitchenSink
{
    [Database]
    public class Book
    {
        public string Author;
        public string Title;
    }

    partial class PaginationPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            var firstBook = Db.SQL<Book>("SELECT b FROM KitchenSink.Book b").First;
            if (firstBook == null)
            {
                createBooks(100);
            }

            this.Library.Data = Db.SQL<Book>("SELECT b FROM KitchenSink.Book b FETCH ? OFFSET ?", this.EntriesPerPage, this.EntriesPerPage * (currentValue - 1));
        }

        public void createBooks(int numberOfBooks)
        {
            Db.Transact(() =>
            {
                for (int i = 1; i < numberOfBooks + 1; i++)
                {
                    var book = new Book()
                    {
                        Author = "George R.R Martin",
                        Title = "Game of Thrones " + i.ToString()
                    };
                }
            });
        }

        long currentValue = 0;

        void Handle(Input.EntriesPerPage action)
        {
            this.Library.Data = Db.SQL<Book>("SELECT b FROM KitchenSink.Book b FETCH ? OFFSET ?", action.Value, action.Value * (currentValue - 1));
        }

        void Handle(Input.ChangePage action)
        {
            currentValue = action.Value;
            this.Library.Data = Db.SQL<Book>("SELECT b FROM KitchenSink.Book b FETCH ? OFFSET ?", this.EntriesPerPage, this.EntriesPerPage * (action.Value - 1));
        }

        void Handle(Input.PreviousPage action)
        {
            if (currentValue > 0)
            {
                currentValue = currentValue - this.EntriesPerPage;
                this.Library.Data = Db.SQL<Book>("SELECT b FROM KitchenSink.Book b FETCH ? OFFSET ?", this.EntriesPerPage, currentValue);
            }
        }

        void Handle(Input.NextPage action)
        {
            if (currentValue < countBooks() - this.EntriesPerPage)
            {
                currentValue = currentValue + this.EntriesPerPage;
                this.Library.Data = Db.SQL<Book>("SELECT b FROM KitchenSink.Book b FETCH ? OFFSET ?", this.EntriesPerPage, currentValue);
            }
        }

        private int countBooks()
        {
            var books = Db.SQL<Book>("SELECT b FROM KitchenSink.Book");
            int count = 0;
            foreach (var book in books)
            {
                count++;
            }
            return count;
        }
    }
}