using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Magikarp.Utility.XBRL.Taxonomy
{
    /// <summary>
    /// 定義 Element 節點資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class ElementTag : BaseTag<ElementTag>
    {
        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 深層複製功能。
        /// </summary>
        /// <returns>獨立的 ComplexTypeLag 執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        internal override ElementTag Clone()
        {
            ElementTag objReturn = base.Clone();

            if (ComplexTypeTag != null)
            {
                objReturn.ComplexTypeTag = ComplexTypeTag.Clone();
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
        internal override ElementTag Loading(XElement pi_objSource)
        {
            ElementTag objReturn = base.Loading(pi_objSource);
            if (pi_objSource.Parent.Name.LocalName.ToUpper() == "sequence".ToUpper())
            {
                objReturn = null;
            }
            else
            {
                IEnumerable<XElement> objQuery =
                    from XElement objElement in pi_objSource.Descendants()
                    where objElement.Name.LocalName.ToUpper() == "complexType".ToUpper()
                    select objElement;

                if (objQuery.Any())
                {
                    objReturn.ComplexTypeTag = new ComplexTypeTag().Loading(objQuery.First());
                }
            }

            return objReturn;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定 name 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string name { get; set; }

        /// <summary>
        /// 取得或設定 id 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string id { get; set; }

        /// <summary>
        /// 取得或設定 type 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string type { get; set; }

        /// <summary>
        /// 取得或設定 substitutionGroup 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string substitutionGroup { get; set; }

        /// <summary>
        /// 取得或設定 @abstract 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string @abstract { get; set; }

        /// <summary>
        /// 取得或設定 nillable 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string nillable { get; set; }

        /// <summary>
        /// 取得或設定 periodType 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string periodType { get; set; }

        /// <summary>
        /// 取得或設定 ComplexTypeTag 節點。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public ComplexTypeTag ComplexTypeTag { get; set; }

        #endregion

    }
}
