using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
	{
		private List<Character> party;
		private List<Item> pool;
		public WarController()
		{
            party = new List<Character>();	
			pool= new List<Item>();

        }

		public string JoinParty(string[] args)
		{
			string characterType = args[0];
			string name = args[1];

			if (characterType != nameof(Warrior) && characterType != nameof(Priest))
			{
				throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, characterType));
            }

			Character character;

			if (characterType == nameof(Warrior))
			{
				character = new Warrior(name);
			}
			else
			{
                character = new Priest(name);
            }

			party.Add(character);
			return string.Format(SuccessMessages.JoinParty, name);

		}

		public string AddItemToPool(string[] args)
		{
            string itemName = args[0];

            if (itemName != nameof(FirePotion) && itemName != nameof(HealthPotion))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemName));
            }

			Item item;

			if (itemName == nameof(FirePotion))
			{
				item = new FirePotion();
			}
			else
			{
				item = new HealthPotion();
			}
			pool.Add(item);
            return string.Format(SuccessMessages.AddItemToPool, itemName);
        }

        public string PickUpItem(string[] args)
		{
            string characterName = args[0];

			var character = party.FirstOrDefault(x => x.Name == characterName);
			if (character == null)
			{
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

			if (!pool.Any())
			{
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemPoolEmpty));
            }

			var item = pool.Last();
			character.Bag.AddItem(item);
            return string.Format(SuccessMessages.PickUpItem, characterName, item.GetType().Name);
        }

        public string UseItem(string[] args)
		{
            string characterName = args[0];
            string itemName = args[1];

			var character = party.FirstOrDefault(x => x.Name == characterName);
			if (character == null)
			{
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

			var item = pool.FirstOrDefault(x => x.GetType().Name == itemName);
			character.UseItem(item);
            return string.Format(SuccessMessages.UsedItem, characterName, itemName);

        }

        public string GetStats()
		{
			var sortedParty = party.OrderByDescending(x => x.IsAlive).ThenByDescending(x => x.Health);
			
			StringBuilder sb = new StringBuilder();

			foreach (var character in sortedParty)
			{
				sb.AppendLine($"{character.Name} - HP: {character.Health}/{character.BaseHealth}, AP: {character.Armor}/{character.BaseArmor}, Status: {(character.IsAlive == true ? "Alive" : "Dead")}");
            }

			return sb.ToString().TrimEnd();
		}

		public string Attack(string[] args)
		{
            string attackerName = args[0];
            string receiverName = args[1];

			var attacker = party.FirstOrDefault(x => x.Name == attackerName);
            var receiver = party.FirstOrDefault(x => x.Name == receiverName);

			if (attacker == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, attackerName));
            }
            if (receiver == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receiverName));
            }

			if (attacker.GetType().Name != nameof(Warrior))
			{
                throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attackerName));
            }

			(attacker as Warrior).Attack(receiver);

			StringBuilder sb = new StringBuilder();

			sb.Append($"{attackerName} attacks {receiverName} for {attacker.AbilityPoints} hit points!");
            sb.AppendLine($"{receiverName} has {receiver.Health}/{receiver.BaseHealth} HP and {receiver.Armor}/{receiver.BaseArmor} AP left!");

			if (!receiver.IsAlive)
			{
				sb.AppendLine($"{receiver.Name} is dead!");
            }

			return sb.ToString().TrimEnd();
        }

        public string Heal(string[] args)
		{
            string healerName = args[0];
            string healingReceiverName = args[1];

            var healer = party.FirstOrDefault(x => x.Name == healerName);
            var receiver = party.FirstOrDefault(x => x.Name == healingReceiverName);

            if (healer == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healerName));
            }
            if (receiver == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healingReceiverName));
            }

            if (healer.GetType().Name != nameof(Priest))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healerName));
            }


            (healer as Priest).Heal(receiver);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{healer.Name} heals {receiver.Name} for {healer.AbilityPoints}! {receiver.Name} has {receiver.Health} health now!");

            return sb.ToString().TrimEnd();
        }
	}
}
