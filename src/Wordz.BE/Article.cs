namespace Wordz.BE {
    public class Article {
        private int id;
        private string title;
        private string body;
        private string siteUrl;
        private int categoryId;
        private int siteId;

        public int Id {
            get { return id; }
            set { id = value; }
        }

        public string Title {
            get { return title; }
            set { title = value; }
        }

        public string Body {
            get { return body; }
            set { body = value; }
        }

        public int CategoryId {
            get { return categoryId; }
            set { categoryId = value; }
        }

        public int SiteId {
            get { return siteId; }
            set { siteId = value; }
        }

        public string SiteUrl {
            get { return siteUrl; }
            set { siteUrl = value; }
        }

        public Article() {}

        public Article(int id, string title, int categoryId) {
            this.id = id;
            this.title = title;
            this.categoryId = categoryId;
        }

        public Article(string title, string body, string siteUrl) {
            this.title = title;
            this.body = body;
            this.siteUrl = siteUrl;
        }
    }
}