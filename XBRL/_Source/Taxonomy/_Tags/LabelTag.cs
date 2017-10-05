using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Magikarp.Utility.XBRL.Taxonomy
{

    /// <summary>
    /// 定義 Label 節點資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class LabelTag : BaseTag<LabelTag>
    {
        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 載入資料。
        /// </summary>
        /// <param name="pi_objSource"></param>
        /// <returns>執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        internal override LabelTag Loading(XElement pi_objSource)
        {
            LabelTag objReturn = base.Loading(pi_objSource);

            objReturn.TagValue = pi_objSource.Value;

            return objReturn;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

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
        /// 取得或設定 label 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string label { get; set; }

        /// <summary>
        /// 取得或設定 title 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string title { get; set; }

        /// <summary>
        /// 取得或設定 lang 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string lang { get; set; }

        /// <summary>
        /// 取得或設定節點 Value 值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string TagValue { get; set; }

        #endregion
    }
}
