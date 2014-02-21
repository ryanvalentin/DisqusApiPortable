using System.Threading.Tasks;
using System;

namespace Disqus.Api.Extensions
{
    /// <summary>
    /// Extension method helper to seprate Aggregate exceptions and throw them individually
    /// Courtesy of http://msmvps.com/blogs/jon_skeet/archive/2011/06/22/eduasync-part-11-more-sophisticated-but-lossy-exception-handling.aspx
    /// </summary>
    public static class TaskExtensions
    {
        public static Task<T> ThrowAllExceptions<T>(this Task<T> task)
        {
            TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();

            task.ContinueWith(ignored =>
                {
                    switch (task.Status)
                    {
                        case TaskStatus.Canceled:
                            taskCompletionSource.SetCanceled();
                            break;
                        case TaskStatus.RanToCompletion:
                            taskCompletionSource.SetResult(task.Result);
                            break;
                        case TaskStatus.Faulted:
                            taskCompletionSource.SetException(task.Exception);
                            break;
                        default:
                            taskCompletionSource.SetException(new InvalidOperationException("Continuation called illegally."));
                            break;
                    } 
                });

            return taskCompletionSource.Task;
        }
    }
}
