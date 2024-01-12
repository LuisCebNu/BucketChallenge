using System;
using Microsoft.AspNetCore.Builder;

WebApplicationBuilder Builder =
    WebApplication.CreateBuilder(args);

WebApplication App =
    Builder.Build();

App.MapGet("/",

    () => "Sorry, Mario. The Princess is in another castle."
    );

App.Run();