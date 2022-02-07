using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace assignment2_w2022_n01519118.Controllers
{//Note: I tried renaming controller to ChallengeControllerJ1 but encountered problems debugging. After that discovered it must teminate with "Controller"
    ///<summary>
    /// 2009 Challenge J1 - Description:
    /// The International Standard Book Number (ISBN) is a 13-digit code for identifying books. These
    /// numbers have a special property for detecting whether the number was written correctly.
    /// The 1-3-sum of a 13-digit number is calculated by multiplying the digits alternately by 1’s and 3’s
    /// (see example) and then adding the results.For example, to compute the 1-3-sum of the number
    /// 9780921418948 we add
    /// 9 ∗ 1 + 7 ∗ 3 + 8 ∗ 1 + 0 ∗ 3 + 9 ∗ 1 + 2 ∗ 3 + 1 ∗ 1 + 4 ∗ 3 + 1 ∗ 1 + 8 ∗ 3 + 9 ∗ 1 + 4 ∗ 3 + 8 ∗ 1
    /// to get 120.
    /// 
    /// Write a program to compute the 1-3-sum of a 13-digit number. To reduce the amount of typing,
    /// you may assume that the first ten digits will always be 9780921418, like the example above.Your
    /// program should input the last three digits and then print its 1-3-sum.Use a format similar to the
    /// samples below.
    /// </summary>
    public class ChallengeController : ApiController
    {
        /// <summary>
        /// This function receives a numericString and return its 1-3-sum. It is going to be used in j1 challenge
        /// </summary>
        /// <param name="isbnString"></param>
        /// <returns>the total sum of each charactere in isbnString alternatively multiplied by 1 and 3 </returns>
        /// Proposal 1)We could upgrade this function to receive two integers that should be
        /// use to multiply each number received in 
        ///          isbnString. This would be useful to transform this controller from a ISBN check sum validator into a more flexible 
        /// one
        public int Sum1_3(string isbnString)
        {
            //string  isbNumber= isbn.ToString();
            int _totalSum1_3 = 0; // used to store the total sum
            for (int i = 0; i < isbnString.Length; i++)
            {
                if (i % 2 == 0) //if i is even add isbn to total
                {
                    _totalSum1_3 += (int)Char.GetNumericValue(isbnString[i]); //transform char into number, then transform it into Integer
                }
                else //if i is odd then multiply by 3 before adding to total
                {
                    _totalSum1_3 += (int)(3 * Char.GetNumericValue(isbnString[i])); // same case transformation case
                }
            }
            return _totalSum1_3;
        }
        /// <summary>
        /// For the Assignment:
        /// I am going to create to API Get request that receives 3 parameteres which will be the ending 
        /// numbers of ISBN 9780921418. Then, the API will return the 1-3 sum.
        /// Route:"api/ChallengeController/j1/{a}/{b}/{c}"
        /// 
        /// Example 1:
        ///     Input: GET "api/ChallengeController/j1/9/4/8"
        ///     Output: The 1-3-sum is 120.
        ///     
        /// Example 2:
        ///     Input: GET "api/ChallengeController/j1/0/5/2"
        ///     Output: The 1-3-sum is 108.
        ///     
        /// Proposal 1) Together wih altering the function, we could receive through API the number which 
        ///             is going do be validated + the type of sum for exemple 1-3 or 1-9.
        ///             route: GET api/ChallengeController/j1/{numberToCheck}/{firstMultiplier}/{secondMultiplier}
        ///             api/ChallengeController/j1/9780921418948/1/3
        ///             This would also remove the first 10 hardcoded digits

        /// </summary>
        /// 
        [HttpGet]
        [Route("api/ChallengeController/j1/{a}/{b}/{c}")]
        public string Get(int a, int b, int c)
        {
            //stores the hardcoded first 10 ISBN digits
            long firstIsbnNumber = 9780921418;
            //concatenate the input with the hardcoded first 10 digits
            string isbnAsNumber = firstIsbnNumber + a.ToString() + b.ToString() + c.ToString(); //I transformed isbn into a string to access its values based on index
            return "The 1-3-sum is " + Sum1_3(isbnAsNumber)+".";
        }

        

    }
}
