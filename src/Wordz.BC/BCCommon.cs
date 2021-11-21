using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Wordz.BC.Comparers.VerbElement;
using Wordz.BC.Comparers.WordElement;
using Wordz.BE;
using Wordz.BE.Dto;
using Wordz.DB;
using Wordz.DB.Accessors;
using Wordz.Lng;
using Wordz.LngOpt;

namespace Wordz.BC {
    public class BCCommon {
        //private BCCommon() {}//test

        private /*static*/ int currentAccountId = -1;
        private /*static*/ int currentNativeLanguageId = -1;
        private /*static*/ int currentLearnLanguageId = -1;
        private static int[] articeIdsWithCSS = new int[] {39, 40, 41, 42, 43};

        public static string WordsSeparatorsAdvanced = "?!.";
        public static string WordsSeparatorsSimple = "\t " + Environment.NewLine;
        public static int MinPartsPerPhraseologism = 1;
        public static int MaxPartsPerPhraseologism = 3;
        public static string WordsSeparator = " ";
        public static char WordsIdSeparator = 'w';
        public static char WordsIdSeparator2 = 'd';
        public static string DictionaryWordSeparator = "  ";
        public static char Separator = '\f';
        public static char Separator2 = '\t';
        public static string OkText = "ok";
        public static string WrongDictionaryWordPattern = "_\\w*\\.";
        public static int MinLengthSignedWord = 3;
        public static long MaxTextLengthForAnalysis = 2000;
        public static long MaxTextLengthForProcess = 10000;
        public static int MaxWordLengthForChinese = 5; //12 actually but too small words (5 for perfomance reason)
        public static int MaxWordLengthForJapanese = 10; //17 actually

        public static string WrongColorPattern = "color=\"[^\"]*\"";
        public static string RightColorPattern = "color=\"black\"";
        public static string WrongHrefPattern = "href=\"[^\"]*\"";
        public static string RightHrefPattern = "";
        public static string WrongStylePattern = "style=\"[^\"]*\"";
        public static string RightStylePattern = "";
        public static string WrongCompileDeclarationStartPattern = "<!--[if !supportLineBreakNewLine]-->";
        public static string RightCompileDeclarationStartPattern = "";
        public static string WrongCompileDeclarationEndPattern = "<!--[endif]-->";
        public static string RightCompileDeclarationEndPattern = "";

        public static string WrongSymbolsExample = "'“’,?”!;:\".- —()‘][}{*+/\\0123456789";
        public static string WordHTMLPattern = "<span w={0} class=\"{1}\" onclick=\"ChangeState('{0}')\">{2}</span>";
        public static string UnknownWordClass = "unknown";
        public static string KnownWordClass = "known";

        public static string LoadDataParam = "LoadData";
        public static string UseProcessedWordsParam = "UseProcessedWords";
        public static int AnonymousAccountId = -1;

        public static int GetAccountsCount() {
            return DBCommon.GetAccountsCount();
        }

        public static int GetWordsCount(int learnLanguageId, int nativeLanguageId) {
            return DBCommon.GetWordsCount(learnLanguageId, nativeLanguageId);
        }

        public static int GetWordsCountSounded(int nativeLanguageId, int learnLanguageId) {
            return (Database.UseSoundFiles)
                ? DBCommon.GetWordsCountSoundedFromFile(nativeLanguageId, learnLanguageId)
                : DBCommon.GetWordsCountSounded();
        }

        public static void SetSoundPath(string path) {
            DBCommon.SetSoundPath(path);
        }

        public static void SetConnectionString(string connectionString) {
            DBCommon.SetConnectionString(connectionString);
        }

        public static void SetUseSoundFiles(bool useSoundFiles) {
            DBCommon.SetUseSoundFiles(useSoundFiles);
        }

        public static void SetTranscriptRussianWords(bool transcriptRussianWords) {
            DBCommon.SetTranscriptRussianWords(transcriptRussianWords);
        }

        public static string GetVocabularyInfo(int accountId, int learnLanguageId, int nativeLanguageId) {
            int totalWordsCount = DBCommon.GetWordsCount(learnLanguageId, nativeLanguageId);
            int accountWordsCount = DBCommon.GetWordsCountForAccount(accountId, learnLanguageId);
            decimal percent = Math.Round(((decimal) accountWordsCount/totalWordsCount)*100, 3);

            return string.Format(CurrentLanguage.MessageFromFormatString,
                accountWordsCount, percent, totalWordsCount);
        }

        [Obsolete("Use only for UT and WinApp - without AccountId")]
        public static Hashtable ProcessText(string text, int nativeLanguageId, int learnLanguageId) {
            Hashtable words = CreateWordsCollection(text, false, learnLanguageId);
            //without AccountId param
            DBCommon.FillWordsTranslation(ref words, nativeLanguageId, learnLanguageId);
            return words;
        }

        public static Hashtable CreateWordsCollection(string sourceText, bool useSpecialWordSeparator, int learnLanguageId) {
            string text = sourceText;
            if (!useSpecialWordSeparator) {
                text = RemoveWrongSymbols(text, learnLanguageId);
            }
            text = NormalizeText(text);

            Hashtable result = new Hashtable();
            int originalOrder = 0;
            Random random = new Random();

            char separator = (useSpecialWordSeparator) ? Separator2 : WordsSeparator[0];
            string[] words = 
                (learnLanguageId == (int)Language.Chinese
                || learnLanguageId == (int)Language.Japanese)
                ? GetAsianWords(text, false, -1, CurrentLanguage.NativeId, learnLanguageId)
                : text.Split(separator);
            foreach (string word in words) {
                if (result[word] == null) {
                    result[word] = new WordElement(word, "", 1, ++originalOrder, random.Next());
                } else {
                    WordElement wordElement = (WordElement) result[word];
                    wordElement.Frequency++;
                    result[word] = wordElement;
                }
            }

            return result;
        }

        public static string ShowProcessText(Hashtable words, bool learnFirstWord, SortBy sortBy) {
            IdNamePair[] wordIds;
            return ShowProcessText(words, learnFirstWord, sortBy, out wordIds);
        }

        public static string ShowProcessText(Hashtable words, bool learnFirstWord,
            SortBy sortBy, out IdNamePair[] wordIds) {
            wordIds = new IdNamePair[words.Count];
            int i = 0;
            IComparer comparer = new OriginalOrderComparer();
            switch (sortBy) {
                case SortBy.Alphabetically:
                    if (learnFirstWord) {
                        comparer = new AlphabeticallyComparer();
                    } else {
                        comparer = new AlphabeticallyInversiveComparer();
                    }
                    break;
                case SortBy.Frequency:
                    comparer = new FrequencyComparer();
                    break;
                case SortBy.MixedOrder:
                    comparer = new MixedOrderComparer();
                    break;
                case SortBy.OriginalOrder:
                    comparer = new OriginalOrderComparer();
                    break;
                case SortBy.WordsLength:
                    comparer = new WordsLengthComparer();
                    break;
            }

            WordElement[] processedWords = new WordElement[words.Count];
            words.Values.CopyTo(processedWords, 0);
            Array.Sort(processedWords, comparer);

            StringBuilder builder = new StringBuilder();
            foreach (WordElement word in processedWords) {
                wordIds[i++] = new IdNamePair(word.Id, word.Original, word.Translation, word.Sounded);
                if (learnFirstWord) {
                    builder.Append(word.Original + " " + word.Translation + Environment.NewLine);
                } else {
                    builder.Append(word.Translation + " " + word.Original + Environment.NewLine);
                }
            }

            return builder.ToString();
        }

