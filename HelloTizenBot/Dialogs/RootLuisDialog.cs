using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;

namespace HelloTizenBot.Dialogs
{
    [LuisModel("64f464ab-0f2a-4412-9be1-af3e7fca947b", "d14db6f08ac845cda0873c80437bbb97")]
    [Serializable]
    public class RootLuisDialog : LuisDialog<object>
    {
        override public async Task StartAsync(IDialogContext context)
        {
            /*context.Wait(this.MessageReceived);

            return Task.CompletedTask;*/
            string message = $"Hi man, how are you doing?";
            await context.PostAsync(message);
            //context.Wait(this.MessageReceived);
        }
        
        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            //string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";
            string message = $"Sorry, I did not understand";
            await context.PostAsync(message);
            //context.Wait(this.MessageReceived);
        }

        [LuisIntent("Greetings")]
        public async Task Greetings(IDialogContext context, LuisResult result)
        {
            //await context.PostAsync($"Greeting Intent: We are analyzing your message: '{result.Query}'...");
            await context.PostAsync($"Greeting Intent: We are analyzing your message...");
            //context.Wait(this.MessageReceived);
        }

        [LuisIntent("Home")]
        public async Task Home(IDialogContext context, LuisResult result)
        {
            //await context.PostAsync($"Greeting Intent: We are analyzing your message: '{result.Query}'...");
            await context.PostAsync($"Home Intent: We are analyzing your message...");
            //context.Wait(this.MessageReceived);
        }
    }
}
