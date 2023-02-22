using System;
using NUnit.Framework;

public class HeroRepositoryTests
{
    [Test]
    public void Test_Hero_Constructor()
    {
        Hero hero = new Hero("Pesho", 6);

        Assert.That(hero, Is.Not.Null);
        Assert.That(hero.Name, Is.EqualTo("Pesho"));
        Assert.That(hero.Level, Is.EqualTo(6));
    }


    [Test]
    public void Test_Create_Hero()
    {
        HeroRepository heroRepo = new HeroRepository();
        Hero hero = new Hero("Pesho", 6);

        string expected = "Successfully added hero Pesho with level 6";
        string actual = heroRepo.Create(hero);

        Assert.That(heroRepo.Heroes.Count, Is.EqualTo(1));
        Assert.That(actual, Is.EqualTo(expected));

    }

    [Test]
    public void Test_Create_Hero_Null_Exception()
    {
        HeroRepository heroRepo = new HeroRepository();
        Assert.Throws<ArgumentNullException>(() =>
        {
            heroRepo.Create(null);
        }, "Hero is null");
    }

    [Test]
    public void Test_Create_Hero_Exist_Exception()
    {
        HeroRepository heroRepo = new HeroRepository();
        Hero hero = new Hero("Pesho", 6);
        heroRepo.Create(hero);

        Assert.Throws<InvalidOperationException>(() =>
        {
            heroRepo.Create(hero);
        }, "Hero with name Pesho already exists");
    }

    [Test]
    public void Test_Remove_Hero()
    {
        HeroRepository heroRepo = new HeroRepository();
        Hero hero1 = new Hero("Pesho", 6);
        Hero hero2 = new Hero("Gosho", 8);
        Hero hero3 = new Hero("Ivan", 12);

        heroRepo.Create(hero1);
        heroRepo.Create(hero2);
        heroRepo.Create(hero3);

        heroRepo.Remove("Pesho");

        Assert.That(heroRepo.Heroes.Count, Is.EqualTo(2));
        Assert.That(heroRepo.Remove("Ivan"), Is.EqualTo(true));
    }

    [Test]
    public void Test_Remove_Hero_Null_Exception()
    {
        HeroRepository heroRepo = new HeroRepository();
        Hero hero1 = new Hero("Pesho", 6);
        Hero hero2 = new Hero("Gosho", 8);

        heroRepo.Create(hero1);
        heroRepo.Create(hero2);

        Assert.Throws<ArgumentNullException>(() =>
        {
            heroRepo.Remove(null);
        }, "Name cannot be null");
    }
    [Test]
    public void Test_Remove_Hero_Invalid_Name_Exception()
    {
        HeroRepository heroRepo = new HeroRepository();
        Hero hero1 = new Hero("Pesho", 6);
        Hero hero2 = new Hero("Gosho", 8);

        heroRepo.Create(hero1);
        heroRepo.Create(hero2);

        Assert.That(heroRepo.Heroes.Count, Is.EqualTo(2));
        Assert.That(heroRepo.Remove("Ivan"), Is.EqualTo(false));
    }

    [Test]
    public void Test_GetHeroWithHighestLevel()
    {
        HeroRepository heroRepo = new HeroRepository();
        Hero hero1 = new Hero("Pesho", 6);
        Hero hero2 = new Hero("Gosho", 8);
        Hero hero3 = new Hero("Ivan", 12);

        heroRepo.Create(hero1);
        heroRepo.Create(hero2);
        heroRepo.Create(hero3);

        Assert.That(heroRepo.GetHeroWithHighestLevel(), Is.EqualTo(hero3));
    }

    [Test]
    public void Test_GetHero()
    {
        HeroRepository heroRepo = new HeroRepository();
        Hero hero1 = new Hero("Pesho", 6);
        Hero hero2 = new Hero("Gosho", 8);
        Hero hero3 = new Hero("Ivan", 12);

        heroRepo.Create(hero1);
        heroRepo.Create(hero2);
        heroRepo.Create(hero3);

        Assert.That(heroRepo.GetHero("Ivan"), Is.EqualTo(hero3));
    }


}