        public static string ShowProcessText(IdNamePair[] wordIds) {
            StringBuilder builder = new StringBuilder();
            foreach (IdNamePair pair in wordIds) {
                builder.Append(pair.Value + Environment.NewLine);
            }

            return builder.ToString();
        }

        public static string RemoveWrongSymbols(string text, int learnLanguageId) {
            return Regex.Replace(text, LanguageOptions.GetWrongSymbolsPattern(learnLanguageId), WordsSeparator);
        }

        public static string NormalizeText(string text) {
            string result = text.Trim(WordsSeparator[0]);
            result = Regex.Replace(result, "(" + WordsSeparator + ")+", WordsSeparator);
            result = result.ToLower();
            return result;
        }

        public static WordsInfo GetWordsInfo(Hashtable words, decimal minFrequency,
            int maxSignedWords, bool notUseWellKnownWords, bool notUseNotKnownWords,
            bool notUseNotSoundedWords) {
            WordsInfo info = new WordsInfo();
            info.ProcessedWords = new Hashtable();

            foreach (DictionaryEntry word in words) {
                WordElement wordElement = (WordElement) word.Value;

                bool wellKnownResult = (!notUseWellKnownWords || !wordElement.WellKnown);
                bool frequencyResult = (wordElement.Frequency >= minFrequency);
                bool notKnownResult = (!notUseNotKnownWords || (wordElement.Translation.Length > 0));
                bool soundedResult = (!notUseNotSoundedWords || wordElement.Sounded);

                bool result = wellKnownResult && frequencyResult && notKnownResult && soundedResult;

                if (result) {
                    wordElement.Translation = GetSignedWords(wordElement.Translation, maxSignedWords);
                    info.ProcessedWords[word.Key] = wordElement;
                    if (info.MaxFrequency < wordElement.Frequency) {
                        info.MaxFrequency = wordElement.Frequency;
                    }
                }
            }
            info.WordsCount = info.ProcessedWords.Count;
            return info;
        }

        public static string GetSignedWords(string words, int maxSignedWords) {
            if (maxSignedWords == 0) {
                return "";
            }

            StringBuilder builder = new StringBuilder();
            int signedWordsCount = 0;
            int max = (maxSignedWords <= 0) ? int.MaxValue : maxSignedWords;

            foreach (string wordPart in words.Split(WordsSeparator.ToCharArray())) {
                if (wordPart.Length > 0) {
                    builder.Append(wordPart + WordsSeparator);
                    signedWordsCount += (wordPart.Length >= MinLengthSignedWord) ? 1 : 0;

                    if (signedWordsCount == max) {
                        break;
                    }
                }
            }
            return builder.ToString().Trim();
        }

        public static void FillWordInfo(ref WordElement wordElement, int accountId, int nativeLanguageId, int learnLanguageId) {
            DBCommon.FillWordInfo(ref wordElement, accountId, nativeLanguageId, learnLanguageId);
        }

        public static void FillWordInfo(ref WordElement wordElement, int nativeLanguageId, int learnLanguageId) {
            DBCommon.FillWordInfo(ref wordElement, nativeLanguageId, learnLanguageId);
        }

        public static WordElement GetWordInfo(int wordId, int nativeLanguageId, int learnLanguageId) {
            return DBCommon.GetWordInfo(wordId, nativeLanguageId, learnLanguageId);
        }

        public static Account GetAccount(string nick, string password) {
            return AccountRelatedAccessor.GetAccount(nick, password);
        }

        public static Account AddAccount(Account account) {
            return AccountRelatedAccessor.AddAccount(account);
        }

        public static bool UpdateAccount(Account account) {
            return AccountRelatedAccessor.UpdateAccount(account);
        }

        public static string GetFileContent(Stream stream, long maxDataLength) {
            long dataLength = 
                (stream.Length < maxDataLength)
                ? stream.Length : maxDataLength;
            byte[] buffer = new byte[dataLength];
            stream.Read(buffer, 0, (int) dataLength);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }

        public static byte[] GetWordSound(int wordId, string wordOriginal, string wordTranslation, bool learnFirstWord, int nativeLanguageId, int learnLanguageId) {
            return GetWordSound(wordId, wordOriginal, wordTranslation, learnFirstWord, SoundingType.Both, nativeLanguageId, learnLanguageId);
        }

        public static byte[] GetWordSound(int wordId, string wordOriginal, string wordTranslation, bool learnFirstWord, SoundingType soundingType, int nativeLanguageId, int learnLanguageId) {
            return (Database.UseSoundFiles)
                ? DBCommon.GetWordSoundFromFile(wordId, wordOriginal, wordTranslation, learnFirstWord, soundingType, nativeLanguageId, learnLanguageId)
                : DBCommon.GetWordSound(wordId, learnFirstWord, soundingType);
        }

        public static byte[] GetVerbSoundFromFile(int verbId, int nativeLanguageId, int learnLanguageId) {
            return DBCommon.GetVerbSoundFromFile(verbId, nativeLanguageId, learnLanguageId);
        }

        public static byte[] GetWordSoundFromText(string word) {
            return DBCommon.GetWordSoundFromText(word);
        }

        public static byte[] GetWordSoundTranslation(int wordId, bool learnFirstWord) {
            return DBCommon.GetWordSoundTranslation(wordId, learnFirstWord);
        }

        public static void CreateFile(byte[] content, string fileName) {
            DBCommon.CreateFile(content, fileName);
        }

        public static IdName[] GetDomains(int nativeLanguageId) {
            return DBCommon.GetDomains(nativeLanguageId);
        }

        public static void AddWordsToAccount(int accountId, string[] wordIds, int learnLanguageId) {
            foreach (string wordId in wordIds) {
                if (wordId.Length > 0) {
                    DBCommon.AddWordToAccount(accountId, int.Parse(wordId), learnLanguageId);
                }
            }
        }

        public static void DeleteWordsFromAccount(int accountId, string[] wordIds, int learnLanguageId) {
            foreach (string wordId in wordIds) {
                if (wordId.Length > 0) {
                    DBCommon.DeleteWordFromAccount(accountId, int.Parse(wordId), learnLanguageId);
                }
            }
        }

        public static IdNamePair[] GetWordIds(int accountId, WordsSelector wordsSelector, int domainId, int wordCount, int wordStartIndex, int nativeLanguageId, int learnLanguageId) {
            switch (wordsSelector) {
                case WordsSelector.ForDomain:
                    return DBCommon.GetWordIdsForDomain(accountId, domainId, wordCount, wordStartIndex, nativeLanguageId, learnLanguageId);
                case WordsSelector.Random:
                    return DBCommon.GetWordIdsRandom(accountId, wordCount, nativeLanguageId, learnLanguageId);
                case WordsSelector.Ordered:
                    return DBCommon.GetWordIdsOrdered(accountId, wordCount, wordStartIndex, nativeLanguageId, learnLanguageId);
                default:
                    return null;
            }
        }

