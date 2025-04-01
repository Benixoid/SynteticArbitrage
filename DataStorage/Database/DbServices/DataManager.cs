using DataStorage.Database.DbServices;

namespace DataStorage.Database.DbServices
{
    public class DataManager : IDataManager
    {
        public IPriceDifferencesRepository PriceDifferences { get; }

        public IPriceRepository Prices { get; }



        //public IUsersRepository Users { get; private set; }
        //public IRolesRepository Roles { get; private set; }
        public DataManager(IPriceDifferencesRepository priceDifferences, IPriceRepository price)
        {
            PriceDifferences = priceDifferences;
            Prices = price;
        }
    }
}
