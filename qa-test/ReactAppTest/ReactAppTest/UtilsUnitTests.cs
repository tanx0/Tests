using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ReactAppTest
{
    [TestFixture]
    public class UtilsUnitTests
    {
        [Test, TestCaseSource("TestCasesData")]
        public void FindMiddleIndexTest(Tuple<int[], int?> testData)
        {
            var array = testData.Item1;
            var expectedIndex = testData.Item2;
            var actualIndex = Utils.FindMiddleIndex(array);
            Assert.AreEqual(expectedIndex, actualIndex);
        }

        private static Tuple<int[], int?>[] TestCasesData =
        {
            new Tuple<int[], int?>(new int[] {}, null),
            new Tuple<int[], int?>(new[] {10}, null),
            new Tuple<int[], int?>(new[] {10, 1}, null),
            new Tuple<int[], int?>(new[] { 10, 1, 10 }, 1),
            new Tuple<int[], int?>(new[] { 10, 15, 5, 7, 1, 24, 36, 2 }, 5),
            new Tuple<int[], int?>(new[] { 100, 99, 1, 5, 9, 15, 34, 3, 23, 10 }, 1),
            new Tuple<int[], int?>(new[] { 23,50,63,90,10,30,155,23,18 }, 4),
            new Tuple<int[], int?>(new[] {133,60,23,92,6,7,168,16,19}, 3),
            new Tuple<int[], int?>(new[] {30,43,29,10,50,40,99,51,12}, 5),
            new Tuple<int[], int?>(new[] { 10, 1, 1, 10 }, null),
        };

        [Test]
        [Timeout(2000)]
        public void PerformanceTest()
        {

            var array = GenerateDataSet(10000).ToArray();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Utils.FindMiddleIndex(array);
            stopWatch.Stop();
            Console.WriteLine("{0} ms", stopWatch.ElapsedMilliseconds);
        }

        private static IEnumerable<int> GenerateDataSet(int size)
        {

            const int one = 1;
            for (int i = 0; i < size; i++)
            {
                yield return one;
            }
        }
    }
}