        public static VerbElement[] GetVerbs(int accountId, bool loadPopular, SortColumn sortColumn, bool notUseWellKnownVerbs, int wordCount) {
            VerbElement[] elements = DBCommon.GetVerbs(accountId,
                loadPopular, notUseWellKnownVerbs, wordCount);

            IComparer comparer = new OriginalOrderComparer();
            switch (sortColumn) {
                case SortColumn.Form1:
                    comparer = new Form1Comparer();
                    break;
                case SortColumn.Random:
                    comparer = new RandomComparer();
                    break;
                case SortColumn.Translate:
                    comparer = new TranslateComparer();
                    break;
                case SortColumn.VerbType:
                    comparer = new VerbTypeComparer();
                    break;
            }

            Array.Sort(elements, comparer);

            return elements;
        }

        public static void DeleteAllVerbsInAccount(int accountId) {
            DBCommon.DeleteAllVerbsInAccount(accountId);
        }

        public static void AddVerbsToAccount(int accountId, string[] verbIds) {
            foreach (string verbId in verbIds) {
                if (verbId.Length > 0) {
                    DBCommon.AddVerbToAccount(accountId, int.Parse(verbId));
                }
            }
        }

        public static string GetAccountVerbsInfo(int accountId) {
            return DBCommon.GetAccountVerbsInfo(accountId);
        }

        public static IdNamePair[] GetWordIds(int accountId, int learnLanguageId) {
            return DBCommon.GetWordIdsForAccount(accountId, learnLanguageId);
        }

        public static string GetCheckedWordsHTML(IdNamePair[] pairs) {
            StringBuilder builder = new StringBuilder();
            foreach (IdNamePair idNamePair in pairs) {
                builder.Append(string.Format("<input id=w{0} type=checkbox name=w{0}/><label for=w{0}>{1}</label><br>",
                    idNamePair.Id, idNamePair.Value));
            }
            return builder.ToString();
        }

        public static string GetTestedWordsHTML(IdNamePair[] pairs, bool addCutHeaderInfo, int nativeLanguageId, int learnLanguageId) {
            StringBuilder builder1 = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();

            foreach (IdNamePair idNamePair in pairs) {
                builder1.Append(string.Format("<input id=p{0} type=text name=p{0} size=3 readOnly=true /><input id=t{0} type=text name=t{0} size=15 onkeyup=\"WordCheck('t{0}');\"/> {3} <input id=w{0} type=checkbox name=w{0} /><label id=l{0} for=w{0} title=\"{1}\">{2}</label><br>",
                    idNamePair.Id, idNamePair.Name2, idNamePair.Name, GetFlashPlayerHTML(idNamePair, WordsOrder.Native, nativeLanguageId, learnLanguageId)));
                builder2.Append(string.Format("<input id=r{0} type=text name=r{0} size=3 readOnly=true /><input id=x{0} type=text name=x{0} size=15 onkeyup=\"WordCheck('x{0}');\"/> {3} <input id=d{0} type=checkbox name=d{0} /><label id=a{0} for=d{0} title=\"{1}\">{2}</label><br>",
                    idNamePair.Id, idNamePair.Name, idNamePair.Name2, GetFlashPlayerHTML(idNamePair, WordsOrder.Learn, nativeLanguageId, learnLanguageId)));
            }

            string cutHeaderInfo = 
                (addCutHeaderInfo)
                ? "<b>" + string.Format(CurrentLanguage.MessageCutDataForAnalysisFormatString,
                MaxTextLengthForProcess) + "</b><br><br>" : string.Empty;

            return cutHeaderInfo + builder1.ToString() + Separator
                + cutHeaderInfo + builder2.ToString();
        }

        private static string GetFlashPlayerHTML(IdNamePair word, WordsOrder language, int nativeLanguageId, int learnLanguageId) {
            if (word.Flag) { //sounded
                string wordPath = string.Format("{0}{1}{2}.mp3",
                    LanguageOptions.SoundPath,
                    //TODO: fix bug with incorrect path for en-en and other setellite sites
                    //Database.Instance.SoundPath,
                    (language == WordsOrder.Learn)
                    ? LanguageOptions.GetWordsPath(learnLanguageId)
                    : LanguageOptions.GetWordsPath(nativeLanguageId), word.Id);
                return string.Format(
                    @"<embed width=17 height=17 flashvars='song_url={0}' src=player.swf type='application/x-shockwave-flash'/>", wordPath);
            } else { //not sounded
                return "<img src='" + LanguageOptions.ResourcePath + "/img/minus.jpg' alt='-'/>";
            }
        }

        public static string GetTestedVerbsHTML(VerbElement[] elements, ShowColumn showColumn, int nativeLanguageId) {
            Random random = new Random();
            StringBuilder builder1 = new StringBuilder();
            foreach (VerbElement element in elements) {
                ShowColumn showColumnReal = showColumn;
                if (showColumn == ShowColumn.Random) {
                    showColumnReal = (ShowColumn) random.Next((int) ShowColumn.Form1, (int) ShowColumn.Random);
                }

                string info1, info2, info3, info4;
                if (showColumnReal == ShowColumn.All) {
                    info1 = string.Format("value=\"{0}\" readOnly=true", HttpUtility.HtmlEncode(element.Form1));
                    info2 = string.Format("value=\"{0}\" readOnly=true", HttpUtility.HtmlEncode(element.Form2));
                    info3 = string.Format("value=\"{0}\" readOnly=true", HttpUtility.HtmlEncode(element.Form3));
                    info4 = string.Format("value=\"{0}\" readOnly=true",
                        (nativeLanguageId == (int)Language.Russian)
                        ? HttpUtility.HtmlEncode(element.Translation)
                        : "");
                } else {
                    info1 = (showColumnReal == ShowColumn.Form1)
                        ? string.Format("value=\"{0}\" readOnly=true", HttpUtility.HtmlEncode(element.Form1))
                        : string.Format("v=\"{0}\"", HttpUtility.HtmlEncode(element.Form1));
                    info2 = (showColumnReal == ShowColumn.Form2)
                        ? string.Format("value=\"{0}\" readOnly=true", HttpUtility.HtmlEncode(element.Form2))
                        : string.Format("v=\"{0}\"", HttpUtility.HtmlEncode(element.Form2));
                    info3 = (showColumnReal == ShowColumn.Form3)
                        ? string.Format("value=\"{0}\" readOnly=true", HttpUtility.HtmlEncode(element.Form3))
                        : string.Format("v=\"{0}\"", HttpUtility.HtmlEncode(element.Form3));
                    info4 = (showColumnReal == ShowColumn.Translation)
                        ? string.Format("value=\"{0}\" readOnly=true", HttpUtility.HtmlEncode(element.Translation))
                        : string.Format("v=\"{0}\"",
                        (nativeLanguageId == (int)Language.Russian)
                        ? HttpUtility.HtmlEncode(element.Translation) : "") +
                        ((nativeLanguageId != (int)Language.Russian)
                        ? " readOnly=true" : "");
                }

                builder1.Append(string.Format(
                    "<input id=w{0} type=checkbox name=w{0} />" +
                    "<input id=a{0} type=text size=8 onkeyup=\"VerbCheck('a{0}');\" {1} />" +
                    "<input id=b{0} type=text size=8 onkeyup=\"VerbCheck('b{0}');\" {2} />" +
                    "<input id=c{0} type=text size=8 onkeyup=\"VerbCheck('c{0}');\" {3} />" +
                    "<input id=d{0} type=text size=29 onkeyup=\"VerbCheck('d{0}');\" {4} /><br>",
                    element.Id, info1, info2, info3, info4));
            }
            return builder1.ToString();
        }

