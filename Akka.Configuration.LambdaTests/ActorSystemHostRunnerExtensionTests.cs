using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Configuration.Lambdas;
using FakeItEasy;
using NUnit.Framework;
using static Akka.Configuration.Lambdas.ActorSystemHostRunnerExtensions;

namespace Akka.Configuration.LambdaTests
{
    [TestFixture]
    public class ActorSystemHostRunnerExtensionTests
    {
        [Test]
        public void Should_be_able_to_create_action_from_dictionary_and_installer()
        {
            var blockingStrategy = A.Fake<IActorSystemBlockingStrategy>();
            var installer = A.Fake<IActorSystemInstaller>();

            var entries = new Dictionary<string, string>()
            {
                {"key1", "value1"},
                {"key2", "value2"},
                {"key3", "value3"}
            };

            Action<ActorSystem> installAction = installer.InstallActors;

            var runAction = CreateHostRunnerFrom(entries, installAction, blockingStrategy);
            runAction("FakeSystem");

            A.CallTo(() => installer.InstallActors(A<ActorSystem>.Ignored))
                .MustHaveHappened();

            A.CallTo(() => blockingStrategy.AwaitTermination(A<ActorSystem>.Ignored))
                .MustHaveHappened();
        }
    }
}