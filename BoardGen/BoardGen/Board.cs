using BrandonPlayerGen;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Random = BrandonPlayerGen.Random;

namespace BrandonPlayerGen
{
    public class Board
    {
        public Tile[,,] board;   
        public int height;
        public int width;
        public int NumofFloors;
        public int NumofRooms;
        public int[,,] midpointCoords;
        public int[] playerCords;
        

        public Board()
        {
            
            NumofRooms = 10;
            NumofFloors = 5;
            midpointCoords = new int[NumofFloors,NumofRooms,2];
            height = 50;
            width = 100;
            playerCords = new int[3];
            Console.Clear();
            Tile[,,] board = makeBoard(NumofFloors,height, width);
            CreateRooms();
            MakeCorridors();
            placeStairs();
            placePlayer(0);
            
        }
        
        // As name implies makes board.
        public Tile[,,] makeBoard(int FloorNums ,int height, int width)
        {
            Tile[,,] gameboard = new Tile[FloorNums,height, width];
            for (int z = 0; z < (FloorNums); z++)
            {
                for (int i = 0; i < (height); i++)
                {
                    for (int j = 0; j < (width); j++)
                    {
                        Tile empty = new Tile("#", ConsoleColor.Black, ConsoleColor.White);
                        gameboard[z, i, j] = empty;

                        board = gameboard;
                    }
                }
            }

            
            return board;
        }

        // Creates the cords and height and width and then calls
        // makeRoom multiple times.
        public Tile[,,] CreateRooms()
        {

            for (int z = 0; z < (NumofFloors); z++)
            {
                for (int i = 0; i < (NumofRooms); i++)
                {

                    int hcord = Random.randInt(1, (height - 2));
                    int wcord = Random.randInt(1, (width - 2));
                    int hrand = Random.randInt(10, 20);
                    int wrand = Random.randInt(10, 20);
                    while (hcord + hrand > (height - 2) || wrand + wcord > (width - 2))
                    {
                        hcord = Random.randInt(1, (height - 2));
                        wcord = Random.randInt(1, (width - 2));
                        hrand = Random.randInt(4, 8);
                        wrand = Random.randInt(4, 8);
                    }
                    int midpointx = (hcord + (hcord + hrand)) / 2;
                    int midpointy = (wcord + (wcord + wrand)) / 2;
                    midpointCoords[z, i, 0] = midpointx;
                    midpointCoords[z, i, 1] = midpointy;

                    MakeRoom(z,hcord, wcord, hrand, wrand);

                }
            }
            return board;
        }

        // Creates 1 room off given coords and a height and width.
        public Tile[,,] MakeRoom(int floorNum,int coord1, int coord2, int bheight, int bwidth)
        {
            
            for (int i = 0; i < bheight; i++)
            {
                for (int j = 0; j < bwidth; j++)
                {
                    
                    Tile roomfloor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    board[floorNum,coord1+i,coord2+j] = roomfloor;
                }
            }
            return board;
        }

        // Compares two sets of cords to create a corridor.
        public Tile[,,] MakeCorridors()
        {

            for (int z = 0; z < (NumofFloors); z++)
            {
                for (int i = 0; i < (NumofRooms-1); i++)
                {
                    int hcord1 = midpointCoords[z, i, 0];
                    int wcord1 = midpointCoords[z, i, 1];
                    int hcord2 = midpointCoords[z, (i + 1), 0];
                    int wcord2 = midpointCoords[z, (i + 1), 1];
                    if (hcord1 > hcord2)
                    {
                        if (wcord1 > wcord2)
                        {
                            while (wcord1 > wcord2)
                            {

                                Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                                board[z, hcord1, wcord1] = floor;
                                wcord1--;

                            }

                            while (hcord1 > hcord2)
                            {
                                Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                                board[z, hcord1, wcord1] = floor;
                                hcord1--;
                            }
                        }
                        else if (wcord1 <= wcord2)
                        {
                            while (wcord1 <= wcord2)
                            {
                                Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                                board[z, hcord2, wcord2] = floor;
                                wcord2--;
                            }
                            while (hcord1 > hcord2)
                            {
                                Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                                board[z, hcord1, wcord1] = floor;
                                hcord1--;
                            }
                        }
                    }
                    else if (hcord1 <= hcord2)
                    {
                        if (wcord1 > wcord2)
                        {
                            while (hcord1 < hcord2)
                            {
                                Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                                board[z, hcord1, wcord1] = floor;
                                hcord1++;
                            }
                            while (wcord1 > wcord2)
                            {
                                Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black); ;
                                board[z, hcord1, wcord2 + 1] = floor;
                                wcord2++;
                            }
                        }
                        else if (wcord1 <= wcord2)
                        {
                            while (hcord2 > hcord1)
                            {
                                Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                                board[z, hcord1, wcord1] = floor;
                                hcord1++;
                            }
                            while (wcord2 >= wcord1)
                            {
                                Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                                board[z, hcord1, wcord1] = floor;
                                wcord1++;
                            }
                        }
                    }
                }
            }
            return board;
        }

