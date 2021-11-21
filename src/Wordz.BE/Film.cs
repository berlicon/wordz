using System.Collections;
using System.Runtime.Serialization;
using Wordz.LngOpt;

namespace Wordz.BE
{
    [DataContract]
    public class Film
    {
        #region Consts

        public const string IdDbPropertyName = "id";
        public const string ImageUrlDbPropertyName = "image_url";
        public const string UrlDbPropertyName = "url";
        public const string NameDbPropertyName = "name";
        public const string DescriptionDbPropertyName = "description";
        public const string IsEditableDbPropertyName = "is_editable";
        public const string AccountIdDbPropertyName = "account_id";
        public const string CategoryDbPropertyName = "category_id";
        public const string PlayerCodeDbPropertyName = "player_code";

        #endregion

        private ArrayList partUrls = new ArrayList();

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Description { get; set; }
        
        [DataMember]
        public string ImageUrl { get; set; }
        
        [DataMember]
        public string PlayerPattern { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public int? CategoryId { get; set; }

        [DataMember]
        public int? AccountId { get; set; }

        [DataMember]
        public bool IsEditable { get; set; }

        [DataMember]
        public string PlayerCode { get; set; }

        public ArrayList PartUrls
        {
            get { return partUrls; }
            private set { partUrls = value; }
        }

        public void AddPartUrl(string partUrl)
        {
            partUrls.Add(partUrl);
        }

        public Film() { }

        public static string GetFilmTextLinkHtml(int id, string name, string className)
        {
            string paramName = name.Replace(" ", "-");
            return string.Format("<a href='{0}' class='{1}'>{2}</a>",
                "/OnlineFilm/" + id + "/" + paramName, className, name);
        }

        public static string GetFilmImageLinkHtml(int id, string caption, string imageUrl, int? accountId)
        {
            string paramName = caption.Replace(" ", "-");
            
            if (accountId == null)
            {
                return string.Format("<a href='{0}'><img src='{1}/img/Films/{2}.gif' alt='{3}' title='{3}'></a>",
                    "/OnlineFilm/" + id + "/" + paramName, LanguageOptions.ResourcePath,
                    id, caption) + "<br>" + GetFilmTextLinkHtml(id, caption, "tinytext");    
            }
            else
            {
                return string.Format("<a href='{0}'><img src='{1}' alt='{2}' title='{2}'></a>",
                                     "/OnlineFilm/" + id + "/" + paramName, imageUrl,
                                     caption) + "<br>" + GetFilmTextLinkHtml(id, caption, "tinytext");
            }
        }

        public string GetFilmTitleHtml(int currentPart)
        {
            string paramName = Name.Replace(" ", "-");
            string result = Name;

            if (PartUrls.Count > 0)
            {
                result += ":&nbsp;";
            }
            else
            {
                return result;
            }

            result += (1 == currentPart)
                ? "<b>" + 1.ToString() + "</b>"
                : string.Format("<a href='{0}' class='smalltext'>{1}</a>",
                "/OnlineFilm/" + Id + "/" + paramName, 1);

            for (int i = 0; i < PartUrls.Count; i++)
            {
                result += "&nbsp;";
                result += (i + 2 == currentPart)
                    ? "<b>" + (i + 2).ToString() + "</b>"
                    : string.Format("<a href='{0}' class='smalltext'>{1}</a>",
                    "/OnlineFilm/" + Id + "/" + paramName + "/" + (i + 2), (i + 2));
            }

            return result;
        }
    }
}