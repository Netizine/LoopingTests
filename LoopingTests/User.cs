namespace LoopingTests
{
    public class User
    {
        private string _firstName;
        private string _lastName;
        private string _email;

        private User()
        {

        }
        public User(string firstName, string lastName, string email)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
        }
        public string FirstName
        {
            get => _firstName;
            set => _firstName = value;
        }

        public string LastName
        {
            get => _lastName;
            set => _lastName = value;
        }

        public string Email
        {
            get => _email;
            set => _email = value;
        }

        public string GetFirstName()
        {
            return _firstName;
        }

        public string GetLastName()
        {
            return _firstName;
        }

        public string GetEmail()
        {
            return _email;
        }
    }

}
