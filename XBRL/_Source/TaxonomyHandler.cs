using Magikarp.Utility.XBRL.Taxonomy;
using System;
using System.Collections.Generic;

namespace Magikarp.Utility.XBRL
{
    /// <summary>
    /// 提供 Taxonomy 文件操作。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class TaxonomyHandler : IDisposable
    {

        #region -- 變數宣告 ( Declarations ) --   

        private string l_sDirectoryPath = string.Empty;

        private TaxonomyFileInfo l_objStartFileInfo = null;
        private List<ElementTag> l_objAllElementTags = null;
        private List<LabelTag> l_objAllLabelTags = null;
        private List<LocTag> l_objAllLocTags = null;
        private List<CalculationArcTag> l_objAllCalculationArcTags = null;
        private List<LabelArcTag> l_objAllLabelArcTags = null;
        private List<PresentationArcTag> l_objAllPresentationArcTags = null;

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_sDirectoryPath">根目錄。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public TaxonomyHandler(string pi_sDirectoryPath)
        {
            if (System.IO.Directory.Exists(pi_sDirectoryPath))
            {
                this.l_sDirectoryPath = pi_sDirectoryPath;
            }
        }

        /// <summary>
        /// 解構元。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        ~TaxonomyHandler()
        {
            this.CleanMemory();
        }

        #endregion

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 設定起始檔案。
        /// </summary>
        /// <param name="pi_objStartFilePathParser">起始檔案路徑解析器。</param>
        /// <returns>起始檔案初始是否正常。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public Boolean SetStartFileInfo(IStartFilePathParser pi_objStartFilePathParser)
        {
            Boolean bReturn = false;

            string sFullPath = string.Format("{0}{2}{1}",
                this.l_sDirectoryPath,
                pi_objStartFilePathParser.GetFullPath(),
                System.IO.Path.DirectorySeparatorChar);

            this.l_objStartFileInfo = new Taxonomy.TaxonomyFileInfo(sFullPath, null);
            this.l_objAllElementTags = null;
            this.l_objAllLabelTags = null;

            return bReturn;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得起始檔案執行物件。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public Taxonomy.TaxonomyFileInfo StartFileInfo
        {
            get { return this.l_objStartFileInfo; }
        }

        /// <summary>
        /// 取得所有 ElementTag 節點清單。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public List<Taxonomy.ElementTag> AllElementTags
        {
            get
            {
                if (this.l_objAllElementTags == null)
                {
                    this.l_objAllElementTags = this.l_objStartFileInfo.GetAllTags<ElementTag>("element");
                }
                return this.l_objAllElementTags;
            }
        }

        /// <summary>
        /// 取得所有 LabelTag 節點清單。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public List<Taxonomy.LabelTag> AllLabelTags
        {
            get
            {
                if (this.l_objAllLabelTags == null)
                {
                    this.l_objAllLabelTags = this.l_objStartFileInfo.GetAllTags<LabelTag>("label");
                }
                return this.l_objAllLabelTags;
            }
        }

        /// <summary>
        /// 取得所有 LocTag 節點清單。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public List<LocTag> AllLocTags
        {
            get
            {
                if (this.l_objAllLocTags == null)
                {
                    this.l_objAllLocTags = this.l_objStartFileInfo.GetAllTags<LocTag>("loc");
                }
                return this.l_objAllLocTags;
            }
        }

        /// <summary>
        /// 取得所有 CalculationArcTag 節點清單。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public List<CalculationArcTag> AllCalculationArcTags
        {
            get
            {
                if (this.l_objAllCalculationArcTags == null)
                {
                    this.l_objAllCalculationArcTags = this.l_objStartFileInfo.GetAllTags<CalculationArcTag>("calculationArc");
                }
                return this.l_objAllCalculationArcTags;
            }
        }

        /// <summary>
        /// 取得所有 LabelArcTag 節點清單。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public List<LabelArcTag> AllLabelArcTags
        {
            get
            {
                if (this.l_objAllLabelArcTags == null)
                {
                    this.l_objAllLabelArcTags = this.l_objStartFileInfo.GetAllTags<LabelArcTag>("labelArc");
                }
                return this.l_objAllLabelArcTags;
            }
        }

        /// <summary>
        /// 取得所有 LabelArcTag 節點清單。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public List<PresentationArcTag> AllPresentationArcTags
        {
            get
            {
                if (this.l_objAllPresentationArcTags == null)
                {
                    this.l_objAllPresentationArcTags = this.l_objStartFileInfo.GetAllTags<PresentationArcTag>("presentationArc");
                }
                return this.l_objAllPresentationArcTags;
            }
        }

        #endregion

        #region -- 介面實做 ( Implements ) - [IDisposable] --

        /// <summary>
        /// 解構物件。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        void IDisposable.Dispose()
        {
            this.CleanMemory();
        }

        #endregion

        #region -- 私有函式 ( Private Method) --

        /// <summary>
        /// 清除記錄體。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void CleanMemory()
        {
            this.l_objAllElementTags = null;
            this.l_objAllLabelTags = null;
            if (this.l_objStartFileInfo != null)
            {
                ((IDisposable)this.l_objStartFileInfo).Dispose();
            }
            this.l_objStartFileInfo = null;
        }

        #endregion

    }
}
