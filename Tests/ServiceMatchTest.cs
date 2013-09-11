using KataTennis.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using KataTennis.Events;
using System.Collections.Generic;
using KataTennis.States;

namespace Tests
{
    
	[TestClass()]
	public class ServiceMatchTest
	{
		private TestContext testContextInstance;

		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Attributs de tests supplémentaires
		// 
		//Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
		//
		//Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		[TestMethod()]
		public void TestMatchStart()
		{
			ServiceMatch target = new ServiceMatch();
			List<IMatchEvent> events = new List<IMatchEvent>() 
			{ 
				new MatchStarted("p1","p2"),
			};

			Match actual = target.Replay(events);
		}

		[TestMethod()]
		public void TestPoint()
		{
			ServiceMatch target = new ServiceMatch();
			List<IMatchEvent> events = new List<IMatchEvent>() 
			{ 
				new MatchStarted("p1","p2"),
 				new MatchPoint("p1"),
			};

			Match actual = target.Replay(events);
			Assert.AreEqual(actual.Score[0], 1);
			Assert.AreEqual(actual.Score[1], 0);
		}

		[TestMethod()]
		public void TestDeuce()
		{
			ServiceMatch target = new ServiceMatch();
			List<IMatchEvent> events = new List<IMatchEvent>() 
			{ 
				new MatchStarted("p1","p2"),
 				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2")
			};

			Match actual = target.Replay(events);
			Assert.AreEqual(actual.Score[0], 3);
			Assert.AreEqual(actual.Score[1], 3);
			Assert.AreEqual(actual.Deuce, true);
		}

		[TestMethod()]
		public void TestAdvantage()
		{
			ServiceMatch target = new ServiceMatch();
			List<IMatchEvent> events = new List<IMatchEvent>() 
			{ 
				new MatchStarted("p1","p2"),
 				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1")
			};

			Match actual = target.Replay(events);
			Assert.AreEqual(actual.Score[0], 3);
			Assert.AreEqual(actual.Score[1], 3);
			Assert.AreEqual(actual.Deuce, false);
			Assert.AreEqual(actual.AdvantagePlayer, "p1");
		}

		[TestMethod()]
		public void TestDeuceAfterAdvantage()
		{
			ServiceMatch target = new ServiceMatch();
			List<IMatchEvent> events = new List<IMatchEvent>() 
			{ 
				new MatchStarted("p1","p2"),
 				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2")
			};

			Match actual = target.Replay(events);
			Assert.AreEqual(actual.Score[0], 3);
			Assert.AreEqual(actual.Score[1], 3);
			Assert.AreEqual(actual.Deuce, true);
			Assert.AreEqual(actual.AdvantagePlayer, string.Empty);
		}

		[TestMethod()]
		public void TestDeuceAfterAdvantageBis()
		{
			ServiceMatch target = new ServiceMatch();
			List<IMatchEvent> events = new List<IMatchEvent>() 
			{ 
				new MatchStarted("p1","p2"),
 				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2")
			};

			Match actual = target.Replay(events);
			Assert.AreEqual(actual.Score[0], 3);
			Assert.AreEqual(actual.Score[1], 3);
			Assert.AreEqual(actual.Deuce, true);
			Assert.AreEqual(actual.AdvantagePlayer, string.Empty);
		}

		[TestMethod()]
		public void TestGameAfterAdvantage()
		{
			ServiceMatch target = new ServiceMatch();
			List<IMatchEvent> events = new List<IMatchEvent>() 
			{ 
				new MatchStarted("p1","p2"),
 				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p1")
			};

			Match actual = target.Replay(events);
			Assert.AreEqual(actual.Score[0], 0);
			Assert.AreEqual(actual.Score[1], 0);
			Assert.AreEqual(actual.Score[2], 1);
			Assert.AreEqual(actual.Score[3], 0);
		}

		[TestMethod()]
		public void TestGameAfterOneDeuceAdvantage()
		{
			ServiceMatch target = new ServiceMatch();
			List<IMatchEvent> events = new List<IMatchEvent>() 
			{ 
				new MatchStarted("p1","p2"),
 				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p2"),
				new MatchPoint("p1"),
				new MatchPoint("p1")
			};

			Match actual = target.Replay(events);
			Assert.AreEqual(actual.Score[0], 0);
			Assert.AreEqual(actual.Score[1], 0);
			Assert.AreEqual(actual.Score[2], 1);
			Assert.AreEqual(actual.Score[3], 0);
		}

		[TestMethod()]
		public void TestGame()
		{
			ServiceMatch target = new ServiceMatch();
			List<IMatchEvent> events = new List<IMatchEvent>() 
			{ 
				new MatchStarted("p1","p2"),
 				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1")
			};

			Match actual = target.Replay(events);
			Assert.AreEqual(actual.Score[0], 0);
			Assert.AreEqual(actual.Score[1], 0);
			Assert.AreEqual(actual.Score[2], 1);
			Assert.AreEqual(actual.Score[3], 0);
		}

		[TestMethod()]
		public void TestSet()
		{
			ServiceMatch target = new ServiceMatch();
			List<IMatchEvent> events = new List<IMatchEvent>() 
			{ 
				new MatchStarted("p1","p2"),
 				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),

				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),

				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),

				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),

				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),

				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1"),
				new MatchPoint("p1")
			};

			Match actual = target.Replay(events);
			Assert.AreEqual(actual.Score[0], 0);
			Assert.AreEqual(actual.Score[1], 0);
			Assert.AreEqual(actual.Score[2], 0);
			Assert.AreEqual(actual.Score[3], 0);
			Assert.AreEqual(actual.Score[4], 1);
			Assert.AreEqual(actual.Score[5], 0);
		}
	}
}
