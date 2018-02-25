using System;

namespace DAL.Database
{
    internal class DBConnections
    {
        internal static string GetAppHarborConnection()
        {
            return "Server=bb2ac5e7-511f-48f9-8bbe-a88d01129099.sqlserver.sequelizer.com;" +
                "Database=dbbb2ac5e7511f48f98bbea88d01129099;" +
                "User ID=nagfzhmlepitgqrd;" +
                "Password=hejfP6tts3pPfQxgVabeJSW4GxcBHbH6KXJcXNKHQ53ccXvCno7qKJFwpoEq4ZSr;";
        }

        internal static string GetAzureConnection()
        {
            throw new NotImplementedException();
        }
    }
}