using System;
using Cysharp.Threading.Tasks;
using TMPro;
using Ui.Components;
using Ui.View;
using UnityEngine;

namespace Ui.Popup
{
    [Flags]
    public enum AlertPopupButtonType
    {
        None = 0,
        Ok = 1,
        Cancel = 2,
        OkCancel = Ok | Cancel
    }

    public struct AlertInfo
    {
        public string Title { get; }
        public string Description { get; }
        public AlertPopupButtonType ButtonType { get; }
        public string ActionLabel { get; }
        public string CancelLabel { get; }

        public AlertInfo(string title, string description, AlertPopupButtonType buttonType,
            string actionLabel = "Ok",
            string cancelLabel = "Cancel")
        {
            Title = title;
            Description = description;
            ButtonType = buttonType;
            ActionLabel = actionLabel;
            CancelLabel = cancelLabel;
        }
    }

    public enum AlertPopupResult
    {
        Ok,
        Cancel
    }

    public class AlertPopup : AbstractView
    {
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private AlertPopupButton actionButton;
        [SerializeField] private AlertPopupButton cancelButton;

        private UniTaskCompletionSource<AlertPopupResult> _completionSource;

        public UniTask<AlertPopupResult> Result => _completionSource.Task;

        private void Awake()
        {
            _completionSource = new UniTaskCompletionSource<AlertPopupResult>();
        }

        private void OnEnable()
        {
            actionButton.OnClick.AddListener(OnAction);
            cancelButton.OnClick.AddListener(OnCancel);
        }

        private void OnDisable()
        {
            actionButton.OnClick.RemoveListener(OnAction);
            cancelButton.OnClick.RemoveListener(OnCancel);
        }

        private void OnAction()
        {
            _completionSource?.TrySetResult(AlertPopupResult.Ok);
        }

        private void OnCancel()
        {
            _completionSource?.TrySetResult(AlertPopupResult.Cancel);
        }

        public void SetData(AlertInfo alertInfo)
        {
            titleText.text = alertInfo.Title;
            descriptionText.text = alertInfo.Description;
            actionButton.Set(alertInfo.ActionLabel);
            cancelButton.Set(alertInfo.CancelLabel);
            actionButton.SetActive(alertInfo.ButtonType.HasFlag(AlertPopupButtonType.Ok));
            cancelButton.SetActive(alertInfo.ButtonType.HasFlag(AlertPopupButtonType.Cancel));
        }
    }
}