        public static string GetTestedVerbsTEXT(VerbElement[] elements, SortColumn sortColumn) {
            IdName[] verbTypes = new IdName[] {};

            if (sortColumn == SortColumn.VerbType) {
                verbTypes = BCCommon.GetVerbTypes();
            }

            int verbType = int.MinValue;
            StringBuilder builder1 = new StringBuilder();
            foreach (VerbElement element in elements) {
                if (sortColumn == SortColumn.VerbType && verbType != element.VerbType) {
                    builder1.Append(Environment.NewLine);
                    builder1.Append(verbTypes[element.VerbType - 1].Name);
                    builder1.Append(Environment.NewLine);
                    verbType = element.VerbType;
                }
                builder1.Append(string.Format("{0} {1} {2} {3}",
                    element.Form1, element.Form2, element.Form3, element.Translation));
                builder1.Append(Environment.NewLine);
            }
            return builder1.ToString().Trim();
        }

        public static void AddWordToDictionary(string dictionaryLine, bool createNewWord, int nativeLanguageId, int learnLanguageId) {
            string wordLine = dictionaryLine.Trim();
            if (wordLine.Length > 0) {
                int separatorIndex = wordLine.IndexOf(DictionaryWordSeparator);

                if (separatorIndex >= 0) {
                    string original = wordLine.Substring(0, separatorIndex);
                    string translation = wordLine.Substring(separatorIndex + DictionaryWordSeparator.Length);
                    translation = Regex.Replace(translation, WrongDictionaryWordPattern, "");

                    WordElement wordElement = new WordElement(original.Trim(), translation.Trim());

                    if (createNewWord) {
                        DBCommon.FillWordInfo(ref wordElement, nativeLanguageId, learnLanguageId);
                        if (!wordElement.ExistsInDB) {
                            DBCommon.AddWordToDictionary(wordElement, nativeLanguageId, learnLanguageId);
                        }
                    } else {
                        if (original.Trim().Length > 29) {
                            wordElement.Id = DBCommon.GetWordIdByText(
                                original.Trim().Substring(0, 29), learnLanguageId);
                            if (wordElement.Id > 18631) {
                                DBCommon.UpdateWordInDictionary(wordElement, nativeLanguageId, learnLanguageId);
                            }
                        }
                    }
                }
            }
        }

        public static void AddWordToDictionaryUseExist(string wordsPair, int nativeLanguageId, int learnLanguageId) {
            string original = wordsPair.Substring(
                0, Constants.EnglishWordLengthInDictionary).Replace("\0", "").Trim();
            string translation = wordsPair.Substring(
                Constants.EnglishWordLengthInDictionary).Replace("\0", "").Trim();
            Debug.Assert(original.Length > 0 && translation.Length > 0);
            
            original = GetSimplifiedString(original);
            translation = GetSimplifiedString(translation);
            WordElement wordElement = new WordElement(original, translation);
            DBCommon.AddWordToDictionaryUseExist(wordElement, nativeLanguageId, learnLanguageId);
        }

        public static void AddWordToDictionaryUseExist(string original, string translation, int nativeLanguageId, int learnLanguageId) {
            original = GetSimplifiedString(original);
            translation = GetSimplifiedString(translation);
            WordElement wordElement = new WordElement(original, translation);
            DBCommon.AddWordToDictionaryUseExist(wordElement, nativeLanguageId, learnLanguageId);
        }

        public static string GetSimplifiedString(string text) {
            text = Regex.Replace(text, "( )*\\{[^\\}]*\\}", "");
            text = Regex.Replace(text, "( )*\\([^\\)]*\\)", "");
            text = Regex.Replace(text, "( )*\\[[^\\]]*\\]", "");
            return text.Trim();
        }

        public static IdName[] GetSites() {
            return DBCommon.GetSites();
        }

        public static IdName[] GetCategories(int nativeLanguageId) {
            return DBCommon.GetCategories(nativeLanguageId);
        }

        public static Article[] GetArticles(int nativeLanguageId, int learnLanguageId) {
            return DBCommon.GetArticles(nativeLanguageId, learnLanguageId);
        }

        public static Article GetArticle(int id) {
            return DBCommon.GetArticle(id);
        }

        public static string GetArticlesListHTML(int nativeLanguageId, int learnLanguageId) {
            IdName[] categories = GetCategories(nativeLanguageId);
            Article[] articles = GetArticles(nativeLanguageId, learnLanguageId);

            StringBuilder builder = new StringBuilder();
            Hashtable articlesList = new Hashtable();

            foreach (IdName category in categories) {
                articlesList[category.Id] = new ArrayList();
            }

            foreach (Article article in articles) {
                ArrayList list = (ArrayList) articlesList[article.CategoryId];
                list.Add(article);
                articlesList[article.CategoryId] = list;
            }

            foreach (IdName category in categories) {
                builder.Append(string.Format("<h3>{0}</h3>", category.Name));
                foreach (Article article in (ArrayList) articlesList[category.Id]) {
                    builder.Append(string.Format("<a href=\"{0}\">{1}</a><br>",
                        "/Articles/" + article.Id, article.Title));
                }
            }

            return builder.ToString();
        }

        public static string GetArticleHTML(int id, out string title) {
            StringBuilder builder = new StringBuilder();
            Article article = GetArticle(id);

            bool useCssStyles = false;
            for (int i = 0; i < articeIdsWithCSS.Length; i++) {
                if (articeIdsWithCSS[i] == id) {
                    useCssStyles = true;
                    break;
                }
            }

            title = article.Title;
            builder.Append(string.Format("<h3>{0}</h3>", article.Title));
            builder.Append(string.Format("{0}", NormalizeHTML(article.Body, useCssStyles)));
            builder.Append(string.Format("<br><br><i>{0}:&nbsp;<a href=\"{1}\">{1}</a></i>",
                CurrentLanguage.MessageSource, article.SiteUrl));
            builder.Append(string.Format("<br><a href=\"{0}\">{1}</a>",
                "/Articles", CurrentLanguage.MessageBackToArticles));

            return builder.ToString();
        }

