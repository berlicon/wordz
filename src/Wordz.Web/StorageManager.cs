using System.Web;
using Wordz.BE;
using Wordz.BE.Dto;
using Wordz.Lng;

namespace Wordz.Web {
    public class StorageManager {
        public StorageManager() {}

        public static bool UserLogined {
            get { return CurrentAccount != null; }
        }

        public static int CurrentAccountId {
            get { return (UserLogined) ? CurrentAccount.Id : Account.EmptyId; }
        }

        public static Account CurrentAccount {
            get { return HttpContext.Current.Session["CurrentAccount"] as Account; }
            set { HttpContext.Current.Session["CurrentAccount"] = value; }
        }

        public static string SourceText {
            get {
                object text = HttpContext.Current.Session["SourceText"];
                return (text != null) ? text.ToString() : "";
            }
            set { HttpContext.Current.Session["SourceText"] = value; }
        }

        public static string ProcessedText {
            get {
                object text = HttpContext.Current.Session["ProcessedText"];
                return (text != null) ? text.ToString() : "";
            }
            set { HttpContext.Current.Session["ProcessedText"] = value; }
        }

        public static IdNamePair[] ProcessedWordIds {
            get {
                object ids = HttpContext.Current.Session["ProcessedWordIds"];
                return (ids != null) ? (IdNamePair[]) ids : new IdNamePair[0];
            }
            set { HttpContext.Current.Session["ProcessedWordIds"] = value; }
        }

        public static bool LearnFirstWord {
            get {
                object flag = HttpContext.Current.Session["LearnFirstWord"];
                return (flag != null) ? bool.Parse(flag.ToString()) : true;
            }
            set { HttpContext.Current.Session["LearnFirstWord"] = value; }
        }

        public static VerbElement[] ProcessedVerbs {
            get {
                object ids = HttpContext.Current.Session["ProcessedVerbs"];
                return (ids != null) ? (VerbElement[]) ids : new VerbElement[0];
            }
            set { HttpContext.Current.Session["ProcessedVerbs"] = value; }
        }

        public static bool UseSpecialWordSeparator {
            get {
                object flag = HttpContext.Current.Session["UseSpecialWordSeparator"];
                return (flag != null) ? bool.Parse(flag.ToString()) : false;
            }
            set { HttpContext.Current.Session["UseSpecialWordSeparator"] = value; }
        }

        public static bool FilmIconMode {
            get {
                object flag = HttpContext.Current.Session["FilmIconMode"];
                return (flag != null) ? bool.Parse(flag.ToString()) : true;
            }
            set { HttpContext.Current.Session["FilmIconMode"] = value; }
        }

        public static FilmListItemDto[] FoundedFilms {
            get {
                object films = HttpContext.Current.Session["FoundedFilms"];
                return (films != null) ? (FilmListItemDto[])films : null;
            }
            set { HttpContext.Current.Session["FoundedFilms"] = value; }
        }

        public static int LanguageLearnId {
            get {
                object id = HttpContext.Current.Session["LanguageLearnId"];
                return (id != null) ? (int) id : CurrentLanguage.LearnId;
            }
            set { HttpContext.Current.Session["LanguageLearnId"] = value; }
        }

        public static int LanguageNativeId {
            get {
                object id = HttpContext.Current.Session["LanguageNativeId"];
                return (id != null) ? (int) id : CurrentLanguage.NativeId;
            }
            set { HttpContext.Current.Session["LanguageNativeId"] = value; }
        }
    }
}