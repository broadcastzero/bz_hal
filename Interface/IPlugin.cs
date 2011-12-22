using System;
using System.Collections.Generic;
using Interface;

namespace Interface
{
    public interface IPlugin
    {
        /// <summary>
        /// Does this plugin know how to handle the sentence? - search for keywords!
        /// </summary>
        /// <param name="wordlist"></param>
        /// <returns></returns>
        int GetPriority(List<Word> wordlist);

        /// <summary>
        /// Calculates a sentence depending on the Plugins priorities
        /// </summary>
        /// <param name="wordlist">Method gets a List of words, therefore class Word is stored in Interface.</param>
        string CalculateSentence(List<Word> wordlist);
    }
}
