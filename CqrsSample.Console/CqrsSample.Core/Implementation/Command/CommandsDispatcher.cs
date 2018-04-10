using CqrsSample.Core.Abstraction;
using LightInject;
using System;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace CqrsSample.Core.Implementation
{
    public class CommandsDispatcher : ICommandsDispatcher
    {
        private readonly IServiceContainer _container;

        public CommandsDispatcher(IServiceContainer container)
        {
            _container = container;
        }
        public async Task ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken)) where TCommand : ICommand
        {
            try
            {
                await ExecuteCommandAsync(command, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                throw;
            }
        }

        private async Task ExecuteCommandAsync(ICommand command, CancellationToken cancellationToken)
        {
            var commandType = command.GetType();
            var handler = CreateAsyncHandler(commandType);
            var method = handler.GetType().GetMethod(nameof(IAsyncCommandHandler<ICommand>.ExecuteAsync), new[] { commandType, cancellationToken.GetType() });

            try
            {
                await (Task)method.Invoke(handler, new object[] { command, cancellationToken });
            }
            catch (TargetInvocationException exception)
            {
                ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            }
        }

        private object CreateAsyncHandler(Type commandType)
        {
            var handlerType = typeof(IAsyncCommandHandler<>).MakeGenericType(commandType);
            return _container.GetInstance(handlerType);
        }
    }
}
