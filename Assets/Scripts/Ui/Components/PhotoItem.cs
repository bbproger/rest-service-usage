using Data;
using UnityEngine;

namespace Ui.Components
{
    public class PhotoItemInfo
    {
        public PhotoData PhotoData { get; set; }
        public Texture2D MainTexture { get; set; }
        public Texture2D ThumbnailTexture { get; set; }
    }

    public class PhotoItem : AbstractItem<PhotoItemInfo>
    {
        [SerializeField] private Field idField;
        [SerializeField] private Field albumIdField;
        [SerializeField] private Field titleField;
        [SerializeField] private PhotoField mainPhotoField;
        [SerializeField] private PhotoField thumbnailPhotoField;

        protected override void UpdateUI()
        {
            idField.SetFieldValue($"{nameof(Data.PhotoData.Id)}", $"{Data.PhotoData.Id}");
            albumIdField.SetFieldValue($"{nameof(Data.PhotoData.AlbumId)}", $"{Data.PhotoData.AlbumId}");
            titleField.SetFieldValue($"{nameof(Data.PhotoData.Title)}", $"{Data.PhotoData.Title}");
            mainPhotoField.SetTexture(Data.MainTexture);
            thumbnailPhotoField.SetTexture(Data.ThumbnailTexture);
        }
    }
}