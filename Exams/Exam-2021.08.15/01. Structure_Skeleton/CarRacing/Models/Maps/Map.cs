namespace CarRacing.Models.Maps
{
    using Contracts;
    using Racers.Contracts;
    public class Map : IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {

            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return $"Race cannot be completed because both racers are not available!";
            }

            else if (!racerOne.IsAvailable())
            {
                return string.Format($"{racerTwo.Username} wins the race! {racerOne.Username} was not available to race!");
            }
            else if (!racerTwo.IsAvailable())
            {
                return string.Format($"{racerOne.Username} wins the race! {racerTwo.Username} was not available to race!");
            }
            else
            {
                racerOne.Race();
                racerTwo.Race();

                var racerOneChanceOfWinning = racerOne.Car.HorsePower * racerOne.DrivingExperience * (racerOne.RacingBehavior == "strict" ? 1.2 : 1.1);
                var racerTwoChanceOfWinning = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * (racerTwo.RacingBehavior == "strict" ? 1.2 : 1.1);

                IRacer winner;

                if (racerOneChanceOfWinning > racerTwoChanceOfWinning)
                {
                    winner = racerOne;
                }
                else
                {
                    winner = racerTwo;
                }

                return string.Format($"{racerOne.Username} has just raced against {racerTwo.Username}! {winner.Username} is the winner!");
            }
        }
    }
}
