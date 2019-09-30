using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using HelloWorldActor.Interfaces;

namespace ActorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IHelloWorld actor = ActorProxy.Create<IHelloWorld>(ActorId.CreateRandom(), new Uri("fabric:/ServiceFabric/HelloWorldActorService"));
            Task<string> retval = actor.Connect();
            Console.Write(retval.Result);
            retval = actor.SendHelloWorldAsync();
            Console.Write(retval.Result);
            Console.ReadLine();
        }
    }
}