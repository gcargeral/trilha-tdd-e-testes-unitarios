using StocksB3;
using StocksB3.Services;

namespace StocksB3.Tests
{
    public class StockTest
    {
        [Theory]
        [InlineData("GCAC20", "")]
        [InlineData("", "GC Ativos")]
        public void create_CodeAndNameMandatory_Exception(string code, string name)
        {
            Stock stock;
            Assert.Throws<Exception>(() => { stock = new Stock(code, name); });
        }

        [Theory]
        [InlineData("GCACT21", "GC Ativos 21")]
        public void create_CodeMaxSizeIs6_Exception(string code, string name)
        {
            Stock stock;
            Assert.Throws<Exception>(() => { stock = new Stock(code, name); });
        }

        [Theory]
        [InlineData("GCAC22", "GC Ativos 22")]
        public void create_Success(string code, string name)
        {
            Stock stock;
            stock = new Stock(code, name);

            Assert.Equal("GCAC22", stock.B3Code);
            Assert.Equal("GC Ativos 21", stock.B3Name);
        }

    }
}