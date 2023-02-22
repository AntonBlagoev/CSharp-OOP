namespace CarRacing.Models.Racers
{
    using Cars.Contracts;
    public class ProfessionalRacer : Racer
    {
        private const int CurrDrivingExperience = 30;
        private const string CurrRacingBehavior = "strict";

        public ProfessionalRacer(string username, ICar car) : base(username, CurrRacingBehavior, CurrDrivingExperience, car)
        {
        }
        public override void Race()
        {
            base.Race();
            this.DrivingExperience += 10;
        }
    }
}
