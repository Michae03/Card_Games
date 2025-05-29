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
    public partial class GameData
    {
        public int gameNumber { get; set; }
        public DateTime dateTime { get; set; }
        public string category { get; set; }
        public string winner { get; set; }
        public int numberOfPlayers { get; set; }
    }
    public partial class History
    {
        public List<GameData> Games = new List<GameData>();
        private readonly string FilePath = "history.json";

        public void SerializeToJSON()
        {
            File.Create("history.json").Close();
            string json = JsonSerializer.Serialize(Games, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("history.json", json);
        }

        public void DeserializeFromJSON()
        {
            if (File.Exists("history.json"))
            {
                string json = File.ReadAllText("history.json");
                Games = JsonSerializer.Deserialize<List<GameData>>(json) ?? new List<GameData>();
            }
        }

        public void AddRandomRecords()
        {
         
            GameData GameData = new GameData();
            GameData.dateTime = DateTime.Now;
            GameData.gameNumber = 2;
            GameData.category = "member";
            GameData.winner = "Jagoda";
            GameData.numberOfPlayers = 3;

     
            GameData GameData2 = new GameData();
            GameData2.dateTime = DateTime.Now;
            GameData2.gameNumber = 3;
            GameData2.category = "membe";
            GameData2.winner = "Erty";
            GameData2.numberOfPlayers = 2;

            Games.Add(GameData);
            Games.Add(GameData2);
            SerializeToJSON();
        }
        public void Add(GameData GameData)
        {
            Games.Add(GameData);
            SerializeToJSON();
        }

        public void ClearHistory()
        {
            File.WriteAllText("history.json", "[]");
        }
        public string Show()
        {
            DeserializeFromJSON();
      
            string result = "";
          
            foreach (GameData g in Games)
            {
                result += g.gameNumber;
                result += ". ";
                result += g.category;
                result += " wygrał ";
                result += g.winner;
                result += " liczba graczy: ";
                result += g.numberOfPlayers;
                result += " data gry: ";
                result += g.dateTime;
                result += "\n";
            }
            return result;
        
        }
    }
}
