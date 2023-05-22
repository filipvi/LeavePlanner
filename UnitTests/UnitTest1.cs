namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest() => Assert.Equal(9, Multiply(3, 3));
        [Fact]
        public void FailingTest() => Assert.Equal(10, Multiply(3, 3));


        private int Multiply(int a, int b)
        {
            return a * b;
        }

        bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(9)]
        public void TheoryTest(int value)
        {
            Assert.True(IsEven(value));
        }

    }
}