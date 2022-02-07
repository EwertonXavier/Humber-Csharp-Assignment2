using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace assignment2_w2022_n01519118.Controllers
{

    public class J2ChallengeController : ApiController
    {
        /// <summary>
        /// <problemdescription>
        ///     2014 - J2
        ///     A vote is held after singer A and singer B compete in the final round of a singing competition.
        ///     Your job is to count the votes and determine the outcome.
        ///     Input Specification
        ///     The input will be two lines.The first line will contain V (1 ≤ V ≤ 15), the total number of
        ///     votes.The second line of input will be a sequence of V characters, each of which will be A or B,
        ///     representing the votes for a particular singer.
        ///     Output Specification
        ///     The output will be one of three possibilities:
        ///     • A, if there are more A votes than B votes;
        ///     • B, if there are more B votes than A votes;
        ///     • Tie, if there are an equal number of A votes and B votes
        ///</problemdescription>
        ///<assignment>
        /// For the Assignment:
        ///      I will create an API which receives a string representing votes, and returns the total number of votes
        ///     for each participant as well as which one is the winner.
        /// 
        ///     Route:GET api/J2ChallengeController/{ votes}
        ///     Example:
        ///         Input: GET api/J2ChallengeController/AAAABB
        ///         Return: "4 Votes for A
        ///                 2 Votes for B
        ///                 A is the winner"
        ///     Upgrade Made: This API works for any quantity of participants or votes!
        ///       </assignment>
        /// </summary>
        /// 

        [HttpGet]
        [Route("api/J2ChallengeController/{votes}")]
        public string CountVotes(string votes)
        {
            List<char> winners = new List<char>(); //holds the list of winners
            int maxVote = 0; //highest number of votes
            Dictionary<char, int> voteParticipant = new Dictionary<char, int>(); //Creates a Dictionary which I will use to store who
                                                                                 //is the participant (key) and the numbers of votes(value) 
                         
            //This block counts the total number of votes for each participant
            foreach (char vote in votes)
            {
                if (voteParticipant.ContainsKey(vote))
                {
                    voteParticipant[vote] += 1; // add votes
                }
                else
                {
                    voteParticipant.Add(vote, 1); //add participant with a vote
                }
            }

            //this block determines who is the most voted participant and store this value in winners
            foreach (char participant in voteParticipant.Keys)
            {
                if (voteParticipant[participant] > maxVote)
                {
                    maxVote = voteParticipant[participant];
                    winners.Clear(); //clears the array when a greater number of votes is found.
                    winners.Add(participant);
                }
                else if (voteParticipant[participant] == maxVote)
                {
                    winners.Add(participant);
                }
            }
            

            string message = ""; //Variable to store return message
            //block to generate the message informing each participant's quantity of votes and result message
            foreach (char participant in voteParticipant.Keys)
            {
                message += "| "+voteParticipant[participant] + " Votes for " + participant + "\n";
            }
            //based on winners content this step writes in message the winner or if it was a tie
            if(winners.Count == 1)
            {
                message += ". "+winners.First() + " is the winner";
            }
            else
            {
                message += ". It is a Tie.";
            }


            return message;

        }



    }
}
