using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using HelloWorldActor.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace HelloWorldActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class HelloWorld : Actor, IHelloWorld
    {
        HubConnection connection;
        public HelloWorld(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
            connection = new HubConnectionBuilder()
            .WithUrl("http://localhost/DotnetCoreSignalR/chat")
            .Build();
        }
        public Task<string> Connect()
        {
            connection.StartAsync().Wait();
            ActorEventSource.Current.ActorHostInitializationFailed("Connected from my reliable actor!");
            return Task.FromResult("Connect from my reliable actor!");

        }

        public Task<string> SendHelloWorldAsync()
        {
            connection.InvokeAsync("Send", "SendFromActor", "Hello World").Wait();
            return Task.FromResult("SendHelloWorldAsync from my reliable actor!");
        }
    }
}
