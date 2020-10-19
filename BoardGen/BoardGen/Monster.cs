using BrandonPlayerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGen
{
    public class Monster: GameCharacter
    {
        public int row;
        public Monster(Player p)
        {
            level = BrandonPlayerGen.Random.randInt((p.level - 2), (p.level + 1));
            attack = (level *3)+10;
            defense = (level * 2) + 10;
            health = (level + 15) * 2;

            string [] namelist = { "Kobol", "Goblin", "Orc", "Goblin Mage", 
                                   "Hellfire Wolf", "Minotaur", "Skeleton Warrior",
                                   "Undead Lynch"};
            name = BrandonPlayerGen.Random.Choice(namelist);
        }
    }
}
