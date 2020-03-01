using System;
using System.ComponentModel;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using Application = System.Windows.Application;
using Timer = System.Timers.Timer;
using System.Drawing;

namespace ReleaseMemory
{

    /// <summary>
    /// タスクトレイ通知アイコン関連処理
    /// </summary>
    public partial class NotifyIconWrapper : Component
    {
        //private ProcessStartInfo psInfo;
        //private Process doEmptyExe;
        /// <summary>
        /// NotifyIconWrapper クラス を生成、初期化します。
        /// </summary>
        public NotifyIconWrapper()
        {
            this.InitializeComponent();

            this.toolStripMenuItem_Setting.Click += this.ToolStripMenuItem_Setting_Click;
            this.toolStripMenuItem_DoEmpty.Click += this.ToolStripMenuItem_DoEmpty_Click;
            this.toolStripMenuItem_Exit.Click += this.ToolStripMenuItem_Exit_Click;

            using (var mr = new MonitoringResource()) {
                // 一定間隔で実行
                // 1秒だと自動解放しても2回目走るので多めに
                var timer = new Timer(Properties.Settings.Default.MonitoringTime);
                timer.Elapsed += (sender, e) =>
                {
                    mr.RefreshDatas();
                    this.ChangeIcon(mr.UsedPMemoryPercentage);
                    this.ChangeToolChip(mr.UsedPMemoryPercentage);
                    if (Properties.Settings.Default.AutoFlg)
                    {
                        this.AutoRelease(Properties.Settings.Default.AutoFlg, mr.UsedPMemoryPercentage);
                    }
                };
                timer.Start();
            } ;
        }

        /// <summary>
        /// コンテナ を指定して NotifyIconWrapper クラス を生成、初期化します。
        /// </summary>
        /// <param name="container">コンテナ</param>
        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);
            this.InitializeComponent();
        }

        /// <summary>
        /// アイコンの色を変更する
        /// </summary>
        /// <param name="percentage">メモリ使用率</param>
        private void ChangeIcon(int percentage)
        {
            Icon icon_green = Properties.Resources.icon_green;
            Icon icon_yellow = Properties.Resources.icon_yellow;
            Icon icon_red = Properties.Resources.icon_red;

            if (percentage >= GetLimitVal(Properties.Settings.Default.LimitRed))
            {
                this.notifyIcon1.Icon = icon_red;
            }
            else if (percentage >= GetLimitVal(Properties.Settings.Default.LimitYellow))
            {
                this.notifyIcon1.Icon = icon_yellow;
            }
            else
            {
                this.notifyIcon1.Icon = icon_green;
            }

        }
        /// <summary>
        /// タスクトレイToolChipの内容に現在のメモリ使用量を設定する
        /// </summary>
        /// <param name="percentage">メモリ使用率</param>
        private void ChangeToolChip(int percentage)
        {
            this.notifyIcon1.Text = string.Format(Properties.Resources.toolChipText, percentage);
        }

        /// <summary>
        /// インデックスから該当する値を返す
        /// </summary>
        /// <param name="limitIndex">しきい値に使用しているリストのインデックス</param>
        private int GetLimitVal(int limitIndex)
        {
            int limitVal = Properties.Settings.Default.LimitVal2;
            switch (limitIndex)
            {
                case 0: limitVal = Properties.Settings.Default.LimitVal0; break;
                case 1: limitVal = Properties.Settings.Default.LimitVal1; break;
                case 2: limitVal = Properties.Settings.Default.LimitVal2; break;
                case 3: limitVal = Properties.Settings.Default.LimitVal3; break;
                default: break;
            }
            return limitVal;
        }

        /// <summary>
        /// 自動実行がONのとき、自動実行設定値より使用率が高ければ解放する
        /// </summary>
        /// <param name="flg">自動実行フラグ</param>
        /// <param name="percentage">メモリ使用率</param>
        private void AutoRelease(bool flg, int percentage)
        {
            if (flg)
            {
                if (percentage >= GetLimitVal(Properties.Settings.Default.AutoLimit))
                {
                    using (var rm = new ManageResource())
                    {
                        rm.DoReleaseMemory();
                    };
                }                
            }
        }

        /// <summary>
        /// コンテキストメニュー "表示" を選択したとき呼ばれる
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void ToolStripMenuItem_Setting_Click(object sender, EventArgs e)
        {
            var wnd = new SettingWindow();
            wnd.Show();
        }

        /// <summary>
        /// コンテキストメニュー "手動実行" を選択したとき呼ばれる
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void ToolStripMenuItem_DoEmpty_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Properties.Resources.doEmptyDialogMessage,
                                                    Properties.Resources.doEmptyDialogTitle,
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                using (var rm = new ManageResource())
                {
                    rm.DoReleaseMemory();
                };
            }
        }

        /// <summary>
        /// コンテキストメニュー "終了" を選択したとき呼ばれる
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.Icon.Dispose();
            Application.Current.Shutdown();
        }
    }
}
