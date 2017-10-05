using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Magikarp.Utility.XBRL.Taxonomy
{
    /// <summary>
    /// 提供節點載入功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    internal class TagLoadingHelper
    {
        #region -- 靜態方法 (Shared Method ) --

        /// <summary>
        /// 載入指定節點清單。
        /// </summary>
        /// <typeparam name="TTag">節點型別。</typeparam>
        /// <param name="pi_sTagLocalName">節點 LocalName 。</param>
        /// <param name="pi_objSourceDocument">來源文件。</param>
        /// <returns>指定節點清單。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        internal static List<TTag> Loading<TTag>(string pi_sTagLocalName, XDocument pi_objSourceDocument) where TTag : BaseTag<TTag>, new()
        {
            List<TTag> objReturn = new List<TTag>();

            if(pi_objSourceDocument != null)
            {
                IEnumerable<XElement> objElements =
                    from XElement objElement in pi_objSourceDocument.Descendants()
                    where objElement.Name.LocalName.ToUpper() == pi_sTagLocalName.ToUpper()
                    select objElement;

                foreach (XElement objElement in objElements)
                {
                    TTag objTag = new TTag().Loading(objElement);

                    if (objTag != null)
                    {
                        objReturn.Add(objTag);
                    }
                }
            }        

            return objReturn;
        }

        #endregion    
    }
}
