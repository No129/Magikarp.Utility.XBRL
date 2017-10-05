using System.Xml.Linq;

namespace Magikarp.Utility.XBRL.Taxonomy
{
    /// <summary>
    /// 提供檔案操作功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    internal class FileHelper
    {
        #region -- 靜態方法 (Shared Method ) --

        /// <summary>
        /// 載入檔案內容。
        /// </summary>
        /// <param name="pi_sFileFullPath">待載入檔案完整路徑。</param>
        /// <returns>XDocument 執行個體。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        internal static XDocument LoadFile(string pi_sFileFullPath)
        {
            XDocument objReturn = null;

            if (System.IO.File.Exists(pi_sFileFullPath))
            {
                try
                {
                    objReturn = XDocument.Parse(System.IO.File.ReadAllText(pi_sFileFullPath));
                }
                catch
                {
                    objReturn = null;
                }                
            }

            return objReturn;
        }

        #endregion
    }
}
