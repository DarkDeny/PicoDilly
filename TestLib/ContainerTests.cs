using PicoDilly;
using Xunit;

namespace TestLib {
    public class ContainerTests {
        [Fact]
        public void ShouldBeAbleToCreateAutoregistedTypes() {
            var container = new Container();
            var st = container.GetInstance<SomeType>();
            Assert.NotNull(st);
        }
    }
}