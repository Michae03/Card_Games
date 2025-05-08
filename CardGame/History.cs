using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace CardGame
{
    partial class GameData
    {
        public int gameNumber { get; set; }
        public DateTime dateTime { get; set; }
        public string category { get; set; }
        public User winner { get; set; }
        public int numberOfPlayers { get; set; }
    }
    partial class History
    {
        List<GameData> Games = new List<GameData>();
        private readonly string FilePath = "history.json";

        public void SerializeToJSON()
        {
            string json = JsonSerializer.Serialize(Games, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("history.json", json);
        }

        public void DeserializeFromJSON()
        {
            if (File.Exists("history.json"))
            {
                string json = File.ReadAllText("users.json");
                Games = JsonSerializer.Deserialize<List<GameData>>(json) ?? new List<GameData>();
            }
        }


        public void Add(GameData GameData)
        {
            Games.Add(GameData);
            SerializeToJSON();
        }

        public void Show(GameData GameData)
        {
            
        }
    }
}
