namespace WarCroft.Entities.Characters
{
    using System;

    using Characters.Contracts;
    using Inventory;
    using WarCroft.Constants;

    public class Warrior : Character, IAttacker
    {
        public Warrior(string name) : base(name, 100, 50, 40, new Satchel())
        {
        }

        public void Attack(Character character)
        {
            this.EnsureAlive();
            character.EnsureAlive();

            if (this.Name == character.Name)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.CharacterAttacksSelf));

            }
            character.TakeDamage(this.AbilityPoints);
        }
    }
}


