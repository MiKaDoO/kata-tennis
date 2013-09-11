using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KataTennis.States
{
	public class Match
	{
		public string[] Players { get; private set; }
		public int[] Score { get; private set; }

		public bool GameStarted { get; private set; }
		public bool GameEnded { get; private set; }
		public bool Deuce { get; private set; }

		public string AdvantagePlayer { get; private set; }

		private Match(string player1, string player2, int[] score, bool gameStarted, string advantagePlayer, bool deuce, bool gameEnded)
		{
			Score = score;
			Players = new[] { player1, player2 };

			GameStarted = gameStarted;
			GameEnded = gameEnded;
			Deuce = deuce;

			AdvantagePlayer = advantagePlayer;
		}

		public Match StartMatch(string player1, string player2)
		{
			GameStarted = true;
			GameEnded = false;
			Deuce = false;

			AdvantagePlayer = string.Empty;

			Players = new[] { player1, player2 };
			Score = new[] { 0, 0, 0, 0, 0, 0 };

			return this;
		}

		public Match PlayPoint(string player)
		{
			var indexWinner = Array.IndexOf(Players, player);
			var indexLooser = (indexWinner + 1) % 2;

			SetPoint(indexWinner);

			if (Deuce)
				SetAdvantage(indexWinner);

			if (PlayerHasAdvantage(indexLooser) || (EachPlayersHaveThreePoints() && !PlayerHasAdvantage(indexWinner)))
				SetDeuce(indexWinner);

			if (PlayerHasWonFourPoints(indexWinner))
			{
				SetGame(indexWinner);

				if (PlayerHasWonSixGames(indexWinner))
				{
					SetSet(indexWinner);

					if (PlayerHasWonThreeSets(indexWinner))
						EndGame();
				}
			}

			return this;
		}

		private bool PlayerHasAdvantage(int indexPlayer)
		{
			return AdvantagePlayer == Players[indexPlayer];
		}

		private bool EachPlayersHaveThreePoints()
		{
			return Score[0] == Score[1] && Score[1] == 3;
		}

		private bool PlayerHasWonFourPoints(int indexPlayer)
		{
			return Score[indexPlayer] > 3;
		}

		private bool PlayerHasWonSixGames(int indexPlayer)
		{
			return Score[indexPlayer + 2] > 5;
		}

		private bool PlayerHasWonThreeSets(int indexPlayer)
		{
			return Score[indexPlayer + 4] == 3;
		}

		private void SetSet(int indexPlayer)
		{
			Score[2] = 0;
			Score[3] = 0;

			Score[indexPlayer + 4] += 1;
		}

		private void SetGame(int indexPlayer)
		{
			Score[0] = 0;
			Score[1] = 0;
			
			Deuce = false;

			AdvantagePlayer = string.Empty;

			Score[indexPlayer + 2] += 1;
		}

		private void SetDeuce(int indexPlayer)
		{
			Score[indexPlayer] = 3;

			Deuce = true;

			AdvantagePlayer = string.Empty;
		}

		private void SetAdvantage(int indexPlayer)
		{
			Score[indexPlayer] = 3;

			Deuce = false;
			AdvantagePlayer = Players[indexPlayer];
		}

		private void SetPoint(int indexPlayer)
		{
			Score[indexPlayer] += 1;
		}

		private void EndGame()
		{
			GameEnded = true;
		}

		public static Match Empty()
		{
			return new Match(null, null, null, false, string.Empty, false, false);
		}
	}
}
