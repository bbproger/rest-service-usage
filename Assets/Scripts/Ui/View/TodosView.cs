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
    public class TodosView : AbstractView
    {
        [SerializeField] private TodoItem todoItemPrefab;
        [SerializeField] private RectTransform container;
        [SerializeField] private Button backButton;

        private CancellationTokenSource _cancellationTokenSource;

        private ITodoService _todoService;
        private IPopupService _popupService;

        private DynamicContainer<TodoItem, TodoData> _todosDynamicContainer;

        public UnityEvent OnBack => backButton.onClick;

        public UnityEvent<bool> OnPreparing { get; } = new();

        [Inject]
        private void Inject(ITodoService todoService, IPopupService popupService)
        {
            _popupService = popupService;
            _todoService = todoService;
        }

        private void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _todosDynamicContainer = new DynamicContainer<TodoItem, TodoData>(todoItemPrefab, container);
            RetrieveData().Forget();
        }

        private async UniTask RetrieveData()
        {
            OnPreparing?.Invoke(true);
            Result<TodoData[]> result = await _todoService.GetTodosAsync(_cancellationTokenSource.Token);
            OnPreparing?.Invoke(false);
            if (!result.Success)
            {
                AlertPopupResult alertPopupResult = await _popupService.ShowAlertPopup(new AlertInfo(
                    "oops!",
                    "Something went wrong while retrieving todos data",
                    AlertPopupButtonType.Ok
                ));
                Debug.Log($"Alert popup result: {alertPopupResult}");
                return;
            }

            _todosDynamicContainer.Propagate(result.Value);
        }

        private void OnDestroy()
        {
            _todosDynamicContainer.ClearItems();
            _cancellationTokenSource?.Cancel(false);
        }
    }
}