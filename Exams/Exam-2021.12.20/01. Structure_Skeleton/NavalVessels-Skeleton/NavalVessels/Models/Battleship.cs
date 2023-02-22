namespace NavalVessels.Models
{
    using Contracts;
    using System.Text;

    public class Battleship : Vessel, IBattleship
    {
        private const int InitialArmorThickness = 300;
        private const double MainWeaponCaliberIncrease = 40;
        private const double SpeedrDecrease = 5;

        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, InitialArmorThickness)
        {
            this.SonarMode = false;
        }

        public override void RepairVessel()
        {
            this.ArmorThickness = InitialArmorThickness;
        }

        public bool SonarMode { get; private set; }
        public void ToggleSonarMode()
        {
            if (!this.SonarMode)
            {
                this.MainWeaponCaliber += MainWeaponCaliberIncrease;
                this.Speed -= SpeedrDecrease;
            }
            else
            {
                this.MainWeaponCaliber -= MainWeaponCaliberIncrease;
                this.Speed += SpeedrDecrease;
            }
            this.SonarMode = !this.SonarMode;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($" *Sonar mode: {(this.SonarMode ? "ON" : "OFF")}");
            return sb.ToString().TrimEnd();
        }

    }
}
