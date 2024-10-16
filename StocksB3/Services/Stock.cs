using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksB3.Services
{

    public class Stock
    {
        //Código do Ativo na B3
        public string B3Code = "";

        //Nome da Ativo na B3
        public string B3Name = "";
        
        //Constructor
        public Stock(string code, string name)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }
            if (code.Length>6)
            {
                throw new ArgumentOutOfRangeException("code");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            this.B3Code = code;
            this.B3Name = name;
         
        }
    }

}
