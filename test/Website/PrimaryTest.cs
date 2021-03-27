﻿using System.Net.Http;
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
        [InlineData("/lib/clipboard-js/clipboard.min.js", "clipboard.js v2.0.6")]
        [InlineData("/lib/d3/d3.min.js", "this.d3=")]
        [InlineData("/lib/datatables/js/jquery.dataTables.min.js", "DataTables 1.10.20")]
        [InlineData("/lib/editor-md/editormd.js", "https://github.com/pandao/editor.md")]
        [InlineData("/lib/file-saver/FileSaver.min.js", "http://purl.eligrey.com/github/FileSaver.js")]
        [InlineData("/lib/font-awesome/css/all.min.css", "Font Awesome Free 5.13.0")]
        [InlineData("/lib/jquery-validation/jquery.validate.min.js", "jQuery Validation Plugin - v1.19.1")]
        [InlineData("/lib/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js", "jquery.validate.unobtrusive")]
        [InlineData("/lib/katex/katex.min.css", "font-family:KaTeX_AMS;")]
        public async Task FileExists(string requestPath, string flagSubstring)
        {
            var response = await Client.GetAsync(requestPath);
            response.EnsureSuccessStatusCode();
            Assert.Contains(flagSubstring, await response.Content.ReadAsStringAsync());
        }
    }
}
