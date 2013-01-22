using GrayHills.Matches;

namespace GrayHills.FlameCage.Client.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public UserViewModel(User user)
        {
            this.ID = user.ID;
            this.Name = user.Name;
        }
    }
}
