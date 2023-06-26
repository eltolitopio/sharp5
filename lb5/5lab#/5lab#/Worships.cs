using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5lab_
{
    internal class Worships
    {
        public int NumberOfWorkers { get; set; } //кількість працівників в цеху 
        public int NumberOfResources { get; set; } //кількість ресурсі необхідна на 1 продукт
        public int NumberOfProduct { get; set; } //кількість продуктів, які робить 1 робітник
        public int CostOfSell { get; set; } //ціна одного продукту
        public int ResSpend; //кількість ресурсів витрачаємих за день
        public int Earn; //кількість грошей зароблених за день
        public Worships(int workers, int resource, int product, int sell)
        {
            NumberOfWorkers = workers;
            NumberOfResources = resource;
            NumberOfProduct = product;
            CostOfSell = sell;
            ResSpend = workers * resource * product;
            Earn = workers * product * sell;
        }
        


    }
}
