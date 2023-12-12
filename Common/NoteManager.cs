using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace Common
{
    internal class NoteManager
    {
        HttpClient Client { get; set; }

        public NoteManager(string baseAddress)
        {
            Client = new HttpClient();
            if (baseAddress != "" && baseAddress != null) Client.BaseAddress = new Uri(baseAddress);
        }

        //public async Task<Result>
    }

}
