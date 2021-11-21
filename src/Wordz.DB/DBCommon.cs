using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Wordz.BE;
using Wordz.BE.Dto;
using Wordz.Lng;
using Wordz.LngOpt;
using Wordz.Snd;

namespace Wordz.DB {
    public class DBCommon {
        #region Substitutions

        public static string[,] ComplexSubstitution = new string[,] { {
                                                                          "оо", "о"
                                                                      }, {
                                                                             "ее", "е"
                                                                         },
        };

        public static string[,] SimpleSubstitution = new string[,] { {
                                                                         "а", "a"
                                                                     }, {
                                                                            "б", "bb"
                                                                        }, {
                                                                               "в", "vv"
                                                                           }, {
                                                                                  "г", "gg"
                                                                              }, {
                                                                                     "д", "dd"
                                                                                 }, {
                                                                                        "е", "e"
                                                                                    }, {
                                                                                           "ё", "yo"
                                                                                       }, {
                                                                                              "ж", "jj"
                                                                                          }, {
                                                                                                 "з", "zz"
                                                                                             }, {
                                                                                                    "и", "i"
                                                                                                }, {
                                                                                                       "й", "iy"
                                                                                                   }, {
                                                                                                          "к", "kk"
                                                                                                      }, {
                                                                                                             "л", "ll"
                                                                                                         }, {
                                                                                                                "м", "mm"
                                                                                                            }, {
                                                                                                                   "н", "nn"
                                                                                                               }, {
                                                                                                                      "о", "o"
                                                                                                                  }, {
                                                                                                                         "п", "pp"
                                                                                                                     }, {
                                                                                                                            "р", "rr"
                                                                                                                        }, {
                                                                                                                               "с", "ss"
                                                                                                                           }, {
                                                                                                                                  "т", "tt"
                                                                                                                              }, {
                                                                                                                                     "у", "uu"
                                                                                                                                 }, {
                                                                                                                                        "ф", "ff"
                                                                                                                                    }, {
                                                                                                                                           "х", "hh"
                                                                                                                                       }, {
                                                                                                                                              "ц", "ts"
                                                                                                                                          }, {
                                                                                                                                                 "ч", "tch"
                                                                                                                                             }, {
                                                                                                                                                    "ш", "sh"
                                                                                                                                                }, {
                                                                                                                                                       "щ", "sh"
                                                                                                                                                   }, {
                                                                                                                                                          "ь", "i"
                                                                                                                                                      }, {
                                                                                                                                                             "ы", "yi"
                                                                                                                                                         }, {
                                                                                                                                                                "ъ", "i"
                                                                                                                                                            }, {
                                                                                                                                                                   "э", "ie"
                                                                                                                                                               }, {
                                                                                                                                                                      "ю", "yu"
                                                                                                                                                                  }, {
                                                                                                                                                                         "я", "ya"
                                                                                                                                                                     },
        };

        #endregion

        private DBCommon() {}

        public static void SetSoundPath(string path) {
            Database.SoundPath = path;
        }

        public static void SetConnectionString(string connectionString) {
            Database.ConnectionString = connectionString;
        }

        public static void SetUseSoundFiles(bool useSoundFiles) {
            Database.UseSoundFiles = useSoundFiles;
        }

        public static void SetTranscriptRussianWords(bool transcriptRussianWords) {
            Database.TranscriptRussianWords = transcriptRussianWords;
        }

        public static string TestData(int x) {
            return (++x).ToString();
        }

        public static int GetAccountsCount() {
            Database database = null;
            try {
                database = Database.GetInstance();
                object value = database.pr_account_get_count.ExecuteScalar();
                return int.Parse(value.ToString());
            } finally {
                database.CloseConnection();
            }
        }

        public static int GetWordsCount(int learnLanguageId, int nativeLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_word_get_count.Parameters["@learn_language_id"].Value = learnLanguageId;
                database.pr_word_get_count.Parameters["@native_language_id"].Value = nativeLanguageId;
                object value = database.pr_word_get_count.ExecuteScalar();
                return int.Parse(value.ToString());
            } finally {
                database.CloseConnection();
            }
        }

        public static int GetWordsCountSounded() {
            Database database = null;
            try {
                database = Database.GetInstance();
                object value = database.pr_word_get_count_sounded.ExecuteScalar();
                return int.Parse(value.ToString());
            } finally {
                database.CloseConnection();
            }
        }

