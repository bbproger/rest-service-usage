using System.Threading;
using Cysharp.Threading.Tasks;
using Services;
using Services.Data;
using Ui.Components;
using Ui.Popup;
using Ui.Services;
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
        private IPopupService _popupService;

        private DynamicContainer<PhotoItem, PhotoItemInfo> _photosDynamicContainer;

        public UnityEvent OnBack => backButton.onClick;

        public UnityEvent<bool> OnPreparing { get; } = new();

        [Inject]
        private void Inject(IPhotosService photosService, IPopupService popupService)
        {
            _popupService = popupService;
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
            OnPreparing?.Invoke(false);
            if (!result.Success)
            {
                AlertPopupResult alertPopupResult = await _popupService.ShowAlertPopup(new AlertInfo(
                    "oops!",
                    "Something went wrong while retrieving users data",
                    AlertPopupButtonType.Ok
                ));
                Debug.Log($"Alert popup result: {alertPopupResult}");
                return;
            }

            _photosDynamicContainer.Propagate(result.Value);
        }

        private void OnDestroy()
        {
            _photosDynamicContainer.ClearItems();
            _cancellationTokenSource?.Cancel(false);
        }
    }
}