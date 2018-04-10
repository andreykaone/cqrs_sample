using System.Threading;
using System.Threading.Tasks;

namespace CqrsSample.Core.Abstraction
{
    /// <summary>
    ///     Интерфейс диспетчера запуска команд
    /// </summary
    public interface ICommandsDispatcher
    {
        /// <summary>
        ///     Метод для асинхронного запуска команды
        /// </summary>
        /// <typeparam name="TCommand">Запускаемая команда</typeparam>
        /// <param name="command">Данные, необходимые для запуска команды</param>
        /// <param name="cancellationToken"></param>
        Task ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default(CancellationToken))
            where TCommand : ICommand;
    }
}