        public static string NormalizeHTML(string html, bool useCssStyles) {
            string result = html;
            if (!useCssStyles) {
                result = Regex.Replace(result, WrongStylePattern, RightStylePattern, RegexOptions.IgnoreCase);
                result = Regex.Replace(result, WrongColorPattern, RightColorPattern, RegexOptions.IgnoreCase);
            }
            result = Regex.Replace(result, WrongHrefPattern, RightHrefPattern, RegexOptions.IgnoreCase);
            result = result.Replace(WrongCompileDeclarationStartPattern, RightCompileDeclarationStartPattern).
                Replace(WrongCompileDeclarationEndPattern, RightCompileDeclarationEndPattern);
            return result;
        }

        public static void UpdateMixedOrderForSynonyms(ref Hashtable words) {
            Hashtable wordsCopy = (Hashtable) words.Clone();
            Hashtable translations = new Hashtable();

            foreach (object key in wordsCopy.Keys) {
                WordElement wordElement = (WordElement) wordsCopy[key];

                if (translations[wordElement.Translation] == null) {
                    translations.Add(wordElement.Translation, wordElement.MixedOrder);
                } else {
                    wordElement.MixedOrder = (int) translations[wordElement.Translation];
                    words[key] = wordElement;
                }
            }

            wordsCopy.Clear();
            translations.Clear();
        }

        public /*static*/ string GetTextAsHTMLForAnalysis(string text, int accountId, int nativeLanguageId, int learnLanguageId, bool addCutHeaderInfo) {
            currentAccountId = accountId;
            currentNativeLanguageId = nativeLanguageId;
            currentLearnLanguageId = learnLanguageId;
            string cutHeaderInfo = 
                (addCutHeaderInfo)
                ? "<b>" + string.Format(CurrentLanguage.MessageCutDataForAnalysisFormatString,
                MaxTextLengthForAnalysis) + "</b><br><br>" : string.Empty;
            string easyProcessedText = Regex.Replace(text, LanguageOptions.GetWordTextPattern(learnLanguageId), new MatchEvaluator(GetWordAsHTML));
            string phraseologismProcessedText = GetWordHTMLForPhraseologisms(text, easyProcessedText);
            return cutHeaderInfo + easyProcessedText + phraseologismProcessedText;
        }

        private /*static*/ bool CheckWordForms(ref WordElement wordElement,
            string endingIncorrect, string endingCorrect, bool processDoubleLetters) {
            string original = wordElement.Original;

            if (wordElement.Original.EndsWith(endingIncorrect)) {
                int addedCut = (processDoubleLetters &&
                    (wordElement.Original.Length > endingIncorrect.Length)) ? 1 : 0;
                string addedChar = (processDoubleLetters)
                    ? wordElement.Original.Substring(wordElement.Original.Length
                    - endingIncorrect.Length - addedCut, 1) : "";

                wordElement.Original = wordElement.Original.Substring(0,
                    wordElement.Original.Length - endingIncorrect.Length - addedCut) + endingCorrect;
                DBCommon.FillWordInfo(ref wordElement, currentAccountId, currentNativeLanguageId, currentLearnLanguageId);

                if (wordElement.ExistsInDB) {
                    wordElement.Translation = "[" + addedChar + endingIncorrect + "]";
                    return true;
                } else {
                    wordElement.Original = original;
                }
            }
            return false;
        }

        public /*static*/ string GetWordAsHTML(Match match) {
            if (currentLearnLanguageId == (int)Language.Chinese
                || currentLearnLanguageId == (int)Language.Japanese) {
                return GetAsianWords(match.ToString(), true, currentAccountId, currentNativeLanguageId, currentLearnLanguageId)[0];
            } else {
                WordElement wordElement = new WordElement(match.ToString());
                DBCommon.FillWordInfo(ref wordElement, currentAccountId, currentNativeLanguageId, currentLearnLanguageId);
                wordElement.Translation = "";

                if (!wordElement.ExistsInDB && (
                    CheckWordForms(ref wordElement, "s", "", false) ||
                    CheckWordForms(ref wordElement, "es", "", false) ||
                    CheckWordForms(ref wordElement, "ies", "y", false) ||
                    CheckWordForms(ref wordElement, "ing", "", false) ||
                    CheckWordForms(ref wordElement, "ing", "e", false) ||
                    CheckWordForms(ref wordElement, "d", "", false) ||
                    CheckWordForms(ref wordElement, "ed", "", false) ||
                    CheckWordForms(ref wordElement, "ied", "y", false) ||
                    CheckWordForms(ref wordElement, "ing", "", true) ||
                    CheckWordForms(ref wordElement, "ed", "", true)
                    )
                    ) {
                    //nothing
                }

                return (wordElement.ExistsInDB) ?
                    GetWordHTML(wordElement.Original, wordElement.Id, wordElement.WellKnown)
                    + wordElement.Translation
                    : wordElement.Original;                
            }
        }

        public static string[] GetAsianWords(string text, bool getHtmlContent, int accountId, int nativeLanguageId, int learnLanguageId) {
            ArrayList list = new ArrayList();

            StringBuilder builder = new StringBuilder();
            int maxWordLength = (learnLanguageId == (int)Language.Chinese)
                ? MaxWordLengthForChinese : MaxWordLengthForJapanese;

            for (int i = 0; i < text.Length; /***/) {
                for (int j = maxWordLength; j > 0; j--) {
                    string word = 
                        (i + j <= text.Length)
                        ? text.Substring(i, j)
                        : text.Substring(i);

                    WordElement wordElement = new WordElement(word);
                    DBCommon.FillWordInfo(ref wordElement, accountId, nativeLanguageId, learnLanguageId);

                    if (wordElement.ExistsInDB) {
                        builder.Append(GetWordHTML(
                            wordElement.Original, wordElement.Id,
                            wordElement.WellKnown));
                        list.Add(wordElement.Original);
                        i += wordElement.Original.Length;
                        goto end;
                    }
                }

                builder.Append(text[i++]);

                end: i+=0;
            }

            return (getHtmlContent)
                ? new string[] {builder.ToString()}
                : (string[]) list.ToArray(typeof (string));
        }

        public static string GetWordHTML(string processedWord, int id, bool isWellKnown) {
            string wordClass = (isWellKnown) ? KnownWordClass : UnknownWordClass;
            return string.Format(BCCommon.WordHTMLPattern, id, wordClass, processedWord);
        }

        private /*static*/ string GetWordHTMLForPhraseologisms(string text, string easyProcessedText) {
            Debug.Assert(WordsSeparatorsAdvanced.Length == 3);
            StringBuilder builder = new StringBuilder();
            string[] rawWords = text.Split(WordsSeparatorsSimple.ToCharArray());
            string[] words = GetPureList(new ArrayList(rawWords));

            string content;
            for (int partsCount = MinPartsPerPhraseologism;
                partsCount <= MaxPartsPerPhraseologism; partsCount++) {
                for (int start = 0; start <= (words.Length - partsCount); start++) {
                    string[] processedWords = new string[partsCount];
                    for (int i = 0; i < partsCount; i++) {
                        processedWords[i] = words[start + i];
                    }

                    content = GetWordHTMLForPhraseologism(processedWords);
                    if (easyProcessedText.IndexOf(content) < 0) {
                        builder.Append(WordsSeparator + content);
                    }
                    for (int i = 0; i < processedWords.Length; i++) {
                        if (processedWords[i].EndsWith(WordsSeparatorsAdvanced[0].ToString())
                            || processedWords[i].EndsWith(WordsSeparatorsAdvanced[1].ToString())
                            || processedWords[i].EndsWith(WordsSeparatorsAdvanced[2].ToString())
                            ) {
                            string copy = processedWords[i];
                            processedWords[i] = processedWords[i].Substring(0, processedWords[i].Length - 1);
                            content = GetWordHTMLForPhraseologism(processedWords);
                            if (easyProcessedText.IndexOf(content) < 0) {
                                builder.Append(WordsSeparator + content);
                            }
                            processedWords[i] = copy;
                        }
                    }
                }
            }

            return "<hr>" + builder.ToString();
        }

