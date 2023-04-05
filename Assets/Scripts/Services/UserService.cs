using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Network;
using Ui;
using UnityEngine;

namespace Services
{
    public interface IUserService
    {
        UniTask<Result<UserData[]>> GetUsersAsync(CancellationToken cancellationToken = default);
    }

    public class UserService : IUserService
    {
        private readonly IRestClient _restClient;

        public UserService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async UniTask<Result<UserData[]>> GetUsersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                UserData[] result = await _restClient.GetAsync<UserData[]>("usersss", cancellationToken);
                return Result<UserData[]>.Ok(result);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to get users");
                return Result<UserData[]>.Fail(e);
            }
        }
    }
}