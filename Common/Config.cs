namespace Common
{
    public static class Config
    {
        public static string DatabasePath { get; } = "/data/database.db";
        public static string DataSourcePath { get; } = "/data/source";
        public static int NumberOfFoldersToIndex { get; } = 0; // Use 0 or less for indexing all folders

        //C:\Users\SamuelBartek\Desktop\load-balancer
    }
}