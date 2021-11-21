using System;
using System.Collections;
using System.Web;
using NUnit.Framework;
using Wordz.BC;
using Wordz.BC.Comparers.WordElement;
using Wordz.BE;
using Wordz.DB;
using Wordz.LngOpt;
using Wordz.Web;

namespace Wordz.UT {
    [TestFixture]
    public class BCCommon_es_en_Test : DbTransactionalFixture {
        public BCCommon_es_en_Test() {
            word = "abandon";
            word2 = "agent";
            wordTranslation = "desamparar, abandona, dejar, desmantelar, expedir";
            word2Translation = "reactivo, agente, represantante, encargado, facto";
        }

        private sealed class TestComparer : IComparer {
            public int Compare(object x, object y) {
                return -string.Compare((string) x, (string) y);
            }
        }

        [Test]
        public void SortedListTest() {
            Hashtable list = new Hashtable();
            list["a"] = 2;
            list["b"] = 1;

            SortedList list2 = new SortedList(list, new TestComparer());
            Assert.AreEqual("b", list2.GetKey(0));
            Assert.AreEqual("a", list2.GetKey(1));
        }

        [Test]
        public void RemoveWrongSymbols() {
            string text =
                BCCommon.WrongSymbolsExample + "helper" +
                BCCommon.WrongSymbolsExample + "weapon" +
                BCCommon.WrongSymbolsExample;
            string processedText = BCCommon.RemoveWrongSymbols(text, (int) Language.English);

            int indexhelper = processedText.IndexOf("helper");
            int indexweapon = processedText.IndexOf("weapon");
            Assert.IsTrue(indexhelper >= 0);
            Assert.IsTrue(indexweapon >= 0);
            Assert.IsTrue(indexhelper < indexweapon);
        }

        [Test]
        public void NormalizeText() {
            string text = "   helper   weapon   ";
            string processedText = BCCommon.NormalizeText(text);
            Assert.AreEqual("helper weapon", processedText);
        }

        [Test]
        public void CreateWordsCollection() {
            string text = "helper weapon helper weapon helper vend";
            Hashtable words = BCCommon.CreateWordsCollection(text, false, (int) Language.English);
            Assert.AreEqual(3, words.Count);

            WordElement helperElement = (WordElement) words["helper"];
            WordElement weaponElement = (WordElement) words["weapon"];
            WordElement vendElement = (WordElement) words["vend"];

            Assert.AreEqual("helper", helperElement.Original);
            Assert.AreEqual("", helperElement.Translation);
            Assert.AreEqual(3, helperElement.Frequency);
            Assert.AreEqual(1, helperElement.OriginalOrder);
            Assert.IsTrue(helperElement.MixedOrder > 0);

            Assert.AreEqual("weapon", weaponElement.Original);
            Assert.AreEqual("", weaponElement.Translation);
            Assert.AreEqual(2, weaponElement.Frequency);
            Assert.AreEqual(2, weaponElement.OriginalOrder);
            Assert.IsTrue(weaponElement.MixedOrder > 0);

            Assert.AreEqual("vend", vendElement.Original);
            Assert.AreEqual("", vendElement.Translation);
            Assert.AreEqual(1, vendElement.Frequency);
            Assert.AreEqual(3, vendElement.OriginalOrder);
            Assert.IsTrue(vendElement.MixedOrder > 0);
        }

        [Test]
        public void CreateWordsCollectionUseSpecialWordSeparator() {
            string text = "really?" + BCCommon.Separator2 + "you! don't say!";
            Hashtable words = BCCommon.CreateWordsCollection(text, true, (int) Language.English);
            Assert.AreEqual(2, words.Count);
        }

