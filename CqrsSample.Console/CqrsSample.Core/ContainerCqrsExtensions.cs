
namespace CqrsSample.Core
{
    using CqrsSample.Core.Abstraction;
    using CqrsSample.Core.Implementation;
    using LightInject;
    public static class ContainerCqrsExtensions
    {
        public static IServiceContainer AddCqrs(this IServiceContainer container)
        {
            container.Register<ICommandsDispatcher, CommandsDispatcher>();
            container.Register<IQueriesDispatcher, QueriesDispatcher>();
            return container;
        }
    }
}

