using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetBackground.LanguageAPI
{
    public class MicrosoftTextAnalytics : IlanguageProvider
    {
        public MicrosoftTextAnalytics()
        {
            var k1 = "69416bac944d49478e92e29dd5355e3f";
            var k2 = "8ef1f05f3b084e2cac7269cb54a56258";
        }
        public string GetLanguage(string text)
        {
            throw new NotImplementedException();
        }

        public string TranslateTo(string originalText, string originalLanguage, string targetLanguage)
        {
            throw new NotImplementedException();
        }

        public string TranslateTo(string originalText, string targetLanguage)
        {
            throw new NotImplementedException();
        }
    }
}
