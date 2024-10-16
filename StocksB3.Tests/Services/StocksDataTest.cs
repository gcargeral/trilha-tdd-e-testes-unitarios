using StocksB3.Services;
using System.Xml.Linq;

namespace StocksB3.Tests
{
    public class StocksDataTest
    {
        private StocksData _stocksData = new StocksData();

        [Theory]
        [InlineData("GCAC20", "GCAC20")]
        public void add_StockAlreadyExists_False(string code1, string code2)
        {
            //Add a new stock without error
            _stocksData.addStock(new Stock(code1, code1 + "Stock 1"));
            var result = _stocksData.addStock(new Stock(code2, code2 + "Stock 2"));
            Assert.True(!result);
        }

        [Theory]
        [InlineData("GCAC21", "2024-08-01")]
        public void add_StockNotExistsToAddDayMovement_Exception(string code, string day1)
        {
            //Add a new StockDailyValue again
            Assert.Throws<Exception>(() => { _stocksData.addStockDailyValue(new StockDailyValue(code, day1, 1, 20.23)); });
        }

        [Theory]
        [InlineData("GCAC22", "2024-08-01")]
        public void add_StockDayMovementAlreadyExists_Exception(string code, string day1)
        {
            //Add a stock
            _stocksData.addStock(new Stock(code, code + "Stock"));
            //Add a new StockDailyValue without error
            _stocksData.addStockDailyValue(new StockDailyValue(code, day1, 0, 12.23));

            //Add a new StockDailyValue again
            var result = _stocksData.addStockDailyValue(new StockDailyValue(code, day1, 1, 20.23));
            Assert.True(!result);
        }

        [Theory]
        [InlineData("GCAC23", "2024-08-01")]
        [InlineData("GCAC23", "2024-08-02")]
        public void add_StockDayMovemet_Success(string code, string day)
        {
            //Add a stock
            _stocksData.addStock(new Stock(code, code + "Stock"));

            var result = false;
            StockDailyValue? movement;

            result = _stocksData.addStockDailyValue(new StockDailyValue(code, day, 0, 2));
            movement = _stocksData.getStockDailyValue(code, DateOnly.Parse(day));

            Assert.True(result);
            Assert.NotNull(movement);
            Assert.Equal(code, movement.B3Code);
            Assert.Equal(DateOnly.Parse(day), movement.Day);
            Assert.Equal(0, movement.ValueMin);
            Assert.Equal(2, movement.ValueMax);
        }

        private void AddDataToTest(string code)
        {
            //Add a stock
            _stocksData.addStock(new Stock(code, code + "Stock"));
            //Add movements
            _stocksData.addStockDailyValue(new StockDailyValue(code, "2024-05-31", 10.1, 20.2));
            _stocksData.addStockDailyValue(new StockDailyValue(code, "2024-06-01", 10.22, 20.33));
            _stocksData.addStockDailyValue(new StockDailyValue(code, "2024-06-03", 30.33, 50.55));
            _stocksData.addStockDailyValue(new StockDailyValue(code, "2024-06-04", 20.44, 40.66));
            _stocksData.addStockDailyValue(new StockDailyValue(code, "2024-06-05", 50.55, 70.77));
            _stocksData.addStockDailyValue(new StockDailyValue(code, "2024-06-06", 60.66, 80.88));
        }

        [Theory]
        [InlineData("GCAC24", "2024-06-01", "2024-06-04", 50.55)]
        [InlineData("GCAC24", "2024-06-01", "2024-06-05", 70.77)]
        public void get_maxValueFromPeriod(string code, string initday, string finalday, double expectedvalue)
        {
            AddDataToTest(code);

            DateOnly init = DateOnly.Parse(initday);
            DateOnly final = DateOnly.Parse(finalday);

            var result = _stocksData.maxValueFromPeriod(code, init, final);
            
            Assert.Equal(expectedvalue, result);
        }

        [Theory]
        [InlineData("GCAC24", "2024-06-01", "2024-06-04", 10.22)]
        [InlineData("GCAC24", "2024-06-04", "2024-06-05", 20.44)]
        public void get_minValueFromPeriod(string code, string initday, string finalday, double expectedvalue)
        {
            AddDataToTest(code);

            DateOnly init = DateOnly.Parse(initday);
            DateOnly final = DateOnly.Parse(finalday);

            var result = _stocksData.minValueFromPeriod(code, init, final);

            Assert.Equal(expectedvalue, result);
        }

        [Theory]
        [InlineData("GCAC24", "2024-06-01", "2024-06-04", 30.385)]
        [InlineData("GCAC24", "2024-06-04", "2024-06-05", 45.605)]
        [InlineData("GCAC24", "2024-07-04", "2024-07-05", 0)]
        public void get_avgValueFromPeriod(string code, string initday, string finalday, double expectedvalue)
        {
            AddDataToTest(code);

            DateOnly init = DateOnly.Parse(initday);
            DateOnly final = DateOnly.Parse(finalday);

            var result = _stocksData.avgValueFromPeriod(code, init, final);

            Assert.Equal(expectedvalue, result);
        }


    }
}