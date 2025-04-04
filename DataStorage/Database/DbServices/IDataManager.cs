﻿namespace DataStorage.Database.DbServices
{
    public interface IDataManager
    {
        public IPriceDifferencesRepository PriceDifferences { get; }
        public IPriceRepository Prices { get; }
    }
}
