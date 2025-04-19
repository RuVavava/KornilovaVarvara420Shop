using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KornilovaVarvara420Shop.DB
{
    internal class DBConnection
    {
        public static KornilovaVarvara420ShopEntities shopEntities = new KornilovaVarvara420ShopEntities();

        public static Users logginedUser;
    }
}
