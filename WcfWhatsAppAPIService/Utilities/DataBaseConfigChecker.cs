using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace WcfWhatsAppAPIService.Utilities
{
	public class DataBaseConfigChecker
	{
        private static bool _initialized = false;
        private static Exception _lastError = null;
        private static DateTime _lastRefreshTime = DateTime.MinValue;

        public static string AccessToken { get; private set; }
        public static string Version { get; private set; }
        public static string PhoneNumberId { get; private set; }
        public static string ApiUrl { get; private set; }

        public static string ConnectionString
        {
            get
            {
                var configValue = ConfigurationManager.ConnectionStrings["WhatsAppDB"]?.ConnectionString;

                if (string.IsNullOrEmpty(configValue))
                    throw new ApplicationException("La cadena de conexión no está configurada en Web.config.");

                return configValue;
            }
        }

        static DataBaseConfigChecker()
        {
            try
            {
                InitializeConfig();
                _initialized = true;
            }
            catch (Exception ex)
            {
                _lastError = ex;
                throw new ApplicationException("Error inicializando WhatsAppConfig. Ver inner exception para detalles.", ex);
            }
        }

        private static void InitializeConfig()
        {
            if (!TestConnection(ConnectionString))
                throw new ApplicationException("No se pudo establecer conexión con la base de datos.");

            if (!TableExists("UrlConfig"))
                throw new ApplicationException("La tabla 'UrlConfig', donde almacena la configuración, no existe en la base de datos.");

            const string query = @"
                SELECT TOP 1
                    UserAccessToken,
                    Version,
                    PhoneNumberId,
                    ApiUrl
                FROM UrlConfig
                ORDER BY LastUpdate DESC";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        AccessToken = reader["UserAccessToken"] as string ?? string.Empty;
                        Version = reader["Version"] as string ?? string.Empty;
                        PhoneNumberId = reader["PhoneNumberId"] as string ?? string.Empty;
                        ApiUrl = reader["ApiUrl"] as string ?? string.Empty;
                    }
                    else
                    {
                        throw new ApplicationException("No se encontraron registros en la tabla UrlConfig.");
                    }
                }
            }
        }

        private static bool TestConnection(string connectionString)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    return conn.State == ConnectionState.Open;
                }
            }
            catch (SqlException ex)
            {
                LogError("Error de conexión SQL", ex);
                return false;
            }
        }

        private static bool TableExists(string tableName)
        {
            string query = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableName}'";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private static void LogError(string message, Exception ex)
        {
            try
            {
                EventLog.WriteEntry("WcfWhatsAppConfigService", $"{message}: {ex.Message}", EventLogEntryType.Error);
            }
            catch
            {
                // Ignorar errores de logging para evitar bucles de excepción
            }
        }

        public static void EnsureInitialized()
        {
            TimeSpan refreshInterval = TimeSpan.FromMinutes(1);

            if (!_initialized || (DateTime.Now - _lastRefreshTime) > refreshInterval)
            {
                try
                {
                    InitializeConfig();
                    _lastRefreshTime = DateTime.Now;
                    _initialized = true;
                    _lastError = null;
                }
                catch (Exception ex)
                {
                    _initialized = false;
                    _lastError = ex;
                    throw new ApplicationException("Error al refrescar la configuración", ex);
                }
            }

            if (!_initialized && _lastError != null)
            {
                throw new ApplicationException("WhatsAppConfig no se inicializó correctamente", _lastError);
            }
        }
    }
}