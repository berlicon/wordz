using System.IO;

namespace Wordz.Snd {
    public class MP3Helper {
        public MP3Helper() {}

        public static byte[] ConvertSoundToMP3(byte[] wavContent) {
            byte[] sound = new byte[] {};

            MemoryStream wavMemoryStream = new MemoryStream(wavContent);
            MemoryStream mp3MemoryStream = new MemoryStream();
            //TODO: assign constants from config
            int rate = 22050;
            int bits = 16;
            int channels = 1;

            Mp3WriterConfig m_Config = new Mp3WriterConfig(
                new WaveFormat(rate, bits, channels),
                new BE_CONFIG(new WaveFormat(rate, bits, channels)));

            WaveStream InStr = new WaveStream(wavMemoryStream);
            try {
                Mp3Writer writer = new Mp3Writer(mp3MemoryStream, m_Config);
                try {
                    byte[] buff = new byte[writer.OptimalBufferSize];
                    int read = 0;
                    while ((read = InStr.Read(buff, 0, buff.Length)) > 0) {
                        writer.Write(buff, 0, read);
                    }
                } finally {
                    sound = mp3MemoryStream.GetBuffer();
                    writer.Close();

//                    SpFileStream st = new SpFileStream();
//                    st.Open("d:\\test.mp3", SpeechStreamFileMode.SSFMCreateForWrite, false);;
//                    st.Write(sound);
//                    st.Close();
                }
            } finally {
                InStr.Close();
            }
            return sound;
        }
    }
}