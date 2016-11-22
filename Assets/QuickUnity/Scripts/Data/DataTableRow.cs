using QuickUnity.Utilities;
using System.Collections.Generic;

namespace QuickUnity.Data
{
    /// <summary>
    /// Class DataTable.
    /// </summary>
    public abstract class DataTableRow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableRow"/> class.
        /// </summary>
        public DataTableRow()
        {
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            string output = string.Empty;
            Dictionary<string, object> map = ReflectionUtility.GetObjectFields(this);

            foreach (KeyValuePair<string, object> kvp in map)
            {
                output += string.Format("{0}: {1}, ", kvp.Key, kvp.Value);
            }

            return base.ToString() + string.Format("({0})", output.Substring(0, output.Length - 2));
        }
    }
}