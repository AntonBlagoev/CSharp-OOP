using FakeAxeAndDummy.Contracts;
using System;

public class Dummy : ITarget
{
    private int health;
    private int experience;

    public Dummy(int health, int experience)
    {
        this.health = health;
        this.experience = experience;
    }

    public int Health
    {
        get { return this.health; }
    }

    public void TakeAttack(int attackPoints)
    {
        if (this.IsDead())
        {
            throw new InvalidOperationException(String.Format("{0} is dead.",
                this.GetType().Name));
        }

        this.health -= attackPoints;
    }

    public int GiveExperience()
    {
        if (!this.IsDead())
        {
            throw new InvalidOperationException(String.Format("{0} is not dead.",
                this.GetType().Name));
        }

        return this.experience;
    }

    public bool IsDead()
    {
        return this.health <= 0;
    }
}