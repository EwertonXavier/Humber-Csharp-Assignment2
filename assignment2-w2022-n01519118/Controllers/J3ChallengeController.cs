using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace assignment2_w2022_n01519118.Controllers
{
    /// <summary>
    /// 2014
    /// Problem J3: Double Dice
    /// Antonia and David are playing a game.
    /// Each player starts with 100 points.
    /// The game uses standard six-sided dice and is played in rounds.During one round, each player
    /// rolls one die.The player with the lower roll loses the number of points shown on the higher die.If
    /// both players roll the same number, no points are lost by either player.
    /// Write a program to determine the final scores.
    /// </summary>
    /// Upgrade Proposal 1) Use a global dictionary players instead of passing one for each function which needs it.
    public class J3ChallengeController : ApiController
    {
        //global random number generator
        Random GLOBALRANDOM = new Random(); //tried to create as global function but each call is like a
       

        /// <summary>
        /// This function is going to be called each round for each player to roll a dice.
        /// </summary>
        /// <returns>random number between 1 and 6</returns>
        public int ThrowDice()
        {
            //Random random = new Random(); // this was generating duplicate numbers due to random seed being the same.
            return GLOBALRANDOM.Next(1,6);
        }
        /// <summary>
        /// This function is going to be called each round to check who is going to loose points.
        /// <input>Array dices representing the result of each player</input>
        /// <param name="dices"></param>
        /// <returns>array the same size as dices.length with quantity of lost points for each player</returns>
        /// </summary>
        public int[] CheckGameRules(List<int> dices)
        {
            int[] resultArray = new int[dices.Count];
            
            
            for (int i = 0; i < dices.Count; i++) //for each dice
            {
                if (dices.ElementAt(i) < dices.Max()) //check if the value is less than the greater dice number (dices.Max())
                {
                    resultArray.SetValue(dices.Max(),i);// if so put dices.Max() in the result array
                }
                else
                {
                    resultArray.SetValue(0, i); // stores 0 because the winner of this round doesn`t loose points
                }
                
            }

            return (resultArray);
        }
        /// <summary>
        /// This functions will subtract players` point each round
        /// </summary>
        /// <param name="totalPoint"></param>
        /// <param name="lostPoints"></param>
        /// <returns>Remaining player points</returns>
        public int[] SubtractPoint(int[] totalPoint, int[] lostPoints)
        {
            //int[] newTotalPoints = totalPoint;
            for (int i = 0; i < totalPoint.Length; i++)
            {
                totalPoint[i] = totalPoint[i] - lostPoints[i];
            }

            return totalPoint;
        }
        /// <summary>
        /// This functions will make the setup creating a dictionary which will be
        /// used to store player number and points regardless the number of players
        /// </summary>
        /// <param name="numPlayers"></param>
        /// <param name="points"></param>
        /// <returns>Dictionary containing all players and their points</returns>
        public Dictionary<string, int> GameSetup(int numPlayers, int points)
        {
            Dictionary<string, int> playersPoint = new Dictionary<string, int>(); // 
            for (int i = 1; i <= numPlayers; i++)
            {
                playersPoint.Add("Player" + i, points);
            }
            return playersPoint;
        }
        /// <summary>
        /// This functions Calls other functions and makes a game round.
        /// Each round is composed of:
        ///     Players roll dices
        ///     Compare dice results
        ///     Subtract points
        ///     generate log message
        /// </summary>
        /// <param name="newPlayersPoints"></param>
        /// <param name="roundLog"></param>
        /// <returns name="newPlayersPoints">updated players dictionary with current points</returns>
        /// <returns name="roundLog">string with info about what happened this round</returns>
        public void Round(Dictionary<string, int> players, int roundNumber, out Dictionary<string, int> newPlayersPoints, out string roundLog)
        {
            string message = "";
            List<int> rolledDice = new List<int>();
            for (int i = 0; i < players.Count; i++)
            {
                rolledDice.Add(ThrowDice()); //PLayers roll dices
            }
            int[] pointsLost = CheckGameRules(rolledDice); //compare dice results
            int[] newPoints = SubtractPoint(players.Values.ToArray<int>(), pointsLost);//subtract points
            for (int i = 0; i < players.Count; i++) //log message
            {
                players[players.ElementAt(i).Key] = newPoints[i];
                message += "("+players.ElementAt(i).Key + " " + rolledDice.ElementAt(i)+") ";
            }
            newPlayersPoints = players;
            roundLog = "Round" + roundNumber + ": " + message+" .";

        }

        public string CurrentPointsMessage(Dictionary<string, int> playersPoints)
        {
            string message = "";
            foreach(string player in playersPoints.Keys)
            {
                message += player + " " + playersPoints[player]+ "|";
            }

            return message;
        }

        /// <summary>
        /// <assignment>
        ///     I will create an api which will receive the number of players, starting points and number of rounds. 
        ///     After that, following the game instructions, I will present the result of each round and final health
        /// 
        /// Route: GET api/J3ChallengeController/{numPlayers}/{points}/{round}
        /// Example:
        ///     Input: GET api/J3ChallengeController/2/1/2
        ///     Return: "Round 1: | Player1Dice 1: {p1Dice} | Player2Dice: {p2Dice}
        ///              Round 2: | Player1Dice 1: {p1Dice} | Player2Dice: {p2Dice}
        ///              Final Points Player 1:{p1Points} | Player 2:{p2Points}"
        ///     Note: The values p1Dice, p2Dice are going to be randomly assigned,
        ///           and p2Points and p2Points depends on p1Dice and p2Dice.
        /// </assignment>
        /// </summary>
        /// <param name="numPlayers"></param>
        /// <param name="health"></param>
        /// <param name="round"></param>
        /// <returns></returns>
       

        [HttpGet]
        [Route("api/J3ChallengeController/{numPlayers}/{points}/{round}")]
        public string Get(int numPlayers, int points, int round)
        {
            string finalMessage = ""; //will contain the result of all rounds + ramaining points
            string roundLog = ""; //will contain the result of each round
            Dictionary<string, int> playersPoints = GameSetup(numPlayers, points);//create a dictionary which will hold player (key) points(value)
            
            for(int i = 0; i < round; i++)
            {
                Round(playersPoints, (i+1), out playersPoints, out roundLog); //runs a game round -> updated players points on return
                finalMessage += roundLog+"///////";
            }
            finalMessage += "Total Points: " + CurrentPointsMessage(playersPoints);


            return finalMessage;
        }

    }
}