        // placeStairs should be called last so a corridor does not go overtop of it.
        public Tile[,,] placeStairs()
        {
            for (int z = 0; z < (NumofFloors-1); z++)
            {
                int randommidpoint = Random.randInt(0, (NumofRooms));
                int randomx = midpointCoords[z, randommidpoint, 0];
                int randomy = midpointCoords[z, randommidpoint, 1];
                Tile stairs = new Tile("^", ConsoleColor.Red, ConsoleColor.Yellow,
                    true, false);
                board[z, randomx, randomy] = stairs;
            }
            return board;
        }

        public Tile[,,] placePlayer(int current_level)
        {
            int randommidpoint = Random.randInt(current_level, (NumofRooms-1));
            int randomx = Random.randInt((midpointCoords[current_level, randommidpoint, 0])-2,(midpointCoords[current_level, randommidpoint, 0]+2));
            int randomy = Random.randInt(((midpointCoords[current_level, randommidpoint, 1]) - 2), (midpointCoords[current_level, randommidpoint, 1] + 2));
            Tile player = new Tile("@", ConsoleColor.Green, ConsoleColor.Black, false, true);

            board[current_level, randomx, randomy]=player;
            playerCords[0] = current_level;
            playerCords[1] = randomx;
            playerCords[2] = randomy;
            return board;
        }

        // Shows board.
        public void showBoard(int currentLevel)
        {

            for (int i = 0; i < (height - 1); i++)
            {
                for (int j = 0; j < (width - 1); j++)
                {

                    board[currentLevel, i, j].DrawTile();
                }
                Console.Write("\n");
            }

            for (int i = 0; i < 64; i++)
            {
                Console.SetCursorPosition(99, i);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("|");
            }


            for (int i = 0; i < 100; i++)
            {
                Console.SetCursorPosition(i, 49);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("-");
            }

        }




        public void CheckPlayerPos()
        {
            for (int z = 0; z < (NumofFloors); z++)
            {
                for (int i = 0; i < (height); i++)
                {
                    for (int j = 0; j < (width); j++)
                    {
                        if (board[z, i, j].Occupied)
                        {
                            playerCords[0] = z;
                            playerCords[1] = i;
                            playerCords[2] = j;
                        }
                    }
                }
            }
        }



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

