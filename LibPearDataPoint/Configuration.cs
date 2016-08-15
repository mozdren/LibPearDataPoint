using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Caching;
using System.Linq;
using System.Xml.Linq;

namespace LibPearDataPoint
{
    /// <summary>
    /// This class provides basic interface for configurations stored in various xml files.
    /// Concrete configuration properties should be written into separated partial implementations.
    /// The example of such code can be seen in Configuration.Helper.cs file.
    /// </summary>
    public partial class Configuration
    {
        #region private constants

        /// <summary>
        /// The configuration cache expiration in seconds
        /// </summary>
        private const int ConfigurationCacheExpiration = 60;

        /// <summary>
        /// The tracer constant
        /// </summary>
        private const string TracerConstant = "Configuration";

        /// <summary>
        /// The no such configuration constant
        /// </summary>
        private const string NoSuchConfiguration = "No such configuration";

        /// <summary>
        /// The wrong configuration constant
        /// </summary>
        private const string WrongConfiguration = "Wrong configuration";

        /// <summary>
        /// The error reading from cache constant
        /// </summary>
        private const string ErrorReadingFromCache = "Error reading from cache";

        #endregion

        #region private fields and properties

        /// <summary>
        /// The cache for method getValue
        /// </summary>
        private ObjectCache _getValueCache = MemoryCache.Default;

        /// <summary>
        /// The cache for method getValues
        /// </summary>
        private ObjectCache _getValuesCache = MemoryCache.Default;

        /// <summary>
        /// The configuration file name
        /// </summary>
        private string _configFileName;

        #endregion

        #region private constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Configuration"/> class from being created.
        /// </summary>
        private Configuration()
        {

        }

        #endregion

        #region public constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        /// <param name="filename">The configuration filename.</param>
        public Configuration(string filename)
        {
            _configFileName = filename;
        }

        #endregion

        #region public methods

        /// <summary>
        /// Gets the value from configuration file.
        /// </summary>
        /// <param name="key">The key of the configuration.</param>
        /// <returns>value of the configuration or empty string</returns>
        public string GetValue(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return string.Empty;
            }

            try
            {
                // try get value from cache
                if (_getValueCache.Contains(key))
                {
                    return _getValueCache.Get(key).ToString();
                }

                XElement root = XElement.Load(_configFileName);
                var configItem = (from el in root.Elements()
                                  where el.Attribute("key").Value == key
                                  select el).FirstOrDefault();

                if (null == configItem)
                {
                    Trace.WriteLine(string.Format("Error in {0}: {1} for key: {2}", TracerConstant, NoSuchConfiguration, key));
                    return string.Empty; // always return something, at least an empty collection
                }

                var configValue = configItem.Elements().FirstOrDefault();

                if (string.IsNullOrWhiteSpace(configValue.Value))
                {
                    Trace.WriteLine(string.Format("Error in {0}: {1} for key: {2}", TracerConstant, WrongConfiguration, key));
                    return string.Empty; // always return something, at least an empty collection
                }

                _getValueCache.Set(new CacheItem(key, configValue.Value), new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddSeconds(ConfigurationCacheExpiration) });
                return configValue.Value;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception in {0}: {1} for key: {2}", TracerConstant, ex.Message, key));
                return string.Empty; // always return something, at least an empty string
            }
        }

        /// <summary>
        /// Gets the values from configuration.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>values from configuration or an empty array</returns>
        public IEnumerable<string> GetValues(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return new List<string>();
            }

            try
            {
                // try get value from cache
                if (_getValuesCache.Contains(key))
                {
                    var value = _getValuesCache.Get(key) as IEnumerable<string>;
                    if (value == null)
                    {
                        // We were not able to read value from cache properly, and therefore we trace the error and continue
                        Trace.WriteLine(string.Format("Error in {0}: {1} for key: {2}", TracerConstant, ErrorReadingFromCache, key));
                    }
                    else
                    {
                        return value;
                    }
                }

                XElement root = XElement.Load(_configFileName);
                var configItem = (from el in root.Elements()
                                  where el.Attribute("key").Value == key
                                  select el).FirstOrDefault();

                if (null == configItem)
                {
                    Trace.WriteLine(string.Format("Error in {0}: {1} for key: {2}", TracerConstant, NoSuchConfiguration, key));
                    return new List<string>(); // always return something, at least an empty collection
                }

                var configValueElements = configItem.Elements();
                if (configValueElements.Any(item => item == null || string.IsNullOrWhiteSpace(item.Value)))
                {
                    Trace.WriteLine(string.Format("Error in {0}: {1} for key: {2}", TracerConstant, WrongConfiguration, key));
                    return new List<string>(); // always return something, at least an empty collection
                }

                var values = configValueElements.Select(item => item.Value).ToArray();

                _getValuesCache.Set(new CacheItem(key, values), new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddSeconds(ConfigurationCacheExpiration) });
                return values;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("Exception in {0}: {1} for key: {2}", TracerConstant, ex.Message, key));
                return new List<string>(); // always return something, at least an empty string
            }
        }

        #endregion

        #region private methods

        #endregion
    }
}
