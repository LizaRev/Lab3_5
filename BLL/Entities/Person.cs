namespace BLL.Entities;

public abstract class Person
{
    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }

    protected Person(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
}