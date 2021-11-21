using System.Runtime.Serialization;

namespace Wordz.BE
{
    [DataContract]
    public class Fm
    {
        #region Consts

        public const string IdDbPropertyName = "id";
        public const string ImageUrlDbPropertyName = "image_url";
        public const string UrlDbPropertyName = "url";
        public const string NameDbPropertyName = "name";
        public const string DescriptionDbPropertyName = "description";
        public const string IsEditableDbPropertyName = "is_editable";
        public const string AccountIdDbPropertyName = "account_id";
        public const string UseMediaPlayerDbPropertyName = "use_media_player";

        #endregion

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string ImageUrl { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool UseMediaPlayer { get; set; }

        [DataMember]
        public int? AccountId { get; set; }

        [DataMember]
        public bool IsEditable { get; set; }

        public Fm() { }

        public Fm(int id, string imageUrl, string url, string name, string description, bool useMediaPlayer, int? authorId, bool isEditable)
        {
            Id = id;
            ImageUrl = imageUrl;
            Url = url;
            Name = name;
            Description = description;
            UseMediaPlayer = useMediaPlayer;
            AccountId = authorId;
            IsEditable = isEditable;
        }
    }
}