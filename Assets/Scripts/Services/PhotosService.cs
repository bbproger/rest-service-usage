using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Network;
using Services.Data;
using Ui;
using UnityEngine;

namespace Services
{
    public interface IPhotosService
    {
        UniTask<Result<PhotoItemInfo[]>> GetPhotosAsync(CancellationToken cancellationToken = default);
    }

    public class PhotosService : IPhotosService
    {
        private readonly IRestClient _restClient;

        public PhotosService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async UniTask<Result<PhotoItemInfo[]>> GetPhotosAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                PhotoData[] result = await _restClient.GetAsync<PhotoData[]>("albums/3/photos", cancellationToken);
                PhotoItemInfo[] photoItemInfos = await CollectAsync(result, cancellationToken);
                return Result<PhotoItemInfo[]>.Ok(photoItemInfos);
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to get photos");
                return Result<PhotoItemInfo[]>.Fail(e);
            }
        }

        private async UniTask<PhotoItemInfo[]> CollectAsync(PhotoData[] data,
            CancellationToken cancellationToken = default)
        {
            List<PhotoItemInfo> photoItemInfos = new();
            foreach (PhotoData photoData in data)
            {
                Texture2D thumbnailTask =
                    await _restClient.GetExplicitTextureAsync(photoData.ThumbnailUrl, cancellationToken);
                Texture2D mainTask = await _restClient.GetExplicitTextureAsync(photoData.Url, cancellationToken);

                photoItemInfos.Add(new PhotoItemInfo
                {
                    PhotoData = photoData,
                    ThumbnailTexture = thumbnailTask,
                    MainTexture = mainTask
                });
            }

            return photoItemInfos.ToArray();
        }
    }
}