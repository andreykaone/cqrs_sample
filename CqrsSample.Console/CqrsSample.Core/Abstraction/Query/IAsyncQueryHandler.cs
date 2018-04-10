using System.Threading;
using System.Threading.Tasks;

namespace CqrsSample.Core.Abstraction
{
    /// <summary>
    ///     Query handler interface
    /// </summary>
    /// <typeparam name="TQuery">Query to execute</typeparam>
    /// <typeparam name="TResult">Result</typeparam>
    public interface IAsyncQueryHandler<in TQuery, TResult> where TQuery : IQuery
    {
        /// <summary>
        ///     Method for starting query executing
        /// </summary>
        /// <param name="query">Info to query execution </param>
        /// <param name="cancellationToken"></param>
        /// <returns>Query result</returns>
        Task<TResult> AskAsync(TQuery query, CancellationToken cancellationToken);
    }
}
