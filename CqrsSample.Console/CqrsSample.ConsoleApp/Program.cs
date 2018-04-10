namespace CqrsSample.Console
{
    using CqrsSample.Core;
    using CqrsSample.Core.Abstraction;
    using LightInject;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new ServiceContainer();
            container.RegisterConstructorDependency<IServiceContainer>((factory, parameterInfo) => container);
            container.AddCqrs();
            container.Register<IAsyncQueryHandler<GetStringWithIdQuery, string>, GetStringWithIdQueryHandler>();
            container.Register<IAsyncCommandHandler<SetSomeValuesCommand>, SetSomeValuesCommandHandler>();

            var queryDispatcher = container.GetInstance<IQueriesDispatcher>();
            var commandDispatcher = container.GetInstance<ICommandsDispatcher>();

            var query = new GetStringWithIdQuery(123);
            var queryResult = queryDispatcher.ExecuteAsync(query).Result;

            var command = new SetSomeValuesCommand(1551);
            commandDispatcher.ExecuteAsync(command);

            Console.WriteLine($@"get: '{queryResult}' and execute 'SetSomeValuesCommand' command");
            Console.ReadKey();
        }
    }


    public class GetStringWithIdQuery : IQuery<string>
    {
        public long EntityId { get; private set; }

        public GetStringWithIdQuery(long entityId)
        {
            EntityId = entityId;
        }
    }
    public class SetSomeValuesCommand : ICommand
    {
        public SetSomeValuesCommand(long entityId)
        {
            EntityId = entityId;
        }

        public long EntityId { get; }
    }


    public class GetStringWithIdQueryHandler : IAsyncQueryHandler<GetStringWithIdQuery, string>
    {
        public async Task<string> AskAsync(GetStringWithIdQuery query, CancellationToken cancellationToken)
        {
            return $"We have entityId = {query.EntityId}";
        }
    }
    public class SetSomeValuesCommandHandler : IAsyncCommandHandler<SetSomeValuesCommand>
    {
        public SetSomeValuesCommandHandler()
        {
            //обычно здесь резовлятся разные сервисы, репозитории, etc
        }
        public async Task ExecuteAsync(SetSomeValuesCommand command, CancellationToken cancellationToken)
        {
            //for example - (await) get entity by id
            // (await) change some values
            //if we need to return some values
            // from there we can set some command properties 
        }
    }
}
