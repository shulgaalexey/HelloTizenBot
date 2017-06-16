using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;

namespace HelloTizenBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            //await context.PostAsync($"Hey boss, how are you doing?");

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            
            //await context.PostAsync($"Starting Luis query");

            try
            {
                //LuisService luis = new LuisService(new LuisModelAttribute("86684636-488d-48b3-a4c6-233ef496d3d1", "5f3fca65f0a64d68ab6d4d474b1b0fa6", LuisApiVersion.V2, "westus", true, false, true, true));
                LuisService luis = new LuisService(new LuisModelAttribute("64f464ab-0f2a-4412-9be1-af3e7fca947b", "d14db6f08ac845cda0873c80437bbb97"));
                var luisResult = await luis.QueryAsync(activity.Text, System.Threading.CancellationToken.None);

                //await context.PostAsync($"Luis Result received .. Processing intents");

                switch (luisResult.TopScoringIntent.Intent)
                {
                    case "":
                    case "None":
                        await context.PostAsync($"Top Intent: None\nPlease repeat your query. I did not understand...");
                        break;
                    case "Greetings":
                        await context.PostAsync($"Top Intent: Greeting\nHow may I serve you?");
                        break;
                    case "Home":
                        var entities = $"(";
                        foreach (var entity in luisResult.Entities)
                        {
                            entities += $"{entity.Entity}, {entity.Type}, {entity.Score} = ";
                        }
                        entities += $")";
                        await context.PostAsync($"Top Intent: Home\n | {entities} | What should I do for you?");
                        break;
                    default:
                        await context.PostAsync($"Top Intent: UNKNOWN\nPlease repeat your query. I could not recognize your intent...");
                        break;
                }
            }
            catch (Exception exc)
            {
                await context.PostAsync($"Error while processing Luis query\n{exc}");
            }

            // return our reply to the user
            //await context.PostAsync($"You sent {activity.Text} which was {length} characters");
            context.Wait(MessageReceivedAsync);
            //await context.Forward(new RootLuisDialog(), MessageReceivedAsync, activity, System.Threading.CancellationToken.None);
        }
    }

    [Serializable]
    public class ErrorDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {

            context.PostAsync($"Starting Exception Bot").Wait();


            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user

            await context.PostAsync($"Received an exception. MessageRewceived func of Error Dialog");


            await context.PostAsync($"Received an exception\n{activity?.Text}");
            context.Wait(MessageReceivedAsync);
            //await context.Forward(new RootLuisDialog(), MessageReceivedAsync, activity, System.Threading.CancellationToken.None);
        }
    }
}
