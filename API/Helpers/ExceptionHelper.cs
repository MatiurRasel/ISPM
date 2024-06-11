using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace API.Helpers
{
    public static class ExceptionHelper
    {
        public static string GetExceptionDetails(Exception ex)
        {
            var sb = new StringBuilder();
            AppendExceptionDetails(sb, ex);
            return sb.ToString();
        }

        public static string GetExceptionSummary(Exception ex)
        {
            var sb = new StringBuilder();
            AppendExceptionSummary(sb, ex);
            return sb.ToString();
        }

        private static void AppendExceptionDetails(StringBuilder sb, Exception ex)
        {
            sb.AppendLine("Exception Details:");
            sb.AppendLine($"Message: {ex.Message}");
            sb.AppendLine($"StackTrace: {ex.StackTrace}");
            sb.AppendLine(new string('-', 50));

            if (ex is DbUpdateException dbUpdateEx)
            {
                AppendDbUpdateExceptionDetails(sb, dbUpdateEx);
            }
            else if (ex is SqlException sqlEx)
            {
                AppendSqlExceptionDetails(sb, sqlEx);
            }

            if (ex.InnerException != null)
            {
                sb.AppendLine("Inner Exception:");
                AppendExceptionDetails(sb, ex.InnerException);
            }
        }

        private static void AppendExceptionSummary(StringBuilder sb, Exception ex)
        {
            sb.AppendLine("Exception Summary:");
            sb.AppendLine($"Message: {ex.Message}");
            sb.AppendLine(new string('-', 50));

            if (ex is DbUpdateException dbUpdateEx)
            {
                AppendDbUpdateExceptionSummary(sb, dbUpdateEx);
            }
            else if (ex is SqlException sqlEx)
            {
                AppendSqlExceptionSummary(sb, sqlEx);
            }

            if (ex.InnerException != null)
            {
                sb.AppendLine("Inner Exception:");
                AppendExceptionSummary(sb, ex.InnerException);
            }
        }

        private static void AppendDbUpdateExceptionDetails(StringBuilder sb, DbUpdateException ex)
        {
            sb.AppendLine("DbUpdateException Details:");
            foreach (var entry in ex.Entries)
            {
                sb.AppendLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }
            sb.AppendLine(new string('-', 50));
        }

        private static void AppendSqlExceptionDetails(StringBuilder sb, SqlException ex)
        {
            sb.AppendLine("SqlException Details:");
            for (int i = 0; i < ex.Errors.Count; i++)
            {
                var error = ex.Errors[i];
                sb.AppendLine($"Error #{i}: {error.Message}");
            }
            sb.AppendLine(new string('-', 50));
        }

        private static void AppendDbUpdateExceptionSummary(StringBuilder sb, DbUpdateException ex)
        {
            sb.AppendLine("DbUpdateException Summary:");
            sb.AppendLine($"Entries: {ex.Entries.Count}");
            sb.AppendLine(new string('-', 50));
        }

        private static void AppendSqlExceptionSummary(StringBuilder sb, SqlException ex)
        {
            sb.AppendLine("SqlException Summary:");
            sb.AppendLine($"Errors: {ex.Errors.Count}");
            sb.AppendLine(new string('-', 50));
        }
    }

}
