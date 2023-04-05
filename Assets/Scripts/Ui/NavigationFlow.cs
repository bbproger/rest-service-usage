using Ui.Popup;
using Ui.View;
using Zenject;

namespace Ui
{
    public class NavigationFlow : IInitializable
    {
        private readonly IUiService _uiService;
        private readonly IPopupService _popupService;

        public NavigationFlow(IUiService uiService, IPopupService popupService)
        {
            _uiService = uiService;
            _popupService = popupService;
        }

        void IInitializable.Initialize()
        {
            ShowMainView();
        }

        private void ShowMainView()
        {
            MainView mainView = _uiService.ShowView<MainView>();
            mainView.OnShowUsers.AddListener(ShowUsersView);
            mainView.OnShowTodos.AddListener(ShowTodosView);
            mainView.OnShowPhotos.AddListener(ShowPhotosView);
        }

        private void ShowUsersView()
        {
            UsersView usersView = _uiService.ShowView<UsersView>();
            usersView.OnBack.AddListener(ShowMainView);
            usersView.OnPreparing.AddListener(OnFetchingData);
        }

        private void ShowTodosView()
        {
            TodosView todosView = _uiService.ShowView<TodosView>();
            todosView.OnBack.AddListener(ShowMainView);
            todosView.OnPreparing.AddListener(OnFetchingData);
        }

        private void ShowPhotosView()
        {
            PhotosView photosView = _uiService.ShowView<PhotosView>();
            photosView.OnBack.AddListener(ShowMainView);
            photosView.OnPreparing.AddListener(OnFetchingData);
        }

        private void OnFetchingData(bool state)
        {
            if (state)
            {
                _popupService.ShowPopup<LoadingPopup>();
                return;
            }

            _popupService.CloseFirstPopup<LoadingPopup>();
        }
    }
}