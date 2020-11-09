using BoardGen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BrandonPlayerGen
{
    
    public class Player: GameCharacter, IActor
    {
        /// <summary>
        /// Class for player that derives the 
        /// GameCharacter class as well as
        /// the interface IActor.
        /// </summary>
        /// <param name="name">Name of the character.</param>
        /// <param name="level">Level of the player</param>
        /// <param name="ATK">Attack stat of the character.</param>
        /// <param name="DEF">Defense stat of the character.</param>
        /// <param name="MaxHealth">Max health the player can reach for his level.</param>
        /// <param name="Health">Current health the player has (i.e HP: 80/110  here 80=health and 110=maxhealth).</param>
        /// <param name="History">History of the player's character.</param>
        /// <param name="HomeTown">Hometown of the player's character.</param>
        /// <param name="Parents">Parents of the player's character.</param>
        /// <param name="Job">Job of the player's character.</param>
        /// <param name="trivia">Random trivia of the player's character.</param>
        /// <param name="CurrentDungeonLevel">The current dungeon level the player is on .of the player's character.</param>
        /// <param name="XPNeeded">The amount of EXP points needed to level up.</param>
        /// <param name="CurrentXP">The amount of EXP points a player has earned since last level up.</param>
        /// <param name="TotalXP">Total all time amount of EXP earned.</param>

        public new string name;
        public int ATK;
        public int DEF;
        public int MaxHealth;
        public int Health;
        public string HomeTown;
        public string Parents;
        public string job;
        public string trivia;
        public int CurrentDungeonLevel;
        public string History;
        public int XPNeeded;
        public int CurrentXP;
        public int TotalXP;
        

        
        /// <summary>
        /// IActor interface added.
        /// </summary>
        ConsoleColor IActor.foreColor => ConsoleColor.Green;

        ConsoleColor IActor.backColor => ConsoleColor.Black;

        string IActor.symbol => "@";

        int IActor.row { get; set; }
        int IActor.col { get; set; }
        int IActor.level { get; set; }

        
        /// <summary>
        /// Player Constructor
        /// </summary>
        /// <param name="plevel">The level the player should enter as.</param>
        /// <param name="b">The map  the player is on.</param>
        public Player(int plevel, Board b)
        {
            TotalXP = 0;    
             level= plevel;
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

                MaxHealth = (StaticRandom.Instance.Next(50, 100) + (level*5));
                Health = MaxHealth;
                ATK = (StaticRandom.Instance.Next(10, 20) + level);
                DEF = (StaticRandom.Instance.Next(5,15) + level);
                History = $"\nYou are a {job} from {HomeTown}. \nYour Parents " +
                $"statuses are {Parents}.\n {trivia}";

                
        }


        public override string ToString()
        {
            return $"Name: {name}\nLevel: {level}\nHP: {MaxHealth}\nAttack: {ATK}  Defense: {DEF}\nBackground: {History};";
        }




        /// <summary>
        /// Fills the side and bottom with color for the status screen.
        /// </summary>        
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

        /// <summary>
        /// Prints the Status Screen/HUD for the player on the right.
        /// </summary>
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
            Console.Write($"HP: {Health}/{MaxHealth}");
            Console.SetCursorPosition(105,24);
            Console.Write("ATK: " + ATK+ "  DEF: " + DEF);
            Console.SetCursorPosition(100, 27);
            Console.Write($"XP Needed to level up: {XPNeeded}");

        }

        /// <summary>
        /// Checks if the player has recieved enough exp to level up.
        /// </summary>
        /// <param name="board">Map</param>
        /// <param name="player">Player</param>
        /// <param name="m">Monster killed</param>
        public void checkLevelUP(Board board, IActor m)
        {
            
            int XPGained = (int)Math.Floor((decimal)Math.Pow((m.level +2),1.2));
            TotalXP += XPGained;
            CurrentXP += XPGained;
            XPNeeded = (int)Math.Floor(Math.Pow((level - 1 + level), 1.2));
            Console.SetCursorPosition(5, 58);
            Console.Write($"You gained {XPGained} EXP.");
            if (CurrentXP > XPNeeded)
            {

                int xpgained = CurrentXP - XPNeeded;
                LevelUp(board, m,xpgained);
            }
            else
            {
                Console.SetCursorPosition(5, 59);
                Console.Write($"{XPNeeded} EXP until next level up.");
            }
        }

        /// <summary>
        /// Function used to increase player level and stats.
        /// </summary>
        /// <param name="b">Map.</param>
        /// <param name="p">Player.</param>
        /// <param name="xpsurplus">The excess amount of EXP gained.</param>
        public void LevelUp(Board b, IActor a,int xpsurplus)
        {
            MaxHealth += Random.randInt(8, 16);
            ATK += Random.randInt(4, 8);
            DEF += Random.randInt(2,4);
            level++;
            CurrentXP = xpsurplus;
            checkLevelUP(b,a);
            Health = MaxHealth;
            XPNeeded = (int)Math.Floor((level - 1 + level) * 1.2);
            Console.SetCursorPosition(5, 59);
            Console.Write($"You have increased your level to lvl {level}");
            
        }



        public void UpdateStatus()
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(108, 10);
            Console.Write("Status");
            Console.SetCursorPosition(105, 15);
            Console.Write("Name: " + name);
            Console.SetCursorPosition(105, 18);
            Console.Write("Level: " + level);
            Console.SetCursorPosition(105, 21);
            Console.Write($"HP: {health}/{MaxHealth}");
            Console.SetCursorPosition(105, 24);
            Console.Write("ATK: " + ATK + "  DEF: " + DEF);
            Console.SetCursorPosition(100, 27);
            Console.Write($"XP Needed to level up: {XPNeeded}");
        }



        /// <summary>
        /// The method of interacting with the player.
        /// </summary>
        /// <param name="b">The Board it will take place on.</param>
        /// <param name="a">The IActor to be interacted with</param>
        public void Interact(Board b, IActor a)
        {
            if (a is Monster)
            {
                Console.Clear();
                Console.SetCursorPosition(55, 25);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{name} lvl: {level}");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(60, 28);
                Console.Write("vs");
                Console.SetCursorPosition(55, 31);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{a.name} lvl: {a.level}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                
                Console.Clear();
                string battletext =

       @"
                                ██████╗  █████╗ ████████╗████████╗██╗     ███████╗
                                ██╔══██╗██╔══██╗╚══██╔══╝╚══██╔══╝██║     ██╔════╝
                                ██████╔╝███████║   ██║      ██║   ██║     █████╗  
                                ██╔══██╗██╔══██║   ██║      ██║   ██║     ██╔══╝  
                                ██████╔╝██║  ██║   ██║      ██║   ███████╗███████╗
                                ╚═════╝ ╚═╝  ╚═╝   ╚═╝      ╚═╝   ╚══════╝╚══════╝
                                                  
";

                Console.SetCursorPosition(15, 5);
                Console.Write(battletext);
                Console.SetCursorPosition(10, 10);
                Console.Write($"{name} lvl: {level}");
                Console.SetCursorPosition(10, 12);
                Console.Write($"HP: {Health}/{MaxHealth}");
                Console.SetCursorPosition(95, 10);
                Console.Write($"{a.name} lvl: {a.level}");
                Console.SetCursorPosition(95, 12);
                Console.Write($"HP: {a.health}");


                Console.SetCursorPosition(50,19);
                Console.Write("~~Combat Log~~");

                for (int i=16; i < 97; i++)
                {
                    Console.SetCursorPosition(i,20);
                    Console.Write("=");
                }
                for (int i = 21; i < 55; i++)
                {
                    Console.SetCursorPosition(15, i);
                    Console.Write("||");
                    Console.SetCursorPosition(97, i);
                    Console.Write("||");
                }
                for (int i = 16; i < 97; i++)
                {
                    Console.SetCursorPosition(i, 55);
                    Console.Write("=");
                }
                Console.SetCursorPosition(20, 25);
                Console.Write($"You have encourted {a.name}.");
                Console.SetCursorPosition(20, 27);
                Console.Write($"What do you do?");
                
                

                while (a.health > 0 && Health>0)
                {
                    Console.SetCursorPosition(20, 50);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("(N)ormal Attack");
                    Console.SetCursorPosition(20, 51);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("(H)eavy Attack");
                    Console.SetCursorPosition(20, 52);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("(G)aurd");
                    Console.SetCursorPosition(20, 53);
                    Console.ForegroundColor = ConsoleColor.White;

                    var entry = Console.ReadKey(true).Key;
                    if(entry == ConsoleKey.N)
                    {
                        int AttackBonus = Random.randInt((int)Math.Floor(ATK * -.25), (int)Math.Floor(ATK * .25));
                        int TempAttack = ATK;
                         TempAttack+= AttackBonus;
                        int tempDefense = DEF;
                        int DefenseBonus = Random.randInt((int)Math.Floor(DEF * -.25), (int)Math.Floor(DEF * .25));
                        tempDefense += DefenseBonus;
                        int attackDealt = TempAttack - a.defense;
                        int damagetaken = a.attack - tempDefense;
                        if (damagetaken < 0)
                        {
                            damagetaken = 0;
                        }
                        if (attackDealt < 3)
                        {
                            attackDealt = 3;
                        }
                        a.health -= attackDealt;
                        Health -= damagetaken;
                        Console.SetCursorPosition(20, 29);
                        Console.Write($"You dealt {attackDealt}pts of damage to {a.name}");
                        Console.SetCursorPosition(20, 30);
                        Console.Write($"{a.name} dealt {damagetaken}pts of damage to you.");

                        for (int i = 10; i <110;i++)
                        {
                            Console.SetCursorPosition(i, 12);
                            Console.Write(" ");
                        }
                        Console.SetCursorPosition(10, 12);
                        Console.Write($"HP: {Health}/{MaxHealth}");
                        Console.SetCursorPosition(95, 12);
                        Console.Write($"HP: {a.health}");

                    }
                    else if(entry == ConsoleKey.H)
                    {
                        int tempAttack = (int)Math.Floor(ATK * 1.5);
                        int tempDefense = (int)Math.Floor(DEF *.5);
                        int AttackBonus = Random.randInt((int)Math.Floor(ATK * -.25), (int)Math.Floor(ATK * .25));
                        tempAttack += AttackBonus;
                        int DefenseBonus = Random.randInt((int)Math.Floor(DEF * -.25), (int)Math.Floor(DEF * .25));
                        tempDefense += DefenseBonus;
                        int attackDealt = tempAttack - a.defense;
                        int damagetaken = a.attack - tempDefense;
                        if (damagetaken < 0)
                        {
                            damagetaken = 0;
                        }
                        if (attackDealt < 3)
                        {
                            attackDealt = 3;
                        }
                        a.health -= attackDealt;
                        Health -= damagetaken;
                        Console.SetCursorPosition(20, 29);
                        Console.Write($"You dealt {attackDealt}pts of damage to {a.name}");
                        Console.SetCursorPosition(20, 30);
                        Console.Write($"{a.name} dealt {damagetaken}pts of damage to you.");

                        for (int i = 10; i < 110; i++)
                        {
                            Console.SetCursorPosition(i, 12);
                            Console.Write(" ");
                        }
                        Console.SetCursorPosition(10, 12);
                        Console.Write($"HP: {Health}/{MaxHealth}");
                        Console.SetCursorPosition(95, 12);
                        Console.Write($"HP: {a.health}");
                    }
                    else if(entry == ConsoleKey.G)
                    {
                        int tempAttack = 0;
                        int tempDefense = DEF * 2;
                        int AttackBonus = Random.randInt((int)Math.Floor(ATK * -.25), (int)Math.Floor(ATK * .25));
                        tempAttack += AttackBonus;
                        int DefenseBonus = Random.randInt((int)Math.Floor(DEF * -.25), (int)Math.Floor(DEF * .25));
                        tempDefense += DefenseBonus;
                        int attackDealt =  tempAttack- a.defense;
                        int damagetaken = a.attack - tempDefense;
                        if (damagetaken < 0)
                        {
                            damagetaken = 0;
                        }
                        if (attackDealt < 0)
                        {
                            attackDealt = 0;
                        }
                        a.health -= attackDealt;
                        Health -= damagetaken;
                        Console.SetCursorPosition(20, 29);
                        Console.Write($"You dealt {attackDealt}pts of damage to {a.name}");
                        Console.SetCursorPosition(20, 30);
                        Console.Write($"{a.name} dealt {damagetaken}pts of damage to you.");

                        for (int i = 10; i < 110; i++)
                        {
                            Console.SetCursorPosition(i, 12);
                            Console.Write(" ");
                        }
                        Console.SetCursorPosition(10, 12);
                        Console.Write($"HP: {Health}/{MaxHealth}");
                        Console.SetCursorPosition(95, 12);
                        Console.Write($"HP: {a.health}");
                    }
                    else
                    {
                        Console.SetCursorPosition(20, 54);
                        Console.Write("Invalid Input: Hurry and pick a new option.");
                    }

                }

                if (a.health <= 0)
                {
                    checkLevelUP(b, a);
                }
                Console.Clear();
                b.showBoard();
                b.FillStatusScreens();
                PrintStatusScreen();
            }
        }

        /// <summary>
        /// Causes a game over for the player due to player death.
        /// </summary>
        /// <param name="b">The map.</param>
        public void Death(Board b)
        {
            Console.Clear();
            string gameOver = @"
 .----------------.  .----------------.  .----------------.   .----------------.  .----------------.  .----------------.  .----------------. 
| .--------------. || .--------------. || .--------------. | | .--------------. || .--------------. || .--------------. || .--------------. |
| |  ____  ____  | || |     ____     | || | _____  _____ | | | |  ________    | || |     _____    | || |  _________   | || |  ________    | |
| | |_  _||_  _| | || |   .'    `.   | || ||_   _||_   _|| | | | |_   ___ `.  | || |    |_   _|   | || | |_   ___  |  | || | |_   ___ `.  | |
| |   \ \  / /   | || |  /  .--.  \  | || |  | |    | |  | | | |   | |   `. \ | || |      | |     | || |   | |_  \_|  | || |   | |   `. \ | |
| |    \ \/ /    | || |  | |    | |  | || |  | '    ' |  | | | |   | |    | | | || |      | |     | || |   |  _|  _   | || |   | |    | | | |
| |    _|  |_    | || |  \  `--'  /  | || |   \ `--' /   | | | |  _| |___.' / | || |     _| |_    | || |  _| |___/ |  | || |  _| |___.' / | |
| |   |______|   | || |   `.____.'   | || |    `.__.'    | | | | |________.'  | || |    |_____|   | || | |_________|  | || | |________.'  | |
| |              | || |              | || |              | | | |              | || |              | || |              | || |              | |
| '--------------' || '--------------' || '--------------' | | '--------------' || '--------------' || '--------------' || '--------------' |
 '----------------'  '----------------'  '----------------'   '----------------'  '----------------'  '----------------'  '----------------' 
";
            Console.Write(gameOver);
        }

        
        /// <summary>
        /// Moves the player around the board based off of WASD input.
        /// </summary>
        /// <param name="b">The map.</param>
        /// <param name="actor">The player.</param>
        public void Move(Board b, IActor actor)
        {
            var keyCheck = Console.ReadKey(true).Key;
            if (keyCheck == ConsoleKey.S)
            {
                if (b.board[actor.row + 1, actor.col].symbol == "#")
                {

                }
                else if (b.board[actor.row + 1, actor.col].stairs)
                {
                    CurrentDungeonLevel++;
                    b.makeBoard(b.height, b.width);
                    b.CreateRooms();
                    b.MakeCorridors();
                    b.placeStairs();
                    b.placeActor(actor);
                    b.FillStatusScreens();
                    b.showBoard();
                }
                else if(b.board[actor.row + 1, actor.col].Occupied != null)
                {
                    IActor c = b.board[actor.row + 1, actor.col].Occupied;
                    actor.Interact(b, c);
                }
                else
                {
                    Tile player = new Tile(actor.symbol, actor.backColor, actor.foreColor, false, actor);
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[actor.row + 1, actor.col] = player;
                    b.board[actor.row, actor.col] = floor;
                    Console.SetCursorPosition(actor.col, actor.row);
                    b.board[actor.row, actor.col].DrawTile();
                    Console.SetCursorPosition(actor.col, actor.row + 1);
                    b.board[actor.row + 1, actor.col].DrawTile();
                }
            }

            else if (keyCheck == ConsoleKey.W)
            {
                if (b.board[actor.row - 1, actor.col].symbol == "#")
                {

                }
                else if (b.board[actor.row - 1, actor.col].stairs)
                {
                    CurrentDungeonLevel++;
                    b.makeBoard(b.height, b.width);
                    b.CreateRooms();
                    b.MakeCorridors();
                    b.placeStairs();
                    b.placeActor(actor);
                    b.showBoard();
                    b.FillStatusScreens();
                    PrintStatusScreen();

                }
                else if (b.board[actor.row - 1, actor.col].Occupied != null)
                {
                    IActor c = b.board[actor.row - 1, actor.col].Occupied;
                    actor.Interact(b, c);
                }
                else
                {
                    Tile player = new Tile(actor.symbol, actor.backColor, actor.foreColor, false, actor);
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[actor.row - 1, actor.col] = player;
                    b.board[actor.row, actor.col] = floor;
                    Console.SetCursorPosition(actor.col, actor.row);
                    b.board[actor.row, actor.col].DrawTile();
                    Console.SetCursorPosition(actor.col, actor.row - 1);
                    b.board[actor.row - 1, actor.col].DrawTile();
                }
            }

            else if (keyCheck == ConsoleKey.D)
            {
                if (b.board[actor.row, actor.col+1].symbol == "#")
                {

                }
                else if (b.board[actor.row, actor.col+1].stairs)
                {
                    CurrentDungeonLevel++;
                    b.makeBoard(b.height, b.width);
                    b.CreateRooms();
                    b.MakeCorridors();
                    b.placeStairs();
                    b.placeActor(actor);
                    b.showBoard();
                    b.FillStatusScreens();
                    PrintStatusScreen();
                }
                else if (b.board[actor.row, actor.col+1].Occupied != null)
                {
                    IActor c = b.board[actor.row, actor.col+1].Occupied;
                    actor.Interact(b, c);
                }
                else
                {
                    Tile player = new Tile(actor.symbol, actor.backColor, actor.foreColor, false, actor);
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[actor.row, actor.col+1] = player;
                    b.board[actor.row, actor.col] = floor;
                    Console.SetCursorPosition(actor.col, actor.row);
                    b.board[actor.row, actor.col].DrawTile();
                    Console.SetCursorPosition(actor.col+1, actor.row);
                    b.board[actor.row, actor.col+1].DrawTile();
                }
            }

            else if (keyCheck == ConsoleKey.A)
            {
                if (b.board[actor.row, actor.col - 1].symbol == "#")
                {

                }
                else if (b.board[actor.row, actor.col - 1].stairs)
                {
                    CurrentDungeonLevel++;
                    b.makeBoard(b.height, b.width);
                    b.CreateRooms();
                    b.MakeCorridors();
                    b.placeStairs();
                    b.placeActor(actor);
                    b.showBoard(); 
                    b.FillStatusScreens();
                    PrintStatusScreen();                
                }
                else if (b.board[actor.row, actor.col-1].Occupied != null)
                {
                    IActor c = b.board[actor.row, actor.col-1].Occupied;
                    actor.Interact(b, c);
                }
                else
                {
                    Tile player = new Tile(actor.symbol, actor.backColor, actor.foreColor, false, actor);
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    b.board[actor.row, actor.col - 1] = player;
                    b.board[actor.row, actor.col] = floor;
                    Console.SetCursorPosition(actor.col, actor.row);
                    b.board[actor.row, actor.col].DrawTile();
                    Console.SetCursorPosition(actor.col - 1, actor.row);
                    b.board[actor.row, actor.col - 1].DrawTile();
                }
            }
        }
    }
}
