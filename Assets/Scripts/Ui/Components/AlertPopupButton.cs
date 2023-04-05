using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui.Components
{
    public class AlertPopupButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private UnityEvent onClick;

        public UnityEvent OnClick { get; } = new UnityEvent();

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            onClick?.Invoke();
            OnClick?.Invoke();
        }

        public void Set(string labelText)
        {
            label.text = labelText;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}