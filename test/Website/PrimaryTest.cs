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
        [InlineData("/lib/bootstrap/css/bootstrap.min.css", "Bootstrap v4.4.0")]
        [InlineData("/lib/bootstrap-toggle/css/bootstrap2-toggle.min.css", "bootstrap2-toggle.css v2.2.0")]
        public async Task FileExists(string requestPath, string flagSubstring)
        {
            var response = await Client.GetAsync(requestPath);
            response.EnsureSuccessStatusCode();
            Assert.Contains(flagSubstring, await response.Content.ReadAsStringAsync());
        }
    }
}
