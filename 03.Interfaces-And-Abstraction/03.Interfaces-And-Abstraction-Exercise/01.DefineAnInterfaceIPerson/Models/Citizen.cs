namespace PersonInfo.Models
{
    using System;

    using Interfaces;

    public class Citizen : IPerson
    {
        private string name;
        private int age;
        public Citizen(string name, int age)
        {
            Name = name;
            Age = age;
        }
        public string Name 
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Cannot be null");
                }
                this.name = value;
            } 
        }
        public int Age
        {
            get
            {
                return this.age;
            }
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Must be positive number");
                }
                this.age = value;
            }
        }
    }
}
