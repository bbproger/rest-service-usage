using Data;
using UnityEngine;

namespace Services.Data
{
    public class PhotoItemInfo
    {
        public PhotoData PhotoData { get; set; }
        public Texture2D MainTexture { get; set; }
        public Texture2D ThumbnailTexture { get; set; }
    }
}