using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui.View
{
    public class MainView : AbstractView
    {
        [SerializeField] private Button showUsersButton;
        [SerializeField] private Button showTodosButton;
        [SerializeField] private Button showPhotosButton;

        public UnityEvent OnShowUsers => showUsersButton.onClick;
        public UnityEvent OnShowTodos => showTodosButton.onClick;
        public UnityEvent OnShowPhotos => showPhotosButton.onClick;
    }
}