using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandonPlayerGen
{
    public class Tile
    {
        public string symbol;
        public ConsoleColor foreColor;
        public ConsoleColor backColor;
        public bool stairs;
        public bool Occupied;
        public bool enemy;
        public bool npc;
        

        public Tile(string boardSymbol, ConsoleColor
            back, ConsoleColor fore, bool stairsHere = false,
            bool playerHere = false, bool enemyHere = false,
            bool NPC = false)
        {
            backColor = back;
            foreColor = fore;
            symbol = boardSymbol;
            stairs = stairsHere;
            Occupied = playerHere;
            enemy = enemyHere;
            npc = NPC;
        }
        
        // Converts Tile to something able to be written on.
        public void DrawTile()
        {
            Console.BackgroundColor = backColor;
            Console.ForegroundColor = foreColor;
            Console.Write(symbol);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        


    }
}
