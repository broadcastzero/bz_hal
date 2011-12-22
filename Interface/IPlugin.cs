using System;
using System.Collections.Generic;
using Interface;

namespace Interface
{
    public interface IPlugin
    {
        /// <summary>
        /// Calculates a sentence depending on the Plugins priorities
        /// </summary>
        /// <param name="wordlist">Method gets a List of words, therefore class Word is stored in Interface.</param>
        string CalculateSentence(List<Word> wordlist);
    }
}
