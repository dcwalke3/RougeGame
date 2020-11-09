using BrandonPlayerGen;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BoardGen
{
    
    public class Program

    {
       
        public static void Main(string[] args)
        {

            Board map = new Board();
            Player p;
            
            while (true)
            {
                
                p = new Player(1, map);
                
                Console.WriteLine(p);
                Console.Write("\nWould you like a new character Y/n: ");
                if(Console.ReadKey().Key == ConsoleKey.Y)
                {
                    break;
                }
                Console.Clear();
            }
            map.placeActor(p);
            map.CheckPlayerPos(p);
            Console.Clear();
            Console.SetWindowSize(125,65);
            Console.SetBufferSize(125, 65);
            
            
            Console.CursorVisible = false;
            
            map.showBoard();

            
            
            p.FillStatusScreens();
            p.PrintStatusScreen();
            int monsterCount = 10;
            List<Monster> monsters = new List<Monster>();
            for (int i = 0; i < monsterCount; i++)
            {
                Monster m = new Monster(p);
               
                map.placeActor(m);
                monsters.Add(m);
            }

            map.showBoard();
            

            while (true)
            {
                map.CheckPlayerPos(p);
                
                Console.SetCursorPosition(0,63);
                p.Move(map,p);

                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].health <= 0)
                    {
                        monsters[i].Death(map);
                        Tile floor = new Tile(".", ConsoleColor.White, ConsoleColor.Black);
                        IActor monster = monsters[i];
                        map.board[monster.row, monster.col] = floor;
                        monsters.RemoveAt(i);
                        Monster m = new Monster(p);
                        monsters.Add(m);
                        map.placeActor(m);
                        map.showBoard();

                    }
                    else
                    {

                        monsters[i].Move(map, monsters[i]);
                    }
                }



                if (p.Health <= 0) {
                    break;
                }
            }
            Console.ReadKey();
            
        }

        
    }


}
