using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Funq.Tests
{
    [TestClass]
    public class ContainerFixture
    {
        [TestMethod]
        public void RegisterTypeAndGetInstance()
        {
            var container = new Container();

            container.Register<IBar>(c => new Bar());

            var bar = container.Resolve<IBar>();

            Assert.IsNotNull(bar);
            Assert.IsTrue(bar is Bar);
        }

        [TestMethod]
        public void ResolveGetsDependenciesInjected()
        {
            var container = new Container();

            container.Register<IBar>(c => new Bar());
            container.Register<IFoo>(c => new Foo(c.Resolve<IBar>()));

            var foo = container.Resolve<IFoo>() as Foo;

            Assert.IsNotNull(foo);
            Assert.IsNotNull(foo.Bar);
            Assert.IsTrue(foo is Foo);
        }

        public interface IBar { }
        public interface IFoo { }

        public class Bar : IBar { }
        public class Foo : IFoo
        {
            public IBar Bar { get; private set; }

            public Foo(IBar bar)
            {
                Bar = bar;
            }
        }
    }
}