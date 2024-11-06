using Serial;
using System.Text.Json;

Character Johnny = new Character
{
    FirstName = "Peter",
    LastName = "Parker",
    Description = "Spider-man",
    PlayedBy = null
};

Console.WriteLine(JsonSerializer.Serialize(Johnny));

string serializedCaracter = Johnny.Serialize();
Johnny.ToJson(serializedCaracter);