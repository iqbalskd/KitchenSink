using Starcounter;
using System.Threading;

namespace KitchenSink
{
    partial class LazyLoadingPage : Json
    {
        protected override void OnData()
        {
            base.OnData();
            FillDummyData();
        }

        void Handle(Input.SelectedPersonsData action)
        {
            //var test = action;
            //LazyLoadingPagePeopleData pplData;
            //pplData = this.PeopleData.Add();
            //pplData.Random = "Hello";
        }

        void Handle(Input.SelectedPersonsName action)
        {
            //People[0].FirstName = "Alice";
        }

        private void FillDummyData() // Just some random names, and added "a favorite game" just for fun
        {
            CreatePerson(1, "Alicia", "Alcott", "The Last of Us");
            CreatePerson(2, "Beatrice", "Black", "Dragon Age: Inquisition");
            CreatePerson(3, "Claire", "Clancy", "Final Fantasy XIII");
            CreatePerson(4, "Delilah", "Darcy", "World of Warcraft");
            CreatePerson(5, "Ellie", "Earnhart", "Overwatch");
            CreatePerson(6, "Faith", "Fahrlander", "Pokemon X");
            CreatePerson(7, "Grace", "Gather", "Battlefield 4");
        }

        private void CreatePerson(int order, string firstName, string lastName, string favoriteGame) // function to create a person
        {
            LazyLoadingPagePeople person;
            person = People.Add();
            person.Order = order;
            person.FirstName = firstName;
            person.LastName = lastName;
            person.FavoriteGame = favoriteGame;
        }

        [LazyLoadingPage_json.People]
        partial class LazyLoadingPagePeople : Json
        {
            public LazyLoadingPage ParentPage
            {
                get
                {
                    return this.Parent.Parent as LazyLoadingPage;
                }
            }

            public void Handle(Input.IsHovered action) // TODO: NOT set DataIsLoaded to 1 if the user has left the area
            {
                if (action.Value == 0) // If the item gets de-hovered
                {
                    this.ParentPage.LoadVisualCss = "none"; // Displays the loading
                    this.ParentPage.DataVisualCss = "none";
                    return;
                }

                if (this.DataIsLoaded == 1)
                {
                    this.ParentPage.DataVisualCss = "block"; // Changes the CSS of the data-box to "block" in order for it to appear on the screen
                    return;
                }

                this.ParentPage.LoadVisualCss = "block"; // Displays the loading
                this.ParentPage.DataVisualCss = "none";


                this.CreateData(); // Creates the data
                this.ParentPage.LoadVisualCss = "none"; // Disables the loading and shows the data after a while
                this.ParentPage.DataVisualCss = "block";

               // this.DataToShow = "Test"; // this.DataToShow = CreateDataAccordingly(); -> Return data
                this.ParentPage.DisplayedData.Random = this.DataToShow;
            }

            public void CreateData ()
            {
                Thread.Sleep(1000);
                this.DataToShow = this.FavoriteGame;
                if (this.IsHovered == 1) // If this person is still being hovered
                {
                    this.DataIsLoaded = 1; // Functions like a bool. It sets it to true
                }
                //switch case? compare the name and fill data accordingly?
            }
        }

        [LazyLoadingPage_json.DisplayedData]
        partial class LazyLoadingPageDisplayedData : Json
        {
            public LazyLoadingPage ParentPage
            {
                get
                {
                    return this.Parent.Parent as LazyLoadingPage;
                }
            }
        }
    }
}
