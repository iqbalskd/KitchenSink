using Starcounter;
using System.Linq;

namespace KitchenSink
{
    [Database]
    public class Book
    {
        public string Author;
        public string Title;
        public long Position;
    }

    partial class PaginationPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            GetNewPage();
            SetTotalPages();
            CreateNavButtons();
            SetPageEntries();
        }

        public long TotalEntries = Db.SQL<long>("SELECT COUNT(e) FROM KitchenSink.Book e").FirstOrDefault();

        void Handle(Input.EntriesPerPage action)
        {
            CurrentOffset = 0;
            EntriesPerPage = action.Value;
            SetTotalPages();
            GetNewPage();
        }

        void Handle(Input.ChangePage action)
        {
            CurrentOffset = EntriesPerPage * (action.Value - 1);
            GetNewPage();
        }

        void Handle(Input.NextPageTrigger action)
        {
            if (CurrentOffset + EntriesPerPage < TotalEntries)
            {
                CurrentOffset = CurrentOffset + EntriesPerPage;
            }
            GetNewPage();
        }

        void Handle(Input.PreviousPageTrigger action)
        {
            bool willOverflow = CurrentOffset - EntriesPerPage < 0;

            CurrentOffset = willOverflow ? 0 : CurrentOffset - EntriesPerPage;
            GetNewPage();
        }

        void Handle(Input.LastPageTrigger action)
        {
            long remainder = TotalEntries % EntriesPerPage;
            if (remainder == 0)
            {
                CurrentOffset = TotalEntries - EntriesPerPage;
            }
            else
            {
                CurrentOffset = TotalEntries - remainder;
            }
            GetNewPage();
        }

        void Handle(Input.FirstPageTrigger action)
        {
            CurrentOffset = 0;
            GetNewPage();
        }

        private void SetPageEntries()
        {
            int[] entriesPerPageAlternatives = new int[] { 5, 15, 30 };
            foreach (int entry in entriesPerPageAlternatives)
            {
                PageEntriesElementJson page = PageEntries.Add();
                page.Amount = entry;
                page.Text = $"Show {entry} items per page";
            }
        }

        private void SetTotalPages()
        {
            long totalPagesRounded = TotalEntries / EntriesPerPage;
            TotalPages = TotalEntries % EntriesPerPage == 0 ? totalPagesRounded : totalPagesRounded + 1;
        }

        // Establishes the navigation buttons.
        // Displays all the page buttons if there are less then 10,
        // Otherwise it shows the current page, the two before and after.
        private void CreateNavButtons()
        {
            Pages.Clear();
            CurrentPage = CurrentOffset / EntriesPerPage + 1;

            if (TotalPages < 10)
            {
                for (long i = 1; i < TotalPages + 1; i++)
                {
                    CreateButton(i);
                }
            }

            else if (TotalPages >= 10)
            {
                long pagesBefore = -2;
                long pagesAfter = 3;

                if (CurrentPage + pagesAfter > TotalPages)
                {
                    pagesBefore -= (CurrentPage + pagesAfter - 1) % TotalPages;
                }

                if (CurrentPage + pagesBefore <= 0)
                {
                    pagesAfter -= (CurrentPage + pagesBefore - 1);
                }

                for (long i = CurrentPage + pagesBefore; i < CurrentPage + pagesAfter; i++)
                {
                    if (i > 0 && i < TotalPages + 1)
                    {
                        CreateButton(i);
                    }
                }
            }
            DisableLast = CurrentPage == TotalPages;
            DisableFirst = CurrentPage == 1;
        }

        // Creates a nav button by setting the JSON to the current page number and if it's active.
        private void CreateButton(long pageNumber)
        {
            PagesElementJson page = Pages.Add();
            page.PageNumber = pageNumber;
            page.Active = (CurrentPage == pageNumber);
        }

        private void GetNewPage()
        {
            Library.Data = Db.SQL<Book>("SELECT b FROM KitchenSink.Book b ORDER BY b.Position FETCH ? OFFSET ?", EntriesPerPage, CurrentOffset);
            CreateNavButtons();
        }
    }
}