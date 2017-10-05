using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Magikarp.Utility.XBRL.Taxonomy
{
    /// <summary>
    /// 提供 Attribute 操作功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    internal class AttributeHelper
    {
        #region -- 靜態方法 (Shared Method ) --

        /// <summary>
        /// 尋找指定屬性值。
        /// </summary>
        /// <param name="pi_sTargetAttributeName">目標屬性名稱。</param>
        /// <param name="pi_objSourceAttribute">來源屬性物件。</param>
        /// <returns>屬性值。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        internal static string FindValue(string pi_sTargetAttributeName, XAttribute pi_objSourceAttribute)
        {
            string sReturn = string.Empty;

            if (pi_objSourceAttribute.Name.LocalName == pi_sTargetAttributeName)
            {
                sReturn = pi_objSourceAttribute.Value;
            }
            else if (pi_objSourceAttribute.NextAttribute != null)
            {
                sReturn = FindValue(pi_sTargetAttributeName, pi_objSourceAttribute.NextAttribute);
            }

            return sReturn;
        }

        #endregion     
    }
}
