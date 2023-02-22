namespace CarRacing.Models.Racers
{
    using Cars.Contracts;
    public class StreetRacer : Racer
    {
        private const int CurrDrivingExperience = 10;
        private const string CurrRacingBehavior = "aggressive";
        public StreetRacer(string username, ICar car) : base(username, CurrRacingBehavior, CurrDrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            this.DrivingExperience += 5;
        }
    }
}
