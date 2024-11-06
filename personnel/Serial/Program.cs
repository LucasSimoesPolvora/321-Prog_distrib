using Serial;
using System.Text.Json;

Character Johnny = new Character
{
    FirstName = "Peter",
    LastName = "Parker",
    Description = "Spider-man"
};

Console.WriteLine(JsonSerializer.Serialize(Johnny));
Character load = new Character();
Console.WriteLine(JsonSerializer.Serialize(load.Deserialize()));