        private /*static*/ string GetWordHTMLForPhraseologism(string[] words) {
            string phraseologism = ConcatWords(words);

            WordElement wordElement = new WordElement(phraseologism);
            DBCommon.FillWordInfo(ref wordElement, currentAccountId, currentNativeLanguageId, currentLearnLanguageId);
            if (wordElement.ExistsInDB) {
                return GetWordHTML(wordElement.Original,
                    wordElement.Id, wordElement.WellKnown);
            }

            return "";
        }

        public static string ConcatWords(string[] words) {
            StringBuilder builder = new StringBuilder();

            foreach (string word in words) {
                builder.Append(word + WordsSeparator);
            }

            return builder.ToString().Trim();
        }

        public static IdName[] GetVerbTypes() {
            return DBCommon.GetVerbTypes();
        }

        public static int GetWordIdByOriginal(string value, int learnLanguageId) {
            return DBCommon.GetWordIdByText(value, learnLanguageId);
        }

        public static int GetWordIdByTranslation(string value, int nativeLanguageId) {
            return DBCommon.GetWordIdByText(value, nativeLanguageId);
        }

        public static string[] GetPureList(ArrayList list) {
            ArrayList pureList = new ArrayList();
            foreach (string word in list) {
                if (word.Length > 0) {
                    pureList.Add(word);
                }
            }

            return (string[]) pureList.ToArray(typeof (string));
        }

        public static string[] GetVerbFormWords(VerbElement element) {
            ArrayList list = new ArrayList();
            list.AddRange(element.Form1.Split(WrongSymbolsExample.ToCharArray()));
            list.AddRange(element.Form2.Split(WrongSymbolsExample.ToCharArray()));
            list.AddRange(element.Form3.Split(WrongSymbolsExample.ToCharArray()));
            return GetPureList(list);
        }

        public static string[] GetVerbTranslationWords(VerbElement element) {
            ArrayList list = new ArrayList();
            list.AddRange(element.Translation.Split(WrongSymbolsExample.ToCharArray()));
            return GetPureList(list);
        }

        public static IdNamePair[] GetTOPUsersByVocabulary(int learnLanguageId) {
            return GetHtmlEncodedArray(DBCommon.GetTOPUsersByVocabulary(learnLanguageId));
        }

        public static IdNamePair[] GetTOPUsersByVerbs() {
            return GetHtmlEncodedArray(DBCommon.GetTOPUsersByVerbs());
        }

        public static IdNamePair[] GetTOPUsersByDictionaryQuiz(int learnLanguageId) {
            return GetHtmlEncodedArray(DBCommon.GetTOPUsersByDictionaryQuiz(learnLanguageId));
        }

        public static IdNamePair[] GetTOPUsersByWordsOrderQuiz(int learnLanguageId) {
            return GetHtmlEncodedArray(DBCommon.GetTOPUsersByWordsOrderQuiz(learnLanguageId));
        }

        private static IdNamePair[] GetHtmlEncodedArray(IdNamePair[] pairs) {
            IdNamePair[] pairsEncoded = new IdNamePair[pairs.Length];

            for (int i = 0; i < pairs.Length; i++) {
                IdNamePair pair = pairs[i];
                pair.Name = HttpUtility.HtmlEncode(pair.Name);
                pairsEncoded[i] = pair;

            }
            return pairsEncoded;
        }

        public static string GetDictionaryQuizHTML(int step, int nativeLanguageId, int learnLanguageId) {
            IdNamePair[] namePairs = GetWordIds(0, WordsSelector.Random, 0,
                Constants.DictionaryQuizWordsPerTestCount, 0, nativeLanguageId, learnLanguageId);
            StringBuilder builder = new StringBuilder();

            Random random = new Random();
            int wordIndex = random.Next(0, namePairs.Length - 1);

            builder.Append(string.Format(CurrentLanguage.MessageQuestionFormatString,
                step, Constants.DictionaryQuizTestsCount));
            builder.Append(CurrentLanguage.MessageAskTranslation);
            builder.Append("<b>" + namePairs[wordIndex].Name + "</b><br><br>");
            builder.Append("<div align=left>");

            for (int i = 0; i < namePairs.Length; i++) {
                IdNamePair namePair = namePairs[i];
                builder.Append(string.Format(
                    "<input id={0} type=radio correct={1} name=group value={0} checked={2}/><label for={0}>{3}</label><br>",
                    "rb" + i.ToString(),
                    (i == wordIndex) ? 1 : 0,
                    i == 0,
                    namePair.Name2
                    ));
            }

            builder.Append("</div>");
            return builder.ToString();
        }

        public static string GetDictionaryQuizResultHTML(int accountId, string nick, int successCount, int learnLanguageId) {
            int place = (successCount > 0)
                ? DBCommon.GetDictionaryQuizPlace(accountId, nick, successCount, learnLanguageId)
                : Constants.TOPUsersCount + 1;
            bool success = place <= Constants.TOPUsersCount;

            StringBuilder builder = new StringBuilder();
            if (success) {
                builder.Append(string.Format(CurrentLanguage.DictionaryQuizSuccessFormatString, place));
            } else {
                builder.Append(CurrentLanguage.DictionaryQuizFailFormatString);
            }
            builder.Append("<br><br><br>");
            builder.Append(string.Format("<a href=\"/Statistics#DictionaryQuiz\" class=\"button\">{0}</a><br><br>",
                CurrentLanguage.MessageBackToRecords));
            builder.Append(string.Format("<a href=\"/DictionaryQuiz\" title=\"{0}\" class=\"button\">{1}</a><br>",
                CurrentLanguage.MessageTestAgain, CurrentLanguage.MessageReplyTest));
            return builder.ToString();
        }

        public static string GetWordsOrderQuizHTML(int step, int learnLanguageId) {
            StringBuilder builder = new StringBuilder();

            builder.Append(string.Format(CurrentLanguage.MessageQuestionFormatString,
                step, Constants.WordsOrderQuizTestsCount));
            builder.Append(CurrentLanguage.MessageOrderWords + ":<br><br>");

            string sentence = GetWordsOrderSentence(learnLanguageId).ToUpper();
            builder.Append(string.Format(
                "<div class=\"{0}\" id=\"{1}\" text=\"{2}\">",
                "mainDivWordsOrderQuiz", Constants.MainDivId, sentence));

            string[] words = sentence.Split(' ', '?', '.');
            Array.Sort(words);

            for (int i = 0; i < words.Length; i++) {
                string word = words[i];
                if (word.Length > 0) {
                    builder.Append(string.Format(
                        "<span id={0}><a href=\"#\" onclick=\"SwapNode(true, '{0}', '{1}');return false;\"> <</a>{2}<a href=\"#\" onclick=\"SwapNode(false, '{0}', '{1}');return false;\">> </a></span>",
                        "w" + i.ToString(), Constants.MainDivId, word));
                }
            }

            builder.Append(sentence.Substring(sentence.Length - 1));
            builder.Append("</div>");
            return builder.ToString();
        }

