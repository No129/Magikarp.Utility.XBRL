using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Magikarp.Utility.XBRL.Taxonomy
{

    /// <summary>
    /// 定義 ComplexType 節點資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class ComplexTypeTag : BaseTag<ComplexTypeTag>
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
        internal override ComplexTypeTag Clone()
        {
            ComplexTypeTag objReturn = base.Clone();
            List<RefElementTag> objSequence = new List<RefElementTag>();

            foreach (RefElementTag objRefElementTag in Sequence)
            {
                RefElementTag objTag = new RefElementTag();

                objTag.@ref = objRefElementTag.@ref;
                objSequence.Add(objTag);
            }
            objReturn.Sequence = objSequence;
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
        internal override ComplexTypeTag Loading(XElement pi_objSource)
        {
            ComplexTypeTag objReturn = base.Loading(pi_objSource);

            objReturn.Sequence = this.GetSequence(pi_objSource);

            return objReturn;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定 Sequence 包含的 element 節點清單。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public List<RefElementTag> Sequence { get; set; }

        #endregion

        #region -- 私有函式 ( Private Method) --

        /// <summary>
        /// 取得 Sequence 節點內的 element 節點清單。
        /// </summary>
        /// <param name="pi_objSource">資料來源。</param>
        /// <returns> Sequence 節點內的 element 節點清單。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private List<RefElementTag> GetSequence(XElement pi_objSource)
        {
            List<RefElementTag> objReturn = new List<RefElementTag>();
            IEnumerable<XElement> objElements =
                from XElement objElement in pi_objSource.Descendants()
                where objElement.Name.LocalName.ToUpper() == "element".ToUpper()
                select objElement;

            foreach (XElement objElement in objElements)
            {
                objReturn.Add(new RefElementTag() { @ref = AttributeHelper.FindValue("ref", objElement.FirstAttribute) });
            };

            return objReturn;
        }

        #endregion

    }
}
