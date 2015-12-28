using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Configuration.Builders;
using Akka.Configuration.Builders.Hocon;

namespace Akka.Configuration.Lambdas
{
    public static class ActorSystemHelpers
    {
        public static ActorSystemHost CreateHost(Action<ActorSystem> installAction,
            IEnumerable<KeyValuePair<string, string>> entries, IActorSystemBlockingStrategy blockingStrategy = null)
        {
            return installAction.CreateHost(new ActorSystemBuilder(entries.ToHoconBuilder()), blockingStrategy);
        }

        public static ActorSystemHost CreateHost(Action<ActorSystem> installAction,
            IActorSystemBuilder actorSystemBuilder,
            IActorSystemBlockingStrategy blockingStrategy = null)
        {
            return installAction.CreateHost(actorSystemBuilder, blockingStrategy);
        }
    }
}