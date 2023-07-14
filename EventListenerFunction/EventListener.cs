using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Alx.ASBDupeTest
{
    public static class EventListener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="req"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        [FunctionName("EventListener")]
        [return: ServiceBus("msgqueue", EntityType = Microsoft.Azure.WebJobs.ServiceBus.ServiceBusEntityType.Queue, Connection = "ServiceBusConnection")]
        public static async Task<ServiceBusMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Executing Function Logic.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(requestBody);

            var message = new ServiceBusMessage(requestBody);
            message.MessageId = "123";

            return message;
        }
    }
}
