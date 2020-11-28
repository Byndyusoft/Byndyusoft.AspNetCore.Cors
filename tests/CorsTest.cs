using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Byndyusoft.AspNetCore.Cors
{
    public class CorsTest : MvcTestFixture
    {
        [Fact]
        public async Task Cors_Testing()
        {
            // arrange
            var request = new HttpRequestMessage(HttpMethod.Options, "/values");
            request.Headers.Add("Origin", "http://foo.example");
            request.Headers.Add("Access-Control-Request-Method", "GET");
            request.Headers.Add("Access-Control-Request-Headers", "X-PINGOTHER, Content-Type");

            // act
            var response = await Client.SendAsync(request);

            // assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal("X-PINGOTHER,Content-Type", response.Headers.GetValues("Access-Control-Allow-Headers").Single());
            Assert.Equal("GET", response.Headers.GetValues("Access-Control-Allow-Methods").Single());
            Assert.Equal("*", response.Headers.GetValues("Access-Control-Allow-Origin").Single());
        }
    }
}