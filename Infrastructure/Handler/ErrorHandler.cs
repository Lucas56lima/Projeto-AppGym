using Microsoft.Data.Sqlite;

namespace Infrastructure.Handler
{
    public class ErrorHandler
    {
        public static void HandlerSqliteException(SqliteException ex)
        {
            switch (ex.ErrorCode) 
            {
                case 12:                                    
                    throw new Exception("Não há dados cadastrados", ex);
                   
                case 6:
                    throw new Exception("Erro ao salvar dados no banco");
                default:
                    throw new Exception("Erro ao conectar com o banco");
            }
        }
        public static void HandleException(Exception ex)
        {
            // Log the exception (you can replace this with your preferred logging mechanism)
            LogException(ex);

            // Re-throw the exception if needed
            throw ex;
        }

        private static void LogException(Exception ex)
        {
            // Implement your logging logic here
            // For example, log to a file, database, or monitoring system
            Console.WriteLine("Logging exception: " + ex.ToString());
        }
    }
}
