namespace FoodShortage.Models
{
    using Interfaces;
    public class Rebel : IRebel, IBuyer
    {
        private string name;
        private int age;
        private string group;
        private int food = 0;
        public Rebel(string name, int age, string group)
        {
            this.Name = name;
            this.Age = age;
            this.Group = group;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Age
        {
            get { return age; }
            private set { age = value; }
        }
        public string Group
        {
            get { return group; }
            private set { group = value; }
        }
        public int Food
        {
            get { return food; }
            private set { food = value; }
        }
        public void BuyFood()
        {
            this.Food += 5;
        }
    }
}
