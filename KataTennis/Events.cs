using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataTennis.Events
{
	public interface IMatchEvent { };

	public class MatchStarted : IMatchEvent
	{
		public string Player1 { get;private set; }
		public string Player2 { get; private set; }

		public MatchStarted(string player1, string player2)
		{
			this.Player1 = player1;
			this.Player2 = player2;
		}
	}

	public class MatchPoint : IMatchEvent
	{
		public string Player { get; private set; }

		public MatchPoint(string player)
		{
			this.Player = player;
		}
	}
}
