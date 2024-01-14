using System.Reflection.Metadata.Ecma335;

namespace BucketChallenge
{
    public class Bucket
    {
        int bucketLimit;
        int content = 0;
        public int Content {
            get { return content; } 
        }
        public int BucketLimit { 
      
            get { return bucketLimit; }
        }
        public Bucket(int limit) 
        {
            bucketLimit = limit;
        }

        public void Fill() {

            content = BucketLimit;
        }

        public void Empty()
        {
            content = 0;
        }

        public void Transfer(Bucket bucket)
        {
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
