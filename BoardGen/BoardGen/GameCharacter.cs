using BrandonPlayerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGen
{
	public abstract class GameCharacter
	{
		public string _ID { get; } = Guid.NewGuid().ToString("N");
		public int health { get; set; }
		public int attack { get; set; }
		public int defense { get; set; }
		public string name { get; set; }
		public int level { get; protected set; }

		public override string ToString()
		{
			string returnString = "";
			returnString += "Name: " + name + Environment.NewLine;
			returnString += "Health: " + health + Environment.NewLine;
			returnString += "Level: " + level + Environment.NewLine;

			return returnString;
		}

	}


	public interface IActor
	{
		int row { get; set; }
		int col { get; set; }
		string symbol { get; }
		ConsoleColor foreColor { get; }
		ConsoleColor backColor { get; }

		void Move(Board b, Player p);
		void Interact(Board b, IActor a);
		void Death(Board b);
	}

}