        public static string GetWordsOrderQuizResultHTML(int accountId, string nick, int successCount, int learnLanguageId) {
            int place = (successCount > 0)
                ? DBCommon.GetWordsOrderQuizPlace(accountId, nick, successCount, learnLanguageId)
                : Constants.TOPUsersCount + 1;
            bool success = place <= Constants.TOPUsersCount;

            StringBuilder builder = new StringBuilder();
            if (success) {
                builder.Append(string.Format(CurrentLanguage.WordsOrderQuizSuccessFormatString, place));
            } else {
                builder.Append(CurrentLanguage.WordsOrderQuizFailFormatString);
            }
            builder.Append("<br><br>");
            builder.Append(string.Format("<a href=\"/Statistics#WordsOrderQuiz\" class=\"button\">{0}</a><br><br>",
                CurrentLanguage.MessageBackToRecords));
            builder.Append(string.Format("<a href=\"/WordsOrderQuiz\" title=\"{0}\" class=\"button\">{1}</a><br><br>",
                CurrentLanguage.MessageTestAgain, CurrentLanguage.MessageReplyTest));
            builder.Append(CurrentLanguage.MessageErrors);
            return builder.ToString();
        }

        public static string GetWordsOrderSentence(int learnLanguageId) {
            return DBCommon.GetWordsOrderSentence(learnLanguageId);
        }

		public static IdNamePair[] GetTOPUsersByLevelQuiz(int learnLanguageId) {
			return DBCommon.GetTOPUsersByLevelQuiz(learnLanguageId);
		}
		public static int GetLevelQuizPlace(int accountId, string nick, int successCount, int learnLanguageId) {
			return DBCommon.GetLevelQuizPlace(accountId, nick, successCount, learnLanguageId);
		}
		public static Level GetLevelSentence(int learnLanguageId) {
			return DBCommon.GetLevelSentence(learnLanguageId);
		}
		
		public static string GetLevelQuizHTML(int step, int learnLanguageId) {
			StringBuilder builder = new StringBuilder();

			builder.Append(string.Format(CurrentLanguage.MessageQuestionFormatString,
				step, Constants.LevelQuizTestsCount));

			Level level = GetLevelSentence(learnLanguageId);
			builder.Append(string.Format(
				"<br><div class=\"{0}\" id=\"{1}\">{2}</div><br>",
				"mainDivWordsOrderQuiz", Constants.MainDivId, level.Sentence));
			builder.Append("<div align=left>");

			builder.Append(string.Format(
				"<input id={0} type=radio correct={1} name=group value={0}/><label for={0}>{2}</label><br>",
				"rb1", (1 == level.Correct) ? 1 : 0, level.Answer1));
			builder.Append(string.Format(
				"<input id={0} type=radio correct={1} name=group value={0}/><label for={0}>{2}</label><br>",
				"rb2", (2 == level.Correct) ? 1 : 0, level.Answer2));
			builder.Append(string.Format(
				"<input id={0} type=radio correct={1} name=group value={0}/><label for={0}>{2}</label><br>",
				"rb3", (3 == level.Correct) ? 1 : 0, level.Answer3));			

			builder.Append("</div>");
			return builder.ToString();
		}

		public static string GetLevelQuizResultHTML(int accountId, string nick, int successCount, int learnLanguageId) {
			int place = (successCount > 0)
				? DBCommon.GetLevelQuizPlace(accountId, nick, successCount, learnLanguageId)
				: Constants.TOPUsersCount + 1;
			bool success = place <= Constants.TOPUsersCount;

			StringBuilder builder = new StringBuilder();
			builder.Append(string.Format(CurrentLanguage.LevelQuizFormatString, successCount,
				(successCount <=5) ? CurrentLanguage.MessageLevel1 : 
				(successCount <=10) ? CurrentLanguage.MessageLevel2 : 
				(successCount <=15) ? CurrentLanguage.MessageLevel3 : 
				(successCount <=20) ? CurrentLanguage.MessageLevel4 : CurrentLanguage.MessageLevel5));
			if (success) {
				builder.Append(string.Format(CurrentLanguage.LevelQuizSuccessFormatString, place));
			} else {
				builder.Append(CurrentLanguage.LevelQuizFailFormatString);
			}
			builder.Append("<br><br>");
			builder.Append(string.Format("<a href=\"/Statistics#LevelQuiz\" class=\"button\">{0}</a><br><br>",
				CurrentLanguage.MessageBackToRecords));
			builder.Append(string.Format("<a href=\"/LevelQuiz\" title=\"{0}\" class=\"button\">{1}</a><br><br>",
				CurrentLanguage.MessageTestAgain, CurrentLanguage.MessageReplyTest));
			builder.Append(CurrentLanguage.MessageErrors);
			return builder.ToString();
		}

		public static int[] GetWordsUnsoundedShort(int nativeLanguageId, int learnLanguageId) {
            return DBCommon.GetWordsUnsoundedShort(nativeLanguageId, learnLanguageId);
        }

        public static void FilmClearAll() {
            DBCommon.FilmClearAll();
        }

        public static void AddFilmsWithParts(Film[] films, int nativeLanguageId, int learnLanguageId) {
            foreach (Film film in films) {
                DBCommon.AddFilm(film, nativeLanguageId, learnLanguageId);
                int partNumber = 2;
                foreach (string partUrl in film.PartUrls) {
                    DBCommon.AddFilmPart(film.Id, partNumber++, partUrl);
                }
            }
        }

        public static IdName[] GetFilmCategories(int nativeLanguageId) {
            return DBCommon.GetFilmCategories(nativeLanguageId);
        }

        public static FilmListItemDto[] GetFilmsByCategory(int accountId, int categoryId, int nativeLanguageId, int learnLanguageId) {
            return DBCommon.GetFilmsByCategory(accountId, categoryId, nativeLanguageId, learnLanguageId);
        }

        public static FilmListItemDto[] GetFilmsBySearch(int accountId, string search, int nativeLanguageId, int learnLanguageId)
        {
            return DBCommon.GetFilmsBySearch(accountId, search, nativeLanguageId, learnLanguageId);
        }

        public static FilmWithCategoryAndOrderDto GetFilm(int id) {
            return DBCommon.GetFilm(id, CurrentLanguage.NativeId/* Костыль на время*/);
        }

        public static FilmWithCategoryAndOrderDto GetFilm(int id, int nativeLanguageId)
        {
            return DBCommon.GetFilm(id, nativeLanguageId);
        }

