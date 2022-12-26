using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace TennisMicroserv.Models
{
    public class PlayerContext : DbContext
    {
        public TennisMicroserv.Models.Root ?root { get; set; }

        public PlayerContext() {
            var url = @".\Data\headtohead.json";
            try
            {
                root = JsonConvert.DeserializeObject<Root>(File.ReadAllText(url));
            }
            catch 
            {
                throw new Exception($"Erreur c'est produite lors de la deserialization du fichier json. url : [{url}].");
            }
        }
    }
}