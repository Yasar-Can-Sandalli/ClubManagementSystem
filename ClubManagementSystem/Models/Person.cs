namespace ClubManagementSystem.Models
{
    public abstract class Person : BaseEntity
    {
        private string _firstName;
        private string _lastName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value?.Trim(); } 
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value?.Trim().ToUpper(); }
        }

        public abstract string GetRoleDescription();
    }
}