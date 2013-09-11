using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KataTennis.Events;
using KataTennis.States;

namespace KataTennis.Services
{
	public class ServiceMatch
	{
		public Match Replay(List<IMatchEvent> events)
		{
			return events.Aggregate(Match.Empty(), Apply);
		}

		public Match Apply(Match match,IMatchEvent matchEvent)
		{
			Match res = null;

			if (matchEvent is MatchStarted)
			{
				if (match.GameStarted)
					throw new Exception("The game has already begun");

				var matchStarted = matchEvent as MatchStarted;

				res = match.StartMatch(matchStarted.Player1,matchStarted.Player2);
			}
			else if (matchEvent is MatchPoint)
			{
				if (!match.GameStarted)
					throw new Exception("The game has not started");

				if (match.GameEnded)
					throw new Exception("The game has ended");

				var matchPoint = matchEvent as MatchPoint;

				if (!match.Players.Any(i => i == matchPoint.Player))
					throw new Exception(string.Format("{0} is not in the match", matchPoint.Player));

				res = match.PlayPoint(matchPoint.Player);
			}
			else
				throw new Exception("Invalid event");

			return res;
		}
	}
}
