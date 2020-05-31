using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class ProgressBar
    {
        public int CalculateProgressBar(int quantity)
        {
            var maxQuantity = 50;
            var percent = (double)quantity / maxQuantity;

            return Convert.ToInt32(percent * 100);
        }
    }
}
