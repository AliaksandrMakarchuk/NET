using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace console_ui.models.Test
{
    [TestClass]
    public class IntersectionCheckerTests
    {
        private IIntersectionChecker _intersectionChecker;
        private IAreaBuilder _areaBuilder;
        private AbstractArea _newArea;

        [TestInitialize]
        public void Initialization()
        {
            _intersectionChecker = new IntersectionChecker();
            _areaBuilder = new ConcreteAreaBuilder();
            _newArea = _areaBuilder
                .SetName("new")
                .SetPrinter(new ConcreteAreaPrinter())
                .SetStartPosition(new Position(2, 1))
                .SetAreaSize(new AreaSize(3, 2))
                .Build();
        }

        [TestMethod]
        public void FirstAreaAboveAndLeft_NoIntersection_Test()
        {
            // arrange
            var firstArea = _areaBuilder
                .SetName("first")
                .SetPrinter(new ConcreteAreaPrinter())
                .SetStartPosition(new Position(0, 0))
                .SetAreaSize(new AreaSize(3, 1))
                .Build();

            // act
            var result = _intersectionChecker.AreIntersect(firstArea, _newArea);

            // assert
            Assert.IsFalse(result, "Should not intersect");
        }

        private class ConcreteAreaPrinter : IAreaPrinter
        {
            public void Print<TArea>(TArea area) where TArea : AbstractArea { }
        }

        private class ConcreteArea : AbstractArea
        {
            public ConcreteArea(IAreaPrinter printer, Position startPosition, AreaSize size) : base(printer)
            {
                StartPosition = startPosition;
                Size = size;
            }

            protected override void HandleInput(ConsoleKey key) { }
        }

        private class ConcreteAreaBuilder : IAreaBuilder
        {
            private IAreaPrinter _areaPrinter;
            private string _name;
            private Position _startPosition;
            private AreaSize _size;
            private bool _isConcurrent;

            public IAreaBuilder SetPrinter(IAreaPrinter printer)
            {
                return this;
            }
            public IAreaBuilder SetName(string name)
            {
                return this;
            }
            public IAreaBuilder SetStartPosition(Position startPosition)
            {
                _startPosition = startPosition;
                return this;
            }
            public IAreaBuilder SetAreaSize(AreaSize areaSize)
            {
                _size = areaSize;
                return this;
            }
            public IAreaBuilder SetConcurrency(bool IsConcurrent)
            {
                return this;
            }
            public AbstractArea Build()
            {
                return new ConcreteArea(_areaPrinter, _startPosition, _size);
            }
        }
    }
}