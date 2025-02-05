using SampleCutter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wave16Bit;
using static System.Formats.Asn1.AsnWriter;

namespace SampleCutter;

public partial class Form1 : Form
{
  private readonly int DefaultCutValue = 16;

  public int CutValue { get; set; } = 16;

  public List<string> FilePaths { get; set; } = [];

  public bool IsExecuting { get; private set; }

  public Form1()
  {
    InitializeComponent();
    this.textBox_cutValue.Text = DefaultCutValue.ToString();
    this.button_clearFiles.Enabled = false;
    this.button_execute.Enabled = false;
  }

  private unsafe void Cut(string path)
  {
    using Wave16 wave = Wave16IO.Load(path);
    int slientStartIndex = GetSlientStartIndex((byte*)wave.Data, wave.DataSize);
    wave.Resize(slientStartIndex);
    Wave16IO.Save(wave, path, overwrite: true);
  }

  private unsafe int GetSlientStartIndex(byte* data, int dataSize)
  {
    if (dataSize <= 1)
    {
      return 0;
    }

    int* samples = (int*)(data + dataSize) - 1;
    while (samples >= data)
    {
      short* channels = (short*)samples;

      if (channels[0] >= CutValue || channels[1] >= CutValue)
      {
        return (int)((byte*)(samples + 1) - data);
      }

      samples--;
    }

    return 0;
  }

  private void textBox_cutValue_TextChanged(object sender, EventArgs e)
  {
    if (!int.TryParse(this.textBox_cutValue.Text, out int cut_value))
    {
      this.textBox_cutValue.Text = DefaultCutValue.ToString();
    }
    else if (cut_value < 0)
    {
      this.textBox_cutValue.Text = "0";
    }
    else if (cut_value > 32767)
    {
      this.textBox_cutValue.Text = "32767";
    }
    else if (this.textBox_cutValue.Text.Length > 5)
    {
      this.textBox_cutValue.Text = "32767";
    }
  }

  private void button_openFolder_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();

    openFileDialog.Multiselect = true;
    openFileDialog.Title = "导入（可多选）";
    openFileDialog.Filter = "16bit 2-channel PCM (*.wav)|*.wav";

    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
    {
      FilePaths = [.. FilePaths.Concat(openFileDialog.FileNames).Distinct().Order()];
      if (FilePaths.Count != 0)
      {
        this.button_clearFiles.Enabled = true;
        this.button_execute.Enabled = true;
        this.button_execute.Text = "执行";
        this.label_left.Text = $"导入了 {FilePaths.Count} 个文件";
      }
      else
      {
        this.label_left.Text = $"无导入";
      }
    }
  }

  private void button_execute_Click(object sender, EventArgs e)
  {
    DialogResult dr = MessageBox.Show("该操作将会覆盖原文件，请事先备份好原文件。确认执行操作吗？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
    if (dr == DialogResult.Yes)
    {
      this.textBox_cutValue.Enabled = false;
      this.button_openFolder.Enabled = false;
      this.button_clearFiles.Enabled = false;
      this.button_execute.Enabled = false;
      this.button_execute.Text = "正在执行";

      IsExecuting = true;

      List<(string exception, string path)> errors = [];

      int counter = 0;
      foreach (var path in FilePaths)
      {
        try
        {
          Cut(path);

          ++counter;
          double percent = 100d * counter / FilePaths.Count;
          this.progressBar.Value = (int)Math.Ceiling(percent);
          this.label_left.Text = $"[{percent,6:0.00}%] {counter} / {FilePaths.Count}";
        }
        catch (Exception ex)
        {
          errors.Add((ex.ToString(), path));
          continue;
        }        
      }

      IsExecuting = false;

      this.textBox_cutValue.Enabled = true;
      this.button_openFolder.Enabled = true;
      this.button_clearFiles.Enabled = true;
      this.button_execute.Enabled = true;
      this.button_execute.Text = "执行";
      this.label_left.Text = $"导入了 {FilePaths.Count} 个文件";

      if (errors.Count == 0)
      {
        MessageBox.Show("全部执行成功！", "恭喜你！", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      else
      {
        MessageBox.Show($"执行完成，但是有{errors.Count}个执行时出现错误，请查看在程序目录下生成的错误报告，以检查哪些文件发生了错误。", "恭喜你？", MessageBoxButtons.OK, MessageBoxIcon.Information);
        using StreamWriter writer = File.CreateText($"错误报告-{Guid.NewGuid():N}.log");
        writer.WriteLine("发生错误的文件：");
        writer.WriteLine(string.Join(Environment.NewLine, errors.Select(x => x.path)));
        writer.WriteLine();
        writer.WriteLine("错误详情：");
        errors.ForEach(x =>
        {
          writer.WriteLine($"[{x.path}]");
          writer.WriteLine($"'{x.exception}'");
          writer.WriteLine();
        });
      }      

      this.progressBar.Value = 0;
    }
  }

  private void Form1_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (IsExecuting)
    {
      DialogResult dr = MessageBox.Show("操作还在执行中，真的要关闭吗？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

      if (dr != DialogResult.Yes)
      {
        e.Cancel = true;
      }
    }
  } 

  private void button_clearFiles_Click(object sender, EventArgs e)
  {
    if (FilePaths.Count > 0)
    {
      DialogResult dr = MessageBox.Show($"确定要清空导入的{FilePaths.Count}个文件吗？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
      
      if (dr == DialogResult.Yes)
      {
        FilePaths.Clear();
        this.button_clearFiles.Enabled = false;
        this.button_execute.Enabled = false;
        this.label_left.Text = $"无导入";
      }      
    }
  }
}
