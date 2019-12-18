using System.Linq;
using Xunit;

namespace PaginationExtensions.Test
{
    public class UnitTest
    {

        [Theory]
        [InlineData(100)]
        public void PaginnationResult_ValuesQueryableLength100_ReturnRowCount(int length)
        {
            var query = BuildSequentialEnumerable(length).AsQueryable();

            var paginationResult = query.ToPaginableEnumerable(2, 10);

            Assert.Equal(length, paginationResult.RowCount);
        }

        [Theory]
        [InlineData(100, 12)]
        public void PaginnationResult_ValuesEnumerableLength100_ReturnCurrentPage(int length, int pageSize)
        {
            var query = BuildSequentialEnumerable(length);

            var pageCount = (double)length / pageSize;

            for (int page = 1; page <= pageCount; page++)
            {
                var paginationResult = query.ToPaginableEnumerable(page, pageSize);
                Assert.Equal(page, paginationResult.CurrentPage);
                Assert.Equal(pageSize, paginationResult.PageSize);
                Assert.Equal(length, paginationResult.RowCount);
            }
        }

        private int[] BuildSequentialEnumerable(int length)
        {
            var array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = i + 1;
            }
            return array;
        }
    }
}
