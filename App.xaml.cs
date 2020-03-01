using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ReleaseMemory
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        [STAThread]
        public static void Main()
        {
            const string semaphoreName = "ReleaseMemory";

            // Semaphoreクラスのインスタンスを生成し、アプリケーション終了まで保持する
            using (var semaphore = new System.Threading.Semaphore(1, 1, semaphoreName, out bool createdNew))
            {
                if (!createdNew)
                {
                    // 他のプロセスが先にセマフォを作っていた
                    MessageBox.Show("すでに起動しています", "Release Memory", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return; // プログラム終了
                }
                // アプリ起動
                App app = new App();
                app.InitializeComponent();
                app.Run();
            }
        }
        /// <summary>
        /// タスクトレイに表示するアイコン
        /// </summary>
        private NotifyIconWrapper notifyIcon;

        /// <summary>
        /// System.Windows.Application.Startup イベント を発生させる
        /// </summary>
        /// <param name="e">イベントデータ を格納している StartupEventArgs</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            this.notifyIcon = new NotifyIconWrapper();
        }

        /// <summary>
        /// System.Windows.Application.Exit イベント を発生させます。
        /// </summary>
        /// <param name="e">イベントデータ を格納している ExitEventArgs</param>
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            this.notifyIcon.Dispose();
        }
    }
}
