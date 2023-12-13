using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class EndpointStrings
    {
        public static readonly string PostCommitEndpoint = "https://localhost:7184/api/Notes/AddNote";
        public static readonly string GetCommitsEndpoint = "https://localhost:7184/api/Notes/ListNotes";
    }
}
