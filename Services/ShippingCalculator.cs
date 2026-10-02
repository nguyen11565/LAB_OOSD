using System;
using System.Data.SqlClient;
using eShopping.Data;

namespace eShopping.Services
{
    public class ShippingCalculator
    {
        public decimal CalculateShippingFee(decimal subTotal, string methodId, string regionName)
        {
            if (methodId == "EXPRESS" && subTotal >= 1000000m) return 0m;
            if (methodId == "SAME_DAY" && subTotal >= 5000000m) return 0m;

            object result = Db.Scalar(
                "SELECT base_fee FROM SHIPPING_RATE WHERE method_id = @m AND region_name = @r",
                new SqlParameter("@m", methodId),
                new SqlParameter("@r", regionName)
            );

            if (result != null && result != DBNull.Value)
                return Convert.ToDecimal(result);

            return methodId == "SAME_DAY" ? 100000m : (methodId == "EXPRESS" ? 50000m : 30000m);
        }
    }
}