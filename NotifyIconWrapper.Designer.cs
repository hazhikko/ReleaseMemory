﻿namespace ReleaseMemory
{
    partial class NotifyIconWrapper
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotifyIconWrapper));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_DoEmpty = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Releas Memory";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Setting,
            this.toolStripMenuItem_DoEmpty,
            this.toolStripMenuItem_Exit});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(123, 70);
            // 
            // toolStripMenuItem_Setting
            // 
            this.toolStripMenuItem_Setting.Name = "toolStripMenuItem_Setting";
            this.toolStripMenuItem_Setting.Size = new System.Drawing.Size(122, 22);
            this.toolStripMenuItem_Setting.Text = "設定";
            // 
            // toolStripMenuItem_DoEmpty
            // 
            this.toolStripMenuItem_DoEmpty.Name = "toolStripMenuItem_DoEmpty";
            this.toolStripMenuItem_DoEmpty.Size = new System.Drawing.Size(122, 22);
            this.toolStripMenuItem_DoEmpty.Text = "手動実行";
            // 
            // toolStripMenuItem_Exit
            // 
            this.toolStripMenuItem_Exit.Name = "toolStripMenuItem_Exit";
            this.toolStripMenuItem_Exit.Size = new System.Drawing.Size(122, 22);
            this.toolStripMenuItem_Exit.Text = "終了";
            this.contextMenuStrip1.ResumeLayout(false);

        }

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Setting;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_DoEmpty;

        #endregion

        //private System.Windows.Forms.NotifyIcon notifyIcon1;
        //private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        //private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Open;
        //private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Exit;
    }
}
