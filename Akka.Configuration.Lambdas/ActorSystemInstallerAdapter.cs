using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace Akka.Configuration.Lambdas
{
    public class ActorSystemInstallerAdapter : IActorSystemInstaller
    {
        private Action<ActorSystem> _installAction;

        public ActorSystemInstallerAdapter(Action<ActorSystem> installAction)
        {
            _installAction = installAction;
        }

        public void InstallActors(ActorSystem actorSystem)
        {
            _installAction(actorSystem);
        }
    }
}
