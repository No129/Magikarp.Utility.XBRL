using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Utility.XBRL.Taxonomy
{
    /// <summary>
    /// 定義 PresentationArc 節點資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class PresentationArcTag : BaseTag<PresentationArcTag>
    {
        #region -- 屬性 ( Properties ) --

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
        /// 取得或設定 arcrole 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string arcrole { get; set; }

        /// <summary>
        /// 取得或設定 from 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string from { get; set; }

        /// <summary>
        /// 取得或設定 to 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string to { get; set; }

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
        /// 取得或設定 order 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string order { get; set; }

        #endregion
    }
}
