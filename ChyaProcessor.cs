using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;

namespace EventHubProcessor
{
   public class ChyaProcessor : IEventProcessor
   {
      public Task OpenAsync(PartitionContext context)
      {
         Console.WriteLine($"SimpleEventProcessor initialized. Partition: '{context.PartitionId}'");
         return Task.CompletedTask;
      }

      public Task CloseAsync(PartitionContext context, CloseReason reason)
      {
         Console.WriteLine($"Processor Shutting Down. Partition '{context.PartitionId}', Reason: '{reason}'.");
         return Task.CompletedTask;
      }

      public Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
      {
         foreach (var msg in messages)
         {
            var data = Encoding.UTF8.GetString(msg.Body.Array, msg.Body.Offset, msg.Body.Count);
            Console.WriteLine(data + " Partition: " + context.PartitionId);
         }

         return context.CheckpointAsync();
      }

      public Task ProcessErrorAsync(PartitionContext context, Exception error)
      {
         Console.WriteLine($"Error on Partition: {context.PartitionId}, Error: {error.Message}");
         return Task.CompletedTask;
      }
   }
}
