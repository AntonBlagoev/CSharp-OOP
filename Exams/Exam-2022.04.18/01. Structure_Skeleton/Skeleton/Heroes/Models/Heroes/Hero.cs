namespace Heroes.Models.Heroes
{
    using System;
    using System.Text;
    using Contracts;
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armor;
        private IWeapon weapon;


        protected Hero(string name, int health, int armour)
        {
            this.Name = name;
            this.Health = health;
            this.Armour = armour;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format("Hero name cannot be null or empty."));
                }
                name = value;
            }
        }
        public int Health
        {
            get => health;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format("Hero health cannot be below 0."));
                }
                health = value;
            }
        }
        public int Armour
        {
            get => armor;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format("Hero armour cannot be below 0."));
                }
                armor = value;
            }
        }
        public bool IsAlive => this.Health > 0; // this.Health > 0 ? true : false

        public IWeapon Weapon
        {
            get => weapon;
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException("Weapon cannot be null.");
                }
                weapon = value;
            }
        }
        public void AddWeapon(IWeapon weapon)
        {
            this.weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            var armorLeft = this.Armour - points;

            if (armorLeft > 0)
            {
                this.Armour = armorLeft;
            }
            else
            {
                this.Armour = 0;
                var damage = -armorLeft;
                var healthLeft = this.Health - damage;

                if (healthLeft > 0)
                {
                    this.Health = healthLeft;
                }
                else
                {
                    this.Health = 0;
                }
            }

        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.GetType().Name}: {this.Name}");
            sb.AppendLine($"--Health: {this.Health}");
            sb.AppendLine($"--Armour: {this.Armour}");
            sb.Append($"--Weapon: {(this.Weapon == null ? "Unarmed" : this.Weapon.Name)}");

            return sb.ToString().TrimEnd();
        }
    }
}
