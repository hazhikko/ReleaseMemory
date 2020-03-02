using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management;

namespace ReleaseMemory
{
    /// <summary>
    /// Resourceの管理を行う
    /// </summary>
    public partial class ManageResource : Component
    {
        private readonly ManagementClass mc;
        private ManagementObjectCollection moc;
        public double TotalPMemorySize { get; private set; }    // 合計物理メモリ(KB)
        public double FreePMemorySize { get; private set; }     // 利用可能な物理メモリ(KB)
        public double TotalVMemorySize { get; private set; }    // 合計仮想メモリ(KB)
        public double FreeVMemorySize { get; private set; }     // 利用可能な仮想メモリ(KB)
        public double UsedPMemorySize { get; private set; }     // 物理メモリ使用量(KB)
        public double UsedVMemorySize { get; private set; }     // 仮想メモリ使用量(KB)
        public int UsedPMemoryPercentage { get; private set; }      // 物理メモリの使用量（%）
        public int UsedVMemoryPercentage { get; private set; }      // 仮想メモリの使用量（%）

        public ManageResource()
        {
            InitializeComponent();
            using (mc = new ManagementClass("Win32_OperatingSystem"))
            {
                moc = mc.GetInstances();

                moc.Dispose();
                mc.Dispose();
            };
        }

        /// <summary>
        /// インスタンスを取り直してデータを更新する
        /// </summary>
        public void RefreshDatas()
        {
            moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                TotalPMemorySize = double.Parse(mo["TotalVisibleMemorySize"].ToString());
                FreePMemorySize = double.Parse(mo["FreePhysicalMemory"].ToString());
                TotalVMemorySize = double.Parse(mo["TotalVirtualMemorySize"].ToString());
                FreeVMemorySize = double.Parse(mo["FreeVirtualMemory"].ToString());

                ////合計物理メモリ
                //Console.WriteLine("合計物理メモリ:{0}KB", UsedPMemorySize);
                ////利用可能な物理メモリ
                //Console.WriteLine("利用可能物理メモリ:{0}KB", FreePMemorySize);
                ////合計仮想メモリ
                //Console.WriteLine("合計仮想メモリ:{0}KB", TotalVMemorySize);
                ////利用可能な仮想メモリ
                //Console.WriteLine("利用可能仮想メモリ:{0}KB", FreeVMemorySize);

                // 物理メモリ使用量
                UsedPMemorySize = TotalPMemorySize - FreePMemorySize;
                UsedPMemoryPercentage = (int)Math.Round((UsedPMemorySize / TotalPMemorySize) * 100);

                // 仮想メモリ使用量
                UsedVMemorySize = TotalVMemorySize - FreeVMemorySize;
                UsedVMemoryPercentage = (int)Math.Round((UsedVMemorySize / TotalVMemorySize) * 100);

                mo.Dispose();
            }
        }

        /// <summary>
        /// empty.exeを実行してメモリを解放する
        /// 実行用CMDが見えると邪魔なのでバックグラウンドで実行する
        /// </summary>
        public void DoReleaseMemory()
        {
            ProcessStartInfo psInfo = new ProcessStartInfo();
            // empty.exeをtmpに作成→実行
            string file = Path.GetTempFileName().Replace(".tmp", ".exe");
            byte[] bin = Properties.Resources.emptyExe;
            using (FileStream fs = new FileStream(file, FileMode.Create))
                fs.Write(bin, 0, bin.Length);

            psInfo.FileName = file;         // 実行するファイル
            psInfo.Arguments = "*";         // 実行パラメータ
            psInfo.CreateNoWindow = true;   // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false; // シェル機能を使用しない

            Process doEmptyExe = Process.Start(psInfo);
            doEmptyExe.WaitForExit();
            doEmptyExe.Dispose();
        }

        public ManageResource(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
