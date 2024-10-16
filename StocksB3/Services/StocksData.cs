using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksB3.Services
{
    // Registro da Movimentação dos ativos

    
    public class StocksData
    {
        private List<Stock> stocksInB3 = new List<Stock>();
        private List<StockDailyValue> stockDailyValuesInB3 = new List<StockDailyValue>();

        public bool addStock(Stock stock)
        {
            return false;
        }

        //Add a new Stock Movement, return true if success
        public bool addStockDailyValue(StockDailyValue stockDailyValue)
        {
            return false;
        }

        //return the quantity registers of Stock in period
        public StockDailyValue? getStockDailyValue(string stockCode, DateOnly Date)
        {
            return null;
        }

        public double maxValueFromPeriod(string stockCode, DateOnly initialDate, DateOnly finalDate)       
        {
            return 0;
        }
        
        public double minValueFromPeriod(string stockCode, DateOnly initialDate, DateOnly finalDate)
        {
            return 0;
        }

        public double avgValueFromPeriod(string stockCode, DateOnly initialDate, DateOnly finalDate)
        {
            return 0;
        }
    }
}
