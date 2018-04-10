using CqrsSample.Core.Abstraction;
using LightInject;
using System;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace CqrsSample.Core.Implementation
{
    public class QueriesDispatcher : IQueriesDispatcher
    {
        private readonly IServiceContainer _container;

        public QueriesDispatcher(IServiceContainer container)
        {
            _container = container;
        }

        public async Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default(CancellationToken))
        {
            // can check access to execute query

            var result = await ExecuteQueryAsync(query, cancellationToken).ConfigureAwait(false);

            return result;
        }

        private Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        {
            try
            {
                var queryType = query.GetType();
                var handler = CreateAsyncHandler<TResult>(queryType);
                var method = handler.GetType().GetMethod(nameof(IAsyncQueryHandler<IQuery, TResult>.AskAsync), new[] { queryType, cancellationToken.GetType() });
                return (Task<TResult>)method.Invoke(handler, new object[] { query, cancellationToken });
            }
            catch (TargetInvocationException ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                throw;
            }
        }

        public object CreateAsyncHandler<TResult>(Type queryType)
        {
            var handlerType = typeof(IAsyncQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));
            return _container.GetInstance(handlerType);
        }
    }
}