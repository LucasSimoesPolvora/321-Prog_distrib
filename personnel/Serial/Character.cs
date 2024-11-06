using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Serial
{
    public class Character
    {
        private char SEPARATOR = '#';
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public Actor? PlayedBy { get; set; }

        public string Serialize()
        {
            return $"{FirstName}{SEPARATOR}{LastName}{SEPARATOR}{Description}{SEPARATOR}{PlayedBy}";
        }

        public Character Deserialize()
        {
            string? loadJson = "C:\\Users\\pd57mgs\\Downloads\\a.json";
            string[] json = File.ReadAllLines(loadJson);
            return JsonSerializer.Deserialize<Character>(json[0]);
        }
    }
}
