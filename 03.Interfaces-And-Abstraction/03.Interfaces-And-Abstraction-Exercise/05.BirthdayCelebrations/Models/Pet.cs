namespace BirthdayCelebrations.Models
{
using Interfaces;
    public class Pet : IBirthdays
    {
        private string name;
        private string birthdate;

        public Pet(string name, string birthdate)
        {
            this.name = name;
            this.birthdate = birthdate;
        }

        public string Name
        {
            get { return this.name; }
            private set { this.name = value; }
        }

        public string Birthdate
        {
            get { return this.birthdate; }
            private set { this.birthdate = value; }
        }
    }
}
