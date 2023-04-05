using Data;
using UnityEngine;

namespace Ui.Components
{
    public class UserItem : AbstractItem<UserData>
    {
        [SerializeField] private Field idField;
        [SerializeField] private Field nameField;
        [SerializeField] private Field usernameField;
        [SerializeField] private Field emailField;
        [SerializeField] private Field geoField;

        protected override void UpdateUI()
        {
            idField.SetFieldValue(nameof(Data.Id), Data.Id.ToString());
            nameField.SetFieldValue(nameof(Data.Name), Data.Name);
            usernameField.SetFieldValue(nameof(Data.Username), Data.Username);
            emailField.SetFieldValue(nameof(Data.Email), Data.Email);
            geoField.SetFieldValue(nameof(Data.Address.Geo),
                $"{Data.Address.Geo.Latitude}:{Data.Address.Geo.Longitude}");
        }
    }
}