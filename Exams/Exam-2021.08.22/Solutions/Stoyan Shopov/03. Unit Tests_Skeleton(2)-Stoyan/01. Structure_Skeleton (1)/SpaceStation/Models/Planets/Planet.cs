﻿using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;

        public Planet(string name)
        {
            this.Name = name;

            this.Items = new List<string>();
        }

        public ICollection<string> Items { get; }

        public string Name 
        { 
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Name),
                        ExceptionMessages.InvalidPlanetName);
                }

                name = value;
            }
        }
    }
}
