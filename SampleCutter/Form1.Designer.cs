
namespace SampleCutter
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

    #region Windows 窗体设计器生成的代码

    /// <summary>
    /// 设计器支持所需的方法 - 不要修改
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
      components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      button_openFolder = new Button();
      label2 = new Label();
      textBox_cutValue = new TextBox();
      button_execute = new Button();
      progressBar = new ProgressBar();
      toolTip1 = new ToolTip(components);
      label_left = new Label();
      button_clearFiles = new Button();
      SuspendLayout();
      // 
      // button_openFolder
      // 
      button_openFolder.Location = new Point(13, 33);
      button_openFolder.Margin = new Padding(4);
      button_openFolder.Name = "button_openFolder";
      button_openFolder.Size = new Size(84, 25);
      button_openFolder.TabIndex = 0;
      button_openFolder.Text = "导入文件";
      button_openFolder.UseVisualStyleBackColor = true;
      button_openFolder.Click += button_openFolder_Click;
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new Point(14, 12);
      label2.Margin = new Padding(4, 0, 4, 0);
      label2.Name = "label2";
      label2.Size = new Size(111, 17);
      label2.TabIndex = 2;
      label2.Text = "切割阈值(0-32767)";
      // 
      // textBox_cutValue
      // 
      textBox_cutValue.Location = new Point(133, 9);
      textBox_cutValue.Margin = new Padding(4);
      textBox_cutValue.Name = "textBox_cutValue";
      textBox_cutValue.Size = new Size(62, 23);
      textBox_cutValue.TabIndex = 3;
      toolTip1.SetToolTip(textBox_cutValue, "值越大，采样被切得越多。\r\n16应该会是比较稳妥的值。");
      textBox_cutValue.TextChanged += textBox_cutValue_TextChanged;
      // 
      // button_execute
      // 
      button_execute.Location = new Point(197, 33);
      button_execute.Margin = new Padding(4);
      button_execute.Name = "button_execute";
      button_execute.Size = new Size(84, 25);
      button_execute.TabIndex = 4;
      button_execute.Text = "执行";
      button_execute.UseVisualStyleBackColor = true;
      button_execute.Click += button_execute_Click;
      // 
      // progressBar
      // 
      progressBar.Location = new Point(14, 66);
      progressBar.Margin = new Padding(4);
      progressBar.Name = "progressBar";
      progressBar.Size = new Size(267, 14);
      progressBar.TabIndex = 5;
      // 
      // toolTip1
      // 
      toolTip1.AutomaticDelay = 50;
      toolTip1.AutoPopDelay = 25000;
      toolTip1.InitialDelay = 50;
      toolTip1.ReshowDelay = 10;
      // 
      // label_left
      // 
      label_left.AutoSize = true;
      label_left.Location = new Point(14, 84);
      label_left.Name = "label_left";
      label_left.Size = new Size(44, 17);
      label_left.TabIndex = 6;
      label_left.Text = "无导入";
      // 
      // button_clearFiles
      // 
      button_clearFiles.Location = new Point(105, 33);
      button_clearFiles.Margin = new Padding(4);
      button_clearFiles.Name = "button_clearFiles";
      button_clearFiles.Size = new Size(84, 25);
      button_clearFiles.TabIndex = 0;
      button_clearFiles.Text = "清空导入";
      button_clearFiles.UseVisualStyleBackColor = true;
      button_clearFiles.Click += button_clearFiles_Click;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 17F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(294, 109);
      Controls.Add(label_left);
      Controls.Add(progressBar);
      Controls.Add(button_execute);
      Controls.Add(textBox_cutValue);
      Controls.Add(label2);
      Controls.Add(button_clearFiles);
      Controls.Add(button_openFolder);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Icon = (Icon)resources.GetObject("$this.Icon");
      Margin = new Padding(4);
      MaximizeBox = false;
      MinimizeBox = false;
      Name = "Form1";
      Text = "采样切尾器 2.0";
      FormClosing += Form1_FormClosing;
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Button button_openFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_cutValue;
        private System.Windows.Forms.Button button_execute;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ToolTip toolTip1;
    private Label label_left;
    private Button button_clearFiles;
  }
}

