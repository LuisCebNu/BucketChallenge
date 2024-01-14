using System.Numerics;

namespace BucketChallenge
{   
    
    public class Solver
    {
        
        // Gets both the buckets limits and the amount wanted.
        public static void BucketConstructor(int bucketX, int bucketY, int amount)
        {
            Bucket BucketX = new Bucket(bucketX);
            Bucket BucketY = new Bucket(bucketY);
            int Amount = amount;

            Solution(BucketX, BucketY, Amount);
        }
        // Checks the numbers to see if we should continue the process
        static bool CanMeaseure(int bucketX, int bucketY, int amount)
        {
            return (amount % BigInteger.GreatestCommonDivisor(bucketX, bucketY) == 0);
        }

        static void Solution (Bucket bucketX, Bucket bucketY, int amount)
        {
            if (CanMeaseure(bucketX.BucketLimit,bucketY.BucketLimit,amount) && 
                (amount < bucketX.BucketLimit || amount < bucketY.BucketLimit))
            {
                if (whichBucket(bucketX.BucketLimit, bucketY.BucketLimit, amount))
                {
                    iteration(bucketX, bucketY, amount);
                    
                } else
                {
                    iteration(bucketY, bucketX, amount);
                }
            }
            else
            {
                Console.WriteLine("No solution");

            }
        }

        static bool whichBucket(int limitX, int limitY, int target)
        {
            limitX = (target - limitX) > 0 ? (target - limitX) : -(target - limitX);
            limitY = (target - limitY) > 0 ? (target - limitY) : -(target - limitY);
            bool pourFrom = limitY > limitX;

            return pourFrom;
        }

        static void iteration(Bucket bucketX, Bucket bucketY, int target)
        {   int iterations = 0;
            bucketX.Fill();
            while (bucketX.Content != target && bucketY.Content != target)
            {
                //pour from bucketX to bucketY
                bucketY.Transfer(bucketX);
                if (bucketX.Content == target || bucketY.Content == target)
                {
                    Console.WriteLine($"It took {iterations} to get the solution.");
                }

                
                if (bucketY.Content == bucketY.BucketLimit)
                {
                    bucketX.Empty();
                }

                // Fill bucketX
                if (bucketX.Content == 0)
                {
                    bucketX.Fill();
                }

                ++iterations;

            }
        }

    }
}
