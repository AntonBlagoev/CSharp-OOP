namespace Easter.Models.Workshops
{
    using Contracts;
    using Bunnies.Contracts;
    using Eggs.Contracts;
    using System.Linq;
    using Easter.Models.Dyes.Contracts;
    using System.Collections.Generic;

    public class Workshop : IWorkshop
    {
        public void Color(IEgg egg, IBunny bunny)
        {
            foreach (var dye in bunny.Dyes)
            {
                if (bunny.Energy == 0 && bunny.Dyes.Sum(x => x.Power) == 0)
                {
                    break;
                }
                if (dye.IsFinished())
                {
                    continue;
                }
                while (!dye.IsFinished() && !egg.IsDone() && bunny.Energy > 0)
                {
                    bunny.Work();
                    egg.GetColored();
                    dye.Use();
                }

                if (egg.IsDone())
                {
                    break;
                }
            }

            //List<IDye> dyes = bunny.Dyes.ToList();

            //while (!egg.IsDone() && bunny.Energy > 0 && dyes.Any())
            //{
            //    IDye currentDye = dyes.First();

            //    while (!currentDye.IsFinished() && bunny.Energy > 0 && !egg.IsDone())
            //    {
            //        currentDye.Use();
            //        bunny.Work();
            //        egg.GetColored();
            //    }

            //    if (currentDye.IsFinished())
            //        dyes.Remove(currentDye);
            //}

        }
    }
}
