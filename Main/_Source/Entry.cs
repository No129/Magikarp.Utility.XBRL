using Magikarp.Platform.Behavior;
using Magikarp.Platform.Definition;
using Magikarp.Platform.UI.Entry;
using System;
using System.Windows;
using UI;

namespace Main
{

    /// <summary>
    /// 系統啟動功能。
    /// </summary>
    /// <remarks>
    /// Author: 黃竣祥
    /// Version: 20171005
    /// </remarks>
    public class Entry
    {
        /// <summary>
        /// 系統啟動點。
        /// </summary>
        /// <remarks>
        /// Author: 黃竣祥
        /// Time: 2017/10/05
        /// History: N/A
        /// DB Object: N/A      
        /// </remarks>
        [STAThread]
        public static void Main()
        {
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

            string sResult = ((IController)new FlowController()).DispatchCommand("Show_Main", string.Empty);
        }
    }
}
