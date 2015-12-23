using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Configuration.Builders;
using Akka.Configuration.Builders.Hocon;

namespace Akka.Configuration.Lambdas
{
    public static class ActorSystemHostRunnerExtensions
    {
        public static Action<string> CreateActionFrom(IEnumerable<KeyValuePair<string, string>> entries,
            Action<ActorSystem> installAction,
            IActorSystemBlockingStrategy blockingStrategy)
        {
            return CreateActionFrom(entries, new ActorSystemInstallerAdapter(installAction), blockingStrategy);
        }

        public static Action<string> CreateActionFrom(IEnumerable<KeyValuePair<string, string>> entries, 
            IActorSystemInstaller installer, 
            IActorSystemBlockingStrategy blockingStrategy)
        {
            return CreateActionFrom(new ActorSystemBuilder(entries.ToHoconBuilder()), installer, blockingStrategy);
        }

        public static Action<string> CreateActionFrom(IActorSystemBuilder actorSystemBuilder, 
            IActorSystemInstaller installer, IActorSystemBlockingStrategy blockingStrategy)
        {
            var host = new ActorSystemHost(actorSystemBuilder, installer, blockingStrategy);
            return host.Run;
        }
    }
}