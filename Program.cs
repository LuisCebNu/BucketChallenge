using System;
using System.Text;
using BucketChallenge;
using Microsoft.AspNetCore.Builder;

// It will open browser and start the server.
WebApplicationBuilder Builder =
    WebApplication.CreateBuilder(args);

WebApplication App =
    Builder.Build();

App.MapGet("/",

    () => "Hello there! Please POST the buckets you want to measuare and the target measure."
    );

//In the /POST link of the page, you need to insert the values you of the buckets
// example: localhost:55067/POST?num1=35&num2=45&amount=55
App.MapPost("/POST",
    (string bucketX, string bucketY, string target) => Solution(bucketX, bucketY, target)
    ); ;


App.Run();

//The main function of the program. Gets your previously shared input and send it to the
// Solver class.
static string Solution (string num1, string num2, string amount)
{
    uint bucket1 = ValidationCheck(num1);
    uint bucket2 = ValidationCheck(num2);
    uint target = ValidationCheck(amount);

    Solver.BucketConstructor(bucket1, bucket2, target);
    return Solver.Post();
}

// It checks if the numbers aren't negatives or any type of invalid input
// It uses 0 as the return input since this process won't accept it as a valid input
static uint ValidationCheck (string num)
{
    try
    {
        uint bucket = uint.Parse(num);
        return bucket;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
        return 0;

        throw;
    }
}