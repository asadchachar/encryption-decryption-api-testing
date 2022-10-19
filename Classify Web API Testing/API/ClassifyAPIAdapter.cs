using Classify_Web_API_Testing.Hooks;
using Classify_Web_API_Testing.Model.Request;
using Classify_Web_API_Testing.Model.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classify_Web_API_Testing.API
{
    internal class ClassifyAPIAdapter
    {
        private HttpClient Client;
        private string BaseUrl;

        public ClassifyAPIAdapter()
        {
            this.Client = Helper.HttpClient;
            this.BaseUrl = Helper.BaseURL;
        }
        //KeyGen
        public async Task<Key> GenerateKey(int length = 0, int IncludeSymbols = 0)
        {
            //Call API
            var result = await this.Client.GetAsync(BaseUrl + "/keygen?length=" + length + "&symbols=" + IncludeSymbols);
            string res = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Key>(res);
        }
        //Encrypt
        public async Task<Result> Encrypt(string Message, string Key)
        {
            EncryptionRequest Request = new EncryptionRequest() { data = Message, key = Key };
            //Call API
            var result = await this.Client.PostAsync(BaseUrl + "/encrypt", new StringContent(JsonConvert.SerializeObject(Request)));
            string res = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Result>(res);
        }

        //Decrypt
        public async Task<Result> Decrypt(string Message, string Key)
        {
            EncryptionRequest Request = new EncryptionRequest() { data = Message, key = Key };
            //Call API
            var result = await this.Client.PostAsync(BaseUrl + "/decrypt", new StringContent(JsonConvert.SerializeObject(Request)));
            string res = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Result>(res);
        }

        //KeyGen
        public async Task<Key> Generate(int length = 0, int IncludeSymbols = 0)
        {
            //Call API
            var result = await this.Client.GetAsync(BaseUrl + "/keygen?length=" + length + "&symbols=" + IncludeSymbols);
            string res = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Key>(res);
        }
    }
}
