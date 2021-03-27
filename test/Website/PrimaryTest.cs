using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Website
{
    public class PrimaryTest : IClassFixture<FakeApplicationFixture>
    {
        private HttpClient Client { get; }

        public PrimaryTest(FakeApplicationFixture app)
        {
            Client = app.CreateClient();
        }

        [Theory]
        [InlineData("/lib/jquery/jquery.min.js", "jQuery v3.4.1")]
        [InlineData("/lib/ace/ace.min.js", "\"ace/lib/dom\"")]
        public async Task FileExists(string requestPath, string flagSubstring)
        {
            var response = await Client.GetAsync(requestPath);
            response.EnsureSuccessStatusCode();
            Assert.Contains(flagSubstring, await response.Content.ReadAsStringAsync());
        }
    }
}
