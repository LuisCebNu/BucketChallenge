using System.Reflection.Metadata.Ecma335;

namespace BucketChallenge
{
    public class Bucket
    {
        uint bucketLimit; //The amount of water a bucket can take.
        uint content = 0; //The current amount the bucket is holding.
        public uint Content {
            get { return content; } 
        }
        public uint BucketLimit { 
      
            get { return bucketLimit; }
        }
        public Bucket(uint limit) 
        {
            bucketLimit = limit;
        }

        //There isn't half full or 1/3 full. So we assign the value of its limit
        // To the content
        public void Fill() {

            content = BucketLimit;
        }
        // Resets content.
        public void Empty()
        {
            content = 0;
        }

        //Moves a certain amount of water to one bucket from another.
        public void Transfer(Bucket bucket)
        {
            //We do this check to stop any of the buckets from overflowing.
            // If bucket A has a limit that is smaller than bucket B
            // we subtract the bucket A limit's
            // to the content of bucket B.
            if (bucketLimit < bucket.bucketLimit)
            {
                content += bucketLimit;
                bucket.content -= bucketLimit;

            } else
            { 

                content += bucket.bucketLimit;
                bucket.content -= bucket.bucketLimit;
            }
           
        }

    }
}
