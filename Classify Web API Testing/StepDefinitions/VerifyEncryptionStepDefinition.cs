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
    internal class VerifyEncryptionStepDefinition
    {
        private ScenarioContext _scenarioContext;
        private ClassifyAPIAdapter API;

        public VerifyEncryptionStepDefinition(ScenarioContext ScenarioContext, ClassifyAPIAdapter aPI)
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
            _scenarioContext["key"] = this.API.GenerateKey(length).Result;
        }

        [StepDefinition(@"Key of length (.*) is generated successfully")]
        public void ThenKeyOfLengthIsGeneratedSuccessfully(int expectedLength)
        {
            Key key = (Key)_scenarioContext["key"];
            Assert.Equal(expectedLength, key.key.Length);
        }

        [StepDefinition(@"I encrypt (.*) and key is (.*)")]
        public void GivenIEncryptAndKeyIs(string Message, string Key)
        {
            _scenarioContext["EncryptedMessage"] =  this.API.Encrypt(Message, Key).Result;
        }

        [StepDefinition(@"The message is encrypted")]
        public void ThenTheMessageIsEncrypted()
        {
            Result EncryptedMessage = (Result)_scenarioContext["EncryptedMessage"];
            Assert.NotNull(EncryptedMessage);
        }

        [StepDefinition(@"Encrypted message is generated")]
        public void ThenEncryptedMessageIsGenerated()
        {
            Result EncryptedMessage = (Result) _scenarioContext["EncryptedMessage"];
            Assert.NotNull(EncryptedMessage.result);
            Assert.True(EncryptedMessage.result.Length > 0);
        }

        [StepDefinition(@"I decrypt the Encrypted message using key (.*)")]
        public void WhenIDecryptTheEncryptedMessageUsingKeyAsync(string Key)
        {
            Result EncryptedMessage = (Result)_scenarioContext["EncryptedMessage"];
            _scenarioContext["DecryptedMessage"] = this.API.Decrypt(EncryptedMessage.result, Key).Result;
        }

        [StepDefinition(@"I get (.*) in response")]
        public void ThenIGetInResponse(string Message)
        {
            Result DecryptedMessage = (Result)_scenarioContext["DecryptedMessage"];
            Assert.NotNull(DecryptedMessage);
            Assert.Equal(Message, DecryptedMessage.result);
        }

        [When(@"I encrypt (.*) with the generated key")]
        public void WhenIEncryptPleaseEncryptMeWithTheGeneratedKey(string Message)
        {
            Key GeneratedKey = (Key)_scenarioContext["key"];
            _scenarioContext["EncryptedMessage"] = this.API.Encrypt(Message, GeneratedKey.key).Result;


        }

        [When(@"I decrypt the Encrypted message using the generated key")]
        public void WhenIDecryptTheEncryptedMessageUsingTheGeneratedKey()
        {
            Key GeneratedKey = (Key)_scenarioContext["key"];
            Result EncryptedMessage = (Result)_scenarioContext["EncryptedMessage"];
            _scenarioContext["DecryptedMessage"] = this.API.Decrypt(EncryptedMessage.result, GeneratedKey.key).Result;
        }


    }
}
