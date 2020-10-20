using BrandonPlayerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BoardGen
{
    class Program
    {
        static void Main(string[] args)
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
            map.placeActor(p.CurrentDungeonLevel-1, p);
            Console.Clear();
            Console.SetWindowSize(125,65);
            Console.SetBufferSize(125, 65);
            
            
            Console.CursorVisible = false;
            
            map.showBoard(0);

            
            
            p.FillStatusScreens();
            p.PrintStatusScreen();
            Monster m = new Monster(map,p);
            map.placeActor(0, m);
           
            
            while (true)
            {
                map.CheckPlayerPos();
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0,63);
                p.Move(map,p);

                

                if (p.Health <= 0) {
                    break;
                }
            }
            Console.ReadKey();
            
        }


    }
}
