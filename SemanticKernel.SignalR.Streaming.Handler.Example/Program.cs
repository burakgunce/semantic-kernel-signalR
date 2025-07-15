using Microsoft.SemanticKernel;
using OpenAI;
using SemanticKernel.SignalR.Streaming.Handler.Example.Hubs;
using SemanticKernel.SignalR.Streaming.Handler.Example.Plugins;
using SemanticKernel.SignalR.Streaming.Handler.Example.Services;
using SemanticKernel.SignalR.Streaming.Handler.Example.ViewModels;
using System.ClientModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddKernel()
    .AddOpenAIChatCompletion(
        modelId: "google/gemini-2.0-pro-exp-02-05:free",
        openAIClient: new OpenAIClient(
            credential: new ApiKeyCredential("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"),
            options: new OpenAIClientOptions
            {
                Endpoint = new Uri("https://openrouter.ai/api/v1")
            })
    )
    .Plugins.AddFromType<CalculatorPlugin>()
            .AddFromType<ProductsPlugin>();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy.AllowAnyMethod()
                                       .AllowAnyHeader()
                                       .AllowCredentials()
                                       .SetIsOriginAllowed(s => true)));

builder.Services.AddHttpClient();
builder.Services.AddSignalR();
builder.Services.AddSingleton<AIService>();

var app = builder.Build();
app.UseCors();

app.MapPost("/chat", async (AIService aiService, ChatRequestVM chatRequest, CancellationToken cancellationToken)
    => await aiService.GetMessageStreamAsync(chatRequest.Prompt, chatRequest.ConnectionId, cancellationToken));

app.MapHub<AIHub>("ai-hub");

app.Run();