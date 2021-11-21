using System;
using System.Collections;
using NUnit.Framework;
using Wordz.BC;
using Wordz.BE;
using Wordz.DB;
using Wordz.LngOpt;
using Wordz.Snd;

namespace Wordz.UT {
    [TestFixture]
    public class DBCommon_es_en_Test : DbTransactionalFixture {
        public DBCommon_es_en_Test() {
            word = "abandon";
            word2 = "agent";
            wordTranslation = "desamparar, abandona, dejar, desmantelar, expedir";
            word2Translation = "reactivo, agente, represantante, encargado, facto";
        }

        [Test]
        public void TestData() {
            Assert.AreEqual("7", DBCommon.TestData(6));
        }

        [Test]
        public void GetAccountsCount() {
            int count = DBCommon.GetAccountsCount();
            Assert.IsTrue(count >= 0);
        }

        [Test]
        public void GetWordsCount() {
            int count = DBCommon.GetWordsCount((int) Language.English, (int) Language.Spanish);
            Assert.AreEqual(23906, count);
        }

        [Test/*, Ignore("all sounds in files, not in DB")*/]
        public void GetWordsCountSounded() {
            int count = DBCommon.GetWordsCountSounded();
            Assert.AreEqual(68360, count);
        }

        [Test]
        public void GetWordsCountSoundedFromFile() {
            int count = DBCommon.GetWordsCountSoundedFromFile((int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(2, count);
        }

        [Test]
        public void FillWordInfo() {
            CreateNewAccount();

            WordElement wordElement = new WordElement();
            wordElement.Original = word;
            DBCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(wordTranslation, wordElement.Translation);
            Assert.AreEqual(wordId, wordElement.Id);
            Assert.IsFalse(wordElement.WellKnown);
            Assert.IsTrue(wordElement.Sounded);
        }

        [Test]
        public void FillWordInfoNotSounded() {
            CreateNewAccount();

            WordElement wordElement = new WordElement();
            wordElement.Original = wordWithoutSoundAndOnlyAtEnglish;
            DBCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.IsFalse(wordElement.Sounded);
        }

        [Test]
        public void GetWordInfo() {
            WordElement wordElement = DBCommon.GetWordInfo(wordId, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(word, wordElement.Original);
            Assert.AreEqual(wordTranslation, wordElement.Translation);
        }

        [Test]
        public void GetWordInfoNotInDB() {
            WordElement wordElement = DBCommon.GetWordInfo(int.MinValue, (int) Language.Spanish, (int) Language.English);
            Assert.IsNull(wordElement.Original);
            Assert.IsNull(wordElement.Translation);
        }

        [Test]
        public void AddWordToAccount() {
            CreateNewAccount();

            WordElement wordElement = new WordElement();
            wordElement.Original = word;
            DBCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.IsFalse(wordElement.WellKnown);

            DBCommon.AddWordToAccount(accountId, wordElement.Id, (int) Language.English);
            DBCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.IsTrue(wordElement.WellKnown);

            DBCommon.AddWordToAccount(accountId, wordElement.Id, (int) Language.English);
            DBCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.IsTrue(wordElement.WellKnown);
        }

        [Test]
        public void DeleteWordFromAccount() {
            CreateNewAccount();

            WordElement wordElement = new WordElement();
            wordElement.Original = word;
            DBCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.IsFalse(wordElement.WellKnown);

            DBCommon.AddWordToAccount(accountId, wordElement.Id, (int) Language.English);
            DBCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.IsTrue(wordElement.WellKnown);

            DBCommon.DeleteWordFromAccount(accountId, wordElement.Id, (int)Language.English);
            DBCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            Assert.IsFalse(wordElement.WellKnown);
        }

        [Test]
        public void FillWordsTranslation() {
            Hashtable words = new Hashtable();
            words[word] = new WordElement(word, "", 0, 0, 0);
            words[word2] = new WordElement(word2, "", 0, 0, 0);

            DBCommon.FillWordsTranslation(ref words, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(2, words.Count);
            Assert.AreEqual(wordTranslation, ((WordElement) words[word]).Translation);
            Assert.AreEqual(word2Translation, ((WordElement) words[word2]).Translation);
        }

        [Test, Ignore("all sounds in files, not in DB")]
        public void GetWordSoundTranslation() {
            byte[] sound = DBCommon.GetWordSoundTranslation(wordId, true);
            Assert.AreEqual(6941, sound.Length);
            Assert.AreEqual(0x49, sound[0]);

            sound = DBCommon.GetWordSoundTranslation(wordId, false);
            Assert.AreEqual(5120, sound.Length);
            Assert.AreEqual(0xFF, sound[0]);
        }

        [Test, Ignore("all sounds in files, not in DB")]
        public void GetWordSound() {
            byte[] sound = DBCommon.GetWordSound(wordId, true);
            Assert.AreEqual(12061, sound.Length);
            Assert.AreEqual(0x49, sound[0]);

            sound = DBCommon.GetWordSound(wordId, false);
            Assert.AreEqual(12061, sound.Length);
            Assert.AreEqual(0xFF, sound[0]);

            sound = DBCommon.GetWordSound(wordIdWithoutSound, true);
            Assert.AreEqual(0, sound.Length);

            sound = DBCommon.GetWordSound(wordIdWithoutSound, false);
            Assert.AreEqual(0, sound.Length);

            //            FileStream fs = new FileStream("C:\\sound.mp3", FileMode.CreateNew);
            //            BinaryWriter w = new BinaryWriter(fs);
            //            w.Write(sound);
            //            w.Close();
            //            fs.Close();
        }

        [Test]
        public void GetWordSoundFromFile() {
            byte[] sound = DBCommon.GetWordSoundFromFile(wordId, true, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(13882, sound.Length);
            Assert.AreEqual(0x49, sound[0]);

            sound = DBCommon.GetWordSoundFromFile(wordId, false, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(13882, sound.Length);
            Assert.AreEqual(0x49, sound[0]);

            sound = DBCommon.GetWordSoundFromFile(wordIdWithoutSound, true, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(0, sound.Length);

            sound = DBCommon.GetWordSoundFromFile(wordIdWithoutSound, false, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(0, sound.Length);
        }

        [Test]
        public void GetWordSoundFromFileEnglish() {
            byte[] sound = DBCommon.GetWordSoundFromFile(wordId, "", "", false, SoundingType.Learn, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(6941, sound.Length);
            Assert.AreEqual(0x49, sound[0]);

            sound = DBCommon.GetWordSoundFromFile(wordIdWithoutSound, "", "", false, SoundingType.Learn, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(0, sound.Length);
        }

        [Test]
        public void GetWordSoundFromFileRussian() {
            byte[] sound = DBCommon.GetWordSoundFromFile(wordId, "", "", false, SoundingType.Native, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(6941, sound.Length);
            Assert.AreEqual(0x49, sound[0]);

            sound = DBCommon.GetWordSoundFromFile(wordIdWithoutSound, "", "", false, SoundingType.Native, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(0, sound.Length);
        }

        [Test, Ignore("No verbs")]
        public void GetVerbSoundFromFile() {
            byte[] sound = DBCommon.GetVerbSoundFromFile(verbId, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(11975 + 11975, sound.Length);
            Assert.AreEqual(0x49, sound[0]);

            sound = DBCommon.GetVerbSoundFromFile(verbIdWithoutSound, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(0, sound.Length);
        }

        [Test]
        public void GetDomains() {
            IdName[] names = DBCommon.GetDomains((int) Language.Spanish);
            Assert.AreEqual(22, names.Length);
            Assert.AreEqual("popular palabra (956)", names[0].Name);
            Assert.AreEqual("comunicación (0)", names[1].Name);
        }
        [Test]
        public void GetWordsCountForAccount() {
            int count;
            CreateNewAccount();

            count = DBCommon.GetWordsCountForAccount(accountId, (int) Language.English);
            Assert.AreEqual(0, count);

            WordElement wordElement = new WordElement();
            wordElement.Original = word;
            DBCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            DBCommon.AddWordToAccount(accountId, wordElement.Id, (int) Language.English);

            count = DBCommon.GetWordsCountForAccount(accountId, (int) Language.English);
            Assert.AreEqual(1, count);
        }
        [Test]
        public void GetWordIdsForDomain() {
            //TODO: write test
            IdNamePair[] pairs = DBCommon.GetWordIdsForDomain(accountId, 1, 10, 1, (int) Language.Spanish, (int) Language.English);
            Assert.IsNotNull(pairs);
            Assert.AreEqual(10, pairs.Length);
            Assert.AreEqual("able apto, habilidoso, diestro, capaz, susceptible", pairs[0].Value);
            Assert.AreEqual("about al derredor, en redondo, acerca, acerca de", pairs[1].Value);
        }
        [Test/*, Ignore("Too long test")*/]
        public void GetWordIdsRandom() {
            IdNamePair[] pairs = DBCommon.GetWordIdsRandom(accountId, 10, (int) Language.Spanish, (int) Language.English);
            Assert.IsNotNull(pairs);
            Assert.AreEqual(10, pairs.Length);

            foreach (IdNamePair pair1 in pairs) {
                Assert.IsTrue(pair1.Name.Trim().Length > 0);
                Assert.IsTrue(pair1.Name2.Trim().Length > 0);
                int equalsCount = 0;
                foreach (IdNamePair pair2 in pairs) {
                    if (pair1.Id == pair2.Id) {
                        equalsCount++;
                    }
                }
                Assert.AreEqual(1, equalsCount);
            }
        }
        [Test/*, Ignore("Too long test")*/]
        public void GetWordIdsOrdered() {
            IdNamePair[] pairs = DBCommon.GetWordIdsOrdered(accountId, 2, 200, (int) Language.Spanish, (int) Language.English);
            Assert.IsNotNull(pairs);
            Assert.AreEqual(2, pairs.Length);
            Assert.AreEqual("accoutre equipar", pairs[0].Value);
            Assert.AreEqual("accredit comprobcr", pairs[1].Value);
        }
        [Test]
        public void GetWordIdsForAccount() {
            IdNamePair[] pairs;
            CreateNewAccount();

            pairs = DBCommon.GetWordIdsForAccount(accountId, (int) Language.English);
            Assert.AreEqual(0, pairs.Length);

            WordElement wordElement = new WordElement();
            wordElement.Original = word2;
            DBCommon.FillWordInfo(ref wordElement, accountId, (int) Language.Spanish, (int) Language.English);
            DBCommon.AddWordToAccount(accountId, wordElement.Id, (int) Language.English);

            WordElement wordElement2 = new WordElement();
            wordElement2.Original = word;
            DBCommon.FillWordInfo(ref wordElement2, accountId, (int) Language.Spanish, (int) Language.English);
            DBCommon.AddWordToAccount(accountId, wordElement2.Id, (int) Language.English);

            pairs = DBCommon.GetWordIdsForAccount(accountId, (int) Language.English);
            Assert.AreEqual(2, pairs.Length);
            Assert.AreEqual(wordElement2.Id, pairs[0].Id);
            Assert.AreEqual(wordElement2.Original, pairs[0].Name);
            Assert.AreEqual(wordElement.Id, pairs[1].Id);
            Assert.AreEqual(wordElement.Original, pairs[1].Name);
        }
        [Test]
        public void AddWordToDictionary() {
            string addedWordOriginal = Guid.NewGuid().ToString();
            string addedWordTranslation = Guid.NewGuid().ToString();

            WordElement wordElement = new WordElement();
            wordElement.Original = addedWordOriginal;
            wordElement.Translation = addedWordTranslation;
            
            DBCommon.FillWordInfo(ref wordElement, (int) Language.Spanish, (int) Language.English);
            Assert.IsFalse(wordElement.ExistsInDB);

            DBCommon.AddWordToDictionary(wordElement, (int) Language.Spanish, (int) Language.English);

            DBCommon.FillWordInfo(ref wordElement, (int) Language.Spanish, (int) Language.English);
            Assert.IsTrue(wordElement.ExistsInDB);
            Assert.AreEqual(addedWordOriginal, wordElement.Original);
            Assert.AreEqual(addedWordTranslation, wordElement.Translation);
        }
        [Test]
        public void UpdateWordInDictionary() {
            string addedWordOriginal = Guid.NewGuid().ToString();
            string addedWordTranslation = Guid.NewGuid().ToString();
            string addedWordOriginal2 = Guid.NewGuid().ToString();
            string addedWordTranslation2 = Guid.NewGuid().ToString();

            Assert.IsTrue(addedWordOriginal != addedWordOriginal2);
            Assert.IsTrue(addedWordTranslation != addedWordTranslation2);

            WordElement wordElement = new WordElement();
            wordElement.Original = addedWordOriginal;
            wordElement.Translation = addedWordTranslation;
            DBCommon.AddWordToDictionary(wordElement, (int) Language.Spanish, (int) Language.English);
            wordElement.Id = DBCommon.GetWordIdByText(wordElement.Original, (int)Language.English);
           
            wordElement.Original = addedWordOriginal2;
            wordElement.Translation = addedWordTranslation2;
            DBCommon.UpdateWordInDictionary(wordElement, (int) Language.Spanish, (int) Language.English);

            wordElement = DBCommon.GetWordInfo(wordElement.Id, (int) Language.Spanish, (int) Language.English);
            Assert.AreEqual(addedWordOriginal2, wordElement.Original);
            Assert.AreEqual(addedWordTranslation2, wordElement.Translation);
        }
        [Test]
        public void GetWordTranscription() {
            string wordEn;

            wordEn = DBCommon.GetWordTranscription("боолеен");
            Assert.AreEqual("bbollenn", wordEn);

            wordEn = DBCommon.GetWordTranscription("туфля");
            Assert.AreEqual("ttuuffllya", wordEn);
        }

        [Test]
        public void WavHelper_ConvertSoundToWavEN() {
            byte[] content = WavHelper.ConvertSoundToWav(word);
            Assert.IsTrue(content.Length > 0);
        }
        
        [Test]
        public void WavHelper_ConvertSoundToWavRU() {
            byte[] content = WavHelper.ConvertSoundToWav(wordTranslation);
            Assert.IsTrue(content.Length > 13760);  //not empty file
        }
        
        [Test]
        public void MP3Helper_ConvertSoundToMP3() {
            byte[] wavContent = WavHelper.ConvertSoundToWav(word);
            Assert.IsTrue(wavContent.Length > 0);

            byte[] mp3Content = MP3Helper.ConvertSoundToMP3(wavContent);
            Assert.IsTrue(mp3Content.Length > 0);
        }

        [Test]
        public void GetWordSoundFromText() {
            byte[] sound = DBCommon.GetWordSoundFromText(word, wordTranslation, true);
            Assert.AreEqual(99968, sound.Length);

            sound = DBCommon.GetWordSoundFromText(word, wordTranslation, false);
            Assert.AreEqual(95488, sound.Length);
        }

        [Test]
        public void GetSites() {
            IdName[] names = DBCommon.GetSites();
            Assert.IsTrue(names.Length > 0);
            Assert.AreEqual("http://www.5english.com/", names[0].Name);
        }
        [Test]
        public void GetCategories() {
            IdName[] names = DBCommon.GetCategories((int) Language.Spanish);
            Assert.IsTrue(names.Length > 0);
            Assert.AreEqual("Estudio Inglés", names[0].Name);
        }
        [Test, Ignore("No resources yet")]
        public void GetArticles() {
            Article[] articles = DBCommon.GetArticles((int) Language.Spanish, (int) Language.English);
            Assert.IsTrue(articles.Length > 0);
            Assert.AreEqual("Методы изучения английского языка", articles[0].Title);
            Assert.IsNull(articles[0].Body);
        }
        [Test]
        public void GetArticle() {
            Article article = DBCommon.GetArticle(1);
            Assert.AreEqual("Методы изучения английского языка", article.Title);
            Assert.IsTrue(article.Body.Length > 0);
        }
        [Test]
        public void GetTvChannels() {
            Tv[] tvs = DBCommon.GetTvChannels(BCCommon.AnonymousAccountId, (int)Language.Spanish, (int)Language.English);
            Assert.AreEqual(18, tvs.Length);
            Assert.AreEqual(1, tvs[0].Id);
            Assert.AreEqual("russia_today_tv.jpg", tvs[0].ImageUrl);
            Assert.AreEqual("http://streaming.visionip.tv/Russia_Today", tvs[0].Url);
            Assert.AreEqual("RuToday", tvs[0].Name);
            Assert.AreEqual("Russia Today Canal de televisión ruso en línea en Inglés. Su tarea principal de establecer un visitante extranjero una imagen positiva de Rusia.", tvs[0].Description);
        }
        [Test]
        public void GetTvChannel() {
            Tv tv = DBCommon.GetTvChannel(1, (int)Language.Spanish);
            Assert.AreEqual("http://streaming.visionip.tv/Russia_Today", tv.Url);
            Assert.AreEqual("Russia Today Canal de televisión ruso en línea en Inglés. Su tarea principal de establecer un visitante extranjero una imagen positiva de Rusia.", tv.Description);
        }
        [Test]
        public void GetFmChannels() {
            Fm[] fms = DBCommon.GetFmChannels(BCCommon.AnonymousAccountId, (int)Language.Spanish, (int)Language.English);
            Assert.AreEqual(35, fms.Length);
            Assert.AreEqual(27, fms[26].Id);
            Assert.AreEqual("cnn.jpg", fms[26].ImageUrl);
            Assert.AreEqual("http://edition.cnn.com/audio/radio/liveaudio.asx", fms[26].Url);
            Assert.AreEqual("CNN", fms[26].Name);
            Assert.AreEqual("CNN (Estados Unidos - Nueva York) Noticias de radio en línea en Inglés", fms[26].Description);
            Assert.AreEqual(true, fms[26].UseMediaPlayer);
        }
        [Test]
        public void GetFmChannel() {
            Fm fm = DBCommon.GetFmChannel(27, (int)Language.Spanish);
            Assert.AreEqual("http://edition.cnn.com/audio/radio/liveaudio.asx", fm.Url);
            Assert.AreEqual("CNN (Estados Unidos - Nueva York) Noticias de radio en línea en Inglés", fm.Description);
            Assert.AreEqual(true, fm.UseMediaPlayer);
        }
    }
}