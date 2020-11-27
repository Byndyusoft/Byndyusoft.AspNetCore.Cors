using Microsoft.AspNetCore.Cors.Infrastructure;
using Xunit;

namespace Byndyusoft.AspNetCore.Cors
{
    public class CorsPolicyBuilderPatcherTests
    {
        [Fact]
        public void Patch_Test()
        {
            // arrange
            var builder = new CorsPolicyBuilder().AllowAnyOrigin().AllowCredentials();

            // act
            CorsPolicyBuilderPatcher.Patch();

            // assert
            var policy = builder.Build();
            Assert.NotNull(policy);
        }
    }
}
