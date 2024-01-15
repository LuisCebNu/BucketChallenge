using Microsoft.AspNetCore.Server.IIS;
using System.Numerics;
using System.Runtime.InteropServices.JavaScript;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace BucketChallenge
{   
    
    public class Solver
    {
        static List<string> steps = new List<string>(); //Here is where the steps are safe.
        static bool PourFrom; //This is used to check what is the current bucket that we are transferring too.

        // Works as the main function of the solver class.
        // Creates the buckets and send them to correct methods.
        public static void BucketConstructor(uint bucketX, uint bucketY, uint amount)
        {
            Bucket BucketX = new Bucket(bucketX);
            Bucket BucketY = new Bucket(bucketY);
            
            whichBucket(BucketX.BucketLimit, BucketY.BucketLimit, amount);
            Solution(BucketX, BucketY, amount);
        }
        // Checks the value of formula to see if we should continue the process. 
        // This was solve by using the Bezout's algorithm. In the context of the challenge
        // If the GCD of the two buckets is divisible by the target amount, then is possible
        // to measeure the target using given buckets.
        static bool CanMeaseure(uint bucketX, uint bucketY, uint amount)
        {
            if (bucketX == 0 || bucketY == 0 || amount == 0)
                return false;
            else 
                return (amount % BigInteger.GreatestCommonDivisor(bucketX, bucketY) == 0);
        }

        //Checks if the numbers have a solution and which bucket is the best
        // to solved the problem with the less amount of iterations.
        // It also checks for infinite loops that can happened if the target is bigger than
        // both of the buckets.
        static void Solution (Bucket bucketX, Bucket bucketY, uint amount)
        {
            if (CanMeaseure(bucketX.BucketLimit,bucketY.BucketLimit,amount) && 
                (amount < bucketX.BucketLimit || amount < bucketY.BucketLimit))
            {
                if (PourFrom)
                {
                    steps.Add($"Bucket X is Fill : {bucketX.BucketLimit}");
                    iteration(bucketX, bucketY, amount);
                } else
                {
                    steps.Add($"Bucket Y is Fill : {bucketY.BucketLimit}");
                    iteration(bucketY, bucketX, amount);

                }
            } else if (bucketX.BucketLimit == 0 || bucketY.BucketLimit == 0 || amount == 0){
                steps.Add("Invalid input. The values must be integers greater or equal to 1.");
            }
            else
            {
                steps.Add("No solution");

            }
        }
        //By checking the distance that each of the two buckets need to cover to get
        // to the target, we can make sure to choose the correct bucket to transfer to.
        static void whichBucket(uint BucketX, uint BucketY, uint target)
        {
            int limitX = ((int)target - (int)BucketX) > 0 ? ((int)target - (int)BucketX) : - ((int)target - (int)BucketX);
            int limitY = ((int)target - (int)BucketY) > 0 ? ((int)target - (int)BucketY) : - ((int)target - (int)BucketY);
            bool pourFrom = limitY > limitX;

            PourFrom = pourFrom;
        }
        
        //The loop.
        static void iteration(Bucket bucketX, Bucket bucketY, uint target)
        {   int iterations = 0;
            bucketX.Fill();
            // All PourFrom decisions are here to help user know the correct name
            // Of the bucket that is getting fill up.
            while (bucketX.Content != target && bucketY.Content != target)
            {
                //pour from bucketX to bucketY
                bucketY.Transfer(bucketX);
                if (PourFrom)
                {
                    steps.Add($"Bucket X transfer to Bucket Y: X: {bucketX.Content} Y: {bucketY.Content}");
                } else
                {
                    steps.Add($"Bucket Y transfer to Bucket X: X: {bucketY.Content} Y: {bucketX.Content}");
                }
                if (bucketX.Content == target || bucketY.Content == target)
                {
                    Console.WriteLine($"It took {iterations} to get the solution");
                    steps.Add("SOLVED");
                    break;
                }

                // Empty Bucket Y
                if (bucketY.Content == bucketY.BucketLimit)
                {
                    bucketY.Empty();
                    if (PourFrom)
                    {
                        steps.Add($"Bucket Y is empty: {bucketX.Content}");
                    }
                    else
                    {
                        steps.Add($"Bucket X is Empty : {bucketY.Content}");
                    }
                }

                // Fill bucketX
                if (bucketX.Content == 0)
                {
                    bucketX.Fill();
                    if (PourFrom)
                    {
                        steps.Add($"Bucket X is Fill: {bucketX.Content}");
                    }
                    else
                    {
                        steps.Add($"Bucket Y is Fill : {bucketY.Content}");
                    }
                }

                ++iterations;
                //Safety net
                if (iterations >= 20) {
                    steps.Clear();
                    steps.Add("No solution");
                    break;
                }
            }
        }

        //Sends all the instructions collected through the loop in the form of a json.
        public static string Post()
        {   
            string iterations = System.Text.Json.JsonSerializer.Serialize(steps);
            steps.Clear();
            return iterations;
        }

    }
}
