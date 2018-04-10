using System.Threading;
using System.Threading.Tasks;

namespace CqrsSample.Core.Abstraction
{
    /// <summary>
    /// Интерфейс для обработчика синхронной команды
    /// </summary>
    /// <typeparam name="TCommand">Информация, необходимая для выполнения команды</typeparam>
    public interface IAsyncCommandHandler<TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Метод для запуска выполнения команды
        /// </summary>
        /// <param name="command">Информация, необходимая для выполнения команды</param>
        /// <param name="cancellationToken"></param>
        Task ExecuteAsync(TCommand command, CancellationToken cancellationToken);
    }
}
