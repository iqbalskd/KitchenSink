using Starcounter;

namespace KitchenSink
{
    partial class LazyLoadingPage : Json
    {
        protected override void OnData()
        {
            base.OnData();
            FillDummyData();
        }

        void Handle(Input.ShowLoading action)
        {

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
        }
    }
}
