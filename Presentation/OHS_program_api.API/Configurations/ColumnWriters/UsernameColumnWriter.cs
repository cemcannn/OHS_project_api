using Serilog.Events;
using System;
using System.Data;

namespace OHS_program_api.API.Configurations.ColumnWriters
{
    public interface IColumnWriter
    {
        DataColumn GetDataColumn();
        object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null);
    }

    public class UsernameColumnWriter : IColumnWriter
    {
        public DataColumn GetDataColumn() =>
            new DataColumn("user_name", typeof(string));

        public object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            var (username, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name");
            return value?.ToString() ?? null;
        }
    }
}