using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using System.Linq;

namespace FaqBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        private static string knowledgeBaseId = ConfigurationManager.AppSettings["KnowledgeBaseId"]; //// Knowledge base id of QnA Service.

        private static string qnamakerSubscriptionKey = ConfigurationManager.AppSettings["SubscriptionKey"]; ////Subscription key.

        private static string hostUrl = ConfigurationManager.AppSettings["HostUrl"];


        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            // return our reply to the user
            await context.PostAsync(this.GetAnswerFromService(activity.Text));
            context.Wait(MessageReceivedAsync);
        }

        private string GetAnswerFromService(string inputText)
        {
            //// Build the QnA Service URI
            Uri qnamakerUriBase = new Uri(hostUrl);
            var builder = new UriBuilder($"{qnamakerUriBase}/knowledgebases/{knowledgeBaseId}/generateAnswer");
            var postBody = $"{{\"question\": \"{inputText}\"}}";
            //Add the subscription key header
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Ocp-Apim-Subscription-Key", qnamakerSubscriptionKey);
                client.Headers.Add("Content-Type", "application/json");
                try
                {
                    var response = client.UploadString(builder.Uri, postBody);
                    var json = JsonConvert.DeserializeObject<QnAResult>(response);
                    return json?.answers?.FirstOrDefault().answer;
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }


        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }
    }


    public class QnAResult
    {
        public Answer[] answers { get; set; }
    }

    public class Answer
    {
        public string answer { get; set; }
        public string[] questions { get; set; }
        public float score { get; set; }
    }

}