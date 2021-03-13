using System;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace EventHubProcessor
{
   class Program
   {
      static async Task Main(string[] args)
      {
         var storageConn =
            "DefaultEndpointsProtocol=https;AccountName=chyastorage;AccountKey=uzMzvbJTKwrvFlwcnRQJrFUal6D4tz2YIgqJvFCDurFJuiGxGr/OHJtPdgbdkXjIa3O8YWKb2yzotRHfJGlQig==;EndpointSuffix=core.windows.net";
         var blobContainer = "event";
         var hubConn =
            "Endpoint=sb://chyaeventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=//JboujWToYmJ2tytgSKApsCgERt2OC4O4/H5uXD7CQ=";

         Console.WriteLine("Event Hub Processor...");

         var processorHost = new EventProcessorHost("chyahub",
                                                    PartitionReceiver.DefaultConsumerGroupName,
                                                    hubConn,
                                                    storageConn,
                                                    blobContainer
         );

         await processorHost.RegisterEventProcessorAsync<ChyaProcessor>();

         Console.WriteLine("Receiving. Press ENTER to stop worker.");
         Console.ReadLine();

         processorHost.UnregisterEventProcessorAsync();
      }
   }
}
