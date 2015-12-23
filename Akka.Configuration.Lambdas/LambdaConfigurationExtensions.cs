﻿using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Configuration.Builders;
using Akka.Configuration.Builders.Hocon;

namespace Akka.Configuration.Lambdas
{
    public static class LambdaConfigurationExtensions
    {
        public static ActorSystemHost CreateHost(this Action<ActorSystem> installAction,
            IEnumerable<KeyValuePair<string, string>> entries, IActorSystemBlockingStrategy blockingStrategy)
        {
            return CreateHost(installAction, new ActorSystemBuilder(entries.ToHoconBuilder()), blockingStrategy);
        }

        public static ActorSystemHost CreateHost(this Action<ActorSystem> installAction, 
            IActorSystemBuilder actorSystemBuilder, 
            IActorSystemBlockingStrategy blockingStrategy)
        {
            var host = new ActorSystemHost(actorSystemBuilder, new ActorSystemInstallerAdapter(installAction), blockingStrategy);
            return host;
        }
    }
}