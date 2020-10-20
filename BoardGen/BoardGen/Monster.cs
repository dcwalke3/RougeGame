using BrandonPlayerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BoardGen
{
    public class Monster : GameCharacter, IActor
    {

        public Monster(Board b, Player a)
        {
            level = BrandonPlayerGen.Random.randInt(a.level-1, a.level + 1);
            if (level == 0)
            {
                level ++;
            }
            attack = (level * 3) + 10;
            defense = (level * 2) + 10;
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
            throw new NotImplementedException();
        }

        public void Interact(Board b, IActor a)
        {
            throw new NotImplementedException();
        }

        public void Move(Board b, Player p)
        {
            int randomDirection = BrandonPlayerGen.Random.randInt(1, 4);
            if (randomDirection == 1)
            {
                if ((b.playerCords[1] - 1) < 0 || b.board[b.playerCords[0], (b.playerCords[1] - 1), b.playerCords[2]].symbol == "#")
                {

                }
                else if (b.board[b.playerCords[0], (b.playerCords[1] - 1), b.playerCords[2]].stairs)
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[b.playerCords[0], b.playerCords[1], b.playerCords[2]] = floor;
                    if (b.board[b.playerCords[0], (b.playerCords[1] - 1), b.playerCords[2]].symbol == "v")
                    {
                        if ((b.playerCords[0] + 1) == b.NumofFloors)
                        {

                        }

                        else
                        {

                            b.playerCords[0]++;
                            b.placeActor(b.playerCords[0]);
                            b.showBoard(b.playerCords[0]);
                            b.updateFloor();
                        }
                    }

                    else if (b.board[b.playerCords[0], (b.playerCords[1] - 1), b.playerCords[2]].symbol == "^")
                    {
                        if ((b.playerCords[0] - 1) == b.NumofFloors)
                        {

                        }
                        else
                        {
                            b.playerCords[0]--;
                            b.placeActor(b.playerCords[0]);
                            b.showBoard(b.playerCords[0]);
                            b.updateFloor();
                        }
                    }
                }
                else
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    Tile player = new Tile("!", ConsoleColor.Red, ConsoleColor.Yellow, false, true);
                    b.board[b.playerCords[0], (b.playerCords[1] - 1), b.playerCords[2]] = player;
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]] = floor;
                    Console.SetCursorPosition(b.playerCords[2], (b.playerCords[1]));
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]].DrawTile();
                    Console.SetCursorPosition(b.playerCords[2], (b.playerCords[1] - 1));
                    b.board[b.playerCords[0], (b.playerCords[1] - 1), b.playerCords[2]].DrawTile();
                    b.playerCords[1]--;
                }
            }

            else if (randomDirection == 2)
            {
                if ((b.playerCords[0] + 1 > (b.height - 1)) || b.board[b.playerCords[0], (b.playerCords[1] + 1), b.playerCords[2]].symbol == "#")
                {

                }
                else if (b.board[b.playerCords[0], (b.playerCords[1] + 1), b.playerCords[2]].stairs)
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[b.playerCords[0], b.playerCords[1], b.playerCords[2]] = floor;
                    if (b.board[b.playerCords[0], (b.playerCords[1] + 1), b.playerCords[2]].symbol == "v")
                    {
                        if ((b.playerCords[0] + 1) == b.NumofFloors)
                        {

                        }

                        else
                        {
                            b.playerCords[0]++;
                            b.placeActor(b.playerCords[0]);
                            b.showBoard(b.playerCords[0]);
                            b.updateFloor();
                        }
                    }

                    else if (b.board[b.playerCords[0], (b.playerCords[1] + 1), b.playerCords[2]].symbol == "^")
                    {
                        if ((b.playerCords[0] - 1) == b.NumofFloors)
                        {

                        }
                        else
                        {
                            b.playerCords[0]--;
                            b.placeActor(b.playerCords[0]);
                            b.showBoard(b.playerCords[0]);
                            b.updateFloor();
                        }
                    }
                }
                else
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    Tile player = new Tile("!",ConsoleColor.Red, ConsoleColor.Yellow,false, true);
                    b.board[b.playerCords[0], (b.playerCords[1] + 1), b.playerCords[2]] = player;
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]] = floor;
                    Console.SetCursorPosition(b.playerCords[2], (b.playerCords[1]));
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]].DrawTile();
                    Console.SetCursorPosition(b.playerCords[2], (b.playerCords[1] + 1));
                    b.board[b.playerCords[0], (b.playerCords[1] + 1), b.playerCords[2]].DrawTile();
                    b.playerCords[1]++;
                }
            }

            else if (randomDirection==3)
            {
                if ((b.playerCords[2] - 1) < 0 || b.board[b.playerCords[0], b.playerCords[1], (b.playerCords[2] - 1)].symbol == "#")
                {

                }
                else if (b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] - 1].stairs)
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[b.playerCords[0], b.playerCords[1], b.playerCords[2]] = floor;
                    if (b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] - 1].symbol == "v")
                    {
                        if ((b.playerCords[0] + 1) == b.NumofFloors)
                        {

                        }

                        else
                        {
                            b.playerCords[0]++;
                            b.placeActor(b.playerCords[0]);
                            b.showBoard(b.playerCords[0]);
                            b.updateFloor();
                        }
                    }

                    else if (b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] - 1].symbol == "^")
                    {

                        if ((b.playerCords[0] - 1) == b.NumofFloors)
                        {

                        }
                        else
                        {

                            b.playerCords[0]--;
                            b.placeActor(b.playerCords[0]);
                            b.showBoard(b.playerCords[0]);
                            b.updateFloor();
                        }
                    }
                }
                else
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    Tile player = new Tile("!", ConsoleColor.Red, ConsoleColor.Yellow, false, true);
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] - 1] = player;
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]] = floor;
                    Console.SetCursorPosition((b.playerCords[2] - 1), b.playerCords[1]);
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] - 1].DrawTile();
                    Console.SetCursorPosition(b.playerCords[2], (b.playerCords[1]));
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]].DrawTile();
                    b.playerCords[2]--;
                }
            }

            else if (randomDirection == 4)
            {
                if ((b.playerCords[2] + 1) < 0 || b.board[b.playerCords[0], b.playerCords[1], (b.playerCords[2] + 1)].symbol == "#")
                {

                }
                else if (b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] + 1].stairs)
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[b.playerCords[0], b.playerCords[1], b.playerCords[2]] = floor;
                    if (b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] + 1].symbol == "v")
                    {
                        if ((b.playerCords[0] + 1) == b.NumofFloors)
                        {

                        }

                        else
                        {
                            b.playerCords[0]++;
                            b.placeActor(b.playerCords[0]);
                            b.showBoard(b.playerCords[0]);
                            b.updateFloor();
                        }
                    }

                    else if (b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] + 1].symbol == "^")
                    {
                        if ((b.playerCords[0] - 1) == b.NumofFloors)
                        {

                        }
                        else
                        {
                            b.playerCords[0]--;
                            b.placeActor(b.playerCords[0]);
                            b.showBoard(b.playerCords[0]);
                            b.updateFloor();
                        }
                    }

                }
                else
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    Tile player = new Tile("!", ConsoleColor.Red, ConsoleColor.Yellow, false, true);
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] + 1] = player;
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]] = floor;
                    Console.SetCursorPosition((b.playerCords[2]), b.playerCords[1]);
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]].DrawTile();
                    Console.SetCursorPosition(b.playerCords[2] + 1, (b.playerCords[1]));
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] + 1].DrawTile();
                    b.playerCords[2]++;
                }
            }


        }
    }
}
