using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompletionistPrototype
{
    public class Collectible
    {
        string name { get; set; }

        public Collectible(string name)
        {
            this.name = name;
        }
    }

    public class Weapon : Collectible 
    {

        string weaponType { get; set; }
        string damageType { get; set; }

        // Weapon requirements
        int requiredStrength { get; set; }
        int requiredDexterity { get; set; }
        int requiredIntelligence { get; set; }
        int requiredFaith { get; set; }
        int requiredArcane { get; set; }

        // Base damage values
        int physical { get; set; }   
        int magic { get; set; }
        int fire { get; set; }
        int lightning { get; set; }
        int critical { get; set; }

        // Scaling
        string strengthScaling { get; set; }
        string dexterityScaling { get; set; }
        string intelligenceScaling { get; set; }
        string faithScaling { get; set; }
        string arcaneScaling { get; set; }

        public Weapon(string name) : base(name)
        { 
            
        }

    }
}
