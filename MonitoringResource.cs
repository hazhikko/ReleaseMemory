using System;
using System.ComponentModel;
using System.Management;

namespace ReleaseMemory
{
    /// <summary>
    /// Resourceの監視と情報の取得を行う
    /// </summary>
    public partial class MonitoringResource : Component
    {
        private ManagementClass mc;
        private ManagementObjectCollection moc;
        public double TotalPMemorySize { get; private set; }    // 合計物理メモリ(KB)
        public double FreePMemorySize { get; private set; }     // 利用可能な物理メモリ(KB)
        public double TotalVMemorySize { get; private set; }    // 合計仮想メモリ(KB)
        public double FreeVMemorySize { get; private set; }     // 利用可能な仮想メモリ(KB)
        public double UsedPMemorySize { get; private set; }     // 物理メモリ使用量(KB)
        public double UsedVMemorySize { get; private set; }     // 仮想メモリ使用量(KB)
        public int UsedPMemoryPercentage { get; private set; }      // 物理メモリの使用量（%）
        public int UsedVMemoryPercentage { get; private set; }      // 仮想メモリの使用量（%）
        public MonitoringResource()
        {
            InitializeComponent();

            using (mc = new ManagementClass("Win32_OperatingSystem")) {
                moc = mc.GetInstances();

                this.RefreshDatas();
                // 呼び出し側でusingしたのでいらない、ような気がする
                // moc.Dispose();
                // mc.Dispose();
            } ;

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

        public MonitoringResource(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
