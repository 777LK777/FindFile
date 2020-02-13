using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindFile
{
    class InputData
    {
        private string _path, _fileNameMask, _keyWord;

        public InputData(string path, string fileNameMask, string keyWord)
        {
            _path = path;
            _fileNameMask = fileNameMask;
            _keyWord = keyWord;
        }

        public InputData() { }

        /// <summary>
        /// Путь к папке, в которой хранится файл
        /// </summary>
        public string Path
        {
            get => _path;
            set => _path = value;
        }

        /// <summary>
        /// Часть имени файла или имя файла целиком
        /// </summary>
        public string FileName
        {
            get => _fileNameMask;
            set => _fileNameMask = value;
        }

        /// <summary>
        /// Набор символов, с которым должно быть найдено совпадение в файле
        /// </summary>
        public string KeyWord
        {
            get => _keyWord;
            set => _keyWord = value;
        }
    }
}
