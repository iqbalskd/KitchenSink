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
            CreatePerson(1, "Alicia", "Alcott", "");
            CreatePerson(2, "Beatrice", "Black", "");
            CreatePerson(3, "Claire", "Clancy", "");
            CreatePerson(4, "Delilah", "Darcy", "");
            CreatePerson(5, "Ellie", "Earnhart", "");
            CreatePerson(6, "Faith", "Fahrlander", "");
            CreatePerson(7, "Grace", "Gather", "");
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
                var test = this.FirstName;
                var test2 = this.IsHovered;
                var test3 = action;

                //if (action.Value == 0) // On de-hover
                //{
                //    this.ParentPage.LoadVisualCss = "none";
                //    this.ParentPage.DataVisualCss = "none";
                //    return;
                //}
                //if (this.DataIsLoaded == 0)
                //{
                    //this.ParentPage.LoadVisualCss = "block";
                    //this.ParentPage.DataVisualCss = "none";
                //}
                //else if (this.DataIsLoaded == 1)
                //{
                //    this.ParentPage.LoadVisualCss = "block";
                //    this.ParentPage.DataVisualCss = "none";
                //    return;
                //}

                CreateData();
                //this.ParentPage.LoadVisualCss = "none"; // Disables the loading and shows the data after a while
                //this.ParentPage.DataVisualCss = "block";

                //if (action.Value == 0) // If the item gets de-hovered
                //{
                //    this.ParentPage.LoadVisualCss = "none"; // Displays the loading
                //    this.ParentPage.DataVisualCss = "none";
                //    return;
                //}

                //if (this.DataIsLoaded == 1)
                //{
                //    this.DataToShow = this.FavoriteGame;
                //    this.ParentPage.DataVisualCss = "block"; // Changes the CSS of the data-box to "block" in order for it to appear on the screen
                //    return;
                //}

                //this.ParentPage.LoadVisualCss = "block"; // Displays the loading
                //this.ParentPage.DataVisualCss = "none";


                //this.CreateData(); // Creates the data
                //if (action.Value == 1) // If this person is still being hovered - Need to get current value of isHovered
                //{
                //    this.DataIsLoaded = 1; // Functions like a bool. It sets it to true
                //}

                //this.ParentPage.LoadVisualCss = "none"; // Disables the loading and shows the data after a while
                //this.ParentPage.DataVisualCss = "block";

                // this.DataToShow = "Test"; // this.DataToShow = CreateDataAccordingly(); -> Return data
                //this.ParentPage.DisplayedData.DataContent = this.DataToShow;
            }

            public void CreateData () // Maybe Switch to ScheduledTask?
            {
                Thread.Sleep(1000);
                FakeDataBase(this.FirstName);

                this.ParentPage.DisplayedData.DataContent = this.DataToShow; // Updates the view model to display the correct data
            }

            public void FakeDataBase (string firstName) // Picks out different data depending on who is invoking this function
            {
                switch (firstName)
                {
                    case "Alicia":
                        this.DataToShow = this.FavoriteGame = "The Last of Us";
                        break;

                    case "Beatrice":
                        this.DataToShow = this.FavoriteGame = "Dragon Age: Inquisition";
                        break;

                    case "Claire":
                        this.DataToShow = this.FavoriteGame = "Final Fantasy XIII";
                        break;

                    case "Delilah":
                        this.DataToShow = this.FavoriteGame = "World of Warcraft";
                        break;

                    case "Ellie":
                        this.DataToShow = this.FavoriteGame = "Overwatch";
                        break;

                    case "Faith":
                        this.DataToShow = this.FavoriteGame = "Pokemon X";
                        break;

                    case "Grace":
                        this.DataToShow = this.FavoriteGame = "Counter Strike";
                        break;
                }

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
