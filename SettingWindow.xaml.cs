
using System.Windows;
using System.Windows.Controls;

namespace ReleaseMemory
{
    /// <summary>
    /// SettingWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingWindow : Window
    {
        public SettingWindow()
        {
            InitializeComponent();
            // WindowにはFormみたいにLoadイベントないらしい（なんで）
            Activated += (s, e) => {
                // チェックリスト初期化
                // しきい値設定
                this.LimitSettingRed.SelectedIndex = Properties.Settings.Default.LimitRed;
                this.LimitSettingYellow.SelectedIndex = Properties.Settings.Default.LimitYellow;
                // 自動実行設定
                // TODO:自動設定OFFのときはAutoSettingLimitをOFFにしたいけど再描画できない
                this.AutoSettingFlg.IsChecked = Properties.Settings.Default.AutoFlg;
                this.AutoSettingLimit.SelectedIndex = Properties.Settings.Default.AutoLimit;
            };
        }

        /// <summary>
        /// 設定値を登録してウィンドウを閉じます
        /// </summary>
        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            // チェックリスト設定登録
            // しきい値設定
            Properties.Settings.Default.LimitRed = this.LimitSettingRed.SelectedIndex;
            Properties.Settings.Default.LimitYellow = this.LimitSettingYellow.SelectedIndex;
            // 自動実行設定
            Properties.Settings.Default.AutoFlg = (bool)this.AutoSettingFlg.IsChecked;
            Properties.Settings.Default.AutoLimit = this.AutoSettingLimit.SelectedIndex;
            // 設定を保存
            Properties.Settings.Default.Save();
            this.Close();
        }

        /// <summary>
        /// 設定値を破棄してウィンドウを閉じます
        /// </summary>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


    }
}
