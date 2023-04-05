using UnityEngine;
using UnityEngine.UI;

namespace Ui.Components
{
    public class PhotoField:MonoBehaviour
    {
        [SerializeField] private RawImage rawImage;

        public void SetTexture(Texture2D texture)
        {
            rawImage.texture = texture;
        }
    }
}