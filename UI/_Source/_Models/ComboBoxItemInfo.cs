using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    /// <summary>
    /// 定義下拉選單項目。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class ComboBoxItemInfo
    {
        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 設定或取得 DisplayValue 值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string DisplayValue { get; set; }

        /// <summary>
        /// 設定或取得 SelectedValue 值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string SelectedValue { get; set; }

        #endregion
    }
}
