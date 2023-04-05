using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using Services;
using Ui.Components;
using Ui.Popup;
using Ui.Services;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Ui.View
{
    public class UsersView : AbstractView
    {
        [SerializeField] private UserItem userItemPrefab;
        [SerializeField] private RectTransform container;
        [SerializeField] private Button backButton;

        private CancellationTokenSource _cancellationTokenSource;

        private IUserService _userService;
        private IPopupService _popupService;

        private DynamicContainer<UserItem, UserData> _usersDynamicContainer;

        public UnityEvent OnBack => backButton.onClick;

        public UnityEvent<bool> OnPreparing { get; } = new();

        [Inject]
        private void Inject(IUserService userService, IPopupService popupService)
        {
            _popupService = popupService;
            _userService = userService;
        }

        private void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _usersDynamicContainer = new DynamicContainer<UserItem, UserData>(userItemPrefab, container);
            RetrieveData().Forget();
        }

        private async UniTask RetrieveData()
        {
            OnPreparing?.Invoke(true);
            Result<UserData[]> result = await _userService.GetUsersAsync(_cancellationTokenSource.Token);
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

            _usersDynamicContainer.Propagate(result.Value);
        }

        private void OnDestroy()
        {
            _usersDynamicContainer.ClearItems();
            _cancellationTokenSource?.Cancel(false);
        }
    }
}