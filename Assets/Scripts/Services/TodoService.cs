using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Network;
using Ui;
using UnityEngine;

namespace Services
{
    public interface ITodoService
    {
        UniTask<Result<TodoData[]>> GetTodosAsync(CancellationToken cancellationToken = default);
    }

    public class TodoService : ITodoService
    {
        private readonly IRestClient _restClient;

        public TodoService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async UniTask<Result<TodoData[]>> GetTodosAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                TodoData[] result = await _restClient.GetAsync<TodoData[]>("todos", cancellationToken);
                return Result<TodoData[]>.Ok(result);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to get todos");
                return Result<TodoData[]>.Fail(e);
            }
        }
    }
}