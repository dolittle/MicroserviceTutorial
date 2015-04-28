using Bifrost.Configuration;
using Bifrost.Execution;
using Bifrost.Ninject;
using Ninject;

namespace Web
{
    public class ContainerCreator : ICanCreateContainer
    {
        public readonly static IKernel Kernel;

        static ContainerCreator()
        {
            Kernel = new StandardKernel();
        }

        public IContainer CreateContainer()
        {
            
            var container = new Container(Kernel);
            return container;
        }
    }
}