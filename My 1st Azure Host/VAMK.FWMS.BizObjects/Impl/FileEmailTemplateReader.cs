using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VAMK.FWMS.BizObjects.Impl
{
    public class FileEmailTemplateReader : IEmailTemplateReader
    {
        private const int DefaultBufferSize = 4096;
        private const FileOptions DefaultOptions = FileOptions.Asynchronous | FileOptions.SequentialScan;
        public string filePath
        {
            get
            {
                return filePath;
            }

            set
            {
                filePath = value;
            }
        }


        public StringBuilder ReadAllLines(string path, Encoding encoding)
        {
            var lines = new StringBuilder();

            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, DefaultBufferSize, DefaultOptions))
            using (var reader = new StreamReader(stream, encoding))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Append(line);
                }
            }

            return lines;
        }
    }
}
