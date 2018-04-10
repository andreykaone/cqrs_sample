using System.Threading;
using System.Threading.Tasks;

namespace CqrsSample.Core.Abstraction
{
    /// <summary>
    ///     Интерфейс диспетчера выполнения запросов
    /// </summary>
    public interface IQueriesDispatcher
    {
        /// <summary>
        ///     Метод для запуска запроса
        /// </summary>
        /// <typeparam name="TResult">Тип результата запроса</typeparam>
        /// <param name="query">Данные, необходимые для запуска запроса</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Результат выполнения запроса</returns>
        Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default(CancellationToken));
    }
}
