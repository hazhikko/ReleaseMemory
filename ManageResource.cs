using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace ReleaseMemory
{
    /// <summary>
    /// Resourceの管理を行う
    /// </summary>
    public partial class ManageResource : Component
    {
        private ProcessStartInfo psInfo;
        private Process doEmptyExe;
        public ManageResource()
        {
            InitializeComponent();
        }

        /// <summary>
        /// empty.exeを実行してメモリを解放する
        /// 実行用CMDが見えると邪魔なのでバックグラウンドで実行する
        /// </summary>
        public void DoReleaseMemory()
        {
            psInfo = new ProcessStartInfo();
            // empty.exeをtmpに作成→実行
            // 使いまわすしサイズ小さいしtmpだから消さなくてもいいでしょう
            string file = Path.GetTempFileName().Replace(".tmp", ".exe");
            byte[] bin = Properties.Resources.emptyExe;
            using (FileStream fs = new FileStream(file, FileMode.Create))
                fs.Write(bin, 0, bin.Length);

            psInfo.FileName = file;         // 実行するファイル
            psInfo.Arguments = "*";         // 実行パラメータ
            psInfo.CreateNoWindow = true;   // コンソール・ウィンドウを開かない
            psInfo.UseShellExecute = false; // シェル機能を使用しない

            doEmptyExe = Process.Start(psInfo);
            doEmptyExe.WaitForExit();
            // empty.exe側でプロセス殺してそうな雰囲気
            // Console.WriteLine("Started psInfo process Id = " + doEmptyExe.Id);
        }

        public ManageResource(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
