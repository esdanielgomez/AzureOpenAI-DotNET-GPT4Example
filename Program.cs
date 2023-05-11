using Azure;
using Azure.AI.OpenAI;

var key = "YOUR_KEY";
var endpoint = "YOUR_ENDPOINT";
var deploymentOrModelName = "YOUR_MODEL_NAME";

var client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
var chatCompletionsOptions = new ChatCompletionsOptions()
{
    Messages =
        {
            new ChatMessage(ChatRole.System, @"You are an assistant that knows about Azure resources, and who only answers if the resource name is of type Platform as a Service (PaaS), or Infrastructure as a Service (IaaS)"),
            new ChatMessage(ChatRole.User, @"Azure App Service"),
            new ChatMessage(ChatRole.Assistant, @"Platform as a Service (PaaS)"),
            new ChatMessage(ChatRole.User, @"Azure SQL Database"),
            new ChatMessage(ChatRole.Assistant, @"Platform as a Service (PaaS)"),
            new ChatMessage(ChatRole.User, @"Azure Virtual Machine"),
            new ChatMessage(ChatRole.Assistant, @"Infrastructure as a Service (IaaS)")
    },
    Temperature = (float)0.5,
    MaxTokens = 800,
    NucleusSamplingFactor = (float)0.95,
    FrequencyPenalty = 0,
    PresencePenalty = 0,
};

Console.WriteLine("Azure OpenAI for Azure Resources context\n");

while (true)
{
    var question = Console.ReadLine();
    var messageUser = new ChatMessage(ChatRole.User, question);
    chatCompletionsOptions.Messages.Add(messageUser);

    var responseWithoutStream = await client.GetChatCompletionsAsync(
    deploymentOrModelName, chatCompletionsOptions);

    var completions = responseWithoutStream.Value;
    string messageAssistant = completions.Choices[0].Message.Content;

    Console.WriteLine(messageAssistant);
    Console.WriteLine("-----------------------------\n");

}