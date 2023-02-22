using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.Cards
{
    public class Program
    {
        public class Card
        {
            private readonly string face;
            private readonly string suit;

            public Card(string face, string suit)
            {
                this.face = face;
                this.suit = suit;
            }

            public override string ToString()
            {
                return $"[{this.face}{this.suit}]";
            }
        }
        static void Main(string[] args)
        {
            string[] cards = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries);
            List<Card> list = new List<Card>();
            foreach (var item in cards)
            {
                string face = item.Split()[0];
                string suit = item.Split()[1];
                try
                {
                    Card card = CreateCard(face, suit);
                    list.Add(card);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
            Console.WriteLine(String.Join(' ', list));



        }
        private static Card CreateCard(string face, string suit)
        {
            HashSet<string> faces = new HashSet<string> { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

            Dictionary<string, string> suits = new Dictionary<string, string>
            {
                {"S", "\u2660"},
                {"H", "\u2665"},
                {"D", "\u2666"},
                {"C", "\u2663"},
            };

            if (faces.All(f => f != face) || suits.All(s => s.Key != suit))
            {
                throw new ArgumentException("Invalid card!");
            }

            Card card = new Card(face, suits[suit]);

            return card;
        }
    }
}
