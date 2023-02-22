namespace Gym.Models.Athletes
{
    using System;

    using Utilities.Messages;
    public class Weightlifter : Athlete
    {
        private const int StaminaConst = 50;
        public Weightlifter(string fullName, string motivation, int numberOfMedals) : base(fullName, motivation, numberOfMedals, StaminaConst)
        {
        }

        public override void Exercise()
        {
            this.Stamina += 10;
        }
    }
}
