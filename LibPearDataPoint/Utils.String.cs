using System.Text.RegularExpressions;

namespace LibPearDataPoint
{
    internal static partial class Utils
    {
        #region private fields

        /// <summary>
        /// Regular expression checker
        /// </summary>
        private static Regex _regEx;

        #endregion

        #region String Utils Initializator

        /// <summary>
        /// Initialization of string utilities
        /// </summary>
        static void InitString()
        {
            _regEx = new Regex(@"^([a-zA-z]+[0-9]*\.)*([a-zA-z]+[0-9]*)+$");
        }

        #endregion

        #region Methods

        /// <summary>
        /// The name should have a specific format representing points separated by dots like in OOP.
        /// Each word should start with character and might end in a numeral. No special characters are
        /// allowed.
        /// </summary>
        /// <param name="name">the string representing name of the dataitem</param>
        /// <returns>true if name is valid</returns>
        internal static bool IsNameValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            return _regEx.IsMatch(name);
        }

        #endregion
    }
}
