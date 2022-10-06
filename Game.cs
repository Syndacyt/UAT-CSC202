using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompletionistPrototype
{
    internal class Game
    {
        //Properties
        public string title { get; set; }
        public string developer { get; set; }
        public string genre { get; set; }
        public string image { get; set; }

        // Constructor
        public Game(string title, string developer, string genre, string boxArt)
        {
            this.title = title;
            this.developer = developer;
            this.genre = genre;
            this.image = boxArt;
        }

    }

    // Example Only
    // I do not feel this is necessary at this time
    internal class AdventureGame : Game
    {
        // Constructor
        public AdventureGame(string title, string developer, string genre, string image) : base(title, developer, genre, image)
        {
            // Other constructor stuff here
        }
    }
}
