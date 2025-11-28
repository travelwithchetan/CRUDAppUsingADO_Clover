namespace CRUDAppUsingADO
{
    public static class ConnectionString
    {
        private static string cs = "server=CHETAN\\SQLEXPRESS;Database=CrudADOdb;Trusted_Connection=True;";
        public static string dbcs { get => cs; }
    }
}
