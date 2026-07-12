namespace WealthSync.Domain.Aggregates.Household;
using System;


public class Household
{
    public Guid Id { get; private set; }
    public String Name { get; private set; }
    
    public Household(String name)
    {
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Invalid Household Name");
        }

        if (name.Length < 3)
        {
            throw new ArgumentException("Household needs 3 letters at least");
        }

        Id = Guid.NewGuid();
        this.Name = name;
    }

}