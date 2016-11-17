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

            public void Handle(Input.IsHovered action)
            {
                CreateData();
            }

            public void CreateData () // Maybe Switch to ScheduledTask?
            {
                Thread.Sleep(1000);
                FakeDataBase(this.FirstName); // Retrieves this persons favorite game

                this.ParentPage.DisplayedData.DataContent = this.DataToShow; // Updates the view model to display the correct data
            }

            public void FakeDataBase (string firstName) // Picks out different "data" depending on who is invoking this function
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
