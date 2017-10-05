using Magikarp.Platform.Definition.MVP;
using Magikarp.Platform.Utility.Region;
using Magikarp.Utility.MVVM;
using Magikarp.Utility.XBRL;
using Magikarp.Utility.XBRL.IFRS;
using Magikarp.Utility.XBRL.Taxonomy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace UI
{
    /// <summary>
    /// 主畫面的 ViewModel 功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class XBRLViewModel : IView, IRegionContent, INotifyPropertyChanged
    {
        #region -- 變數宣告 ( Declarations ) --   

        private MainView m_objView = null;

        private string m_sDTO = string.Empty;
        private IViewPresenter m_objPresenter = null;

        private readonly CollectionView m_objDocumentTypeCollection;
        private readonly CollectionView m_objIndustryTypeCollection;
        private readonly CollectionView m_objReportTypeCollection;

        private StartFilePathParser_20170331 m_objFactorInfo = new StartFilePathParser_20170331();

        private string m_sFileName = string.Empty;
        private string m_sRootDirectory = string.Empty;

        // TaxonomyHandler
        private TaxonomyHandler l_objTaxonomyHandler = null;

        // 檔案關連樹。
        private List<TaxonomyFileInfo> m_objTreeViewItemInfoCollection = new List<TaxonomyFileInfo>();
        // Element 節點清單。
        private List<ElementTag> m_objElementTags = new List<ElementTag>();
        private List<ElementTag> m_objAllElementTags = new List<ElementTag>();
        private List<LabelTag> m_objAllLabelTags = new List<LabelTag>();

        #endregion

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
        public XBRLViewModel()
        {
            this.RelayCommand = new RelayCommand(this.OnExecute);
            this.m_objDocumentTypeCollection = new CollectionView(this.SetDocumentTypeComboBox());
            this.m_objIndustryTypeCollection = new CollectionView(this.SetIndustryTypeComboBox());
            this.m_objReportTypeCollection = new CollectionView(this.SetReportTypeComboBox());
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
        ~XBRLViewModel()
        {
            this.Dispost();
        }

        #endregion

        #region -- 事件處理 ( Event Handlers ) --            

        /// <summary>
        /// 處理命令事件。
        /// </summary>
        /// <param name="pi_objParameter">命令參數。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void OnExecute(object pi_objParameter)
        {
            if (Enum.TryParse<TaxonamyFileFactorEnum>((string)pi_objParameter, out TaxonamyFileFactorEnum nFactory))
            {
                this.FileName = ((IStartFilePathParser)this.m_objFactorInfo).GetFullPath();

                this.IndustryStackPanelIsEnable = this.m_objFactorInfo.IndustryIsNeeded;
                this.ReportStackPanelIsEnable = this.m_objFactorInfo.ReportIsNeeded;
                this.IndustryType = this.m_objFactorInfo.IndustryType;
                this.ReportType = this.m_objFactorInfo.ReportType;
            }
            else
            {
                switch ((string)pi_objParameter)
                {
                    case "SelectRootDirectoryPath":
                        FolderBrowserDialog objDialog = new FolderBrowserDialog();

                        objDialog.ShowDialog();

                        this.RootDirectory = objDialog.SelectedPath;
                        break;

                    case "LoadingFile":
                        this.Dispost();

                        this.l_objTaxonomyHandler = new TaxonomyHandler(this.RootDirectory);
                        StartFilePathParser_20170331 objParser = new StartFilePathParser_20170331()
                        {
                            DocumentType = this.DocumentType,
                            IndustryType = this.IndustryType,
                            ReportType = this.ReportType
                        };

                        this.l_objTaxonomyHandler.SetStartFileInfo(objParser);
                        this.TaxonomyFiles = new List<TaxonomyFileInfo>() { this.l_objTaxonomyHandler.StartFileInfo };
                        this.ElementList = this.l_objTaxonomyHandler.StartFileInfo.ElementTagList;
                        this.AllElementTags = this.l_objTaxonomyHandler.AllElementTags;
                        this.AllLabelTags = this.l_objTaxonomyHandler.AllLabelTags;
                        break;
                }
            }
        }

        #endregion

        #region -- 屬性 ( Properties ) --

        /// <summary>
        /// 取得或設定中繼命令物件。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public ICommand RelayCommand { get; set; }

        public CollectionView DocumentTypeCollection { get { return m_objDocumentTypeCollection; } }

        public CollectionView IndustryTypeCollection { get { return m_objIndustryTypeCollection; } }

        public CollectionView ReportTypeCollection { get { return m_objReportTypeCollection; } }

        public string DocumentType
        {
            get { return this.m_objFactorInfo.DocumentType; }
            set
            {
                if (this.m_objFactorInfo.DocumentType != value)
                {
                    this.m_objFactorInfo.DocumentType = value;
                    this.OnPropertyChanged("DocumentType");
                }
            }
        }

        public string IndustryType
        {
            get { return this.m_objFactorInfo.IndustryType; }
            set
            {
                this.m_objFactorInfo.IndustryType = value;
                this.OnPropertyChanged("IndustryType");
            }
        }

        public string ReportType
        {
            get { return this.m_objFactorInfo.ReportType; }
            set
            {
                this.m_objFactorInfo.ReportType = value;
                this.OnPropertyChanged("ReportType");
            }
        }

        public Boolean DocumentStackPanelIsEnable
        {
            get { return this.m_objFactorInfo.DocumentIsNeeded; }
        }

        public Boolean IndustryStackPanelIsEnable
        {
            get { return this.m_objFactorInfo.IndustryIsNeeded; }
            set
            {
                this.m_objFactorInfo.IndustryIsNeeded = value;
                this.OnPropertyChanged("IndustryStackPanelIsEnable");
            }
        }

        public Boolean ReportStackPanelIsEnable
        {
            get { return this.m_objFactorInfo.ReportIsNeeded; }
            set
            {
                this.m_objFactorInfo.ReportIsNeeded = value;
                this.OnPropertyChanged("ReportStackPanelIsEnable");
            }
        }

        public string FileName
        {
            get { return this.m_sFileName; }
            set
            {
                this.m_sFileName = value;
                this.OnPropertyChanged("FileName");
            }
        }

        public string RootDirectory
        {
            get
            {
                return this.m_sRootDirectory;
            }
            set
            {
                this.m_sRootDirectory = value;
                this.OnPropertyChanged("RootDirectory");
            }
        }


        public List<TaxonomyFileInfo> TaxonomyFiles
        {
            get
            {
                return this.m_objTreeViewItemInfoCollection;
            }
            set
            {
                this.m_objTreeViewItemInfoCollection = value;
                this.OnPropertyChanged("TaxonomyFiles");
            }
        }

        public List<ElementTag> ElementList
        {
            get
            {
                return this.m_objElementTags;
            }
            set
            {
                this.m_objElementTags = value;
                this.OnPropertyChanged("ElementList");
            }
        }

        public List<ElementTag> AllElementTags
        {
            get { return this.m_objAllElementTags; }
            set
            {
                this.m_objAllElementTags = value;
                this.OnPropertyChanged("AllElementTags");
            }
        }

        public List<LabelTag> AllLabelTags
        {
            get { return this.m_objAllLabelTags; }
            set
            {
                this.m_objAllLabelTags = value;
                this.OnPropertyChanged("AllLabelTags");
            }
        }
        #endregion

        #region -- 介面實做 ( Implements ) - [IView] --

        string IView.DTO
        {
            get { return this.m_sDTO; }
            set { this.m_sDTO = value; }
        }

        IViewPresenter IView.Presenter
        {
            get { return this.m_objPresenter; }
            set { this.m_objPresenter = value; }
        }

        void IView.ExitView()
        {
            this.Dispost();
            this.m_objPresenter.OnViewEvent(DefaultCommandEnum.Exit.ToString());
            System.Windows.MessageBox.Show("bye~ bye~");
        }

        string IView.ShowView()
        {
            this.m_objView = new MainView();
            this.m_objView.DataContext = this;

            IRegionManager objRegionManager = ViewRegionCenter.GetInstance().GetRegionManager(typeof(TabRegionManager));
            IRegion objRegion = objRegionManager.Regions("XBRL");
            objRegion.Add(this);

            return string.Empty;
        }

        #endregion

        #region -- 介面實做 ( Implements ) - [IRegionContent] --

        /// <summary>
        /// 取得內容控制項。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        System.Windows.Controls.Control IRegionContent.Content { get { return this.m_objView; } }

        #endregion

        #region -- 私有函式 ( Private Method) --

        /// <summary>
        /// 設定 DocumentType 下拉選單項目。
        /// </summary>
        /// <returns>DocumentType 下拉選單項目。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private List<ComboBoxItemInfo> SetDocumentTypeComboBox()
        {
            this.DocumentType = "NONE";
            List<ComboBoxItemInfo> objReturn = new List<ComboBoxItemInfo>();

            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "查核意見書", SelectedValue = "AR" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "資產負債表", SelectedValue = "BS" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "綜合損益表", SelectedValue = "CI" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "現金流量表", SelectedValue = "SCF" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "權益變動表", SelectedValue = "ES" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "附註", SelectedValue = "NOTES" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "請選擇", SelectedValue = "NONE" });

            return objReturn;
        }

        /// <summary>
        /// 設定 IndustryType 下拉選單項目。
        /// </summary>
        /// <returns>IndustryType 下拉選單項目。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private List<ComboBoxItemInfo> SetIndustryTypeComboBox()
        {
            this.IndustryType = "NONE";
            List<ComboBoxItemInfo> objReturn = new List<ComboBoxItemInfo>();

            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "一般業", SelectedValue = "CI" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "金控業", SelectedValue = "FH" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "金融業", SelectedValue = "BASI" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "保險業", SelectedValue = "INS" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "證券期貨業", SelectedValue = "BD" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "異業合併", SelectedValue = "MIN" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "請選擇", SelectedValue = "NONE" });

            return objReturn;
        }

        /// <summary>
        /// 設定 ReportType 下拉選單項目。
        /// </summary>
        /// <returns>ReportType 下拉選單項目。</returns>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private List<ComboBoxItemInfo> SetReportTypeComboBox()
        {
            this.ReportType = "NONE";
            List<ComboBoxItemInfo> objReturn = new List<ComboBoxItemInfo>();

            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "合併報表", SelectedValue = "CR" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "個別報表", SelectedValue = "IR" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "個體報表", SelectedValue = "ER" });
            objReturn.Add(new ComboBoxItemInfo() { DisplayValue = "請選擇", SelectedValue = "NONE" });

            return objReturn;
        }

        /// <summary>
        /// 解構元。
        /// </summary>
        private void Dispost()
        {
            this.m_objElementTags = null;
            this.m_objAllElementTags = null;
            this.m_objAllLabelTags = null;
            if (this.l_objTaxonomyHandler != null)
            {
                ((IDisposable)this.l_objTaxonomyHandler).Dispose();
            }
            this.l_objTaxonomyHandler = null;
            this.m_sDTO = string.Empty;
            this.m_objView = null;
        }

        #endregion

        #region 屬性更新函式       

        /// <summary>
        /// 屬性改變事件。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 處理屬性改變事件。
        /// </summary>
        /// <param name="propertyName">改變的屬性名稱。</param>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
