namespace NavalVessels.Models
{
    using Contracts;
    using System.Text;

    public class Submarine : Vessel, ISubmarine
    {
        private const int InitialArmorThickness = 200;
        private const double MainWeaponCaliberIncrease = 40;
        private const double SpeedrDecrease = 4;

        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, InitialArmorThickness)
        {
            this.SubmergeMode = false;
        }

        public override void RepairVessel()
        {
            this.ArmorThickness = InitialArmorThickness;
        }

        public bool SubmergeMode { get; private set; }
        public void ToggleSubmergeMode()
        {
            if (!this.SubmergeMode)
            {
                this.MainWeaponCaliber += MainWeaponCaliberIncrease;
                this.Speed -= SpeedrDecrease;
            }
            else
            {
                this.MainWeaponCaliber -= MainWeaponCaliberIncrease;
                this.Speed += SpeedrDecrease;
            }
            this.SubmergeMode = !this.SubmergeMode;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" *Submerge mode: {(this.SubmergeMode ? "ON" : "OFF")}");
            return sb.ToString().TrimEnd();
        }
    }
}
