using Magikarp.Utility.XBRL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magikarp.Utility.XBRL.IFRS
{
    /// <summary>
    /// 提供 IFRS ver:20170331 版本的起始檔案路徑解析功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class StartFilePathParser_20170331 : IStartFilePathParser
    {
        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定文件種類。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string DocumentType { get; set; } = "NONE";

        /// <summary>
        /// 取得或設定行業種類。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string IndustryType { get; set; } = "NONE";

        /// <summary>
        /// 取得或設定報表種類。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string ReportType { get; set; } = "NONE";

        /// <summary>
        /// 取得或設定文件種類是否必須。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public Boolean DocumentIsNeeded { get; set; } = true;

        /// <summary>
        /// 取得或設定行業種類是否必須。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public Boolean IndustryIsNeeded { get; set; } = false;

        /// <summary>
        /// 取得或設定報表種類是否必須。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public Boolean ReportIsNeeded { get; set; } = false;

        #endregion

        #region -- 介面實做 ( Implements ) - [IStartFilePathParser] --

        /// <summary>
        /// 取得起始檔案完整路徑。
        /// </summary>
        /// <returns>起始檔案完整路徑。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Version: 20171005
        /// </remarks>
        string IStartFilePathParser.GetFullPath()
        {
            string sReturn = string.Empty;
            DocumentTypeEnum nDocumentType = DocumentTypeEnum.NOTES;

            int nStep = 0;//程序進度指標。
            Boolean bRun = true;//程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 設定必要參數。
                        if (Enum.TryParse<DocumentTypeEnum>(this.DocumentType, out nDocumentType))
                        {
                            switch (nDocumentType)
                            {
                                case DocumentTypeEnum.AR:
                                    this.IndustryIsNeeded = false;
                                    this.IndustryType = "NONE";
                                    this.ReportIsNeeded = false;
                                    this.ReportType = "NONE";
                                    break;
                                case DocumentTypeEnum.ES:
                                    this.IndustryIsNeeded = false;
                                    this.IndustryType = "NONE";
                                    this.ReportIsNeeded = true;
                                    break;
                                default:
                                    this.IndustryIsNeeded = true;
                                    this.ReportIsNeeded = false;
                                    this.ReportType = "NONE";
                                    break;
                            }
                        }
                        else
                        {
                            this.IndustryIsNeeded = false;
                            this.IndustryType = "NONE";
                            this.ReportIsNeeded = false;
                            this.ReportType = "NONE";
                        }
                        break;

                    case 2:// 判斷是否具備必要參數。
                        bRun = (((this.DocumentIsNeeded == true && this.DocumentType == "NONE") ||
                                (this.IndustryIsNeeded == true && this.IndustryType == "NONE") ||
                                (this.ReportIsNeeded == true && this.ReportType == "NONE")) == false);

                        break;

                    case 3:// 建立完整路徑。
                        string sDocument = string.Empty;
                        string sIndustry = this.IndustryType == "NONE" ? string.Empty : string.Format("-{0}", this.IndustryType);
                        string sReport = this.ReportType == "NONE" ? string.Empty : string.Format("-{0}", this.ReportType);

                        switch (nDocumentType)
                        {
                            case DocumentTypeEnum.BS:
                            case DocumentTypeEnum.CI:
                                sDocument = "BSCI";
                                break;
                            default:
                                sDocument = this.DocumentType;
                                break;
                        }

                        string sDirectory = string.Empty;

                        switch (nDocumentType)
                        {
                            case DocumentTypeEnum.AR:
                                sDirectory = string.Format("{0}{1}", nDocumentType.ToString(), System.IO.Path.DirectorySeparatorChar);
                                break;
                            case DocumentTypeEnum.BS:
                            case DocumentTypeEnum.CI:
                                sDirectory = string.Format("BSCI{0}", System.IO.Path.DirectorySeparatorChar);
                                break;
                            case DocumentTypeEnum.ES:
                                sDirectory = string.Format("{0}{2}{1}{2}", nDocumentType.ToString(), this.ReportType, System.IO.Path.DirectorySeparatorChar);
                                break;
                            case DocumentTypeEnum.NOTES:
                            case DocumentTypeEnum.SCF:
                                sDirectory = string.Format("{0}{2}{1}{2}", nDocumentType.ToString(), this.IndustryType, System.IO.Path.DirectorySeparatorChar);
                                break;
                        }
                        sReturn = string.Format("{3}tifrs-{0}{1}{2}-2017-03-31.xsd", sDocument, sIndustry, sReport, sDirectory);

                        break;
                    default://結束。
                        bRun = false;
                        break;
                }
            }

            return sReturn;
        }

        #endregion
    }
}
