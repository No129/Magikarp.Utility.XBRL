using System.Xml.Linq;

namespace Magikarp.Utility.XBRL.Taxonomy
{
    /// <summary>
    /// 提供 Tag 物件共同操作功能。
    /// </summary>
    /// <typeparam name="TTag">對應的節點型別。</typeparam>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public abstract class BaseTag<TTag> where TTag : new()
    {
        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 深層複制物件。
        /// </summary>
        /// <returns>複制物件。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        internal virtual TTag Clone()
        {
            TTag objReturn = new TTag();

            foreach (System.Reflection.PropertyInfo objProperty in typeof(TTag).GetProperties())
            {
                objProperty.SetValue(objReturn, objProperty.GetValue(this));
            }

            return objReturn;
        }

        /// <summary>
        /// 載入來源資料。
        /// </summary>
        /// <param name="pi_objSource">來源資料。</param>
        /// <returns>資料物件。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        internal virtual TTag Loading(XElement pi_objSource)
        {
            TTag objReturn = new TTag();

            foreach (System.Reflection.PropertyInfo objProperty in typeof(TTag).GetProperties())
            {
                if (objProperty.PropertyType == typeof(string))
                {
                    string sPropertyName = objProperty.Name;
                    string sValue = string.Empty;

                    try
                    {
                        sValue = AttributeHelper.FindValue(sPropertyName, pi_objSource.FirstAttribute);
                    }
                    catch
                    {
                        sValue = string.Empty;
                    }
                   
                    objProperty.SetValue(objReturn, sValue);
                }              
            }

            return objReturn;
        }

        #endregion

    }
}
