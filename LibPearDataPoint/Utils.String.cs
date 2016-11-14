using System.Collections.Generic;
using System.Diagnostics;
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

        /// <summary>
        /// Method spits string to array using a specific separator string
        /// </summary>
        /// <param name="text">text to be splitted</param>
        /// <param name="separatorString">separator of the array</param>
        /// <returns>array of separated strings</returns>
        internal static string[] Split(this string text, string separatorString)
        {
            if (text == null || string.IsNullOrEmpty(separatorString))
            {
                Trace.WriteLine("Splitting null text or separator is not defined");
                throw new System.Exception("text to be splitted cannot be null and separator must be nonempty");
            }

            var processedText = text;

            var retList = new List<string>();
            var index = processedText.IndexOf(separatorString);

            while (0 <= index)
            {
                var prefix = processedText.Substring(0, index);
                retList.Add(prefix);
                processedText = processedText.Substring(index + separatorString.Length);
                index = processedText.IndexOf(separatorString);
            }

            retList.Add(processedText);

            return retList.ToArray();
        }

        #endregion
    }
}
