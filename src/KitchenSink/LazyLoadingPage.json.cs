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

        private void FillDummyData()
        {
            CreatePerson(1, "Alicia", "Alcott", "");
            CreatePerson(2, "Beatrice", "Black", "");
            CreatePerson(3, "Claire", "Clancy", "");
            CreatePerson(4, "Delilah", "Darcy", "");
            CreatePerson(5, "Ellie", "Earnhart", "");
            CreatePerson(6, "Faith", "Fahrlander", "");
            CreatePerson(7, "Grace", "Gather", "");
        }

        private void CreatePerson(int order, string firstName, string lastName, string favoriteGame)
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
            
            // TODO
            // Need to base the data retrieval on ScheduleTasks, And somehow make them not overlap. And somehow make them "canellable" if the user hovers another target.
            // If the user hovers something else, the scheduleTask should not complete (setTimeout?.. somehow),


            public void Handle(Input.IsHovered action)
            {
                if (!this.DataIsLoaded && !this.RetrievingData)
                {
                    this.RetrievingData = true;
                    CreateData();
                    if (this.ParentPage.SelectedPersonsName == this.FirstName) // Not Working - Check if the person is still hovered - Does not get updated during sleep
                    {
                        this.ParentPage.DisplayedData.DataContent = this.FavoriteGame = this.DataToShow; // This should happen once it HAS the data, and the same person is hovered (meaning the loading was completed)
                        this.DataIsLoaded = true; // - similar to the above, move somewhere
                    }
                }
            }

            public void CreateData () // Maybe make this a Scheduled Task?
            {
                Thread.Sleep(1000);
                FakeDataBase(this.FirstName); // Retrieves this persons favorite game
                this.RetrievingData = false;
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
    }
}
