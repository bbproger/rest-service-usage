using TMPro;
using UnityEngine;

namespace Ui.Components
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI keyText;
        [SerializeField] private TextMeshProUGUI valueText;

        public void SetFieldValue(string key, string value)
        {
            keyText.text = key;
            valueText.text = value;
        }
    }
}