namespace Magikarp.Utility.XBRL.Taxonomy
{
    /// <summary>
    /// 定義 Loc 節點資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class LocTag : BaseTag<LocTag>
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
        /// 取得或設定 href 屬性值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string href { get; set; }

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

        #endregion
     
    }
}
