using Wordz.Lng;

namespace Wordz.LngOpt {
    public enum Language {
        Russian = 1,
        English,
        Spanish,
        German,
        French,
        Italian,
        Portuguese,
        Danish,
        Dutch,
        Finnish,
        Icelandic,
        Indonesian,
        Norwegian,
        Swedish,
        Afrikaans,
        Basque,
        Catalan,
        Faroese,
        Malay,
        Swahili,
        Chinese,
        Japanese,
    }

    public class LanguageOptions {
        public LanguageOptions() {}

        //---------- Indifferent language options ---------
        public static string GetWordsPath(int languageId) {
            switch (languageId) {
                case (int) Language.Russian:
                    return "ru\\";
                case (int) Language.English:
                    return "en\\";
                case (int) Language.Spanish:
                    return "es\\";
                case (int) Language.German:
                    return "de\\";
                case (int) Language.French:
                    return "fr\\";
                case (int) Language.Italian:
                    return "it\\";
                case (int) Language.Portuguese:
                    return "pt\\";
                case (int) Language.Danish:
                    return "da\\";
                case (int) Language.Dutch:
                    return "nl\\";
                case (int) Language.Finnish:
                    return "fi\\";
                case (int) Language.Icelandic:
                    return "is\\";
                case (int) Language.Indonesian:
                    return "id\\";
                case (int) Language.Norwegian:
                    return "no\\";
                case (int) Language.Swedish:
                    return "sv\\";
                case (int) Language.Afrikaans:
                    return "af\\";
                case (int) Language.Basque:
                    return "eu\\";
                case (int) Language.Catalan:
                    return "ca\\";
                case (int) Language.Faroese:
                    return "fo\\";
                case (int) Language.Malay:
                    return "ms\\";
                case (int) Language.Swahili:
                    return "sw\\";
                case (int) Language.Chinese:    //simplified Chinese
                    return "zh\\";
                case (int) Language.Japanese:   //kana, not kanji (Chinese signs)
                    return "ja\\";
                default:
                    return "";
            }
        }

        public static string GetWordTextPattern(int languageId) {
            switch (languageId) {
                case (int) Language.Russian:
                    return "([а-яА-Я])+";
                case (int) Language.Chinese:
                    return "([\u4E00-\uFFFF])+";
                case (int) Language.Japanese:
                    return "([\u4E00-\uFFFF\u3040-\u30FF])+";
                default:
                    return "([a-zA-Zà-öø-ÿÀ-ÖØ-ßšœžŠŒŽŸ])+";
            }
        }

        public static string GetWrongSymbolsPattern(int languageId) {
            switch (languageId) {
                case (int) Language.Russian:
                    return "[^а-яА-Я]";
                case (int) Language.Chinese:
                    return "[^\u4E00-\uFFFF]";
                case (int) Language.Japanese:
                    return "[^\u4E00-\uFFFF\u3040-\u30FF]";
                default:
                    return "[^a-zA-Zà-öø-ÿÀ-ÖØ-ßšœžŠŒŽŸ]";
            }
        }

        public static string VerbsPath {
            get { return "verbs\\"; }
        }

        public static string SoundPath {
            get { return "sound\\"; }
        }

        public static bool IsMainSite {
            get {
                return
                    (CurrentLanguage.NativeId == (int) Language.Russian) &&
                    (CurrentLanguage.LearnId == (int) Language.English);
            }
        }

        public static string ResourcePath {
            get { return IsMainSite ? "" : "http://wordz.ru"; }
        }
    }
}