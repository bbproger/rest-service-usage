using System.Threading;
using Cysharp.Threading.Tasks;
using Services;
using Ui.Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Ui.View
{
    public class PhotosView : AbstractView
    {
        [SerializeField] private PhotoItem photoItemPrefab;
        [SerializeField] private RectTransform container;
        [SerializeField] private Button backButton;

        private CancellationTokenSource _cancellationTokenSource;

        private IPhotosService _photosService;

        private DynamicContainer<PhotoItem, PhotoItemInfo> _photosDynamicContainer;

        public UnityEvent OnBack => backButton.onClick;

        public UnityEvent<bool> OnPreparing { get; } = new();

        [Inject]
        private void Inject(IPhotosService photosService)
        {
            _photosService = photosService;
        }

        private void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _photosDynamicContainer = new DynamicContainer<PhotoItem, PhotoItemInfo>(photoItemPrefab, container);
            RetrieveData().Forget();
        }

        private async UniTask RetrieveData()
        {
            OnPreparing?.Invoke(true);
            Result<PhotoItemInfo[]> result = await _photosService.GetPhotosAsync(_cancellationTokenSource.Token);
            if (!result.Success)
            {
                Debug.LogError($"Something went wrong: {result.Exception}");
                return;
            }

            _photosDynamicContainer.Propagate(result.Value);
            OnPreparing?.Invoke(false);
        }

        private void OnDestroy()
        {
            _photosDynamicContainer.ClearItems();
            _cancellationTokenSource?.Cancel(false);
        }
    }
}