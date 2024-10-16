using System;
using System.Collections;
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
            if (stocksInB3.Exists(vl => vl.B3Code == stock.B3Code))
            {
                return false;
            }
            stocksInB3.Add(stock);
            return true;
        }

        //Add a new Stock Movement, return true if success
        public bool addStockDailyValue(StockDailyValue stockDailyValue)
        {
            if (!(stocksInB3.Exists(vl => vl.B3Code == stockDailyValue.B3Code)))
            {
                throw new Exception("stock not added");
            }
            if (stockDailyValuesInB3.Exists(vl => vl.B3Code == stockDailyValue.B3Code && vl.Day == stockDailyValue.Day))
            {
                return false;
            }
            stockDailyValuesInB3.Add(stockDailyValue);
            return true;
        }

        //return the quantity registers of Stock in period
        public StockDailyValue? getStockDailyValue(string stockCode, DateOnly date)
        {
            return stockDailyValuesInB3.Find(vl => vl.B3Code == stockCode && vl.Day == date);

        }

        public double maxValueFromPeriod(string stockCode, DateOnly initialDate, DateOnly finalDate)       
        {
            double resp = 0;
            foreach(StockDailyValue item in stockDailyValuesInB3.FindAll(vl => vl.B3Code == stockCode && vl.Day >= initialDate && vl.Day <= finalDate)){
                resp = Math.Max(resp, item.ValueMax);
            }
            return resp;
        }
        
        public double minValueFromPeriod(string stockCode, DateOnly initialDate, DateOnly finalDate)
        {
            bool founded = false;
            double resp = double.MaxValue;
            foreach (StockDailyValue item in stockDailyValuesInB3.FindAll(vl => vl.B3Code == stockCode && vl.Day >= initialDate && vl.Day <= finalDate))
            {
                founded = true;
                resp = Math.Min(resp, item.ValueMin);
            }
            if (!founded) { resp = 0; }
            return resp;
        }

        public double avgValueFromPeriod(string stockCode, DateOnly initialDate, DateOnly finalDate)
        {
            bool founded = false;
            double max = 0;
            double min = double.MaxValue;
            foreach (StockDailyValue item in stockDailyValuesInB3.FindAll(vl => vl.B3Code == stockCode && vl.Day >= initialDate && vl.Day <= finalDate))
            {
                founded = true;
                max = Math.Max(max, item.ValueMax);
                min = Math.Min(min, item.ValueMin);
            }
            if (!founded) { return 0; }
            return  Math.Round(((max - min) / 2) + min, 5);

        }
    }
}
