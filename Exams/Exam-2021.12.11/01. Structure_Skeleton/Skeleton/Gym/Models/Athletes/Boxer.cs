namespace Gym.Models.Athletes
{
    using System;

    using Utilities.Messages;
    public class Boxer : Athlete
    {
        private const int StaminaConst = 60;
        public Boxer(string fullName, string motivation, int numberOfMedals) : base(fullName, motivation, numberOfMedals, StaminaConst)
        {
        }

        public override void Exercise()
        {
            this.Stamina += 15;
        }
    }
}
