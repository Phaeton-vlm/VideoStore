using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore
{
    class ServicesItem
    {
        string _name;
        double _price;

        public string Name 
        {
            get
            {
                return _name;
            }
        }

        public double Price
        {
            get
            {
                return _price;
            }
        }

        public ServicesItem(string name, double price)
        {
            _name = name;
            _price = price;
        }
    }

    static class ServicesModel
    {
        static ServicesItem[] servModels = new ServicesItem[5];
        

        public static void addSerice(int i, string name, double price)
        {
            servModels[i] = new ServicesItem(name, price);

        }

        public static double FindPriceByName(string name)
        {
            for (int i = 0; i < servModels.Length; i++)
            {
                if(servModels[i].Name == name)
                {
                    return servModels[i].Price;
                }
            }

            return 0;
        }
    }
}
