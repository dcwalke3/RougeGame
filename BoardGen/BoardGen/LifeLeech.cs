using BrandonPlayerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGen
{
    class LifeLeech : GameCharacter, IActor
    {
        public string name = ("Life Leech");

        public int row { get; set; }
        public int col { get; set; }

        public string symbol => "X";

        public ConsoleColor foreColor => ConsoleColor.Green;

        public ConsoleColor backColor => ConsoleColor.Yellow;

        int IActor.level { get {
                return 1;
            } set { 
            } }

        public void Death(Board b)
        {
            throw new NotImplementedException();
        }

        public void Interact(Board b, IActor a)
        {
            throw new NotImplementedException();
        }

        public void Move(Board b, IActor a)
        {
            int randomDirection = BrandonPlayerGen.Random.randInt(1, 5);
            if (randomDirection == 1)
            {
                while (b.board[a.row+1,col].symbol != "#" && b.board[a.row + 1, col].Occupied == null)
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
        }
    }
}
