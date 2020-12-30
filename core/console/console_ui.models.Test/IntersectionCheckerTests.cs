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
            _newArea = GenerateArea(1, 2, 3, 2);
        }

        [TestMethod]
        [DataRow(0, 0, 3, 1, DisplayName = "ExistingAreaAboveLeft")]
        [DataRow(1, 0, 2, 3, DisplayName = "ExistingAreaOnLeft")]
        [DataRow(3, 0, 3, 3, DisplayName = "ExistingAreaBelowLeft")]
        [DataRow(2, 5, 4, 2, DisplayName = "ExistingAreaOnRight")]
        [DataRow(0, 5, 1, 2, DisplayName = "ExistingAreaAboveRight")]
        [DataRow(4, 5, 3, 3, DisplayName = "ExistingAreaBelowRight")]
        [DataRow(0, 2, 3, 1, DisplayName = "ExistingAreaAbove")]
        [DataRow(3, 2, 3, 2, DisplayName = "ExistingAreaBelow")]
        public void AreIntersect_NoIntersection_Test(int top, int left, int width, int height)
        {
            // arrange
            var existingArea = GenerateArea(top, left, width, height);

            // act
            var result = _intersectionChecker.AreIntersect(existingArea, _newArea);

            // assert
            Assert.IsFalse(result, "Should not intersect");
        }

        [TestMethod]
        [DataRow(0, 0, 3, 2, DisplayName = "ExistingAreaAboveLeft")]
        [DataRow(1, 0, 3, 1, DisplayName = "ExistingAreaOnLeft")]
        [DataRow(2, 1, 3, 2, DisplayName = "ExistingAreaBelowLeft")] //
        [DataRow(0, 4, 2, 1, DisplayName = "ExistingAreaAboveRight")] //
        [DataRow(1, 3, 3, 3, DisplayName = "ExistingAreaOnRight")]
        [DataRow(2, 4, 3, 2, DisplayName = "ExistingAreaBelowRight")]
        [DataRow(1, 2, 3, 2, DisplayName = "ExistingAreaOverlapNewOne")]
        public void AreIntersect_AreasIntersected_Test(int top, int left, int width, int height)
        {
            // arrange
            var existingArea = GenerateArea(top, left, width, height);

            // act
            var result = _intersectionChecker.AreIntersect(existingArea, _newArea);

            // assert
            Assert.IsTrue(result, "Should intersect");
        }

        private AbstractArea GenerateArea(int top, int left, int width, int height)
        {
            return _areaBuilder
                .SetPrinter(new ConcreteAreaPrinter())
                .SetStartPosition(new Position(top, left))
                .SetAreaSize(new AreaSize(width, height))
                .Build();
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