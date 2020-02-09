using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindFileLib
{
    public class ReadFileEventArgs
    {
        readonly bool _matchesFound; //Совпадение

        /// <summary>
        /// Информация о количестве совпадений, передаваемой получателям уведомления о событии
        /// </summary>
        /// <param name="countCoincidence">Количество совпадений</param>
        public ReadFileEventArgs(bool matchesFound)
        {
            _matchesFound = matchesFound;
        }

        public bool MatchWasFound
        {
            get => _matchesFound;
        }
    }
}
