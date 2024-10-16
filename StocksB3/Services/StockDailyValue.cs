﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksB3.Services
{
    public class StockDailyValue
    {
        //Código do Ativo na B3
        public string B3Code = "";
        //Dia da movimentação
        public DateOnly Day = new DateOnly();
        //Valor máximo no dia
        public double ValueMax = 0;
        //Valor mínimo no dia
        public double ValueMin = 0;

        public StockDailyValue(string code, string date, double min, double max)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new Exception("code");
            }
            if (string.IsNullOrEmpty(date))
            {
                throw new Exception("date");
            }
            if (max < min)
            {
                throw new Exception("Max is lower of Min");
            }
            this.B3Code = code;
            this.Day = DateOnly.Parse(date, new  System.Globalization.CultureInfo("en-US"));
            this.ValueMin = min;    
            this.ValueMax = max;
        }

    }
}
