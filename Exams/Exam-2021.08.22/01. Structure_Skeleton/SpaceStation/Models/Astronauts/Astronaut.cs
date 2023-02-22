namespace SpaceStation.Models.Astronauts
{
    using System;
    using System.Text;

    using Bags;
    using Bags.Contracts;
    using Contracts;
    public abstract class Astronaut : IAstronaut
    {
        private string name;
        private double oxygen;

        protected Astronaut(string name, double oxygen)
        {
            this.Name = name;
            this.Oxygen = oxygen;
            this.Bag = new Backpack();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(string.Format("Astronaut name cannot be null or empty."));
                }
                name = value;
            }
        }

        public double Oxygen
        {
            get => this.oxygen;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format("Cannot create Astronaut with negative oxygen!"));
                }
                oxygen = value;
            }
        }

        public bool CanBreath => this.Oxygen > 0;

        public IBag Bag { get; }

        public virtual void Breath()
        {
            if (this.Oxygen - 10 < 0)
            {
                this.Oxygen = 0;
            }
            else
            {
                this.Oxygen -= 10;
            }
        }

        public override string ToString()
        {
            string bagItems = this.Bag.Items.Count == 0 ? "none" : string.Join(", ", this.Bag.Items);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {this.Name}");
            sb.AppendLine($"Oxygen: {this.Oxygen}");
            sb.AppendLine($"Bag items: {bagItems}");

            return sb.ToString().TrimEnd();

        }
    }
}
