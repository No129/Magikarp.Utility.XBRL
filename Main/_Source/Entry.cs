using Magikarp.Platform.Behavior;
using Magikarp.Platform.Definition;
using Magikarp.Platform.UI.Entry;
using Magikarp.Utility.TransitData;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Xml.Linq;
using UI;

namespace Main
{

    /// <summary>
    /// 系統啟動功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171020
    /// </remarks>
    public class Entry
    {
        /// <summary>
        /// 系統啟動點。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: 
        ///     提供 Entry 架構的主視窗應用程式啟動資訊。 (黃竣祥 2017/10/20)
        /// DB Object: N/A      
        /// </remarks>
        [STAThread]
        public static void Main()
        {
            int nStep = 0;      // 程序進度指標。
            Boolean bRun = true;// 程序中斷旗標。

            while (bRun)
            {
                nStep += 1;
                switch (nStep)
                {
                    case 1:// 掛載必要函式庫。
                        AssemblyManager.GetInstance().MountAssembly(AssemblyTypeEnum.View,
                            new AssemblyInfoModel()
                            {
                                Namespace = "Magikarp.Platform.UI.Entry",
                                Instance = typeof(Magikarp.Platform.UI.Entry.MainView).Assembly
                            });

                        AssemblyManager.GetInstance().MountAssembly(AssemblyTypeEnum.View,
                            new AssemblyInfoModel()
                            {
                                Namespace = "UI",
                                Instance = typeof(XBRLViewModel).Assembly
                            });
                        break;

                    case 2:// 開啟主視窗。
                        string sParameter = string.Empty;
                        TransitDataAdapter objAdapter = new TransitDataAdapter();
                        MainViewDTO objDTO = new MainViewDTO();
                        List<XElement> objFUnctionEntryModels = new List<XElement>();
                        List<FunctionEntryModel> objModels =
                            new List<FunctionEntryModel>() {
                                new FunctionEntryModel(){
                                    FunctionCommand = "Show_Post",
                                    FunctionTitle = "Post",
                                    FunctionDescription ="提供帳款金額計算功能。"},
                                new FunctionEntryModel(){
                                    FunctionCommand = "Show_XBRL",
                                    FunctionTitle = "XBRL",
                                    FunctionDescription ="XBRL 定義文件讀取測試工具。",
                                    FunctionImagePath ="/UI;component/_Images/XBRLKiosk_l.png"}};

                        foreach (FunctionEntryModel objModel in objModels)
                        {
                            objFUnctionEntryModels.Add(XElement.Parse(objAdapter.Export<FunctionEntryModel>(objModel)));
                        }
                        objDTO.FunctionEntryModels = objFUnctionEntryModels;
                        sParameter = objAdapter.Export<MainViewDTO>(objDTO);

                        string sResult = ((IController)new FlowController()).DispatchCommand("Show_Main", sParameter);

                        break;

                    default:// 結束。
                        bRun = false;
                        break;
                }
            }
        }
    }
}
