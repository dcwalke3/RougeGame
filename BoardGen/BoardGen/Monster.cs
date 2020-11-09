using BrandonPlayerGen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BoardGen
{
    public class Monster : GameCharacter, IActor
    {
        public int level
        {
            get; set;
        }
        public Monster(IActor a)
        {
            level = BrandonPlayerGen.Random.randInt(a.level - 2, a.level + 2);
            if (level <= 0)
            {
                level = 1;
            }
            attack = (level* 3) + 10;
            defense = (level* 2) + 10;
            health = (level + 15) * 2;


            string[] namelist = { "Kobol", "Goblin", "Orc", "Goblin Mage",
                                    "Hellfire Wolf", "Minotaur", "Skeleton Warrior",
                                    "Undead Lynch"};
            name = BrandonPlayerGen.Random.Choice(namelist);

        }
        



       
           
        int IActor.row { get; set; }
        int IActor.col { get; set; }

        string IActor.symbol => "!";

        ConsoleColor IActor.foreColor => ConsoleColor.Red;

        ConsoleColor IActor.backColor => ConsoleColor.Yellow;

        


        public void Death(Board b)
        {
            Console.SetCursorPosition(5, 56);
            string deathmessage = $"{name} lvl {level} has died.";
            Console.SetCursorPosition(5, 57);
            
        }


        public void Interact(Board b, IActor a)
        {
            
        }

        public void Move(Board b, IActor a)
        {
            int randomDirection = BrandonPlayerGen.Random.randInt(1, 5);
            if (randomDirection == 1)
            {
                if (b.board[a.row + 1, a.col].symbol == "#")
                {

                }
                else if (b.board[a.row + 1, a.col].stairs)
                {

                }

                else if (b.board[a.row + 1, a.col].Occupied != null)
                {
                    Interact(b, a);
                }
                else
                {
                    Tile monster = new Tile(a.symbol, a.backColor, a.foreColor, false, a);
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[a.row + 1, a.col] = monster;
                    b.board[a.row, a.col] = floor;
                    Console.SetCursorPosition(a.col, a.row);
                    b.board[a.row, a.col].DrawTile();
                    Console.SetCursorPosition(a.col, a.row + 1);
                    b.board[a.row + 1, a.col].DrawTile();
                    a.row++;
                }
            }

            else if (randomDirection == 2)
            {
                if (b.board[a.row - 1, a.col].symbol == "#")
                {

                }
                else if (b.board[a.row - 1, a.col].stairs)
                {

                }
                else if (b.board[a.row - 1, a.col].Occupied != null)
                {
                    Interact(b, a);
                }
                else
                {
                    Tile monster = new Tile(a.symbol, a.backColor, a.foreColor, false, a);
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[a.row - 1, a.col] = monster;
                    b.board[a.row, a.col] = floor;
                    Console.SetCursorPosition(a.col, a.row);
                    b.board[a.row, a.col].DrawTile();
                    Console.SetCursorPosition(a.col, a.row - 1);
                    b.board[a.row - 1, a.col].DrawTile();
                    a.row--;
                }
            }

            else if (randomDirection==3)
            {
                if (b.board[a.row, a.col+1].symbol == "#")
                {

                }
                else if (b.board[a.row , a.col+1].stairs)
                {

                }
                else if (b.board[a.row, a.col+1].Occupied != null)
                {
                    Interact(b, a);
                }
                else
                {
                    Tile monster = new Tile(a.symbol, a.backColor, a.foreColor, false, a);
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[a.row, a.col+1] = monster;
                    b.board[a.row, a.col] = floor;
                    Console.SetCursorPosition(a.col, a.row);
                    b.board[a.row, a.col].DrawTile();
                    Console.SetCursorPosition(a.col+1, a.row);
                    b.board[a.row, a.col+1].DrawTile();
                    a.col++;
                }
            }

            else
            {
                if (b.board[a.row, a.col - 1].symbol == "#")
                {

                }
                else if (b.board[a.row, a.col -1].stairs)
                {

                }
                else if (b.board[a.row, a.col - 1].Occupied != null)
                {
                    Interact(b, a);
                }
                else
                {
                    Tile monster = new Tile(a.symbol, a.backColor, a.foreColor, false, a);
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[a.row, a.col - 1] = monster;
                    b.board[a.row, a.col] = floor;
                    Console.SetCursorPosition(a.col, a.row);
                    b.board[a.row, a.col].DrawTile();
                    Console.SetCursorPosition(a.col - 1, a.row);
                    b.board[a.row, a.col - 1].DrawTile();
                    a.col--;
                }
            }
        }
    }
}