        public static int CheckAndUpdatePasswordForCource(int accountId, int courseId, string password)
        {
            return CoursesRelatedAccessor.CheckAndUpdatePasswordForCource(accountId, courseId, password);
        }

        public static string GetFilmsListHTML(FilmListItemDto[] films, bool filmIconMode)
        {
            StringBuilder builder = new StringBuilder("&nbsp;&nbsp;");

            if (films.Length == 0) {
                builder.Append(CurrentLanguage.MessageNoFilms);
                return builder.ToString();
            }

            bool showOneCategory = true;
            string category = films[0].CategoryName;
            foreach (FilmListItemDto film in films)
            {
                if (film.CategoryName != category) {
                    category = CurrentLanguage.MessageDifferent;
                    showOneCategory = false;
                    break;
                }
            }
            builder.Append(string.Format("<span class='smalltext'>{0}: <b>{1}</b> {2}: <b>{3}</b></span>",
                CurrentLanguage.MessageCategory, category,
                CurrentLanguage.MessageFilms, films.Length));

            if (filmIconMode) {
                builder.Append("<table width=\"100%\" height=\"100%\" class=\"nobordertable\" cellspacing=\"10px\">");

                const int filmsPerRow = 5;
                for (int i = 0; i < films.Length; i++) {
                    bool newRow = ((i%filmsPerRow) == 0);
                    if (newRow) {
                        builder.Append((i == 0) ? "<tr>" : "</tr><tr>");
                    }

                    builder.Append("<td align=\"center\" valign=\"top\">");
                    builder.Append(Film.GetFilmImageLinkHtml(
                        films[i].Id,
                        films[i].Name + ((showOneCategory) ? "" : " (" + films[i].CategoryName + ")"),
                        films[i].ImageUrl,
                        films[i].AccountId)
                        );
                    builder.Append("</td>");
                }
                builder.Append("</tr>");

                builder.Append("</table>");
            } else {
                builder.Append("<div style='margin-left: 10px;'><ol>");
                foreach (FilmListItemDto film in films)
                {
                    builder.Append(string.Format("<li>{0}&nbsp;{1}</li>",
                        Film.GetFilmTextLinkHtml(film.Id, film.Name, "smalltext"),
                        (showOneCategory) ? "" : "(" + film.CategoryName + ")"));
                }
                builder.Append("</ol></div>");
            }

            return builder.ToString();
        }

        public static IdName[] GetLanguages() {
            return DBCommon.GetLanguages();
        }

        public static int GetLanguageIndex(IdName[] languages, int languageId) {
            for (int i = 0; i < languages.Length; i++) {
                if (languageId == languages[i].Id) {
                    return i;
                }
            }
            return -1;
        }

        public static Fm[] GetOtherFmChannels(int accountId, int nativeLanguageId, int learnLanguageId)
        {
            return DBCommon.GetOtherFmChannels(accountId, nativeLanguageId, learnLanguageId);
        }

        public static TvWithOrderDto[] GetTvChannels(int accountId, int nativeLanguageId, int learnLanguageId) {
            return DBCommon.GetTvChannels(accountId, nativeLanguageId, learnLanguageId);
        }
        public static Tv[] GetOtherTvChannels(int accountId, int nativeLanguageId, int learnLanguageId)
        {
            return DBCommon.GetOtherTvChannels(accountId, nativeLanguageId, learnLanguageId);
        }

        public static Tv GetTvChannel(int id, int nativeLanguageId) {
            return DBCommon.GetTvChannel(id, nativeLanguageId);
        }
        public static FmWithOrderDto[] GetFmChannels(int accountId, int nativeLanguageId, int learnLanguageId)
        {
            return DBCommon.GetFmChannels(accountId, nativeLanguageId, learnLanguageId);
        }
        public static Fm GetFmChannel(int id, int nativeLanguageId) {
            return DBCommon.GetFmChannel(id, nativeLanguageId);
        }

        public static IdNamePair[] GetUsedLanguages() {
            IdName[] languages = GetLanguages();
            ArrayList list = new ArrayList();

            int i = 1;
            foreach (IdName language in languages) {
                IdNamePair pair = new IdNamePair(i++, language.Name);
                pair.Name2 = GetWordsCount(language.Id, language.Id).ToString();
                pair.Name3 = GetWordsCountSounded(language.Id, language.Id).ToString();
                list.Add(pair);
            }
            return (IdNamePair[]) list.ToArray(typeof (IdNamePair));
        }

		public static void UpdateFilmAvailability(int filmId, bool status)
		{
			DBCommon.UpdateFilmAvailability(filmId, status);
		}

        public static IEnumerable<CourseDetailsDto> GetCourseList(int accountId, bool b)
        {
            return CoursesRelatedAccessor.GetCourses(accountId, b);
        }

        public static AccountMoneyInfoDto[] GetAccountMoneyInfos(int accountId)
        {
            return AccountRelatedAccessor.GetAccountMoneyInfos(accountId);
        }

        public static int AddPaymentForModule(int accountId, int moduleId, int currencyId, DateTime paymentDate, decimal paymentValue)
        {
            return PaymentsRelatedAccessor.AddPaymentForModule(accountId, moduleId, currencyId, paymentDate, paymentValue);
        }

        public static int AddPaymentForCourse(int accountId, int courseId, int currencyId, DateTime paymentDate, decimal paymentValue)
        {
            return PaymentsRelatedAccessor.AddPaymentForCourse(accountId, courseId, currencyId, paymentDate, paymentValue);
        }

        public static ExerciseText GetExerciseText(int exerciseId)
        {
            return DBCommon.GetExerciseText(exerciseId);
        }

        public static int AddUserComment(UserComment userComment, int? parentCommentId)
        {
            return UserCommentRelatedAccessor.AddUserComment(userComment, parentCommentId);
        }

        public static IList<UserCommentWithExtraDto> GetUserComments(int accountId, Guid targetElement, int pageSize, int pageNumber)
        {
            return UserCommentRelatedAccessor.GetUserComments(accountId, targetElement, pageSize, pageNumber);
        }

        public static int GetUserCommentsCount(int accountId, Guid targetElement)
        {
            return UserCommentRelatedAccessor.GetUserCommentsCount(accountId, targetElement);
        }

        public static int RateUserComment(int accountId, int userCommentId, bool isPositive)
        {
            return UserCommentRelatedAccessor.RateUserComment(accountId, userCommentId, isPositive);
        }
        
        public static int ClaimUserComment(int accountId, int userCommentId)
        {
            return UserCommentRelatedAccessor.ClaimUserComment(accountId, userCommentId);
        }

        public static FilmWithCategoryAndOrderDto[] GetFilms(int accountId, int nativaLanguageId, int learnLanguageId)
        {
            return FilmRelatedAccessor.GetFilmChannels(accountId, nativaLanguageId, learnLanguageId);
        }

        public static FilmWithCategoryAndOrderDto[] GetOtherFilms(int accountId, int nativeLanguageId, int learnLanguageId)
        {
            return FilmRelatedAccessor.GetOtherFilmChannels(accountId, nativeLanguageId, learnLanguageId);
        }
    }
}