namespace Gym.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Core.Contracts;
    using Models.Athletes;
    using Models.Equipment;
    using Models.Equipment.Contracts;
    using Models.Gyms;
    using Models.Gyms.Contracts;
    using Repositories;
    using Utilities.Messages;

    public class Controller : IController
    {
        private EquipmentRepository equipmentRepository;
        private List<IGym> gyms;

        public Controller()
        {
            this.equipmentRepository = new EquipmentRepository();
            this.gyms = new List<IGym>();
        }

        public string AddGym(string gymType, string gymName)
        {
            Gym gym;
            if (gymType == nameof(BoxingGym))
            {
                gym = new BoxingGym(gymName);
            }
            else if (gymType == nameof(WeightliftingGym))
            {
                gym = new WeightliftingGym(gymName);
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidGymType));
            }
            this.gyms.Add(gym);
            return string.Format(OutputMessages.SuccessfullyAdded, gymType);
        }

        public string AddEquipment(string equipmentType)
        {
            IEquipment equipment;
            if (equipmentType == nameof(BoxingGloves))
            {
                equipment = new BoxingGloves();
            }
            else if (equipmentType == nameof(Kettlebell))
            {
                equipment = new Kettlebell();
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidEquipmentType));
            }
            this.equipmentRepository.Add(equipment);
            return string.Format(OutputMessages.SuccessfullyAdded, equipmentType);
        }

        public string InsertEquipment(string gymName, string equipmentType)
        {
            IEquipment equipment = equipmentRepository.FindByType(equipmentType);
            if (equipment == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentEquipment, equipmentType));
            }
            IGym gym = gyms.FirstOrDefault(x => x.Name == gymName);
            gym.Equipment.Add(equipment);
            equipmentRepository.Remove(equipment);

            return string.Format(OutputMessages.EntityAddedToGym, equipmentType, gymName);
        }

        public string AddAthlete(string gymName, string athleteType, string athleteName, string motivation, int numberOfMedals)
        {
            var gym = gyms.FirstOrDefault(x => x.Name == gymName);

            if (athleteType != nameof(Boxer) && athleteType != nameof(Weightlifter)) 
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidAthleteType));
            }
            else if (athleteType == nameof(Boxer) && gym.GetType().Name != nameof(BoxingGym))
            {
                return string.Format(OutputMessages.InappropriateGym);
            }
            else if (athleteType == nameof(Weightlifter) && gym.GetType().Name != nameof(WeightliftingGym))
            {
                return string.Format(OutputMessages.InappropriateGym);
            }
            else
            {
                if (athleteType == nameof(Boxer) && gym.GetType().Name == nameof(BoxingGym))
                {
                    Boxer boxer = new Boxer(athleteName, motivation, numberOfMedals);
                    gym.AddAthlete(boxer);
                }
                else if (athleteType == nameof(Weightlifter) && gym.GetType().Name == nameof(WeightliftingGym))
                {
                    Weightlifter weightlifter = new Weightlifter(athleteName, motivation, numberOfMedals);
                    gym.AddAthlete(weightlifter);
                }
                return String.Format(OutputMessages.EntityAddedToGym, athleteType, gymName);
            }
        }

        public string TrainAthletes(string gymName)
        {
            int athletesCount = 0;
            foreach (var athlete in this.gyms.Where(x => x.Name == gymName))
            {
                athlete.Exercise();
                athletesCount++;
            }
            return string.Format(OutputMessages.AthleteExercise, athletesCount);
        }

        public string EquipmentWeight(string gymName)
        {
            double totalWeight = 0.0;
            foreach (var equipment in this.gyms.Where(x => x.Name == gymName))
            {
                totalWeight += equipment.EquipmentWeight;
            }
            //return string.Format(OutputMessages.EquipmentTotalWeight, gymName, totalWeight);
            return $"The total weight of the equipment in the gym {gymName} is {totalWeight:F2} grams.";
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var gym in this.gyms)
            {
                sb.AppendLine(gym.GymInfo());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
