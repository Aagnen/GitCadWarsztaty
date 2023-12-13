using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DatabaseModels;

namespace Common
{
    public class NoteManager
    {
        HttpClient Client { get; set; }

        public NoteManager(string baseAddress)
        {
            Client = new HttpClient();
            if (baseAddress != "" && baseAddress != null) Client.BaseAddress = new Uri(baseAddress);
        }

        public async Task<Result> PostNote(NoteItem noteItem)
        {
            try
            {
                var response = await Client.PostAsJsonAsync(EndpointStrings.PostCommitEndpoint, noteItem);
                if (response.IsSuccessStatusCode) return new Result(SolveFlag.OK, "");
                return new Result(SolveFlag.Error, $"[Code {response.StatusCode}]: {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                return new Result(SolveFlag.Error, $"{ex.Message}");
            }
        }

        public async Task<Result<List<NoteItem>>> GetNotes()
        {
            try
            {
                var response = await Client.GetAsync(EndpointStrings.GetCommitsEndpoint);
                if (response != null && response.IsSuccessStatusCode)
                {
                    List<NoteItem> notes = await response.Content.ReadFromJsonAsync<List<NoteItem>>();
                    return new Result<List<NoteItem>>(SolveFlag.OK, notes);
                }
                else if (response != null)
                {
                    return new Result<List<NoteItem>>(SolveFlag.Error, null, $"[Code {response.StatusCode}]: {response.ReasonPhrase}");
                }
                return null;
            }
            catch (Exception ex)
            {
                return new Result<List<NoteItem>>(SolveFlag.Error, null, ex.Message);
            }

        }
    }
}
