using Classify_Web_API_Testing.API;
using Classify_Web_API_Testing.Hooks;
using Classify_Web_API_Testing.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Classify_Web_API_Testing.StepDefinitions
{
    [Binding]
    internal class VerifyEncryption
    {
        private ScenarioContext _scenarioContext;
        private ClassifyAPIAdapter API;

        public VerifyEncryption(ScenarioContext ScenarioContext, ClassifyAPIAdapter aPI)
        {
            _scenarioContext = ScenarioContext;
            API = aPI;
        }

        [StepDefinition(@"Backend Classify Web Service is set up")]
        public void GivenBackendClassifyWebServiceIsSetUp()
        {
            this.API = new ClassifyAPIAdapter();
        }

        [StepDefinition(@"I generate a key of length (.*) using KeyGen functionality")]
        public void WhenIGenerateAKeyOfLengthUsingKeyGenFunctionality(int length)
        {
            _scenarioContext["key"] = this.API.GenerateKey(length).Result.key;
        }

        [StepDefinition(@"Key of length (.*) is generated successfully")]
        public void ThenKeyOfLengthIsGeneratedSuccessfully(int expectedLength)
        {
            string key = (string)_scenarioContext["key"];
            Assert.Equal(expectedLength, key.Length);
        }

        [StepDefinition(@"I encrypt (.*) and key is (.*)")]
        public void GivenIEncryptAndKeyIs(string Message, string Key)
        {
            _scenarioContext["result"] =  this.API.Encrypt(Message, Key).Result;
        }

        [StepDefinition(@"The message is encrypted")]
        public void ThenTheMessageIsEncrypted()
        {
            Result EncryptedMessage = (Result)_scenarioContext["result"];
            Assert.NotNull(EncryptedMessage);
        }

        [StepDefinition(@"Encrypted message is generated")]
        public void ThenEncryptedMessageIsGenerated()
        {
            Result EncryptedMessage = (Result) _scenarioContext["result"];
            Assert.NotNull(EncryptedMessage.result);
            Assert.True(EncryptedMessage.result.Length > 0);
        }

    }
}
