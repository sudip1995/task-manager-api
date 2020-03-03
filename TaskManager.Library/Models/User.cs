namespace TaskManager.Library.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string GetDisplayName()
        {
            var displayName = string.Empty;
            if (string.IsNullOrWhiteSpace(FirstName) == false)
            {
                displayName = FirstName;
            }

            if (string.IsNullOrWhiteSpace(LastName) == false && string.IsNullOrEmpty(displayName) == false)
            {
                displayName += " " + LastName;
            }

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = UserName;
            }

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = Email;
            }
            return displayName;
        }
    }
}