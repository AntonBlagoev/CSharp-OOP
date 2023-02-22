using System;
using NUnit.Framework;

public class HeroRepositoryTests
{
    private HeroRepository heroRepository;

    [SetUp]
    public void SetUp()
    {
        this.heroRepository = new HeroRepository();
    }

    [Test]
    public void CtorShouldInitializeRequiredValues()
    {
        Assert.IsNotNull(heroRepository.Heroes);
    }

    [Test]
    public void CreateShouldThrowExceptionForNull()
    {
        Assert.Throws<ArgumentNullException>(
            () => heroRepository.Create(null));
    }

    [Test]
    public void CreateShouldThrowExceptionForDuplicateHeros()
    {
        var hero = new Hero("Stoyan", 50);

        heroRepository.Create(hero);

        Assert.Throws<InvalidOperationException>(
            () => heroRepository.Create(hero));
    }

    [Test]
    public void CreateShouldCreateHeroWithValidData()
    {
        var hero = new Hero("Stoyan", 50);

        var message = heroRepository.Create(hero);
        var expectedMessage = "Successfully added hero Stoyan with level 50";
        Assert.AreEqual(1, heroRepository.Heroes.Count);
        Assert.AreEqual(expectedMessage, message);
    }

    [TestCase(null)]
    [TestCase(" ")]
    [TestCase("")]
    public void RemoveShouldThrowExceptionForNullOrWhiteSpace(string name)
    {
        Assert.Throws<ArgumentNullException>(
            () => heroRepository.Remove(name));
    }

    [Test]
    public void RemoveShouldRemoveHeroWithValidData()
    {
        var hero = new Hero("Stoyan", 50);
        var message = heroRepository.Create(hero);

        var isRemoved = heroRepository.Remove("Stoyan");
        
        Assert.IsTrue(isRemoved);
        Assert.AreEqual(0, heroRepository.Heroes.Count);
    }

    [Test]
    public void RemoveShouldRemoveHeroWithMissingData()
    {
        var isRemoved = heroRepository.Remove("Stoyan");

        Assert.IsFalse(isRemoved);
        Assert.AreEqual(0, heroRepository.Heroes.Count);
    }

    [Test]
    public void GetHeroWithHighestLevelSuccess()
    {
        var stoyan = new Hero("Stoyan", 50);
        var niki = new Hero("Niki", 100);
        var viktor = new Hero("Viktor", 90);

        heroRepository.Create(stoyan);
        heroRepository.Create(niki);
        heroRepository.Create(viktor);

        var hero = heroRepository.GetHeroWithHighestLevel();

        Assert.IsTrue(niki == hero);
    }

    [Test]
    public void GetHeroShouldReturnHero()
    {
        var stoyan = new Hero("Stoyan", 50);
        var niki = new Hero("Niki", 100);
        var viktor = new Hero("Viktor", 90);

        heroRepository.Create(stoyan);
        heroRepository.Create(niki);
        heroRepository.Create(viktor);

        var hero = heroRepository.GetHero("Viktor");

        Assert.AreSame(viktor, hero);
    }
}
