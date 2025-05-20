using System.Configuration;

namespace VAMK.FWMS.Common
{
    /// <summary>
    /// This Class is used to read configuration settings from the app.config file.
    /// </summary>
    public sealed class ConfigSettings
    {
        #region Constants

        /// <summary>
        /// Database connection string.
        /// </summary>
        public const string DB_CONNECTION_STRING = "VAMK.FWMS.Data.ConnectionString";

        /// <summary>
        /// Key uses to fetch the Database Provider. "SQL, Oracle, etc..."
        /// </summary>
        public const string DB_PROVIDER = "DBProvider";

        #endregion

        #region Constructors

        public ConfigSettings()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reads the given configuration value from the app.config. 
        /// If the key does not exists then returns the default value.
        /// </summary>
        /// <param name="key">Configuration key to be read.</param>
        /// <param name="defaultValue">Default value of the key.</param>
        /// <returns></returns>
        public static string ReadConfigValue(string key, string defaultValue)
        {
            string configValue;

            try
            {
                configValue = ConfigurationManager.AppSettings[key];
                if (configValue == null)
                    configValue = defaultValue;
            }
            catch// (Exception ex)
            {
                configValue = defaultValue;
            }

            return configValue;
        }

        #endregion
    }
}
