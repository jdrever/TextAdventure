using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextAdventure.Application;

namespace TextAdventure.UnitTests
{
    [TestClass]
    public class TextSimplifierUnitTest
    {
        private TextSimplifier textSimplifier=new TextSimplifier();
        [TestMethod]
        public void TestRemovingStopWords()
        {
            string input = "Take the dog.";
            var simplifiedSentences = textSimplifier.SimplifyText(input);
            Assert.IsTrue(simplifiedSentences[0] == "TAKE DOG");
        }
        [TestMethod]
        public void TestSplittingSentences()
        {
            string input = "Take the dog. Give the dog a bone.";
            var simplifiedSentences = textSimplifier.SimplifyText(input);
            Assert.IsTrue(simplifiedSentences[0] == "TAKE DOG");
            Assert.IsTrue(simplifiedSentences[1] == "GIVE DOG BONE");
        }
    }
}
