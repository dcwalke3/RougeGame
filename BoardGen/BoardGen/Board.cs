using BoardGen;
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
        public Tile[,] board;
        public int height;
        public int width;
        public int NumofRooms;
        public int[,] midpointCoords;
        public int[] playerCords;
        public int floorNum;


        public Board()
        {

            NumofRooms = 10;
            floorNum = 10;
            midpointCoords = new int[NumofRooms, 2];
            height = 50;
            width = 100;
            playerCords = new int[2];
            Console.Clear();
            Tile[,] board = makeBoard(height, width);
            CreateRooms();
            MakeCorridors();
            placeStairs();


        }

        // As name implies makes board.
        public Tile[,] makeBoard(int height, int width)
        {
            Tile[,] gameboard = new Tile[height, width];

            for (int i = 0; i < (height); i++)
            {
                for (int j = 0; j < (width); j++)
                {
                    Tile empty = new Tile("#", ConsoleColor.Black, ConsoleColor.White);
                    gameboard[i, j] = empty;

                    board = gameboard;
                }
            }



            return board;
        }

        // Creates the cords and height and width and then calls
        // makeRoom multiple times.
        public Tile[,] CreateRooms()
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
                midpointCoords[i, 0] = midpointx;
                midpointCoords[i, 1] = midpointy;

                MakeRoom(hcord, wcord, hrand, wrand);

            }

            return board;
        }

        // Creates 1 room off given coords and a height and width.
        public Tile[,] MakeRoom(int coord1, int coord2, int bheight, int bwidth)
        {

            for (int i = 0; i < bheight; i++)
            {
                for (int j = 0; j < bwidth; j++)
                {

                    Tile roomfloor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                    board[coord1 + i, coord2 + j] = roomfloor;
                }
            }
            return board;
        }

        // Compares two sets of cords to create a corridor.
        public Tile[,] MakeCorridors()
        {


            for (int i = 0; i < (NumofRooms - 1); i++)
            {
                int hcord1 = midpointCoords[i, 0];
                int wcord1 = midpointCoords[i, 1];
                int hcord2 = midpointCoords[(i + 1), 0];
                int wcord2 = midpointCoords[(i + 1), 1];
                if (hcord1 > hcord2)
                {
                    if (wcord1 > wcord2)
                    {
                        while (wcord1 > wcord2)
                        {

                            Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                            board[hcord1, wcord1] = floor;
                            wcord1--;

                        }

                        while (hcord1 > hcord2)
                        {
                            Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                            board[hcord1, wcord1] = floor;
                            hcord1--;
                        }
                    }
                    else if (wcord1 <= wcord2)
                    {
                        while (wcord1 <= wcord2)
                        {
                            Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                            board[hcord2, wcord2] = floor;
                            wcord2--;
                        }
                        while (hcord1 > hcord2)
                        {
                            Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                            board[hcord1, wcord1] = floor;
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
                            board[hcord1, wcord1] = floor;
                            hcord1++;
                        }
                        while (wcord1 > wcord2)
                        {
                            Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black); ;
                            board[hcord1, wcord2 + 1] = floor;
                            wcord2++;
                        }
                    }
                    else if (wcord1 <= wcord2)
                    {
                        while (hcord2 > hcord1)
                        {
                            Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                            board[hcord1, wcord1] = floor;
                            hcord1++;
                        }
                        while (wcord2 >= wcord1)
                        {
                            Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                            board[hcord1, wcord1] = floor;
                            wcord1++;
                        }
                    }
                }
            }

            return board;
        }

        // placeStairs should be called last so a corridor does not go overtop of it.
        public Tile[,] placeStairs()
        {

            int randommidpoint = Random.randInt(0, (NumofRooms));
            int randomx = midpointCoords[randommidpoint, 0];
            int randomy = midpointCoords[randommidpoint, 1];
            int randommidpoint2 = Random.randInt(0, (NumofRooms));
            int randomx2 = midpointCoords[randommidpoint2, 0];
            int randomy2 = midpointCoords[randommidpoint2, 1];




            Tile stairs = new Tile("v", ConsoleColor.Red, ConsoleColor.Yellow,
            true);
            board[randomx, randomy] = stairs;





            return board;
        }

        public Tile[,] placeActor(IActor a = null)
        {

            int randomx = Random.randInt(0, height - 1);
            int randomy = Random.randInt(0, width - 1);
            while (board[randomx, randomy].symbol != ".")
            {
                randomx = Random.randInt(0, height - 1);
                randomy = Random.randInt(0, width - 1);
            }
            Tile actor = new Tile(a.symbol, a.foreColor, a.backColor, false);

            board[randomx, randomy] = actor;
            if (a.symbol == "@")
            {

                playerCords[0] = randomx;
                playerCords[1] = randomy;
            }
            a.row = randomx;
            a.col = randomy;
            return board;
        }

        // Shows board.
        public void showBoard()
        {

            for (int i = 0; i < (height - 1); i++)
            {
                for (int j = 0; j < (width - 1); j++)
                {
                    Console.SetCursorPosition(j, i);
                    board[i, j].DrawTile();
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




        public void CheckPlayerPos(IActor a)
        {

            for (int i = 0; i < (height); i++)
            {
                for (int j = 0; j < (width); j++)
                {
                    if (board[i, j].symbol == "@")
                    {
                        playerCords[0] = i;
                        playerCords[1] = j;
                        a.row = i;
                        a.col = j;
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

        

        public void UpdateStatus(Player p)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(108, 10);
            Console.Write("Status");
            Console.SetCursorPosition(105, 15);
            Console.Write("Name: " + p.name);
            Console.SetCursorPosition(105, 18);
            Console.Write("Level: " + p.level);
            Console.SetCursorPosition(105, 21);
            Console.Write($"HP: {p.health}/{p.MaxHealth}");
            Console.SetCursorPosition(105, 24);
            Console.Write("ATK: " + p.ATK + "  DEF: " + p.DEF);
            Console.SetCursorPosition(100, 27);
            Console.Write($"XP Needed to level up: {p.XPNeeded}");
        }

        




        




    }
}
    

