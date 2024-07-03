using Azure;
using Azure.AI.OpenAI;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Images;

//var openAiClient = new OpenAIClient(Environment.GetEnvironmentVariable("OPENAIKEY"));
var azureOpenAiClient = new AzureOpenAIClient(
    new Uri(Environment.GetEnvironmentVariable("AZUREOAIENDPOINT")),
    new AzureKeyCredential(Environment.GetEnvironmentVariable("AZUREOAIKEY"))
);

//var chatClient = openAiClient.GetChatClient("gpt-4o");
var chatClient = azureOpenAiClient.GetChatClient("chatgen");

Console.Write("Welcome to the OpenAI .NET Chatbot.\n\nAsk me anything about tech: ");
var input = Console.ReadLine();

ChatCompletion completion = await chatClient.CompleteChatAsync(
    [
        new SystemChatMessage("You are a helpful tech guide that explains tech terms in simple words and less than 100 words."),
        new UserChatMessage(input)
    ]
);

Console.WriteLine(completion);

//var imageClient = openAiClient.GetImageClient("dall-e-3");
var imageClient = azureOpenAiClient.GetImageClient("imagegen");

GeneratedImage image = await imageClient.GenerateImageAsync($"{completion}");

Console.WriteLine($"Generated image: {image.ImageUri}");