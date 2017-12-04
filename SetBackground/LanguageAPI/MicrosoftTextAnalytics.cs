﻿using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetBackground.LanguageAPI
{
    public class MicrosoftTextAnalytics : IlanguageProvider
    {
        ITextAnalyticsAPI _AnalyticsClient;

        public MicrosoftTextAnalytics(string key)
        {
            _AnalyticsClient =  new TextAnalyticsAPI();
            _AnalyticsClient.AzureRegion = AzureRegions.Westus;

            _AnalyticsClient.SubscriptionKey = key;
        }

        public string GetLanguage(string text)
        {
            LanguageBatchResult result = _AnalyticsClient.DetectLanguage(
                new BatchInput(
                    new List<Input>()
                    {
                        new Input("1", text)
                    }));
            if (result.Documents.Any())
                return result.Documents.First().DetectedLanguages.First().Iso6391Name;
            return "en";
        }

        public string TranslateTo(string originalText, string originalLanguage, string targetLanguage)
        {
            throw new NotImplementedException();
        }

        public string TranslateTo(string originalText, string targetLanguage)
        {
            throw new NotImplementedException();
        }

        public string[] ExtractKeyPhrases(string text, string language)
        {
            KeyPhraseBatchResult result = _AnalyticsClient.KeyPhrases(
                new MultiLanguageBatchInput(
                    new List<MultiLanguageInput>()
                    {
                        new MultiLanguageInput(language, "1", text)
                    }));

            if (result.Documents.Any())
                return result.Documents.First().KeyPhrases.ToArray();

            return new string[] { "Restricted" };
        }
    }
}
