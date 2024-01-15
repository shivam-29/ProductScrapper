namespace WebScrapper.Infrastructure.Services;
public class Query
{
    public Person GetPerson() => new("Luke Skywalker");
}

public class Person
{
    public Person(string name)
    {
        Name = name;
    }

    public string Name { get; }
}