        public static int GetWordsCountSoundedFromFile(int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                
                string learnPath = Database.SoundPath + LanguageOptions.GetWordsPath(learnLanguageId);
                string[] learnFileNames = Directory.GetFiles(learnPath);
                int learnFilesCount = learnFileNames.Length;

                string nativePath = Database.SoundPath + LanguageOptions.GetWordsPath(nativeLanguageId);
                string[] nativeFileNames = Directory.GetFiles(nativePath);
                int nativeFilesCount = nativeFileNames.Length;

                return Math.Min(learnFilesCount, nativeFilesCount);
            } finally {
                database.CloseConnection();
            }
        }

        public static int GetWordsCountForAccount(int accountId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_account_word_get_count.Parameters["@account_id"].Value = accountId;
                database.pr_account_word_get_count.Parameters["@language_id"].Value = learnLanguageId;
                object value = database.pr_account_word_get_count.ExecuteScalar();
                return int.Parse(value.ToString());
            } finally {
                database.CloseConnection();
            }
        }

        [Obsolete("Use only for UT and WinApp - with AccountId=1")]
        public static void FillWordsTranslation(ref Hashtable words, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                Hashtable wordsCopy = new Hashtable(words.Count);
                foreach (object key in words.Keys) {
                    WordElement wordElement = (WordElement) words[key];
                    //HACK: Use accountId=1 only for WinForm app
                    FillWordInfo(ref wordElement, 1, nativeLanguageId, learnLanguageId);
                    wordsCopy[key] = wordElement;
                }
                words.Clear();
                words = wordsCopy;
            } finally {
                database.CloseConnection();
            }
        }

        public static void FillWordInfo(ref WordElement wordElement, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                FillWordInfo(ref wordElement, int.MinValue, nativeLanguageId, learnLanguageId);
            } finally {
                database.CloseConnection();
            }
        }

        public static void FillWordInfo(ref WordElement wordElement, int accountId, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_word_get_by_original.Parameters["@original"].Value = wordElement.Original;
                database.pr_word_get_by_original.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_word_get_by_original.Parameters["@learn_language_id"].Value = learnLanguageId;
                using(SqlDataReader reader = database.pr_word_get_by_original.ExecuteReader()){
                    while (reader.Read()) {
                        wordElement.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        wordElement.Translation = reader.GetString(reader.GetOrdinal("translation"));

                        wordElement.Sounded = (Database.UseSoundFiles)
                            ? IsWordFileExists(wordElement.Id, nativeLanguageId, learnLanguageId)
                            : !reader.IsDBNull(reader.GetOrdinal("sounded"));
                    }
                }

                if (wordElement.ExistsInDB) {
                    database.pr_account_word_get.Parameters["@account_id"].Value = accountId;
                    database.pr_account_word_get.Parameters["@word_id"].Value = wordElement.Id;
                    database.pr_account_word_get.Parameters["@language_id"].Value = learnLanguageId;
                    object value = database.pr_account_word_get.ExecuteScalar();
                    wordElement.WellKnown = (value != null);
                }
            } finally {
                database.CloseConnection();
            }
        }

        public static WordElement GetWordInfo(int wordId, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                WordElement wordElement = new WordElement();
                database.pr_word_get_by_id.Parameters["@id"].Value = wordId;
                database.pr_word_get_by_id.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_word_get_by_id.Parameters["@learn_language_id"].Value = learnLanguageId;
                using(SqlDataReader reader = database.pr_word_get_by_id.ExecuteReader()){
                    while (reader.Read()) {
                        wordElement.Original = reader.GetString(reader.GetOrdinal("original"));
                        wordElement.Translation = reader.GetString(reader.GetOrdinal("translation"));
                    }
                }
                return wordElement;
            } finally {
                database.CloseConnection();
            }
        }

        public static int GetWordIdByText(string value, int languageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_word_get_id_by_text.Parameters["@text"].Value = value;
                database.pr_word_get_id_by_text.Parameters["@language_id"].Value = languageId;
                object id = database.pr_word_get_id_by_text.ExecuteScalar();
                return (id != null) ? (int) id : int.MinValue;
            } finally {
                database.CloseConnection();
            }
        }

        public static void AddWordToAccount(int accountId, int wordId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_account_word_ins.Parameters["@account_id"].Value = accountId;
                database.pr_account_word_ins.Parameters["@word_id"].Value = wordId;
                database.pr_account_word_ins.Parameters["@language_id"].Value = learnLanguageId;
                database.pr_account_word_ins.ExecuteNonQuery();
            } finally {
                database.CloseConnection();
            }
        }

        public static void DeleteWordFromAccount(int accountId, int wordId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_account_word_del.Parameters["@account_id"].Value = accountId;
                database.pr_account_word_del.Parameters["@word_id"].Value = wordId;
                database.pr_account_word_del.Parameters["@language_id"].Value = learnLanguageId;
                database.pr_account_word_del.ExecuteNonQuery();
            } finally {
                database.CloseConnection();
            }
        }

        public static byte[] GetWordSound(int wordId, bool learnFirstWord) {
            Database database = null;
            try {
                database = Database.GetInstance();
                return GetWordSound(wordId, learnFirstWord, SoundingType.Both);
            } finally {
                database.CloseConnection();
            }
        }

        public static byte[] GetWordSound(int wordId, bool learnFirstWord, SoundingType soundingType) {
            Database database = null;
            try {
                database = Database.GetInstance();
                byte[] sound = new byte[] {};
                database.pr_word_get_sound_by_id.Parameters["@id"].Value = wordId;

                using(SqlDataReader reader = database.pr_word_get_sound_by_id.ExecuteReader()){
                    while (reader.Read()) {
                        int originalOrdinal = reader.GetOrdinal("original_sound");
                        int translationOrdinal = reader.GetOrdinal("translation_sound");

                        if (reader.IsDBNull(originalOrdinal) || reader.IsDBNull(translationOrdinal)) {
                            break;
                        }

                        long length = reader.GetBytes(originalOrdinal, 0, null, 0, 0);
                        long length2 = reader.GetBytes(translationOrdinal, 0, null, 0, 0);

                        switch (soundingType) {
                            case SoundingType.Both:
                                sound = new byte[length + length2];
                                if (learnFirstWord) {
                                    reader.GetBytes(originalOrdinal, 0, sound, 0, (int) length);
                                    reader.GetBytes(translationOrdinal, 0, sound, (int) length, (int) length2);
                                } else {
                                    reader.GetBytes(translationOrdinal, 0, sound, 0, (int) length2);
                                    reader.GetBytes(originalOrdinal, 0, sound, (int) length2, (int) length);
                                }
                                break;
                            case SoundingType.Learn:
                                sound = new byte[length];
                                reader.GetBytes(originalOrdinal, 0, sound, 0, (int) length);
                                break;
                            case SoundingType.Native:
                                sound = new byte[length2];
                                reader.GetBytes(translationOrdinal, 0, sound, 0, (int) length2);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("soundingType");
                        }
                    }
                }
                return sound;
            } finally {
                database.CloseConnection();
            }
        }

        public static bool IsWordFileExists(int wordId, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                string fileName = wordId + ".mp3";
                string learnPath = Database.SoundPath + LanguageOptions.GetWordsPath(learnLanguageId) + fileName;
                string nativePath = Database.SoundPath + LanguageOptions.GetWordsPath(nativeLanguageId) + fileName;
                return (File.Exists(learnPath) && File.Exists(nativePath));
            } finally {
                database.CloseConnection();
            }
        }

        public static byte[] GetWordSoundFromFile(int wordId, bool learnFirstWord, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                return GetWordSoundFromFile(wordId, "", "", learnFirstWord, SoundingType.Both, nativeLanguageId, learnLanguageId);
            } finally {
                database.CloseConnection();
            }
        }

        public static byte[] GetWordSoundFromFile(int wordId, string wordOriginal, string wordTranslation, bool learnFirstWord, SoundingType soundingType, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                byte[] sound = new byte[] {};
                string fileName = wordId + ".mp3";
                string learnPath = Database.SoundPath + LanguageOptions.GetWordsPath(learnLanguageId) + fileName;
                string nativePath = Database.SoundPath + LanguageOptions.GetWordsPath(nativeLanguageId) + fileName;

                if (!IsWordFileExists(wordId, nativeLanguageId, learnLanguageId)) {
                    return GetWordSoundFromText(wordOriginal, wordTranslation, learnFirstWord);
                }

                FileStream fsEN = new FileStream(learnPath, FileMode.Open, FileAccess.Read);
                BinaryReader rEN = new BinaryReader(fsEN);
                FileStream fsRU = new FileStream(nativePath, FileMode.Open, FileAccess.Read);
                BinaryReader rRU = new BinaryReader(fsRU);

                switch (soundingType) {
                    case SoundingType.Both:
                        sound = new byte[fsEN.Length + fsRU.Length];
                        if (learnFirstWord) {
                            rEN.Read(sound, 0, (int) fsEN.Length);
                            rRU.Read(sound, (int) fsEN.Length, (int) fsRU.Length);
                        } else {
                            rRU.Read(sound, 0, (int) fsRU.Length);
                            rEN.Read(sound, (int) fsRU.Length, (int) fsEN.Length);
                        }
                        break;
                    case SoundingType.Learn:
                        sound = new byte[fsEN.Length];
                        rEN.Read(sound, 0, (int) fsEN.Length);
                        break;
                    case SoundingType.Native:
                        sound = new byte[fsRU.Length];
                        rRU.Read(sound, 0, (int) fsRU.Length);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("soundingType");
                }

                rEN.Close();
                fsEN.Close();
                rRU.Close();
                fsRU.Close();

                return sound;
            } finally {
                database.CloseConnection();
            }
        }

        public static byte[] GetVerbSoundFromFile(int verbId, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                byte[] sound = new byte[] {};
                string fileName = verbId + ".mp3";
                string learnPath = Database.SoundPath + LanguageOptions.VerbsPath
                    + LanguageOptions.GetWordsPath(learnLanguageId) + fileName;
                string nativePath = Database.SoundPath + LanguageOptions.VerbsPath
                    + LanguageOptions.GetWordsPath(nativeLanguageId) + fileName;

                if (!File.Exists(learnPath) || !File.Exists(nativePath)) {
                    return sound;
                }

                FileStream fsEN = new FileStream(learnPath, FileMode.Open, FileAccess.Read);
                BinaryReader rEN = new BinaryReader(fsEN);
                FileStream fsRU = new FileStream(nativePath, FileMode.Open, FileAccess.Read);
                BinaryReader rRU = new BinaryReader(fsRU);

                sound = new byte[fsEN.Length + fsRU.Length];
                rEN.Read(sound, 0, (int) fsEN.Length);
                rRU.Read(sound, (int) fsEN.Length, (int) fsRU.Length);

                rEN.Close();
                fsEN.Close();
                rRU.Close();
                fsRU.Close();

                return sound;
            } finally {
                database.CloseConnection();
            }
        }

        public static byte[] GetWordSoundFromText(string wordOriginal,
            string wordTranslation, bool learnFirstWord) {
            Database database = null;
            try {
                database = Database.GetInstance();
                string word = (learnFirstWord)
                    ? wordOriginal + " " + wordTranslation
                    : wordTranslation + " " + wordOriginal;
                string wordTranscripted = (Database.TranscriptRussianWords)
                    ? GetWordTranscription(word) : word;
                return GetWordSoundFromText(wordTranscripted);
            } finally {
                database.CloseConnection();
            }
        }

        public static byte[] GetWordSoundFromText(string word) {
            Database database = null;
            try {
                database = Database.GetInstance();
                byte[] sound = new byte[] {};
                try {
                    byte[] wav = WavHelper.ConvertSoundToWav(word);
                    sound = MP3Helper.ConvertSoundToMP3(wav);
                } catch /*(Exception e)*/ {
                    //string s = e.Message;
                }
                return sound;
            } finally {
                database.CloseConnection();
            }
        }

        public static string GetWordTranscription(string word) {
            Database database = null;
            try {
                database = Database.GetInstance();
                string result = word;
                for (int i = 0; i < ComplexSubstitution.Length/2; i++) {
                    result = result.Replace(
                        ComplexSubstitution[i, 0],
                        ComplexSubstitution[i, 1]);
                }

                for (int i = 0; i < SimpleSubstitution.Length/2; i++) {
                    result = result.Replace(
                        SimpleSubstitution[i, 0],
                        SimpleSubstitution[i, 1]);
                }

                return result;
            } finally {
                database.CloseConnection();
            }
        }

        public static byte[] GetWordSoundTranslation(int wordId, bool learnSound) {
            Database database = null;
            try {
                database = Database.GetInstance();
                byte[] sound = new byte[] {};
                database.pr_word_get_sound_by_id.Parameters["@id"].Value = wordId;

                using(SqlDataReader reader = database.pr_word_get_sound_by_id.ExecuteReader()){
                    while (reader.Read()) {
                        int originalOrdinal = reader.GetOrdinal("original_sound");
                        int translationOrdinal = reader.GetOrdinal("translation_sound");

                        if (reader.IsDBNull(originalOrdinal) || reader.IsDBNull(translationOrdinal)) {
                            break;
                        }

                        long length = reader.GetBytes(originalOrdinal, 0, null, 0, 0);
                        long length2 = reader.GetBytes(translationOrdinal, 0, null, 0, 0);
                        sound = new byte[(learnSound) ? length : length2];

                        if (learnSound) {
                            reader.GetBytes(originalOrdinal, 0, sound, 0, (int) length);
                        } else {
                            reader.GetBytes(translationOrdinal, 0, sound, 0, (int) length2);
                        }
                    }
                }
                return sound;
            } finally {
                database.CloseConnection();
            }
        }

        public static void CreateFile(byte[] content, string fileName) {
            Database database = null;
            try {
                database = Database.GetInstance();
                FileStream fs = new FileStream(fileName, FileMode.Create);
                BinaryWriter w = new BinaryWriter(fs);
                w.Write(content);
                w.Close();
                fs.Close();
            } finally {
                database.CloseConnection();
            }
        }

        public static IdName[] GetDomains(int nativeLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();

                database.pr_domain_get.Parameters["@language_id"].Value = nativeLanguageId;
                using(SqlDataReader reader = database.pr_domain_get.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        list.Add(new IdName(id, name));
                    }
                }
                return (IdName[]) list.ToArray(typeof (IdName));
            } finally {
                database.CloseConnection();
            }
        }

        public static IdNamePair[] GetWordIdsForDomain(int accountId, int domainId, int wordCount, int wordStartIndex, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();
                database.pr_word_get_by_domain.Parameters["@account_id"].Value = accountId;
                database.pr_word_get_by_domain.Parameters["@domain_id"].Value = domainId;
                database.pr_word_get_by_domain.Parameters["@word_count"].Value = wordCount;
                database.pr_word_get_by_domain.Parameters["@word_start_index"].Value = wordStartIndex;
                database.pr_word_get_by_domain.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_word_get_by_domain.Parameters["@learn_language_id"].Value = learnLanguageId;

                using(SqlDataReader reader = database.pr_word_get_by_domain.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string original = reader.GetString(reader.GetOrdinal("original"));
                        string translation = reader.GetString(reader.GetOrdinal("translation"));
                        list.Add(new IdNamePair(id, original, translation));
                    }
                }
                return (IdNamePair[]) list.ToArray(typeof (IdNamePair));
            } finally {
                database.CloseConnection();
            }
        }

        public static IdNamePair[] GetWordIdsRandom(int accountId, int wordCount, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();
                database.pr_word_get_by_random.Parameters["@account_id"].Value = accountId;
                database.pr_word_get_by_random.Parameters["@word_count"].Value = wordCount;
                database.pr_word_get_by_random.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_word_get_by_random.Parameters["@learn_language_id"].Value = learnLanguageId;

                using(SqlDataReader reader = database.pr_word_get_by_random.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string original = reader.GetString(reader.GetOrdinal("original"));
                        string translation = reader.GetString(reader.GetOrdinal("translation"));
                        list.Add(new IdNamePair(id, original, translation));
                    }
                }
                return (IdNamePair[]) list.ToArray(typeof (IdNamePair));
            } finally {
                database.CloseConnection();
            }
        }

        public static IdNamePair[] GetWordIdsOrdered(int accountId, int wordCount, int wordStartIndex, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();
                database.pr_word_get_by_ordered.Parameters["@account_id"].Value = accountId;
                database.pr_word_get_by_ordered.Parameters["@word_count"].Value = wordCount;
                database.pr_word_get_by_ordered.Parameters["@word_start_index"].Value = wordStartIndex;
                database.pr_word_get_by_ordered.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_word_get_by_ordered.Parameters["@learn_language_id"].Value = learnLanguageId;

                using(SqlDataReader reader = database.pr_word_get_by_ordered.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string original = reader.GetString(reader.GetOrdinal("original"));
                        string translation = reader.GetString(reader.GetOrdinal("translation"));
                        list.Add(new IdNamePair(id, original, translation));
                    }
                }
                return (IdNamePair[]) list.ToArray(typeof (IdNamePair));
            } finally {
                database.CloseConnection();
            }
        }

        public static IdNamePair[] GetWordIdsForAccount(int accountId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();
                database.pr_account_word_get_list.Parameters["@account_id"].Value = accountId;
                database.pr_account_word_get_list.Parameters["@language_id"].Value = learnLanguageId;

                using(SqlDataReader reader = database.pr_account_word_get_list.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string original = reader.GetString(reader.GetOrdinal("original"));
                        list.Add(new IdNamePair(id, original, ""));
                    }
                }
                return (IdNamePair[]) list.ToArray(typeof (IdNamePair));
            } finally {
                database.CloseConnection();
            }
        }

        public static VerbElement[] GetVerbs(int accountId, bool loadPopular, bool notUseWellKnownVerbs, int wordCount) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();
                database.pr_verb_get_list.Parameters["@account_id"].Value = accountId;
                database.pr_verb_get_list.Parameters["@load_popular"].Value = loadPopular;
                database.pr_verb_get_list.Parameters["@not_use_well_known_verbs"].Value = notUseWellKnownVerbs;
                database.pr_verb_get_list.Parameters["@word_count"].Value = wordCount;

                using(SqlDataReader reader = database.pr_verb_get_list.ExecuteReader()){
                    Random random = new Random();
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        int verbType = reader.GetInt32(reader.GetOrdinal("verb_type_id"));
                        string form1 = reader.GetString(reader.GetOrdinal("form1"));
                        string form2 = reader.GetString(reader.GetOrdinal("form2"));
                        string form3 = reader.GetString(reader.GetOrdinal("form3"));
                        string translation = reader.GetString(reader.GetOrdinal("translation"));
                        list.Add(new VerbElement(id, form1, form2, form3, translation, verbType, random.Next()));
                    }
                }
                return (VerbElement[]) list.ToArray(typeof (VerbElement));
            } finally {
                database.CloseConnection();
            }
        }

        public static void DeleteAllVerbsInAccount(int accountId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_account_verb_del_list.Parameters["@account_id"].Value = accountId;
                database.pr_account_verb_del_list.ExecuteNonQuery();
            } finally {
                database.CloseConnection();
            }
        }

        public static void AddVerbToAccount(int accountId, int verbId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_account_verb_ins.Parameters["@account_id"].Value = accountId;
                database.pr_account_verb_ins.Parameters["@verb_id"].Value = verbId;
                database.pr_account_verb_ins.ExecuteNonQuery();
            } finally {
                database.CloseConnection();
            }
        }

        public static string GetAccountVerbsInfo(int accountId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                int iKnow = 0, percentOfPopular = 0, percentOfTotal = 0;

                database.pr_account_verb_get_info.Parameters["@account_id"].Value = accountId;

                using(SqlDataReader reader = database.pr_account_verb_get_info.ExecuteReader()){
                    while (reader.Read()) {
                        iKnow = reader.GetInt32(reader.GetOrdinal("iKnow"));
                        percentOfPopular = reader.GetInt32(reader.GetOrdinal("percentOfPopular"));
                        percentOfTotal = reader.GetInt32(reader.GetOrdinal("percentOfTotal"));
                    }
                }
                return string.Format("{0}|{1}%|{2}%", iKnow, percentOfPopular, percentOfTotal);
            } finally {
                database.CloseConnection();
            }
        }

        public static void AddWordToDictionary(WordElement wordElement, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_word_ins.Parameters["@original"].Value = wordElement.Original;
                database.pr_word_ins.Parameters["@translation"].Value = wordElement.Translation;
                database.pr_word_ins.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_word_ins.Parameters["@learn_language_id"].Value = learnLanguageId;
                database.pr_word_ins.ExecuteNonQuery();
            } finally {
                database.CloseConnection();
            }
        }
        public static void AddWordToDictionaryUseExist(WordElement wordElement, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_word_ins_use_exist.Parameters["@original"].Value = wordElement.Original;
                database.pr_word_ins_use_exist.Parameters["@translation"].Value = wordElement.Translation;
                database.pr_word_ins_use_exist.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_word_ins_use_exist.Parameters["@learn_language_id"].Value = learnLanguageId;
                database.pr_word_ins_use_exist.ExecuteNonQuery();
            } catch(Exception ex) {
                string s = ex.Message;s+="";
            } finally {
                database.CloseConnection();
            }
        }

        public static void UpdateWordInDictionary(WordElement wordElement, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_word_upd.Parameters["@id"].Value = wordElement.Id;
                database.pr_word_upd.Parameters["@original"].Value = wordElement.Original;
                database.pr_word_upd.Parameters["@translation"].Value = wordElement.Translation;
                database.pr_word_upd.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_word_upd.Parameters["@learn_language_id"].Value = learnLanguageId;
                database.pr_word_upd.ExecuteNonQuery();
            } finally {
                database.CloseConnection();
            }
        }

        public static IdName[] GetSites() {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();

                using(SqlDataReader reader = database.pr_site_get.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("url"));
                        list.Add(new IdName(id, name));
                    }
                }
                return (IdName[]) list.ToArray(typeof (IdName));
            } finally {
                database.CloseConnection();
            }
        }

        public static IdName[] GetCategories(int nativeLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();

                database.pr_category_get.Parameters["@language_id"].Value = nativeLanguageId;
                using(SqlDataReader reader = database.pr_category_get.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        list.Add(new IdName(id, name));
                    }
                }
                return (IdName[]) list.ToArray(typeof (IdName));
            } finally {
                database.CloseConnection();
            }
        }

        public static Article[] GetArticles(int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();

                database.pr_article_get.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_article_get.Parameters["@learn_language_id"].Value = learnLanguageId;
                using(SqlDataReader reader = database.pr_article_get.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string title = reader.GetString(reader.GetOrdinal("title"));
                        int category_id = reader.GetInt32(reader.GetOrdinal("category_id"));
                        list.Add(new Article(id, title, category_id));
                    }
                }
                return (Article[]) list.ToArray(typeof (Article));
            } finally {
                database.CloseConnection();
            }
        }

        public static Article GetArticle(int id) {
            Database database = null;
            try {
                database = Database.GetInstance();
                Article article = new Article();

                database.pr_article_get_by_id.Parameters["@id"].Value = id;
                using(SqlDataReader reader = database.pr_article_get_by_id.ExecuteReader()){
                    while (reader.Read()) {
                        article.Title = reader.GetString(reader.GetOrdinal("title"));
                        article.Body = reader.GetString(reader.GetOrdinal("body"));
                        article.SiteUrl = reader.GetString(reader.GetOrdinal("site_url"));
                    }
                }
                return article;
            } finally {
                database.CloseConnection();
            }
        }

        public static IdName[] GetVerbTypes() {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();

                using(SqlDataReader reader = database.pr_verb_type_get_list.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("rule"));
                        list.Add(new IdName(id, name));
                    }
                }
                return (IdName[]) list.ToArray(typeof (IdName));
            } finally {
                database.CloseConnection();
            }
        }

        public static IdNamePair[] GetTOPUsersByVocabulary(int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();
                database.pr_account_word_get_top.Parameters["@language_id"].Value = learnLanguageId;
                using(SqlDataReader reader = database.pr_account_word_get_top.ExecuteReader()){
                    int number = 0;
                    while (reader.Read()) {
                        string nick = reader.GetString(reader.GetOrdinal("nick"));
                        int count = reader.GetInt32(reader.GetOrdinal("count"));
                        list.Add(new IdNamePair(++number, nick, count.ToString()));
                    }
                }
                return (IdNamePair[]) list.ToArray(typeof (IdNamePair));
            } finally {
                database.CloseConnection();
            }
        }

        public static IdNamePair[] GetTOPUsersByVerbs() {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();
                using(SqlDataReader reader = database.pr_account_verb_get_top.ExecuteReader()){
                    int number = 0;
                    while (reader.Read()) {
                        string nick = reader.GetString(reader.GetOrdinal("nick"));
                        int count = reader.GetInt32(reader.GetOrdinal("count"));
                        list.Add(new IdNamePair(++number, nick, count.ToString()));
                    }
                }
                return (IdNamePair[]) list.ToArray(typeof (IdNamePair));
            } finally {
                database.CloseConnection();
            }
        }

        public static IdNamePair[] GetTOPUsersByDictionaryQuiz(int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();
                database.pr_word_quiz_get_top.Parameters["@language_id"].Value = learnLanguageId;
                using(SqlDataReader reader = database.pr_word_quiz_get_top.ExecuteReader()){
                    int number = 0;
                    while (reader.Read()) {
                        string nick = reader.GetString(reader.GetOrdinal("nick"));
                        byte result = reader.GetByte(reader.GetOrdinal("result"));
                        list.Add(new IdNamePair(++number, nick, result.ToString()));
                    }
                }
                return (IdNamePair[]) list.ToArray(typeof (IdNamePair));
            } finally {
                database.CloseConnection();
            }
        }

        public static int GetDictionaryQuizPlace(int accountId, string nick, int successCount, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_word_quiz_get_place.Parameters["@account_id"].Value = null;
                if (accountId != Account.EmptyId) {
                    database.pr_word_quiz_get_place.Parameters["@account_id"].Value = accountId;
                }
                database.pr_word_quiz_get_place.Parameters["@nick"].Value = nick;
                database.pr_word_quiz_get_place.Parameters["@success_count"].Value = successCount;
                database.pr_word_quiz_get_place.Parameters["@language_id"].Value = learnLanguageId;
                object value = database.pr_word_quiz_get_place.ExecuteScalar();
                return int.Parse(value.ToString());
            } finally {
                database.CloseConnection();
            }
        }

        public static IdNamePair[] GetTOPUsersByWordsOrderQuiz(int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();
                database.pr_word_order_quiz_get_top.Parameters["@language_id"].Value = learnLanguageId;
                using(SqlDataReader reader = database.pr_word_order_quiz_get_top.ExecuteReader()){
                    int number = 0;
                    while (reader.Read()) {
                        string nick = reader.GetString(reader.GetOrdinal("nick"));
                        byte result = reader.GetByte(reader.GetOrdinal("result"));
                        list.Add(new IdNamePair(++number, nick, result.ToString()));
                    }
                }
                return (IdNamePair[]) list.ToArray(typeof (IdNamePair));
            } finally {
                database.CloseConnection();
            }
        }

        public static int GetWordsOrderQuizPlace(int accountId, string nick, int successCount, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_word_order_quiz_get_place.Parameters["@account_id"].Value = null;
                if (accountId != Account.EmptyId) {
                    database.pr_word_order_quiz_get_place.Parameters["@account_id"].Value = accountId;
                }
                database.pr_word_order_quiz_get_place.Parameters["@nick"].Value = nick;
                database.pr_word_order_quiz_get_place.Parameters["@success_count"].Value = successCount;
                database.pr_word_order_quiz_get_place.Parameters["@language_id"].Value = learnLanguageId;
                object value = database.pr_word_order_quiz_get_place.ExecuteScalar();
                return int.Parse(value.ToString());
            } finally {
                database.CloseConnection();
            }
        }

        public static string GetWordsOrderSentence(int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_word_order_get_random.Parameters["@language_id"].Value = learnLanguageId;
                object value = database.pr_word_order_get_random.ExecuteScalar();
                return (value != null) ? value.ToString() : CurrentLanguage.MessageNoSentences;
            } finally {
                database.CloseConnection();
            }
        }

		public static IdNamePair[] GetTOPUsersByLevelQuiz(int learnLanguageId) {
			Database database = null;
			try {
				database = Database.GetInstance();
				ArrayList list = new ArrayList();
				database.pr_level_quiz_get_top.Parameters["@language_id"].Value = learnLanguageId;
				using(SqlDataReader reader = database.pr_level_quiz_get_top.ExecuteReader()){
					int number = 0;
					while (reader.Read()) {
						string nick = reader.GetString(reader.GetOrdinal("nick"));
						byte result = reader.GetByte(reader.GetOrdinal("result"));
						list.Add(new IdNamePair(++number, nick, result.ToString()));
					}
				}
				return (IdNamePair[]) list.ToArray(typeof (IdNamePair));
			} finally {
				database.CloseConnection();
			}
		}

		public static int GetLevelQuizPlace(int accountId, string nick, int successCount, int learnLanguageId) {
			Database database = null;
			try {
				database = Database.GetInstance();
				database.pr_level_quiz_get_place.Parameters["@account_id"].Value = null;
				if (accountId != Account.EmptyId) {
					database.pr_level_quiz_get_place.Parameters["@account_id"].Value = accountId;
				}
				database.pr_level_quiz_get_place.Parameters["@nick"].Value = nick;
				database.pr_level_quiz_get_place.Parameters["@success_count"].Value = successCount;
				database.pr_level_quiz_get_place.Parameters["@language_id"].Value = learnLanguageId;
				object value = database.pr_level_quiz_get_place.ExecuteScalar();
				return int.Parse(value.ToString());
			} finally {
				database.CloseConnection();
			}
		}

		public static Level GetLevelSentence(int learnLanguageId) {
			Database database = null;
			try {
				database = Database.GetInstance();
				Level level = new Level();
				database.pr_level_get_random.Parameters["@language_id"].Value = learnLanguageId;
				using(SqlDataReader reader = database.pr_level_get_random.ExecuteReader()){
					while (reader.Read()) {
						level.Sentence = reader.GetString(reader.GetOrdinal("sentence"));
						level.Answer1 = reader.GetString(reader.GetOrdinal("answer1"));
						level.Answer2 = reader.GetString(reader.GetOrdinal("answer2"));
						level.Answer3 = reader.GetString(reader.GetOrdinal("answer3"));
						level.Correct = reader.GetByte(reader.GetOrdinal("correct"));
					}
				}
				return level;
			} finally {
				database.CloseConnection();
			}
		}

        public static int[] GetWordsUnsoundedShort(int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();

                database.pr_word_get_list_unsounded_short.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_word_get_list_unsounded_short.Parameters["@learn_language_id"].Value = learnLanguageId;
                using(SqlDataReader reader = database.pr_word_get_list_unsounded_short.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        list.Add(id);
                    }
                }
                return (int[]) list.ToArray(typeof (int));
            } finally {
                database.CloseConnection();
            }
        }

        public static void FilmClearAll() {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_film_clear_all.ExecuteNonQuery();
            } finally {
                database.CloseConnection();
            }
        }

        public static void AddFilmPart(int id, int number, string url) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_film_part_ins.Parameters["@id"].Value = id;
                database.pr_film_part_ins.Parameters["@number"].Value = number;
                database.pr_film_part_ins.Parameters["@url"].Value = url;
                database.pr_film_part_ins.ExecuteNonQuery();
            } finally {
                database.CloseConnection();
            }
        }

        public static void AddFilm(Film film, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                database.pr_film_ins.Parameters["@id"].Value = film.Id;
                database.pr_film_ins.Parameters["@url"].Value = film.Url;
                database.pr_film_ins.Parameters["@name"].Value = film.Name;
                database.pr_film_ins.Parameters["@category"].Value = film.Category;
                database.pr_film_ins.Parameters["@player_pattern"].Value =
                    film.PlayerPattern.Replace(
                    "width=\"620\" height=\"480\"",
                    "width=\"99%\" height=\"99%\"");
                database.pr_film_ins.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_film_ins.Parameters["@learn_language_id"].Value = learnLanguageId;
                database.pr_film_ins.ExecuteNonQuery();
            } finally {
                database.CloseConnection();
            }
        }

        public static IdName[] GetFilmCategories(int nativeLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();
				list.Add(new IdName(0, "*.*"));

                database.pr_film_category_get_list.Parameters["@language_id"].Value = nativeLanguageId;
                using(SqlDataReader reader = database.pr_film_category_get_list.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        list.Add(new IdName(id, name));
                    }
                }
                return (IdName[]) list.ToArray(typeof (IdName));
            } finally {
                database.CloseConnection();
            }
        }

        public static FilmListItemDto[] GetFilmsByCategory(int accountId, int categoryId, int nativeLanguageId, int learnLanguageId)
        {
            Database database = null;
            try {
                database = Database.GetInstance();
                var list = new List<FilmListItemDto>();

                database.pr_film_get_list_by_category.Parameters["@account_id"].Value = accountId;
                database.pr_film_get_list_by_category.Parameters["@category_id"].Value = categoryId;
                database.pr_film_get_list_by_category.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_film_get_list_by_category.Parameters["@learn_language_id"].Value = learnLanguageId;
                using(SqlDataReader reader = database.pr_film_get_list_by_category.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetValueOrDefault<string>("name");
                        string category = reader.GetValueOrDefault<string>("category");
                        string imageURL = reader.GetValueOrDefault<string>("image_url");
                        int? authorId = reader.GetValueOrDefault<int?>("account_id");
                        list.Add(new FilmListItemDto{Id = id, Name = name, CategoryName = category, ImageUrl = imageURL, AccountId = authorId});
                    }
                }
                return list.ToArray();
            } finally {
                database.CloseConnection();
            }
        }

        public static FilmListItemDto[] GetFilmsBySearch(int accountId, string search, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                var list = new List<FilmListItemDto>();

                database.pr_film_get_list_by_search.Parameters["@account_id"].Value = accountId;
                database.pr_film_get_list_by_search.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_film_get_list_by_search.Parameters["@learn_language_id"].Value = learnLanguageId;
                database.pr_film_get_list_by_search.Parameters["@search"].Value =
                    (search.Length <= 10)
                    ? search
                    : search.Substring(0, 10);
                using(SqlDataReader reader = database.pr_film_get_list_by_search.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string film = reader.GetValueOrDefault<string>("film");
                        string category = reader.GetValueOrDefault<string>("category");
                        string imageURL = reader.GetValueOrDefault<string>("image_url");
                        int? authorId = reader.GetValueOrDefault<int?>("account_id");
                        list.Add(new FilmListItemDto
                                     {
                                         Id = id,
                                         Name = film,
                                         CategoryName = category,
                                         ImageUrl = imageURL,
                                         AccountId = authorId
                                     });
                    }
                }
                return list.ToArray();
            } finally {
                database.CloseConnection();
            }
        }

        public static FilmWithCategoryAndOrderDto GetFilm(int id, int nativeLanguageId)
        {
            Database database = null;
            try {
                database = Database.GetInstance();
                FilmWithCategoryAndOrderDto film = null;

                database.pr_film_get.Parameters["@id"].Value = id;
				database.pr_film_get.Parameters["@native_language_id"].Value = nativeLanguageId;
                using(SqlDataReader reader = database.pr_film_get.ExecuteReader()){
                    while (reader.Read())
                    {
                        film = new FilmWithCategoryAndOrderDto
                                   {
                                       Id = reader.GetValueOrDefault<int>("id"),
                                       Name = reader.GetValueOrDefault<string>("name"),
                                       Url = reader.GetValueOrDefault<string>("url"),
                                       PlayerPattern = reader.GetValueOrDefault<string>("pattern"),
									   CategoryName = "",//TODO: fix sp (no return field category_name): reader.GetValueOrDefault<string>("category_name")
                                       CategoryId = reader.GetValueOrDefault<int?>("film_category_id"),
                                       AccountId = reader.GetValueOrDefault<int?>("account_id"),
                                       Description = reader.GetValueOrDefault<string>(Film.DescriptionDbPropertyName),
                                       PlayerCode = reader.GetValueOrDefault<string>(Film.PlayerCodeDbPropertyName),
                                       ImageUrl = reader.GetValueOrDefault<string>(Film.ImageUrlDbPropertyName)
                                   };
                    }
                    reader.NextResult();
                    while (reader.Read()) {
                        string url = reader.GetString(reader.GetOrdinal("url"));
                        film.AddPartUrl(url);
                    }
                }
                return film;
            } finally {
                database.CloseConnection();
            }
        }

        public static IdName[] GetLanguages() {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();

                using(SqlDataReader reader = database.pr_language_get_list.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        list.Add(new IdName(id, name));
                    }
                }
                return (IdName[]) list.ToArray(typeof (IdName));
            } finally {
                database.CloseConnection();
            }
        }

        public static TvWithOrderDto[] GetTvChannels(int accountId, int nativeLanguageId, int learnLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();

                database.pr_tv_get_list.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_tv_get_list.Parameters["@learn_language_id"].Value = learnLanguageId;
                database.pr_tv_get_list.Parameters["@account_id"].Value = accountId;
                using(SqlDataReader reader = database.pr_tv_get_list.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string imageUrl = reader.GetString(reader.GetOrdinal("image_url"));
                        string url = reader.GetString(reader.GetOrdinal("url"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        string description = reader.GetString(reader.GetOrdinal("description"));
                        int? authorId = reader.GetValueOrDefault<int?>("account_id");
                        bool isEditable = reader.GetValueOrDefault<bool>("is_editable");
                        int orderInList = reader.GetValueOrDefault<int>("order_in_list");
                        list.Add(new TvWithOrderDto
                                     {
                                         AccountId = authorId,
                                         Description = description,
                                         Id = id,
                                         ImageUrl = imageUrl,
                                         IsEditable = isEditable,
                                         Name = name,
                                         OrderInList = orderInList,
                                         Url = url
                                     });
                    }
                }
                return (TvWithOrderDto[]) list.ToArray(typeof (TvWithOrderDto));
            } finally {
                database.CloseConnection();
            }
        }

        public static Tv[] GetOtherTvChannels(int accountId, int nativeLanguageId, int learnLanguageId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var listOfTv = new List<Tv>();
                var command = database.pr_tv_get_other_list;
                var cmdParams = command.Parameters;
                cmdParams["@native_language_id"].Value = nativeLanguageId;
                cmdParams["@learn_language_id"].Value = learnLanguageId;
                cmdParams["@account_id"].Value = accountId;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string imageUrl = reader.GetString(reader.GetOrdinal("image_url"));
                        string url = reader.GetString(reader.GetOrdinal("url"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        string description = reader.GetString(reader.GetOrdinal("description"));
                        int? authorId = reader.GetValueOrDefault<int?>("account_id");
                        bool isEditable = reader.GetValueOrDefault<bool>("is_editable");
                        listOfTv.Add(new Tv
                        {
                            AccountId = authorId,
                            Description = description,
                            Id = id,
                            ImageUrl = imageUrl,
                            IsEditable = isEditable,
                            Name = name,
                            Url = url
                        });
                    }
                }
                return listOfTv.ToArray();
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public static int UpdateTvChannel(int accountId, TvUpdateDto tv)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();
                var command = database.pr_tv_update_or_insert;
                var cmdParams = command.Parameters;
                cmdParams[Tv.AccountIdDbPropertyName.AsDbParam()].Value = accountId;
                cmdParams[Tv.DescriptionDbPropertyName.AsDbParam()].Value = tv.Description;
                cmdParams[Tv.IdDbPropertyName.AsDbParam()].Value = tv.Id;
                cmdParams[Tv.ImageUrlDbPropertyName.AsDbParam()].Value = tv.ImageUrl;
                cmdParams[Tv.IsEditableDbPropertyName.AsDbParam()].Value = tv.IsEditable;
                cmdParams[Tv.NameDbPropertyName.AsDbParam()].Value = tv.Name;
                cmdParams[Tv.UrlDbPropertyName.AsDbParam()].Value = tv.Url;
                cmdParams[TvUpdateDto.NativeLanguageDbPropertyName.AsDbParam()].Value = tv.NativeLanguageId;
                cmdParams[TvUpdateDto.LearnLanguageDbPropertyName.AsDbParam()].Value = tv.LearnLanguageId;
                resultId = command.ExecuteScalar() as int?;
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId.HasValue ? resultId.Value : -1;
        }

        public static Tv GetTvChannel(int id, int nativeLanguageId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();

                database.pr_tv_get.Parameters["@id"].Value = id;
                database.pr_tv_get.Parameters["@native_language_id"].Value = nativeLanguageId;
                using (SqlDataReader reader = database.pr_tv_get.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Tv
                                   {
                                       Id = id,
                                       Url = reader.GetString(reader.GetOrdinal(Tv.UrlDbPropertyName)),
                                       Description = reader.GetString(reader.GetOrdinal(Tv.DescriptionDbPropertyName)),
                                       AccountId = reader.GetValueOrDefault<int?>(Tv.AccountIdDbPropertyName),
                                       ImageUrl = reader.GetValueOrDefault<string>(Tv.ImageUrlDbPropertyName),
                                       IsEditable = reader.GetValueOrDefault<bool>(Tv.IsEditableDbPropertyName),
                                       Name = reader.GetValueOrDefault<string>(Tv.NameDbPropertyName)
                                   };
                    }
                }
            }
            finally
            {
                database.CloseConnection();
            }
            return null;
        }

        public static int DeleteTvChannel(int accountId, int tvId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var command = database.pr_tv_delete;
                var cmdParams = command.Parameters;
                cmdParams["@account_id"].Value = accountId;
                cmdParams["@tv_id"].Value = tvId;
                var result = command.ExecuteScalar() as int?;
                if (result.HasValue)
                {
                    return result.Value;
                }
            }
            finally
            {
                database.CloseConnection();
            }
            return -1;
        }

        public static int UpdateTvChannelOrder(int accountId, EntityOrderingUpdateInfoDto info)
        {
            Database database = null;
            try
            {
                var orderingString = string.Join(";", info.OrderingInfo.Select(oi => oi.Id.ToString() + "=" + oi.OrderIndex.ToString()));
                
                database = Database.GetInstance();
                var command = database.pr_tv_update_order;
                var cmdParams = command.Parameters;
                cmdParams["@account_id"].Value = accountId;
                cmdParams["@ordering_string"].Value = orderingString;
                var result = command.ExecuteScalar() as int?;
                if (result.HasValue)
                {
                    return result.Value;
                }
            }
            finally
            {
                database.CloseConnection();
            }
            return -1;
        }

        public static FmWithOrderDto[] GetFmChannels(int accountId, int nativeLanguageId, int learnLanguageId)
        {
            Database database = null;
            try {
                database = Database.GetInstance();
                ArrayList list = new ArrayList();

                database.pr_fm_get_list.Parameters["@native_language_id"].Value = nativeLanguageId;
                database.pr_fm_get_list.Parameters["@learn_language_id"].Value = learnLanguageId;
                database.pr_fm_get_list.Parameters["@account_id"].Value = accountId;
                using(SqlDataReader reader = database.pr_fm_get_list.ExecuteReader()){
                    while (reader.Read()) {
                        int id = reader.GetInt32(reader.GetOrdinal("id"));
                        string imageUrl = reader.GetString(reader.GetOrdinal("image_url"));
                        string url = reader.GetString(reader.GetOrdinal("url"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        string description = reader.GetString(reader.GetOrdinal("description"));
                        int? authorId = reader.GetValueOrDefault<int?>("account_id");
                        bool isEditable = reader.GetValueOrDefault<bool>("is_editable");
                        bool useMediaPlayer = reader.GetBoolean(reader.GetOrdinal("use_media_player"));
                        int orderInList = reader.GetValueOrDefault<int>("order_in_list");
                        list.Add(new FmWithOrderDto(id, imageUrl, url, name, description, useMediaPlayer, authorId, isEditable, orderInList));
                    }
                }
                return (FmWithOrderDto[]) list.ToArray(typeof (FmWithOrderDto));
            } finally {
                database.CloseConnection();
            }
        }

        public static Fm[] GetOtherFmChannels(int accountId, int nativeLanguageId, int learnLanguageId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var listOfTv = new List<Fm>();
                var command = database.pr_fm_get_other_list;
                var cmdParams = command.Parameters;
                cmdParams["@native_language_id"].Value = nativeLanguageId;
                cmdParams["@learn_language_id"].Value = learnLanguageId;
                cmdParams["@account_id"].Value = accountId;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal(Fm.IdDbPropertyName));
                        string imageUrl = reader.GetString(reader.GetOrdinal(Fm.ImageUrlDbPropertyName));
                        string url = reader.GetString(reader.GetOrdinal(Fm.UrlDbPropertyName));
                        string name = reader.GetString(reader.GetOrdinal(Fm.NameDbPropertyName));
                        string description = reader.GetString(reader.GetOrdinal(Fm.DescriptionDbPropertyName));
                        int? authorId = reader.GetValueOrDefault<int?>(Fm.AccountIdDbPropertyName);
                        bool isEditable = reader.GetValueOrDefault<bool>(Fm.IsEditableDbPropertyName);
                        bool useMediaPlayer = reader.GetValueOrDefault<bool>(Fm.UseMediaPlayerDbPropertyName);
                        listOfTv.Add(new Fm
                        {
                            AccountId = authorId,
                            Description = description,
                            Id = id,
                            ImageUrl = imageUrl,
                            IsEditable = isEditable,
                            Name = name,
                            Url = url,
                            UseMediaPlayer = useMediaPlayer
                        });
                    }
                }
                return listOfTv.ToArray();
            }
            finally
            {
                database.CloseConnection();
            }
        }

        public static Fm GetFmChannel(int id, int nativeLanguageId) {
            Database database = null;
            try {
                database = Database.GetInstance();
                Fm fm = null;

                database.pr_fm_get.Parameters["@id"].Value = id;
                database.pr_fm_get.Parameters["@native_language_id"].Value = nativeLanguageId;
                using(SqlDataReader reader = database.pr_fm_get.ExecuteReader()){
                    while (reader.Read()) {
                        fm = new Fm
                                 {
                                     Id = id,
                                     Description = reader.GetString(reader.GetOrdinal(Fm.DescriptionDbPropertyName)),
                                     UseMediaPlayer =
                                         reader.GetBoolean(reader.GetOrdinal(Fm.UseMediaPlayerDbPropertyName)),
                                     ImageUrl = reader.GetString(reader.GetOrdinal(Fm.ImageUrlDbPropertyName)),
                                     Url = reader.GetString(reader.GetOrdinal(Fm.UrlDbPropertyName)),
                                     Name = reader.GetString(reader.GetOrdinal(Fm.NameDbPropertyName)),
                                     AccountId = reader.GetValueOrDefault<int?>(Fm.AccountIdDbPropertyName),
                                     IsEditable = reader.GetValueOrDefault<bool>(Fm.IsEditableDbPropertyName)
                                 };
                        break;
                    }
                }
                return fm;
            } finally {
                database.CloseConnection();
            }
        }

        public static int UpdateFmChannel(int accountId, FmUpdateDto fm)
        {
            Database database = null;
            int? resultId;
            try
            {
                database = Database.GetInstance();
                var command = database.pr_fm_update_or_insert;
                var cmdParams = command.Parameters;
                cmdParams[Fm.AccountIdDbPropertyName.AsDbParam()].Value = accountId;
                cmdParams[Fm.DescriptionDbPropertyName.AsDbParam()].Value = fm.Description;
                cmdParams[Fm.IdDbPropertyName.AsDbParam()].Value = fm.Id;
                cmdParams[Fm.ImageUrlDbPropertyName.AsDbParam()].Value = fm.ImageUrl;
                cmdParams[Fm.IsEditableDbPropertyName.AsDbParam()].Value = fm.IsEditable;
                cmdParams[Fm.NameDbPropertyName.AsDbParam()].Value = fm.Name;
                cmdParams[Fm.UrlDbPropertyName.AsDbParam()].Value = fm.Url;
                cmdParams[Fm.UseMediaPlayerDbPropertyName.AsDbParam()].Value = fm.UseMediaPlayer;
                cmdParams[FmUpdateDto.NativeLanguageDbPropertyName.AsDbParam()].Value = fm.NativeLanguageId;
                cmdParams[FmUpdateDto.LearnLanguageDbPropertyName.AsDbParam()].Value = fm.LearnLanguageId;
                resultId = command.ExecuteScalar() as int?;
            }
            finally
            {
                database.CloseConnection();
            }

            return resultId.HasValue ? resultId.Value : -1;
        }

        public static int DeleteFmChannel(int accountId, int fmId)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var command = database.pr_fm_delete;
                var cmdParams = command.Parameters;
                cmdParams["@account_id"].Value = accountId;
                cmdParams["@fm_id"].Value = fmId;
                var result = command.ExecuteScalar() as int?;
                if (result.HasValue)
                {
                    return result.Value;
                }
            }
            finally
            {
                database.CloseConnection();
            }
            return -1;
        }

        public static int UpdateFmChannelOrder(int accountId, EntityOrderingUpdateInfoDto info)
        {
            Database database = null;
            try
            {
                var orderingString = string.Join(";", info.OrderingInfo.Select(oi => oi.Id.ToString() + "=" + oi.OrderIndex.ToString()));

                database = Database.GetInstance();
                var command = database.pr_fm_update_order;
                var cmdParams = command.Parameters;
                cmdParams["@account_id"].Value = accountId;
                cmdParams["@ordering_string"].Value = orderingString;
                var result = command.ExecuteScalar() as int?;
                if (result.HasValue)
                {
                    return result.Value;
                }
            }
            finally
            {
                database.CloseConnection();
            }
            return -1;
        }

		public static void UpdateFilmAvailability(int filmId, bool status)
		{
			Database database = null;
			try
			{
				database = Database.GetInstance();
				database.pr_film_upd_status.Parameters["@id"].Value = filmId;
				database.pr_film_upd_status.Parameters["@status"].Value = status;
				database.pr_film_upd_status.ExecuteNonQuery();
			}
			finally
			{
				database.CloseConnection();
			}
		}

        public static ExerciseText GetExerciseText(int id)
        {
            Database database = null;
            try
            {
                database = Database.GetInstance();
                var exerciseText = new ExerciseText();
                database.pr_exercise_text_get.Parameters["@id"].Value = id;
                using (SqlDataReader reader = database.pr_exercise_text_get.ExecuteReader())
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            exerciseText.Id = reader.GetInt32(reader.GetOrdinal("id"));
                            exerciseText.Name = reader.GetString(reader.GetOrdinal("name"));
                        }
                    }
                }
                return exerciseText;
            }
            finally
            {
                database.CloseConnection();
            }
        }
    }
}