using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Runtime.Serialization;


namespace CardGame
{
    public partial class User
    {
        
        public string name { get; set; }
        public string id { get; set; }
        public User()
        {
        }
  
       public User(string name, string id)
            {
                this.name = name;
                this.id = id;
            }
       
    }



    partial class Users
    {
        public List<User> listOfUsers = new List<User>();
        public List<User> listOfPlayers = new List<User>();
        public User currentUser = new User();
        private readonly string FilePath = "users.json";

        public Users()
        {
            DeserializeFromJSON();
        }
        public string ShowUsers()
        {
            string result ="";
            foreach (User u in listOfPlayers)
            {
                result += u.name;
                result += "\n";

            }
            return result;
        }

        public bool AddUniqueUser(string name)
        {
            if (listOfUsers.Any(u => u.name == name))
            {
                return false;
            }

            string uniqueId;
            do
            {
                uniqueId = Guid.NewGuid().ToString();
            } while (listOfUsers.Any(u => u.id == uniqueId));

            User newUser = new User(name, uniqueId);
            listOfUsers.Add(newUser);
            listOfPlayers.Add(newUser);
            SerializeToJSON();
            return true;
        }


        public void Add(string name)
        {
            foreach (User u in listOfUsers)
            {
                if (u.name == name) 
                { 
                    listOfPlayers.Add(u);
                    return;
                }
            }

            AddUniqueUser(name);

        }

        public void SerializeToJSON()
        {
            string json = JsonSerializer.Serialize(listOfUsers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("users.json", json);
        }

        public void DeserializeFromJSON()
        {
            if (File.Exists("users.json"))
            {
                string json = File.ReadAllText("users.json");
                listOfUsers = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
        }
   
    }
}
