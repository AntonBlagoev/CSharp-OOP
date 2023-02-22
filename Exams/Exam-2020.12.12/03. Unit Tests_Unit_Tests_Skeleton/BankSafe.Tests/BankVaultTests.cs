using NuGet.Frameworks;
using NUnit.Framework;
using System;

namespace BankSafe.Tests
{
    public class BankVaultTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Item()
        {
            Item item = new Item("Pesho", "42");

            Assert.That(item, Is.Not.Null);
            Assert.That(item.Owner, Is.EqualTo("Pesho"));
            Assert.That(item.ItemId, Is.EqualTo("42"));
        }

        [Test]
        public void Test_BankVault()
        {
            BankVault bank = new BankVault();

            Assert.That(bank.VaultCells.Count, Is.EqualTo(12));
            Assert.True(bank.VaultCells.ContainsKey("B4"));
        }

        [Test]
        public void Test_BankVault_AddItem()
        {
            BankVault bank = new BankVault();
            Item item = new Item("Pesho", "42");

            var actual = bank.AddItem("B4", item);
            var expected = "Item:42 saved successfully!";

            Assert.That(bank.VaultCells.Count, Is.EqualTo(12));
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Test_BankVault_AddItem_Cell_Not_Exist()
        {
            BankVault bank = new BankVault();
            Item item = new Item("Pesho", "42");

            Assert.Throws<ArgumentException>(() =>
            {
                bank.AddItem("F4", item);
            }, "Cell doesn't exists!");
        }

        [Test]
        public void Test_BankVault_AddItem_Cell_Not_Empty()
        {
            BankVault bank = new BankVault();
            Item item = new Item("Pesho", "42");
            bank.AddItem("B4", item);

            Assert.Throws<ArgumentException>(() =>
            {
                bank.AddItem("B4", item);
            }, "Cell is already taken!");

        }
        [Test]
        public void Test_BankVault_AddItem_Item_Exist()
        {
            BankVault bank = new BankVault();
            Item item = new Item("Pesho", "42");
            bank.AddItem("B4", item);

            Assert.Throws<InvalidOperationException>(() =>
            {
                bank.AddItem("A2", item);
            }, "Item is already in cell!");

        }

        [Test]
        public void Test_Remove_Item()
        {
            BankVault bank = new BankVault();
            Item item = new Item("Pesho", "42");

            bank.AddItem("B4", item);

            var actual = bank.RemoveItem("B4", item);
            var expected = "Remove item:42 successfully!";

            Assert.That(bank.VaultCells.Count, Is.EqualTo(12)); 
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void Test_Remove_Item_Cell_Not_Exist()
        {
            BankVault bank = new BankVault();
            Item item = new Item("Pesho", "42");

            bank.AddItem("B4", item);

            Assert.Throws<ArgumentException>(() =>
            {
                bank.RemoveItem("F4", item);
            }, "Cell doesn't exists!");

        }

        [Test]
        public void Test_Remove_Item_Not_Exist()
        {
            BankVault bank = new BankVault();
            Item item = new Item("Pesho", "42");

            Assert.Throws<ArgumentException>(() =>
            {
                bank.RemoveItem("B4", item);
            }, "Item in that cell doesn't exists!");

        }

    }
}