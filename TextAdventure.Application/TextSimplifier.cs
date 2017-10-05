using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventure.Interface;

namespace TextAdventure.Application
{
    public class TextSimplifier : ITextSimplifier
    {
        public string[] SimplifyText(string input)
        {
            input= RemoveStopWords(input.ToUpper());
            var sentences = SeparateSentences(input);
            return sentences;
        }

        private string RemoveStopWords(string input)
        {
            string[] stopWords = { "THE", "A","AN" };

            foreach (string word in stopWords)
            {
                input=input.Replace(word+" ", "");
            }
            return input;
        }
        private string[] SeparateSentences(string input)
        {
            input = input.Replace("AND ", ".");
            var sentences = input.Split('.').Select(p => p.Trim()).ToArray();
            return sentences;
        }
    }
}