        public void PrintStatusScreen(int name, int Level,int Dlevel, int HP, int attack, int defense)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(108, 10);
            Console.Write("Status");
            Console.SetCursorPosition(105, 12);
            Console.Write("Dungeon Level: " + Dlevel);
            Console.SetCursorPosition(105, 15);
            Console.Write("Name: " + name);
            Console.SetCursorPosition(105, 18);
            Console.Write("Level: " + Level);
            Console.SetCursorPosition(105, 21);
            Console.Write("HP: " + HP);
            Console.SetCursorPosition(105, 24);
            Console.Write("ATK: " + attack + "  DEF: " + defense);

        }


        public Tile[,,] CommandPlayer()
        {
            while(Console.ReadKey(true).Key == ConsoleKey.W)
            {
                if ((playerCords[ 1] - 1) < 0|| board[playerCords[0],(playerCords[1]-1),playerCords[2]].symbol=="#")
                {
                    
                }
                else if (board[playerCords[0], (playerCords[ 1] - 1), playerCords[2]].stairs)
                {
                    playerCords[0]++;
                    placePlayer(playerCords[0]);
                    Console.Clear();
                    showBoard(playerCords[0]);
                }
                else
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    Tile player = new Tile("@", ConsoleColor.Green, ConsoleColor.Black, false, true);
                    board[playerCords[0], (playerCords[1] - 1), playerCords[2]] = player;
                    board[playerCords[0], (playerCords[1]), playerCords[ 2]] = floor;
                    Console.SetCursorPosition(playerCords[2], (playerCords[1]));
                    board[playerCords[0], (playerCords[ 1]), playerCords[2]].DrawTile();
                    Console.SetCursorPosition(playerCords[2], (playerCords[1] - 1));
                    board[playerCords[0], (playerCords[1] - 1), playerCords[2]].DrawTile();
                    playerCords[ 1]--;
                }
                
            }
            while (Console.ReadKey(true).Key == ConsoleKey.S)
            {
                if((playerCords[0] + 1 > (height - 1)) || board[playerCords[0], (playerCords[1] + 1), playerCords[2]].symbol == "#")
                {
                    
                }
                else if (board[playerCords[0], (playerCords[1] + 1), playerCords[2]].stairs)
                {
                    playerCords[0]++;
                    placePlayer(playerCords[0]);
                    Console.Clear();
                    showBoard(playerCords[0]);
                }
                else
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    Tile player = new Tile("@", ConsoleColor.Green, ConsoleColor.Black, false, true);
                    board[playerCords[0], (playerCords[1] + 1), playerCords[2]] = player;
                    board[playerCords[0], (playerCords[1]), playerCords[2]] = floor;
                    Console.SetCursorPosition(playerCords[2], (playerCords[1]));
                    board[playerCords[0], (playerCords[1]), playerCords[2]].DrawTile();
                    Console.SetCursorPosition(playerCords[2], (playerCords[1] + 1));
                    board[playerCords[0], (playerCords[1]+1), playerCords[2]].DrawTile();
                    playerCords[1]++;
                }
            }

            while(Console.ReadKey(true).Key == ConsoleKey.A)
            {
                if((playerCords[2] - 1) < 0 || board[playerCords[0], playerCords[1], (playerCords[2]-1)].symbol == "#")
                {
                    
                }
                else if (board[playerCords[0], (playerCords[1]), playerCords[2]-1].stairs)
                {
                    playerCords[0]++;
                    placePlayer(playerCords[0]);
                    Console.Clear();
                    showBoard(playerCords[0]);
                }
                else
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    Tile player = new Tile("@", ConsoleColor.Green, ConsoleColor.Black, false, true);
                    board[playerCords[0], (playerCords[1]), playerCords[2] - 1] = player;
                    board[playerCords[0], (playerCords[1]), playerCords[2]] = floor;
                    Console.SetCursorPosition((playerCords[2] - 1), playerCords[1]);
                    board[playerCords[0], (playerCords[1]), playerCords[2]-1].DrawTile();
                    Console.SetCursorPosition(playerCords[2], (playerCords[1]));
                    board[playerCords[0], (playerCords[1]), playerCords[2]].DrawTile();
                    playerCords[2]--;
                }
            }

            while(Console.ReadKey(true).Key == ConsoleKey.D)
            {
                if ((playerCords[2] + 1) < 0 || board[playerCords[0], playerCords[1], (playerCords[2] + 1)].symbol == "#")
                {
                   
                }
                else if (board[playerCords[0], (playerCords[1]), playerCords[2] + 1].stairs)
                {
                    playerCords[0]++;
                    placePlayer(playerCords[0]);
                    Console.Clear();
                    showBoard(playerCords[0]);
                    
                }
                else
                {
                    Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    Tile player = new Tile("@", ConsoleColor.Green, ConsoleColor.Black, false, true);
                    board[playerCords[0], (playerCords[1]), playerCords[2] + 1] = player;
                    board[playerCords[0], (playerCords[1]), playerCords[2]] = floor;
                    Console.SetCursorPosition((playerCords[2]), playerCords[1]);
                    board[playerCords[0], (playerCords[1]), playerCords[2]].DrawTile();
                    Console.SetCursorPosition(playerCords[2] + 1, (playerCords[1]));
                    board[playerCords[0], (playerCords[1]), playerCords[2] + 1].DrawTile();
                    playerCords[2]++;
                }
            }

            return board;
        }
        //((playerCords[2]-1)<0||
          //             (playerCords[2]+1)>width)




        
    }
}
