using System;

namespace DAL.Database
{
    internal class DBConnections
    {
        internal static string GetAppHarborConnection()
        {
            return ""; ///TODO: AppHarbor Connection String
        }

        internal static string GetAzureConnection()
        {
            throw new NotImplementedException();
        }
    }
}