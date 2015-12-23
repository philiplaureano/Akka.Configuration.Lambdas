using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration.Lambdas;
using FakeItEasy;
using NUnit.Framework;

namespace Akka.Configuration.LambdaTests
{
    [TestFixture]
    public class LambdaConfigurationTests
    {
        [Test]
        public void Should_be_able_to_adapt_lambda_to_actor_system_installer()
        {
            var installer = A.Fake<IActorSystemInstaller>();
            Action<ActorSystem> installAction = installer.InstallActors;

            var actorSystem = ActorSystem.Create("FakeSystem");
            IActorSystemInstaller adapter = new ActorSystemInstallerAdapter(installAction);
            adapter.InstallActors(actorSystem);

            A.CallTo(() => installer.InstallActors(A<ActorSystem>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Should_be_able_to_create_host_from_blocking_strategy_and_lambda_and_builder()
        {
            var blockingStrategy = A.Fake<IActorSystemBlockingStrategy>();
            var installer = A.Fake<IActorSystemInstaller>();
            var builder = A.Fake<IActorSystemBuilder>();

            Action<ActorSystem> installAction = installer.InstallActors;

            var host = installAction.CreateHost(builder, blockingStrategy);
            host.Run("FakeSystem");

            A.CallTo(() => installer.InstallActors(A<ActorSystem>.Ignored))
                .MustHaveHappened();

            A.CallTo(() => blockingStrategy.AwaitTermination(A<ActorSystem>.Ignored))
                .MustHaveHappened();
        }

        [Test]
        public void Should_be_able_to_create_host_using_dictionary()
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

            var host = installAction.CreateHost(entries, blockingStrategy);
            host.Run("FakeSystem");

            A.CallTo(() => installer.InstallActors(A<ActorSystem>.Ignored))
                .MustHaveHappened();

            A.CallTo(() => blockingStrategy.AwaitTermination(A<ActorSystem>.Ignored))
                .MustHaveHappened();
        }
    }
}
