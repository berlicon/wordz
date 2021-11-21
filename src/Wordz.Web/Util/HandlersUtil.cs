using System.IO;

namespace Wordz.Web.Util {
    public class HandlersUtil {
        private HandlersUtil() {}

        public static string GetContent(Stream stream) {
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }
    }
}