        [Test]
        public void ProcessText() {
            string text = "helper weapon helper weapon helper vend";
            Hashtable words = BCCommon.ProcessText(text, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(3, words.Count);
        }

        [Test]
        public void ShowProcessTextLearnFirstWord() {
            string text = "helper weapon helper weapon helper vend";
            Hashtable words = BCCommon.ProcessText(text, (int) Language.Spanish, (int) Language.English);
            string processedText = BCCommon.ShowProcessText(words, true, SortBy.Alphabetically);
            string expectedText =
                "helper ayudante, ayudador, auxiliar" + Environment.NewLine +
                "vend vender" + Environment.NewLine +
                "weapon arma" + Environment.NewLine;
            Assert.AreEqual(expectedText, processedText);
        }

        [Test]
        public void ShowProcessTextNativeFirstWord() {
            string text = "helper weapon helper weapon helper vend";
            Hashtable words = BCCommon.ProcessText(text, (int) Language.Spanish, (int) Language.English);
            string processedText = BCCommon.ShowProcessText(words, false, SortBy.Alphabetically);
            string expectedText =
                "arma weapon" + Environment.NewLine + 
                "ayudante, ayudador, auxiliar helper" + Environment.NewLine +
                "vender vend" + Environment.NewLine;
            Assert.AreEqual(expectedText, processedText);
        }

        [Test]
        public void ShowProcessTextForWordIds() {
            IdNamePair[] pairs = new IdNamePair[] {
                                                      new IdNamePair(1, "a", "b"),
                                                      new IdNamePair(2, "c", "d"),
            };
            string processedText = BCCommon.ShowProcessText(pairs);
            string expectedText =
                "a b" + Environment.NewLine +
                "c d" + Environment.NewLine;
            Assert.AreEqual(expectedText, processedText);
        }

        [Test]
        public void GetWordsInfoCheckMinFrequency() {
            string text = "helper weapon helper weapon helper vend";
            Hashtable words = BCCommon.ProcessText(text, (int) Language.Spanish, (int) Language.English);

            WordsInfo info;

            info = BCCommon.GetWordsInfo(words, 0, int.MaxValue, false, false, false);
            Assert.AreEqual(3, info.MaxFrequency);
            Assert.AreEqual(3, info.WordsCount);
            Assert.AreEqual(3, info.ProcessedWords.Count);
            Assert.IsNotNull(info.ProcessedWords["helper"]);
            Assert.IsNotNull(info.ProcessedWords["weapon"]);
            Assert.IsNotNull(info.ProcessedWords["vend"]);

            info = BCCommon.GetWordsInfo(words, 1, int.MaxValue, false, false, false);
            Assert.AreEqual(3, info.MaxFrequency);
            Assert.AreEqual(3, info.WordsCount);
            Assert.AreEqual(3, info.ProcessedWords.Count);
            Assert.IsNotNull(info.ProcessedWords["helper"]);
            Assert.IsNotNull(info.ProcessedWords["weapon"]);
            Assert.IsNotNull(info.ProcessedWords["vend"]);

            info = BCCommon.GetWordsInfo(words, 2, int.MaxValue, false, false, false);
            Assert.AreEqual(3, info.MaxFrequency);
            Assert.AreEqual(2, info.WordsCount);
            Assert.AreEqual(2, info.ProcessedWords.Count);
            Assert.IsNotNull(info.ProcessedWords["helper"]);
            Assert.IsNotNull(info.ProcessedWords["weapon"]);

            info = BCCommon.GetWordsInfo(words, 3, int.MaxValue, false, false, false);
            Assert.AreEqual(3, info.MaxFrequency);
            Assert.AreEqual(1, info.WordsCount);
            Assert.AreEqual(1, info.ProcessedWords.Count);
            Assert.IsNotNull(info.ProcessedWords["helper"]);

            info = BCCommon.GetWordsInfo(words, 4, int.MaxValue, false, false, false);
            Assert.AreEqual(0, info.MaxFrequency);
            Assert.AreEqual(0, info.WordsCount);
            Assert.AreEqual(0, info.ProcessedWords.Count);
        }

        [Test]
        public void GetWordsInfoCheckMaxSignedWords() {
            Hashtable words = new Hashtable();
            words["w1"] = new WordElement("w1", "1> улучшение 2> мелиорация", 1, 1, 1);
            words["w2"] = new WordElement("w2", "дом", 1, 1, 1);
            words["w3"] = new WordElement("w3", "а> патронный ящик б> коробка для патронной ленты в> ниша для боеприпасов (в окопе и т. п.)", 1, 1, 1);
            words["w4"] = new WordElement("w4", "_I of amplifier усилитель _II of ampoule ампула _III of ampere ампер", 1, 1, 1);

            WordsInfo info;

            info = BCCommon.GetWordsInfo(words, 0, int.MaxValue, false, false, false);
            Assert.AreEqual("1> улучшение 2> мелиорация", ((WordElement) info.ProcessedWords["w1"]).Translation);
            Assert.AreEqual("дом", ((WordElement) info.ProcessedWords["w2"]).Translation);
            Assert.AreEqual("а> патронный ящик б> коробка для патронной ленты в> ниша для боеприпасов (в окопе и т. п.)", ((WordElement) info.ProcessedWords["w3"]).Translation);
            Assert.AreEqual("_I of amplifier усилитель _II of ampoule ампула _III of ampere ампер", ((WordElement) info.ProcessedWords["w4"]).Translation);

            info = BCCommon.GetWordsInfo(words, 0, 0, false, false, false);
            Assert.AreEqual("", ((WordElement) info.ProcessedWords["w1"]).Translation);
            Assert.AreEqual("", ((WordElement) info.ProcessedWords["w2"]).Translation);
            Assert.AreEqual("", ((WordElement) info.ProcessedWords["w3"]).Translation);
            Assert.AreEqual("", ((WordElement) info.ProcessedWords["w4"]).Translation);

            info = BCCommon.GetWordsInfo(words, 0, 1, false, false, false);
            Assert.AreEqual("1> улучшение", ((WordElement) info.ProcessedWords["w1"]).Translation);
            Assert.AreEqual("дом", ((WordElement) info.ProcessedWords["w2"]).Translation);
            Assert.AreEqual("а> патронный", ((WordElement) info.ProcessedWords["w3"]).Translation);
            Assert.AreEqual("_I of amplifier", ((WordElement) info.ProcessedWords["w4"]).Translation);

            info = BCCommon.GetWordsInfo(words, 0, 2, false, false, false);
            Assert.AreEqual("1> улучшение 2> мелиорация", ((WordElement) info.ProcessedWords["w1"]).Translation);
            Assert.AreEqual("дом", ((WordElement) info.ProcessedWords["w2"]).Translation);
            Assert.AreEqual("а> патронный ящик", ((WordElement) info.ProcessedWords["w3"]).Translation);
            Assert.AreEqual("_I of amplifier усилитель", ((WordElement) info.ProcessedWords["w4"]).Translation);

            info = BCCommon.GetWordsInfo(words, 0, 3, false, false, false);
            Assert.AreEqual("1> улучшение 2> мелиорация", ((WordElement) info.ProcessedWords["w1"]).Translation);
            Assert.AreEqual("дом", ((WordElement) info.ProcessedWords["w2"]).Translation);
            Assert.AreEqual("а> патронный ящик б> коробка", ((WordElement) info.ProcessedWords["w3"]).Translation);
            Assert.AreEqual("_I of amplifier усилитель _II", ((WordElement) info.ProcessedWords["w4"]).Translation);
        }

        [Test]
        public void GetWordsInfoCheckNotUseWellKnownWords() {
            Hashtable words = new Hashtable();

            WordElement element1 = new WordElement("helper", "", 1, 0, 0);
            element1.WellKnown = true;
            words["helper"] = element1;

            WordElement element2 = new WordElement("weapon", "", 1, 0, 0);
            element2.WellKnown = false;
            words["weapon"] = element2;

            WordsInfo info;

            info = BCCommon.GetWordsInfo(words, 0, int.MaxValue, false, false, false);
            Assert.AreEqual(2, info.WordsCount);
            Assert.IsNotNull(info.ProcessedWords["helper"]);
            Assert.IsNotNull(info.ProcessedWords["weapon"]);

            info = BCCommon.GetWordsInfo(words, 0, int.MaxValue, true, false, false);
            Assert.AreEqual(1, info.WordsCount);
            Assert.IsNotNull(info.ProcessedWords["weapon"]);
        }

        [Test]
        public void GetWordsInfoCheckNotUseNotSoundedWords() {
            Hashtable words = new Hashtable();

            WordElement element1 = new WordElement("helper", "", 1, 0, 0);
            element1.Sounded = true;
            words["helper"] = element1;

            WordElement element2 = new WordElement("weapon", "", 1, 0, 0);
            element2.Sounded = false;
            words["weapon"] = element2;

            WordsInfo info;

            info = BCCommon.GetWordsInfo(words, 0, int.MaxValue, false, false, false);
            Assert.AreEqual(2, info.WordsCount);
            Assert.IsNotNull(info.ProcessedWords["helper"]);
            Assert.IsNotNull(info.ProcessedWords["weapon"]);

            info = BCCommon.GetWordsInfo(words, 0, int.MaxValue, false, false, true);
            Assert.AreEqual(1, info.WordsCount);
            Assert.IsNotNull(info.ProcessedWords["helper"]);
        }

        [Test]
        public void GetWordsInfoCheckNotUseNotKnownWords() {
            Hashtable words = new Hashtable();

            WordElement element1 = new WordElement("helper", "", 1, 0, 0);
            words["helper"] = element1;

            WordElement element2 = new WordElement("weapon", "weapon", 1, 0, 0);
            words["weapon"] = element2;

            WordsInfo info;

            info = BCCommon.GetWordsInfo(words, 0, int.MaxValue, false, false, false);
            Assert.AreEqual(2, info.WordsCount);
            Assert.IsNotNull(info.ProcessedWords["helper"]);
            Assert.IsNotNull(info.ProcessedWords["weapon"]);

            info = BCCommon.GetWordsInfo(words, 0, int.MaxValue, false, true, false);
            Assert.AreEqual(1, info.WordsCount);
            Assert.IsNull(info.ProcessedWords["helper"]);
            Assert.IsNotNull(info.ProcessedWords["weapon"]);
        }

        [Test]
        public void AlphabeticallyComparerTest() {
            WordElement[] list = new WordElement[] {
                                                       new WordElement("b", "", 0, 0, 0),
                                                       new WordElement("a", "", 0, 0, 0),
                                                       new WordElement("c", "", 0, 0, 0)
                                                   };

            Array.Sort(list, new AlphabeticallyComparer());
            Assert.AreEqual("a", list[0].Original);
            Assert.AreEqual("b", list[1].Original);
            Assert.AreEqual("c", list[2].Original);
        }

        [Test]
        public void AlphabeticallyInversiveComparerTest() {
            WordElement[] list = new WordElement[] {
                                                       new WordElement("", "b", 0, 0, 0),
                                                       new WordElement("", "a", 0, 0, 0),
                                                       new WordElement("", "c", 0, 0, 0)
                                                   };

            Array.Sort(list, new AlphabeticallyInversiveComparer());
            Assert.AreEqual("a", list[0].Translation);
            Assert.AreEqual("b", list[1].Translation);
            Assert.AreEqual("c", list[2].Translation);
        }

        [Test]
        public void FrequencyComparerTest() {
            WordElement[] list = new WordElement[] {
                                                       new WordElement("", "", 2, 0, 0),
                                                       new WordElement("", "", 1, 0, 0),
                                                       new WordElement("", "", 3, 0, 0)
                                                   };

            Array.Sort(list, new FrequencyComparer());
            Assert.AreEqual(3, list[0].Frequency);
            Assert.AreEqual(2, list[1].Frequency);
            Assert.AreEqual(1, list[2].Frequency);
        }

        [Test]
        public void MixedOrderComparerTest() {
            WordElement[] list = new WordElement[] {
                                                       new WordElement("", "", 0, 0, 2),
                                                       new WordElement("", "", 0, 0, 1),
                                                       new WordElement("", "", 0, 0, 3)
                                                   };

            Array.Sort(list, new MixedOrderComparer());
            Assert.AreEqual(1, list[0].MixedOrder);
            Assert.AreEqual(2, list[1].MixedOrder);
            Assert.AreEqual(3, list[2].MixedOrder);
        }

        [Test]
        public void OriginalOrderComparerTest() {
            WordElement[] list = new WordElement[] {
                                                       new WordElement("", "", 0, 2, 0),
                                                       new WordElement("", "", 0, 1, 0),
                                                       new WordElement("", "", 0, 3, 0)
                                                   };

            Array.Sort(list, new OriginalOrderComparer());
            Assert.AreEqual(1, list[0].OriginalOrder);
            Assert.AreEqual(2, list[1].OriginalOrder);
            Assert.AreEqual(3, list[2].OriginalOrder);
        }

        [Test]
        public void WordsLengthComparerTest() {
            WordElement[] list = new WordElement[] {
                                                       new WordElement("cc", "", 0, 0, 0),
                                                       new WordElement("b", "", 0, 0, 0),
                                                       new WordElement("aaa", "", 0, 0, 0)
                                                   };

            Array.Sort(list, new WordsLengthComparer());
            Assert.AreEqual("b", list[0].Original);
            Assert.AreEqual("cc", list[1].Original);
            Assert.AreEqual("aaa", list[2].Original);
        }

        [Test]
        public void AddGetAccountTest() {
            string nick = Guid.NewGuid().ToString().Substring(0, 20);
            string nick2 = Guid.NewGuid().ToString().Substring(0, 20);
            string email = Guid.NewGuid().ToString().Substring(0, 20);
            string password = Guid.NewGuid().ToString().Substring(0, 20);

            Account account;
            Account account2;

            account = BCCommon.GetAccount(nick, password);
            Assert.IsTrue(account.IsEmpty);

            account = new Account(0, nick, email, password);

            account2 = BCCommon.AddAccount(account);
            Assert.IsTrue(account2.Id > 0);

            account = BCCommon.GetAccount(nick, password);
            Assert.AreEqual(account2.Id, account.Id);
            Assert.AreEqual(nick, account.Nick);
            Assert.AreEqual(email, account.Email);
            Assert.AreEqual(password, account.Password);

            account2 = BCCommon.AddAccount(new Account(0, nick2, "", ""));
            Assert.IsTrue(account2.Id > 0);

            account = BCCommon.GetAccount(nick2, "");
            Assert.AreEqual(account2.Id, account.Id);
            Assert.AreEqual(nick2, account.Nick);
            Assert.AreEqual("", account.Email);
            Assert.AreEqual("", account.Password);

            account2 = BCCommon.AddAccount(new Account(0, nick2, email, password));
            Assert.AreEqual(0, account2.Id);
            account2 = BCCommon.AddAccount(new Account(0, nick, email, password));
            Assert.AreEqual(0, account2.Id);
        }

        [Test]
        public void UpdateAccountTest() {
            string nick = Guid.NewGuid().ToString().Substring(0, 20);
            string nick2 = Guid.NewGuid().ToString().Substring(0, 20);
            string nick22 = Guid.NewGuid().ToString().Substring(0, 20);
            string email = Guid.NewGuid().ToString().Substring(0, 20);
            string email2 = Guid.NewGuid().ToString().Substring(0, 20);
            string password = Guid.NewGuid().ToString().Substring(0, 20);
            string password2 = Guid.NewGuid().ToString().Substring(0, 20);

            Account account = new Account(0, nick, email, password);
            account = BCCommon.AddAccount(account);
            Account account2 = new Account(0, nick2, "", "");
            account2 = BCCommon.AddAccount(account2);

            account2.Nick = nick;
            Assert.IsFalse(BCCommon.UpdateAccount(account2));

            account2.Nick = nick22;
            Assert.IsTrue(BCCommon.UpdateAccount(account2));

            //HACK: Email doesn't unique (problem with some emails
            //with email=NULL - constraint appears...
            account2.Email = email;
            Assert.IsTrue(BCCommon.UpdateAccount(account2));

            account2.Email = email2;
            Assert.IsTrue(BCCommon.UpdateAccount(account2));

            account2.Password = password2;
            Assert.IsTrue(BCCommon.UpdateAccount(account2));

            account = BCCommon.GetAccount(account2.Nick, account2.Password);
            Assert.AreEqual(nick22, account2.Nick);
            Assert.AreEqual(email2, account2.Email);
            Assert.AreEqual(password2, account2.Password);
        }

        [Test]
        public void UpdateAccountUnicodeSymbolsTest() {
            string nick = "iatsAISTQW-1";
            string email = "iatsAISTQW-2";
            string password = "iatsAISTQW-3";

            Account account = new Account(0, nick, email, password);
            BCCommon.AddAccount(account);

            account = BCCommon.GetAccount(nick, password);
            Assert.AreEqual(nick, account.Nick);
            Assert.AreEqual(email, account.Email);
            Assert.AreEqual(password, account.Password);

            account.Nick += "!";
            account.Email += "!";
            account.Password += "!";
            BCCommon.UpdateAccount(account);

            account = BCCommon.GetAccount(nick + "!", password + "!");
            Assert.AreEqual(nick + "!", account.Nick);
            Assert.AreEqual(email + "!", account.Email);
            Assert.AreEqual(password + "!", account.Password);
        }

        [Test]
        public void ShowProcessTextCheckWordIds() {
            string text = "helper weapon helper weapon helper vend";
            Hashtable words = BCCommon.ProcessText(text, (int) Language.Spanish, (int) Language.English);

            IdNamePair[] wordIds;
            BCCommon.ShowProcessText(words, true, SortBy.Alphabetically, out wordIds);

            Assert.AreEqual(words.Count, wordIds.Length);
            Assert.AreEqual(7435, wordIds[0].Id);
            Assert.AreEqual("helper", wordIds[0].Name);
            Assert.AreEqual("ayudante, ayudador, auxiliar", wordIds[0].Name2);
            Assert.AreEqual(66048, wordIds[1].Id);
            Assert.AreEqual("vend", wordIds[1].Name);
            Assert.AreEqual("vender", wordIds[1].Name2);
            Assert.AreEqual(18183, wordIds[2].Id);
            Assert.AreEqual("weapon", wordIds[2].Name);
            Assert.AreEqual("arma", wordIds[2].Name2);
        }

        [Test]
        public void GetCheckedWordsHTML() {
            IdNamePair[] pairs = new IdNamePair[2];
            pairs[0] = new IdNamePair(1, "a", "b");
            pairs[1] = new IdNamePair(2, "c", "d");

            string result = BCCommon.GetCheckedWordsHTML(pairs);
            string expectedResult =
                "<input id=w1 type=checkbox name=w1/><label for=w1>a b</label><br>" +
                "<input id=w2 type=checkbox name=w2/><label for=w2>c d</label><br>";
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetTestedWordsHTML() {
            IdNamePair[] pairs = new IdNamePair[2];
            pairs[0] = new IdNamePair(1, "a", "b", true);
            pairs[1] = new IdNamePair(2, "c", "d", false);

            string result = BCCommon.GetTestedWordsHTML(pairs, false, (int) Language.Spanish, (int) Language.English);
            string expectedResult =
                "<input id=p1 type=text name=p1 size=3 readOnly=true /><input id=t1 type=text name=t1 size=15 onkeyup=\"WordCheck('t1');\"/>"
                + @" <embed width=17 height=17 flashvars='song_url=sound\es\1.mp3' src=player.swf type='application/x-shockwave-flash'/> "
                + "<input id=w1 type=checkbox name=w1 /><label id=l1 for=w1 title=\"b\">a</label><br>"
                
                + "<input id=p2 type=text name=p2 size=3 readOnly=true /><input id=t2 type=text name=t2 size=15 onkeyup=\"WordCheck('t2');\"/>"
                + @" <img src='http://wordz.ru/img/minus.jpg' alt='-'/> "
                + "<input id=w2 type=checkbox name=w2 /><label id=l2 for=w2 title=\"d\">c</label><br>"
                
                + BCCommon.Separator
                
                + "<input id=r1 type=text name=r1 size=3 readOnly=true /><input id=x1 type=text name=x1 size=15 onkeyup=\"WordCheck('x1');\"/>"
                + @" <embed width=17 height=17 flashvars='song_url=sound\en\1.mp3' src=player.swf type='application/x-shockwave-flash'/> "
                + "<input id=d1 type=checkbox name=d1 /><label id=a1 for=d1 title=\"a\">b</label><br>"
                
                + "<input id=r2 type=text name=r2 size=3 readOnly=true /><input id=x2 type=text name=x2 size=15 onkeyup=\"WordCheck('x2');\"/>"
                + @" <img src='http://wordz.ru/img/minus.jpg' alt='-'/> "
                + "<input id=d2 type=checkbox name=d2 /><label id=a2 for=d2 title=\"c\">d</label><br>";
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void AddWordToAccount() {
            CreateNewAccount();

            string[] ids = new string[2] {
                                             wordId.ToString(), wordId2.ToString()
                                         };

            WordElement wordElement = new WordElement();
            wordElement.Original = word;
            BCCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.IsFalse(wordElement.WellKnown);

            WordElement wordElement2 = new WordElement();
            wordElement2.Original = word2;
            BCCommon.FillWordInfo(ref wordElement2, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.IsFalse(wordElement2.WellKnown);

            BCCommon.AddWordsToAccount(accountId, ids, (int) Language.English);

            BCCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.IsTrue(wordElement.WellKnown);

            BCCommon.FillWordInfo(ref wordElement2, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.IsTrue(wordElement2.WellKnown);
        }

        [Test]
        public void GetWordIds() {
            IdNamePair[] pairs = new IdNamePair[2];
            pairs[0] = new IdNamePair(1, "a", "b");
            pairs[1] = new IdNamePair(2, "c", "d");

            string result = BCCommon.GetCheckedWordsHTML(pairs);
            string expectedResult =
                "<input id=w1 type=checkbox name=w1/><label for=w1>a b</label><br>" +
                "<input id=w2 type=checkbox name=w2/><label for=w2>c d</label><br>";
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void GetVocabularyInfo() {
            string info;
            CreateNewAccount();

            int wordsCount = BCCommon.GetWordsCount((int) Language.English, (int) Language.Spanish);
            info = BCCommon.GetVocabularyInfo(accountId, (int) Language.English, (int) Language.Spanish);
            Assert.AreEqual("0 (0% de " + wordsCount + ")", info);

            WordElement wordElement = new WordElement();
            wordElement.Original = word;
            BCCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            BCCommon.AddWordsToAccount(accountId, new string[] {wordElement.Id.ToString()}, (int) Language.English);

            info = BCCommon.GetVocabularyInfo(accountId, (int) Language.English, (int) Language.Spanish);
            decimal percent = Math.Round((decimal) 100/wordsCount, 3);
            Assert.AreEqual("1 (" + percent + "% de " + wordsCount + ")", info);
        }

        [Test]
        public void AddWordToDictionarySuccess() {
            string addedWordOriginal = Guid.NewGuid().ToString();
            string addedWordTranslation = "йралкдфелцупдылетбичь";
            string removedPiece = "_adj.";

            string dictionaryLine = string.Format("{0}{1}{2} {3} {4}",
                addedWordOriginal, BCCommon.DictionaryWordSeparator,
                removedPiece, addedWordTranslation, removedPiece);

            WordElement wordElement = new WordElement();
            wordElement.Original = addedWordOriginal;
            wordElement.Translation = addedWordTranslation;

            BCCommon.FillWordInfo(ref wordElement, (int) Language.Spanish, (int) Language.English);
            Assert.IsFalse(wordElement.ExistsInDB);

            BCCommon.AddWordToDictionary(dictionaryLine, true, (int) Language.Spanish, (int) Language.English);

            BCCommon.FillWordInfo(ref wordElement, (int) Language.Spanish, (int) Language.English);
            Assert.IsTrue(wordElement.ExistsInDB);
            Assert.AreEqual(addedWordOriginal, wordElement.Original);
            Assert.AreEqual(addedWordTranslation, wordElement.Translation);
        }

        [Test]
        public void AddWordToDictionaryFailed() {
            string addedWordOriginal = word;
            string addedWordTranslation = wordTranslation;
            string removedPiece = "_adj.";

            string dictionaryLine = string.Format("{0}{1}{2} {3} {4}",
                addedWordOriginal, BCCommon.DictionaryWordSeparator,
                removedPiece, addedWordTranslation, removedPiece);

            WordElement wordElement = new WordElement();
            wordElement.Original = addedWordOriginal;
            wordElement.Translation = addedWordTranslation;

            BCCommon.FillWordInfo(ref wordElement, (int) Language.Spanish, (int) Language.English);
            Assert.IsTrue(wordElement.ExistsInDB);

            BCCommon.AddWordToDictionary(dictionaryLine, true, (int) Language.Spanish, (int) Language.English);

            BCCommon.FillWordInfo(ref wordElement, (int) Language.Spanish, (int) Language.English);
            Assert.IsTrue(wordElement.ExistsInDB);
        }

        [Test]
        public void NormalizeHTML() {
            string ASIS = "<span style=\"color: rgb(107, 125, 163); font-family: Times New Roman;\"><a hREf=\"http://www.5ENGlish.ru/\"><font size=\"6\" cOLOr=\"#FF0000\">Как???</font></a></span><!--[if !supportLineBreakNewLine]--><br/>&nbsp;<!--[endif]-->";
            string TOBE = "<span ><a ><font size=\"6\" color=\"black\">Как???</font></a></span><br/>&nbsp;";
            Assert.AreEqual(TOBE, BCCommon.NormalizeHTML(ASIS, false));
        }

        [Test]
        public void NormalizeHTMLUseCssStyles() {
            string ASIS = "<span style=\"color: rgb(107, 125, 163); font-family: Times New Roman;\"><a hREf=\"http://www.5ENGlish.ru/\"><font size=\"6\" cOLOr=\"#FF0000\">Как???</font></a></span><!--[if !supportLineBreakNewLine]--><br/>&nbsp;<!--[endif]-->";
            string TOBE = "<span style=\"color: rgb(107, 125, 163); font-family: Times New Roman;\"><a ><font size=\"6\" cOLOr=\"#FF0000\">Как???</font></a></span><br/>&nbsp;";
            Assert.AreEqual(TOBE, BCCommon.NormalizeHTML(ASIS, true));
        }

        [Test]
        public void UpdateMixedOrderForSynonyms() {
            Hashtable words = new Hashtable();
            words.Add("home", new WordElement("home", "дом", 0, 0, 111));
            words.Add("mess", new WordElement("mess", "беспорядок", 0, 0, 222));
            words.Add("turmoil", new WordElement("turmoil", "беспорядок", 0, 0, 333));

            BCCommon.UpdateMixedOrderForSynonyms(ref words);

            Assert.AreEqual(3, words.Count);
            Assert.AreEqual(111, ((WordElement) words["home"]).MixedOrder);
            Assert.AreEqual(222, ((WordElement) words["mess"]).MixedOrder);
            Assert.AreEqual(222, ((WordElement) words["turmoil"]).MixedOrder);
        }


        [Test]
        public void GetTextAsHTMLForAnalysis() {
            CreateNewAccount();
            DBCommon.AddWordToAccount(accountId, wordId, (int) Language.English);

            string text =
                "'" +
                word +
                ". " +
                wordAbsentInDB +
                ";" + Environment.NewLine +
                word2 +
                "!?";
            string expected =
                "'" +
                BCCommon.GetWordHTML(word, wordId, true) +
                ". " +
                wordAbsentInDB +
                ";" + Environment.NewLine +
                BCCommon.GetWordHTML(word2, wordId2, false) +
                "!?<hr>";

            string result = new BCCommon().GetTextAsHTMLForAnalysis(text, accountId, (int) Language.Spanish, (int) Language.English, false);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetTextAsHTMLForAnalysisSmokeTests() {
            Assert.AreEqual("<hr>", new BCCommon().GetTextAsHTMLForAnalysis("", accountId, (int) Language.Spanish, (int) Language.English, false));
            Assert.AreEqual("fdsajkleae<hr>", new BCCommon().GetTextAsHTMLForAnalysis("fdsajkleae", accountId, (int) Language.Spanish, (int) Language.English, false));
            Assert.AreEqual("65!,? -<hr>", new BCCommon().GetTextAsHTMLForAnalysis("65!,? -", -1, (int) Language.Spanish, (int) Language.English, false));
            Assert.AreEqual("nov th ing<hr>", new BCCommon().GetTextAsHTMLForAnalysis("nov th ing", -1, (int) Language.Spanish, (int) Language.English, false));
        }

        [Test]
        public void GetTextAsHTMLForAnalysisPluralTime() {
            string text = "borders foxes families dictionaries";
            string expected =
                BCCommon.GetWordHTML("border", 1768, false) + "[s] " +
                BCCommon.GetWordHTML("fox", 6415, false) + "[es] " +
                BCCommon.GetWordHTML("family", 5812, false) + "[ies] " +
                BCCommon.GetWordHTML("dictionary", 4387, false) + "[ies]<hr>";

            string result = new BCCommon().GetTextAsHTMLForAnalysis(text, -1, (int) Language.Spanish, (int) Language.English, false);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetTextAsHTMLForAnalysisIngForm() {
            string text = "completing sticking";
            string expected =
                BCCommon.GetWordHTML("complete", 3048, false) + "[ing] " +
                BCCommon.GetWordHTML("stick", 15663, false) + "[ing]<hr>";

            string result = new BCCommon().GetTextAsHTMLForAnalysis(text, -1, (int) Language.Spanish, (int) Language.English, false);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetTextAsHTMLForAnalysisPastForm() {
            string text = "ended supervised denied identified";
            string expected =
                BCCommon.GetWordHTML("end", 5267, false) + "[ed] " +
                BCCommon.GetWordHTML("supervise", 16049, false) + "[d] " +
                BCCommon.GetWordHTML("deny", 4187, false) + "[ied] " +
                BCCommon.GetWordHTML("identify", 7841, false) + "[ied]<hr>";

            string result = new BCCommon().GetTextAsHTMLForAnalysis(text, -1, (int) Language.Spanish, (int) Language.English, false);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetTextAsHTMLForAnalysisTwinEnding() {
            string text = "submitting slugging cancelled";
            string expected =
                BCCommon.GetWordHTML("submit", 15893, false) + "[ting] " +
                BCCommon.GetWordHTML("slug", 59374, false) + "[ging] " +
                BCCommon.GetWordHTML("cancel", 2192, false) + "[led]<hr>";

            string result = new BCCommon().GetTextAsHTMLForAnalysis(text, -1, (int) Language.Spanish, (int) Language.English, false);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetTextAsHTMLForAnalysisPhraseologism() {
            string text = "really? you!\tdon't" + Environment.NewLine + 
                "say! N.A.A.F.I LT.-Col. O. K. the Great War. hello?";
            string expected
                = BCCommon.GetWordHTML("really",	13166, false) + "? "
                + BCCommon.GetWordHTML("you",		18601, false) + "!\t"
                + BCCommon.GetWordHTML("don",		 4782, false) + "'"
                + BCCommon.GetWordHTML("t",			61792, false) + Environment.NewLine
                + BCCommon.GetWordHTML("say",		14205, false) + "! N."
                + BCCommon.GetWordHTML("A",			18731, false) + "."
                + BCCommon.GetWordHTML("A",			18731, false) + ".F."
                + BCCommon.GetWordHTML("I",			40325, false) + " LT.-Col. "
                + BCCommon.GetWordHTML("O",			48956, false) + ". K. "
                + BCCommon.GetWordHTML("the",		16532, false) + " "
                + BCCommon.GetWordHTML("Great",		 7029, false) + " "
                + BCCommon.GetWordHTML("War",		18084, false) + ". "
                + BCCommon.GetWordHTML("hello",		 7431, false) + "?"
                + "<hr> "
                + BCCommon.GetWordHTML("really?",			55242, false) + ""
                ;

            string result = new BCCommon().GetTextAsHTMLForAnalysis(text, -1, (int) Language.Spanish, (int) Language.English, false);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ConcatWords() {
            string result = BCCommon.ConcatWords(new string[] {"hello", "world", "!"});
            Assert.AreEqual("hello world !", result);
        }

        [Test]
        public void AddVerbsToAccount() {
            VerbElement[] elements;
            string info;

            CreateNewAccount();

            elements = BCCommon.GetVerbs(accountId, false, SortColumn.Form1, false, int.MaxValue);
            Assert.IsTrue(elements.Length > 0);

            info = BCCommon.GetAccountVerbsInfo(accountId);
            Assert.AreEqual("0|0%|0%", info);

            BCCommon.AddVerbsToAccount(accountId, new string[] {
                                                                   "", elements[0].Id.ToString(), elements[1].Id.ToString(), elements[0].Id.ToString(),
                                                                   elements[2].Id.ToString(), elements[3].Id.ToString(), elements[4].Id.ToString()
                                                               });
            info = BCCommon.GetAccountVerbsInfo(accountId);
            Assert.AreEqual("5|3%|1%", info);

            BCCommon.DeleteAllVerbsInAccount(accountId);
            info = BCCommon.GetAccountVerbsInfo(accountId);
            Assert.AreEqual("0|0%|0%", info);
        }

        [Test]
        public void GetVerbsChecknotUseWellKnownVerbs() {
            VerbElement[] elements;
            int totalVerbs;

            CreateNewAccount();

            elements = BCCommon.GetVerbs(accountId, false, SortColumn.Form1, true, int.MaxValue);
            totalVerbs = elements.Length;
            Assert.IsTrue(totalVerbs > 0);

            BCCommon.AddVerbsToAccount(accountId, new string[] {
                                                                   elements[0].Id.ToString()
                                                               });

            elements = BCCommon.GetVerbs(accountId, false, SortColumn.Form1, true, int.MaxValue);
            Assert.AreEqual(elements.Length, totalVerbs - 1);

            elements = BCCommon.GetVerbs(accountId, false, SortColumn.Form1, false, int.MaxValue);
            Assert.AreEqual(elements.Length, totalVerbs);

            BCCommon.DeleteAllVerbsInAccount(accountId);
            elements = BCCommon.GetVerbs(accountId, false, SortColumn.Form1, true, int.MaxValue);
            Assert.AreEqual(elements.Length, totalVerbs);
        }

        [Test]
        public void GetVerbsCheckPopular() {
            VerbElement[] elements;
            VerbElement[] elements2;

            elements = BCCommon.GetVerbs(accountId, false, SortColumn.Form1, false, int.MaxValue);
            Assert.IsTrue(elements.Length > 0);

            elements2 = BCCommon.GetVerbs(accountId, true, SortColumn.Form1, false, int.MaxValue);
            Assert.IsTrue(elements2.Length > 0);

            Assert.IsTrue(elements.Length > elements2.Length);
        }

        [Test]
        public void GetVerbsCheckSort() {
            VerbElement[] elements;
            VerbElement[] elements2;

            elements = BCCommon.GetVerbs(accountId, false, SortColumn.Form1, false, int.MaxValue);
            Assert.IsTrue(string.Compare(elements[0].Form1, elements[9].Form1) < 0);
            Assert.IsTrue(string.Compare(elements[0].Form1, elements[0].Form1) == 0);

            elements = BCCommon.GetVerbs(accountId, false, SortColumn.Translate, false, int.MaxValue);
            Assert.IsTrue(string.Compare(elements[0].Translation, elements[9].Translation) < 0);

            elements = BCCommon.GetVerbs(accountId, false, SortColumn.VerbType, false, int.MaxValue);
            Assert.IsTrue(elements[0].VerbType < elements[elements.Length - 1].VerbType);

            elements2 = BCCommon.GetVerbs(accountId, false, SortColumn.Random, false, int.MaxValue);
            Assert.AreEqual(elements.Length, elements2.Length);
            Assert.IsTrue(elements[0].Form1 + elements[5].Form1 + elements[9].Form1 !=
                elements2[0].Form1 + elements2[5].Form1 + elements2[9].Form1);
        }

        [Test]
        public void GetVerbsCheckWordCount() {
            VerbElement[] elements;

            elements = BCCommon.GetVerbs(accountId, false, SortColumn.Form1, false, 3);
            Assert.AreEqual(3, elements.Length);
        }

        [Test]
        public void GetVerbTypes() {
            IdName[] names = BCCommon.GetVerbTypes();
            Assert.IsTrue(names.Length > 0);

            for (int i = 0; i < names.Length - 2; i++) {
                Assert.IsTrue(names[i].Id == names[i + 1].Id - 1);
            }
        }

        [Test]
        public void GetTestedVerbsTEXT() {
            string result, expected;
            IdName[] verbTypes = BCCommon.GetVerbTypes();

            VerbElement[] elements = new VerbElement[] {
                                                           new VerbElement(1, "a", "b", "c", "d", verbTypes[0].Id, 1),
                                                           new VerbElement(2, "e", "f", "g", "h", verbTypes[1].Id, 2),
                                                           new VerbElement(3, "i", "j", "k", "l", verbTypes[1].Id, 3),
            };

            result = BCCommon.GetTestedVerbsTEXT(elements, SortColumn.Form1);
            expected =
                "a b c d" + Environment.NewLine +
                "e f g h" + Environment.NewLine +
                "i j k l";
            Assert.AreEqual(expected, result);

            result = BCCommon.GetTestedVerbsTEXT(elements, SortColumn.VerbType);
            expected =
                verbTypes[0].Name + Environment.NewLine +
                "a b c d" + Environment.NewLine +
                Environment.NewLine + verbTypes[1].Name + Environment.NewLine
                + "e f g h" + Environment.NewLine
                + "i j k l";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetTestedVerbsHTML() {
            string result, expected;

            VerbElement[] elements = new VerbElement[] {
                                                           new VerbElement(1, "a \"", "b \"", "c \"", "d \"", 1, 1),
            };

            result = BCCommon.GetTestedVerbsHTML(elements, ShowColumn.Form2, (int) Language.Spanish);
            expected =
                string.Format(
                "<input id=w{0} type=checkbox name=w{0} />" + 
                "<input id=a{0} type=text size=8 onkeyup=\"VerbCheck('a{0}');\" v=\"{1}\" />" + 
                "<input id=b{0} type=text size=8 onkeyup=\"VerbCheck('b{0}');\" value=\"{2}\" readOnly=true />" + 
                "<input id=c{0} type=text size=8 onkeyup=\"VerbCheck('c{0}');\" v=\"{3}\" />" + 
                "<input id=d{0} type=text size=29 onkeyup=\"VerbCheck('d{0}');\" v=\"{4}\" readOnly=true /><br>",
                elements[0].Id,
                HttpUtility.HtmlEncode(elements[0].Form1),
                HttpUtility.HtmlEncode(elements[0].Form2),
                HttpUtility.HtmlEncode(elements[0].Form3),
                "");
            Assert.AreEqual(expected, result);

            result = BCCommon.GetTestedVerbsHTML(elements, ShowColumn.Form1, (int) Language.Spanish);
            Assert.AreEqual(expected.Length, result.Length);

            result = BCCommon.GetTestedVerbsHTML(elements, ShowColumn.Form3, (int) Language.Spanish);
            Assert.AreEqual(expected.Length, result.Length);
        }
        [Test]
        public void GetWordIdByOriginal() {
            Assert.AreEqual(wordId, BCCommon.GetWordIdByOriginal(word, (int) Language.English));
            Assert.AreEqual(int.MinValue, BCCommon.GetWordIdByOriginal(wordAbsentInDB, (int) Language.English));
        }

        [Test]
        public void GetWordIdByTranslation() {
            Assert.AreEqual(wordId, BCCommon.GetWordIdByTranslation(wordTranslation, (int) Language.Spanish));
            Assert.AreEqual(int.MinValue, BCCommon.GetWordIdByTranslation(wordAbsentInDB, (int) Language.Spanish));
        }

        [Test]
        public void GetVerbFormWords() {
            VerbElement element = new VerbElement(1,
                "?ab, \" \' ;-cd+",
                "?ef, \" \' ;-gh+",
                "?ab, \" \' ;-cd+",
                "?ZZ, \" \' ;-YY+",
                1, 1);

            string[] words = BCCommon.GetVerbFormWords(element);
            Assert.AreEqual(6, words.Length);
            Assert.AreEqual("ab", words[0]);
            Assert.AreEqual("cd", words[1]);
            Assert.AreEqual("ef", words[2]);
            Assert.AreEqual("gh", words[3]);
            Assert.AreEqual("ab", words[4]);
            Assert.AreEqual("cd", words[5]);
        }

        [Test]
        public void GetVerbTranslationWords() {
            VerbElement element = new VerbElement(1,
                "?ab, \" \' ;-cd+",
                "?ab, \" \' ;-cd+",
                "?ab, \" \' ;-cd+",
                "?ab, \" \' ;-cd+",
                1, 1);

            string[] words = BCCommon.GetVerbTranslationWords(element);
            Assert.AreEqual(2, words.Length);
            Assert.AreEqual("ab", words[0]);
            Assert.AreEqual("cd", words[1]);
        }

        [Test]
        public void GetTOPUsersByVocabulary() {
            CreateNewAccount();
            IdNamePair[] namePairs = BCCommon.GetTOPUsersByVocabulary((int) Language.English);
            Assert.IsTrue(namePairs.Length > 0);

            CreateNewAccount();
            IdNamePair[] namePairs2 = BCCommon.GetTOPUsersByVocabulary((int) Language.English);
            Assert.AreEqual(namePairs.Length, namePairs2.Length);

            BCCommon.AddWordsToAccount(accountId, new string[] {wordId.ToString()}, (int) Language.English);
            IdNamePair[] namePairs3 = BCCommon.GetTOPUsersByVocabulary((int) Language.English);
            Assert.AreEqual(namePairs.Length + 1, namePairs3.Length);
            
            bool find = false;
            foreach (IdNamePair namePair in namePairs3) {
                if (namePair.Name == accountNick) {
                    Assert.AreEqual("1", namePair.Name2);
                    find = true;
                }
            }
            Assert.IsTrue(find);
        }
        [Test]
        public void GetTOPUsersByVerbs() {
            CreateNewAccount();
            IdNamePair[] namePairs = BCCommon.GetTOPUsersByVerbs();
            Assert.IsTrue(namePairs.Length > 0);

            CreateNewAccount();
            IdNamePair[] namePairs2 = BCCommon.GetTOPUsersByVerbs();
            Assert.AreEqual(namePairs.Length, namePairs2.Length);

            BCCommon.AddVerbsToAccount(accountId, new string[] {verbId.ToString()} );
            IdNamePair[] namePairs3 = BCCommon.GetTOPUsersByVerbs();
            Assert.AreEqual(namePairs.Length + 1, namePairs3.Length);
            
            bool find = false;
            foreach (IdNamePair namePair in namePairs3) {
                if (namePair.Name == accountNick) {
                    Assert.AreEqual("1", namePair.Name2);
                    find = true;
                }
            }
            Assert.IsTrue(find);
        }
        [Test]
        public void GetDictionaryQuizHTML() {
            string content = BCCommon.GetDictionaryQuizHTML(0, (int) Language.Spanish, (int) Language.English);
            Assert.IsTrue(content.Length > 0);
            Assert.IsTrue(content.IndexOf("type=radio") >= 0);
            Assert.IsTrue(content.IndexOf("correct=1") >= 0);
            Assert.IsTrue(content.IndexOf("correct=0") >= 0);
        }

        [Test]
        public void GetDictionaryQuizResultHTML() {
            IdNamePair[] pairs = BCCommon.GetTOPUsersByDictionaryQuiz((int) Language.English);
            int length = pairs.Length;

            CreateNewAccount();
            string content = BCCommon.GetDictionaryQuizResultHTML(accountId, "", 100, (int) Language.English);
            pairs = BCCommon.GetTOPUsersByDictionaryQuiz((int) Language.English);
            Assert.AreEqual(length + 1, pairs.Length);
            Assert.IsTrue(content.IndexOf("Felicidades!") >= 0);

            CreateNewAccount();
            content = BCCommon.GetDictionaryQuizResultHTML(accountId, "", 0, (int) Language.English);
            pairs = BCCommon.GetTOPUsersByDictionaryQuiz((int) Language.English);
            Assert.AreEqual(length + 1, pairs.Length);
            Assert.IsTrue(content.IndexOf("No se molesta!") >= 0);

            content = BCCommon.GetDictionaryQuizResultHTML(Account.EmptyId, accountNick, 100, (int) Language.English);
            pairs = BCCommon.GetTOPUsersByDictionaryQuiz((int) Language.English);
            Assert.AreEqual(length + 2, pairs.Length);
            Assert.IsTrue(content.IndexOf("Felicidades!") >= 0);
        }

        [Test]
        public void GetWordsOrderQuizResultHTML() {
            IdNamePair[] pairs = BCCommon.GetTOPUsersByWordsOrderQuiz((int) Language.English);
            int length = pairs.Length;

            CreateNewAccount();
            string content = BCCommon.GetWordsOrderQuizResultHTML(accountId, "", 100, (int) Language.English);
            pairs = BCCommon.GetTOPUsersByWordsOrderQuiz((int) Language.English);
            Assert.AreEqual(length + 1, pairs.Length);
            Assert.IsTrue(content.IndexOf("Felicidades!") >= 0);

            CreateNewAccount();
            content = BCCommon.GetWordsOrderQuizResultHTML(accountId, "", 0, (int) Language.English);
            pairs = BCCommon.GetTOPUsersByWordsOrderQuiz((int) Language.English);
            Assert.AreEqual(length + 1, pairs.Length);
            Assert.IsTrue(content.IndexOf("No se molesta!") >= 0);

            content = BCCommon.GetWordsOrderQuizResultHTML(Account.EmptyId, accountNick, 100, (int) Language.English);
            pairs = BCCommon.GetTOPUsersByWordsOrderQuiz((int) Language.English);
            Assert.AreEqual(length + 2, pairs.Length);
            Assert.IsTrue(content.IndexOf("Felicidades!") >= 0);
        }
        [Test]
        public void GetWordsOrderSentence() {
            string sentence = BCCommon.GetWordsOrderSentence((int) Language.English);
            Assert.IsNotNull(sentence);

            string sentence2 = BCCommon.GetWordsOrderSentence((int) Language.English);
            Assert.IsFalse(sentence == sentence2);
        }

        [Test]
        public void GetWordsOrderQuizHTML() {
            string content = BCCommon.GetWordsOrderQuizHTML(0, (int) Language.English);
            Assert.IsTrue(content.Length > 0);
            Assert.IsTrue(content.IndexOf("PREGUNTA:") >= 0);
            Assert.IsTrue(content.IndexOf("<div class=\"mainDivWordsOrderQuiz\"") >= 0);
            Assert.IsTrue(content.IndexOf("<span id=w") >= 0);
        }
        [Test]
        public void GetWordsUnsoundedShort() {
            int[] pairs = BCCommon.GetWordsUnsoundedShort((int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(4702, pairs.Length);
        }
        [Test]
        public void GetFilmCategories() {
            IdName[] names = BCCommon.GetFilmCategories((int) Language.Spanish);
            Assert.AreEqual(11, names.Length);
            Assert.AreEqual("AGUDO", names[0].Name);
        }
        [Test]
        public void GetFilmsByCategory()
        {
            var accountId = 0;
            IdName[] categories = BCCommon.GetFilmCategories((int) Language.Spanish);
            var films = BCCommon.GetFilmsByCategory(accountId, categories[0].Id, (int)Language.Spanish, (int)Language.English);
            Assert.AreEqual(91, films.Length);
            Assert.AreEqual(529, films[0].Id);
            Assert.AreEqual("12 Rounds (2009)", films[0].Name);
        }
        [Test]
        public void GetFilmsBySearch() {
            var accountId = 0;
            var films = BCCommon.GetFilmsBySearch(accountId, "ali", (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(6, films.Length);
            Assert.AreEqual(134, films[0].Id);
            Assert.AreEqual("Alice in Wonderland", films[0].Name);
            Assert.AreEqual("DIBUJOS", films[0].CategoryName);
            Assert.AreEqual(436, films[1].Id);
            Assert.AreEqual("Aliens", films[1].Name);
            Assert.AreEqual("AGUDO", films[1].CategoryName);
        }
        [Test]
        public void GetFilm() {
            Film film1 = BCCommon.GetFilm(131);
            Assert.AreEqual(131, film1.Id);
            Assert.AreEqual(null, film1.Category);
            Assert.AreEqual("Dumbo", film1.Name);
            Assert.IsTrue(film1.PlayerPattern.IndexOf("static.youku.com/v1.0.0234") > 0);
            Assert.AreEqual("XMTA4Njg3MTY", film1.Url);
            Assert.AreEqual(0, film1.PartUrls.Count);
            
            Film film2 = BCCommon.GetFilm(133);
            Assert.AreEqual(2, film2.PartUrls.Count);
        }
    }
}