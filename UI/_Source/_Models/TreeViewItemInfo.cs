using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    /// <summary>
    /// 樹狀結構項目資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class TreeViewItemInfo
    {

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public TreeViewItemInfo()
        {
            this.Items = new ObservableCollection<TreeViewItemInfo>();
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定項目 Title 值。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string Title { get; set; }

        /// <summary>
        /// 取得或設定子項清單。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public ObservableCollection<TreeViewItemInfo> Items { get; set; }

        #endregion
    }
}
