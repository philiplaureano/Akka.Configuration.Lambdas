using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Configuration.Builders;
using Akka.Configuration.Builders.Hocon;

namespace Akka.Configuration.Lambdas
{
    public static class ActorSystemHostRunnerExtensions
    {
        public static Action<string> CreateHostRunnerFrom(IEnumerable<KeyValuePair<string, string>> entries,
            Action<ActorSystem> installAction,
            IActorSystemBlockingStrategy blockingStrategy)
        {
            return CreateHostRunnerFrom(entries, new ActorSystemInstallerAdapter(installAction), blockingStrategy);
        }

        public static Action<string> CreateHostRunnerFrom(IEnumerable<KeyValuePair<string, string>> entries, 
            IActorSystemInstaller installer, 
            IActorSystemBlockingStrategy blockingStrategy)
        {
            return CreateHostRunnerFrom(new ActorSystemBuilder(entries.ToHoconBuilder()), installer, blockingStrategy);
        }

        public static Action<string> CreateHostRunnerFrom(IActorSystemBuilder actorSystemBuilder, 
            IActorSystemInstaller installer, IActorSystemBlockingStrategy blockingStrategy)
        {
            var host = new ActorSystemHost(actorSystemBuilder, installer, blockingStrategy);
            return host.Run;
        }
    }
}