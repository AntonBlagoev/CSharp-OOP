namespace BirthdayCelebrations.Models
{
    using Interfaces;
    public class Robot
    {
        private string model;
        private string id;

        public Robot(string model, string id)
        {
            this.model = model;
            this.id = id;
        }

        public string Model
        {
            get { return model; }
            private set { model = value; }
        }

        public string Id
        {
            get { return this.id; }
            private set { this.id = value; }
        }
    }
}
