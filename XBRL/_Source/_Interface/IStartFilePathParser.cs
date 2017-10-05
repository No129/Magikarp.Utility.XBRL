using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Utility.XBRL
{

    /// <summary>
    /// 定義起始檔案完整路徑功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public interface IStartFilePathParser
    {

        /// <summary>
        /// 取得起始檔案完整路徑。
        /// </summary>
        /// <returns>起始檔案完整路徑。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Version: 20171005
        /// </remarks>
        string GetFullPath();

    }
}
