using BoardGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BrandonPlayerGen
{
    /// <summary>
    /// 
    /// </summary>
    public class Player: GameCharacter, IActor
    {
        public new string name;
        public int ATK;
        public int DEF;
        public int Health;
        public string HomeTown;
        public string Parents;
        public string job;
        public string trivia;
        public int CurrentDungeonLevel;
        public string History;
        

        

        ConsoleColor IActor.foreColor => ConsoleColor.Green;

        ConsoleColor IActor.backColor => ConsoleColor.Black;

        string IActor.symbol => "@";

        int IActor.row { get; set; }
        int IActor.col { get; set; }

        public Player(int plevel, Board b)
        {
                level = plevel;
                string[] hometownList = {"Williamsburg","Bikini Bottom","SouthPark","Atlantis",
                "Gotham","Millesville"};

                HomeTown = Random.Choice(hometownList);

                
                string[] jobList = { "baker", "farmer", "soldier", "blacksmith", "tailor",
                "innkeeper", "merchant", "thief" };

                job = Random.Choice(jobList);


                string[] parentsList = {"both your parents are dead","your father died as a soldier"+
                    "and your mother works as a innkeeper","both of your parents are bakers",
                    "your father is a blacksmith and mom is a tailor","your mother is a adventurer and your father is a baker",
                    "both of your parents are jobless","Your mother is a mercahnt and your father is a soldier",
                    "both of your parents are traveling merchants."};

                Parents = Random.Choice(parentsList);



                string[] triviaList = {"You dream of one day becoming a adventurer.",
                    "You desire to obtain the infinity gauntlet.","You believe santa is still real.",
                    "You are actually 3 kids in a trenchcoat stacked on top of each other.","You want to become a artist.",
                    "You love doing pinecone arts and crafts.","You are a extremely holy man."};


                CurrentDungeonLevel = (b.playerCords[0]+1);
                


                trivia = Random.Choice(triviaList);
                Console.Write("Enter Character Name Here: ");
                name = Console.ReadLine();


                Console.Clear();

                Health = (StaticRandom.Instance.Next(50, 100) + (level*5));
                ATK = (StaticRandom.Instance.Next(10, 20) + level);
                DEF = (StaticRandom.Instance.Next(10, 20) + level);
                History = $"\nYou are a {job} from {HomeTown}. \nYour Parents " +
                $"statuses are {Parents}.\n {trivia}";

                
        }


        public override string ToString()
        {
            return $"Name: {name}\nLevel: {level}\nHP: {Health}\nAttack: {ATK}  Defense: {DEF}\nBackground: {History};";
        }




        // These 2 functions are for 
        public void FillStatusScreens()
        {
            for (int i = 0; i < 64; i++)
            {
                for (int j = 100; j < 125; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            for (int i = 50; i < 64; i++)
            {
                for (int j = 0; j < 99; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
        }

        public void PrintStatusScreen()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(108,10);
            Console.Write("Status");
            Console.SetCursorPosition(105,12);
            Console.Write("Dungeon Level: " + CurrentDungeonLevel);
            Console.SetCursorPosition(105, 15);
            Console.Write("Name: " + name);
            Console.SetCursorPosition(105, 18);
            Console.Write("Level: " + level);
            Console.SetCursorPosition(105, 21);
            Console.Write("HP: " + Health);
            Console.SetCursorPosition(105,24);
            Console.Write("ATK: " + ATK+ "  DEF: " + DEF);
            
        }

        

        public void LevelUp(Board b, Player p)
        {
            Health += Random.randInt(4, 8);
            ATK += Random.randInt(2, 4);
            DEF += Random.randInt(2, 4);
            b.UpdateStatus(p);
        }

        


        
        

        void IActor.Interact(Board b, IActor a)
        {
            throw new NotImplementedException();
        }

        void IActor.Death(Board b)
        {
            throw new NotImplementedException();
        }

        public void Move(Board b, Player p)
        {
            var keyCheck = Console.ReadKey(true).Key;
            if (keyCheck == ConsoleKey.W)
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
                    Tile player = new Tile("@", ConsoleColor.Green, ConsoleColor.Black, false, a);
                    b.board[b.playerCords[0], (b.playerCords[1] - 1), b.playerCords[2]] = player;
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]] = floor;
                    Console.SetCursorPosition(b.playerCords[2], (b.playerCords[1]));
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]].DrawTile();
                    Console.SetCursorPosition(b.playerCords[2], (b.playerCords[1] - 1));
                    b.board[b.playerCords[0], (b.playerCords[1] - 1), b.playerCords[2]].DrawTile();
                    b.playerCords[1]--;
                }
            }

            else if (keyCheck == ConsoleKey.S)
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
                    Tile player = new Tile("@", ConsoleColor.Green, ConsoleColor.Black, false, true);
                    b.board[b.playerCords[0], (b.playerCords[1] + 1), b.playerCords[2]] = player;
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]] = floor;
                    Console.SetCursorPosition(b.playerCords[2], (b.playerCords[1]));
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]].DrawTile();
                    Console.SetCursorPosition(b.playerCords[2], (b.playerCords[1] + 1));
                    b.board[b.playerCords[0], (b.playerCords[1] + 1), b.playerCords[2]].DrawTile();
                    b.playerCords[1]++;
                }
            }

            else if (keyCheck == ConsoleKey.A)
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
                    Tile player = new Tile("@", ConsoleColor.Green, ConsoleColor.Black, false, true);
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] - 1] = player;
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]] = floor;
                    Console.SetCursorPosition((b.playerCords[2] - 1), b.playerCords[1]);
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2] - 1].DrawTile();
                    Console.SetCursorPosition(b.playerCords[2], (b.playerCords[1]));
                    b.board[b.playerCords[0], (b.playerCords[1]), b.playerCords[2]].DrawTile();
                    b.playerCords[2]--;
                }
            }

            else if (keyCheck == ConsoleKey.D)
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
                    Tile player = new Tile("@", ConsoleColor.Green, ConsoleColor.Black, false, true);
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
