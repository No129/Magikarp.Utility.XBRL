using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Utility.XBRL.Taxonomy
{
    /// <summary>
    /// 提供 monetaryItemType 的 element 指定借貸方。
    /// </summary>
   public enum  BalanceEnum
    {
        /// <summary>
        /// 借餘科目。
        /// </summary>
        debit, 

        /// <summary>
        /// 貸餘科目。
        /// </summary>
        credit,

        /// <summary>
        /// 非金額項。
        /// </summary>
        NotMonetaryItemType
    }
}
