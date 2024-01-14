using System;
using BucketChallenge;
using Microsoft.AspNetCore.Builder;

WebApplicationBuilder Builder =
    WebApplication.CreateBuilder(args);

WebApplication App =
    Builder.Build();

App.MapGet("/",

    () => "Sorry, Mario. The Princess is in another castle."
    );

// Add Post
App.MapPost("/POST",
    (String num1, String num2) => test(num1,num2)
    ) ;


App.Run();

static void test (string num1, string num2)
{
    int bucket1 = int.Parse(num1);
    int bucket2 = int.Parse(num2);

    Solver.BucketConstructor(bucket1, bucket2, 96);
}