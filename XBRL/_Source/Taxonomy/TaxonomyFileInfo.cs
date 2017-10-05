using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Magikarp.Utility.XBRL.Taxonomy
{
    /// <summary>
    /// 定義 Taxonomy 文件資訊。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class TaxonomyFileInfo : IDisposable
    {

        #region -- 變數宣告 ( Declarations ) -- 

        private string m_sFileFullPath = string.Empty;
        private TaxonomyFileInfo m_objParent = null;
        private List<TaxonomyFileInfo> m_objChildren = null;
        private XDocument l_objDocument = null;

        #endregion

        #region -- 建構/解構 ( Constructors/Destructor ) --

        /// <summary>
        /// 建構元。
        /// </summary>
        /// <param name="pi_sFileFullPath">檔案完整路徑。</param>
        /// <param name="pi_objParent">檔案的母項。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public TaxonomyFileInfo(string pi_sFileFullPath, TaxonomyFileInfo pi_objParent)
        {
            this.m_sFileFullPath = pi_sFileFullPath;
            this.m_objParent = pi_objParent;
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
        ~TaxonomyFileInfo()
        {
            this.CleanMemory();
        }

        #endregion

        #region -- 方法 ( Public Method ) --

        /// <summary>
        /// 取得所有指定 LocalName 的節點清單。
        /// </summary>
        /// <typeparam name="TTag">節點型別。</typeparam>
        /// <param name="pi_sTagLocalName">節點 LocalName 。</param>
        /// <returns>所有指定 LocalName 的節點清單。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        internal List<TTag> GetAllTags<TTag>(string pi_sTagLocalName) where TTag : BaseTag<TTag>, new()
        {
            List<TTag> objReturn = new List<TTag>();
            List<TTag> objCurrent = null;

            if (this.l_objDocument == null)
            {
                this.l_objDocument = FileHelper.LoadFile(this.m_sFileFullPath);
            }

            objCurrent = TagLoadingHelper.Loading<TTag>(pi_sTagLocalName, this.l_objDocument);

            foreach (TTag objTag in objCurrent)
            {
                objReturn.Add(objTag.Clone());
            }

            foreach (TaxonomyFileInfo objChild in this.Children)
            {
                foreach (TTag objTag in objChild.GetAllTags<TTag>(pi_sTagLocalName))
                {
                    objReturn.Add(objTag.Clone());
                }
            }

            return objReturn;
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得檔案名稱。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public string FileName
        {
            get { return System.IO.Path.GetFileName(this.m_sFileFullPath); }
        }

        /// <summary>
        /// 取得上層檔案。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public TaxonomyFileInfo Parent
        {
            get { return this.m_objParent; }
        }

        /// <summary>
        /// 取得次層檔案。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public List<TaxonomyFileInfo> Children
        {
            get
            {
                if (this.m_objChildren == null)
                {
                    this.LoadChildren();
                }
                return this.m_objChildren;
            }
        }

        /// <summary>
        /// 取得 Element 節點清單。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public List<ElementTag> ElementTagList
        {
            get
            {
                if (this.l_objDocument == null)
                {
                    this.l_objDocument = FileHelper.LoadFile(this.m_sFileFullPath);
                }
                return TagLoadingHelper.Loading<ElementTag>("element", this.l_objDocument);
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
        /// 載入次層檔案清單。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void LoadChildren()
        {
            List<TaxonomyFileInfo> objChildren = new List<TaxonomyFileInfo>();

            int nStep = 0;      // 程序進度指標。
            Boolean bRun = true;// 程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 載入當前文件。                        
                        if (this.l_objDocument == null)
                        {
                            this.l_objDocument = FileHelper.LoadFile(this.m_sFileFullPath);
                            bRun = (this.l_objDocument != null);
                        }
                        break;

                    case 2:// 取得 linkbaseRef 物件。
                        IEnumerable<XElement> objLinkbaseRefList =
                            from XElement objElement in this.l_objDocument.Descendants()
                            where objElement.Name.LocalName.ToUpper() == "linkbaseRef".ToUpper()
                            select objElement;

                        foreach (XElement objElement in objLinkbaseRefList)
                        {
                            string sHREF = AttributeHelper.FindValue("href", objElement.FirstAttribute);
                            string sFullPath = string.Empty;

                            if (sHREF.StartsWith("http:") == false)
                            {
                                sFullPath = string.Format("{0}{2}{1}", System.IO.Path.GetDirectoryName(this.m_sFileFullPath), sHREF, System.IO.Path.DirectorySeparatorChar);
                                objChildren.Add(new TaxonomyFileInfo(sFullPath, this));
                            }
                        }
                        break;

                    case 3:// 取得 import 物件。
                        IEnumerable<XElement> objImportList =
                            from XElement objElement in this.l_objDocument.Descendants()
                            where objElement.Name.LocalName.ToUpper() == "import".ToUpper()
                            select objElement;

                        foreach (XElement objElement in objImportList)
                        {
                            string sSchemaLocation = AttributeHelper.FindValue("schemaLocation", objElement.FirstAttribute);
                            string sFullPath = string.Empty;

                            if (sSchemaLocation.StartsWith("http:") == false)
                            {
                                sFullPath = string.Format("{0}{2}{1}", System.IO.Path.GetDirectoryName(this.m_sFileFullPath), sSchemaLocation, System.IO.Path.DirectorySeparatorChar);
                                objChildren.Add(new TaxonomyFileInfo(sFullPath, this));
                            }
                        }

                        break;
                    default://結束。
                        bRun = false;
                        break;
                }
            }
            this.m_objChildren = objChildren;
        }

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
            this.m_sFileFullPath = string.Empty;
            this.m_objParent = null;
            this.l_objDocument = null;
            if (this.m_objChildren != null)
            {
                foreach (TaxonomyFileInfo objChild in this.m_objChildren)
                {
                    ((IDisposable)objChild).Dispose();
                }
            }
            this.m_objChildren = null;
        }

        #endregion
    }
}
