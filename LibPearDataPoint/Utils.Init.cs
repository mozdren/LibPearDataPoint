namespace LibPearDataPoint
{
    /// <summary>
    /// Utilities entry point. Initialization of all utils modules, and general utilities.
    /// </summary>
    internal static partial class Utils
    {
        #region static constructor

        /// <summary>
        /// Initializes each part of utilities (String etc.)
        /// </summary>
        static Utils()
        {
            InitString();
        }

        #endregion
    }
}
