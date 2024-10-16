using StocksB3.Services;

namespace StocksB3.Tests
{
    public class StocksDailyValueTest
    {
        [Theory]
        [InlineData("GCAC20", "", 0, 1)]
        [InlineData("", "2024-08-01", 0, 1)]
        public void create_CodeAndDateMandatory_Exception(string code, string date, double min, double max)
        {
            StockDailyValue stockDailyValue;
            Assert.Throws<Exception>(() => { stockDailyValue = new StockDailyValue(code, date, min, max); });
        }

        [Theory]
        [InlineData("GCAC20", "2024-13-01", 1, 2)]
        [InlineData("GCAC20", "2024-01-32", 1, 2)]
        [InlineData("GCAC20", "2024", 1, 2)]
        public void create_DateInvalid_Exception(string code, string date, double min, double max)
        {
            StockDailyValue stockDailyValue;
            Assert.Throws<Exception>(() => { stockDailyValue = new StockDailyValue(code, date, min, max); });
        }

        [Theory]
        [InlineData("GCAC20", "2024-08-01", 2, 1)]        
        public void create_MaxLowerMin_Exception(string code, string date, double min, double max)
        {
            StockDailyValue stockDailyValue;
            Assert.Throws<Exception>(() => { stockDailyValue = new StockDailyValue(code, date, min, max); });
        }


        [Theory]
        [InlineData("GCAC20", "2024-08-01", 1.57, 3.78)]
        [InlineData("GCAC20", "2024-01-08", 3.67, 5.887)]
        public void create_Success(string code, string date, double min, double max)
        {
            StockDailyValue stockDailyValue;
            stockDailyValue = new StockDailyValue(code, date, min, max);

            Assert.Equal(code, stockDailyValue.B3Code);
            Assert.Equal(new DateOnly(2024, 08, 01), stockDailyValue.Day);
            Assert.Equal(min, stockDailyValue.ValueMin);
            Assert.Equal(max, stockDailyValue.ValueMax);
        }
    }
}