using Data;
using UnityEngine;

namespace Ui.Components
{
    public class TodoItem : AbstractItem<TodoData>
    {
        [SerializeField] private Field idField;
        [SerializeField] private Field userIdField;
        [SerializeField] private Field titleField;
        [SerializeField] private Field completedField;

        protected override void UpdateUI()
        {
            idField.SetFieldValue($"{nameof(Data.Id)}", $"{Data.Id}");
            userIdField.SetFieldValue($"{nameof(Data.UserId)}", $"{Data.UserId}");
            titleField.SetFieldValue($"{nameof(Data.Title)}", $"{Data.Title}");
            completedField.SetFieldValue($"{nameof(Data.Completed)}", $"{Data.Completed}");
        }
    }
}