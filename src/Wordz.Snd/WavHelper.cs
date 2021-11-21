using System;
using System.Threading;
using SpeechLib;

namespace Wordz.Snd {
    public class WavHelper {
        public WavHelper() {}

        public static byte[] ConvertSoundToWav(string word) {
                try {
                SpeechVoiceSpeakFlags flags = SpeechVoiceSpeakFlags.SVSFlagsAsync;
                SpVoice speech = new SpVoice();
                SpMemoryStream stream = new SpMemoryStream();
                speech.AudioOutputStream = stream;

                //lock (typeof(SpVoice)) {
                    speech.Speak(word, flags);
                    speech.WaitUntilDone(Timeout.Infinite);
                //}

                byte[] header = new byte[] {0x52, 0x49, 0x46, 0x46, 0x6C, 0xA7, 0x0, 0x0, 0x57, 0x41, 0x56, 0x45, 0x66, 0x6D, 0x74, 0x20, 0x12, 0x0, 0x0, 0x0, 0x1, 0x0, 0x1, 0x0, 0x22, 0x56, 0x0, 0x0, 0x44, 0xAC, 0x0, 0x0, 0x2, 0x0, 0x10, 0x0, 0x0, 0x0
                /* word 'data'*/, 0x64, 0x61, 0x74, 0x61
                /* length of data next 4 bytes default = 1Mb*/, 0x00, 0x00, 0x10, 0x00};
                byte[] data = (byte[]) stream.GetData();
                byte[] buffer = new byte[header.Length + data.Length];
                Array.Copy(header, 0, buffer, 0, header.Length);

                buffer[header.Length - 1 - 3] = (byte)(data.Length & 0x000000FF);
                buffer[header.Length - 1 - 2] = (byte)((data.Length & 0x0000FF00) >> (int)Math.Log(0x0000100, 2));
                buffer[header.Length - 1 - 1] = (byte)((data.Length & 0x00FF0000) >> (int)Math.Log(0x0010000, 2));
                buffer[header.Length - 1 - 0] = (byte)((data.Length & 0xFF000000) >> (int)Math.Log(0x1000000, 2));
                
                Array.Copy(data, 0, buffer, header.Length, data.Length);

//                SpFileStream st = new SpFileStream();
//                st.Open("d:\\test.wav", SpeechStreamFileMode.SSFMCreateForWrite, false);;
//                st.Write(data);
//                st.Close();

                return buffer;
            } catch (Exception e) {
                string s = e.Message;
            }
            return null;
        }
    }
}