using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NotebookApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> list = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void 隐藏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Title = "Plz choose your files";
            ofd.InitialDirectory = @"C:\Users\Samzhong\Desktop";
            ofd.Filter = "所有文件|*.*|文本文件|*.txt|PDF文件|*.pdf";
            ofd.ShowDialog();

            string path = ofd.FileName;
            string filename = Path.GetFileName(path);
            list.Add(path);
            listBox1.Items.Add(filename);

            if(path=="")
            {
                return;
            }

            using(FileStream fsRead=new FileStream(path,FileMode.OpenOrCreate,FileAccess.Read))
            {
                byte[] buffer=new byte[1024*1024*5];
                int r = fsRead.Read(buffer, 0, buffer.Length);
                textBox1.Text = Encoding.Default.GetString(buffer, 0, r);
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Title="Save your file.";
            sf.Filter = "所有文件|*.*|文本文件|*.txt|PDF文件|*.pdf";
            sf.InitialDirectory = @"C:\Users\Samzhong\Desktop";
            sf.ShowDialog();

            string path=sf.FileName;
            if(path=="")
            {
                return;
            }

            using(FileStream fsWrite=new FileStream(path,FileMode.OpenOrCreate,FileAccess.Write))
            {
                //byte[] buffer=new byte[1024*1024*5];
                byte[] buffer = Encoding.Default.GetBytes(textBox1.Text);
                fsWrite.Write(buffer, 0, buffer.Length);
            }

            MessageBox.Show("Successfully saved!");
        }

        private void 自动换行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(自动换行ToolStripMenuItem.Text=="自动换行")
            {
                textBox1.WordWrap = true;
                自动换行ToolStripMenuItem.Text = "取消自动换行";
            }
            else if(自动换行ToolStripMenuItem.Text=="取消自动换行")
            {
                textBox1.WordWrap = false;
                自动换行ToolStripMenuItem.Text = "自动换行";
            }
        }

        private void 字体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            textBox1.Font=fd.Font;
            fd.ShowDialog();
        }

        private void 颜色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            textBox1.ForeColor=cd.Color;
            cd.ShowDialog();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            string path=list[listBox1.SelectedIndex];

            using(FileStream fsRead=new FileStream(path,FileMode.OpenOrCreate,FileAccess.Read))
            {
                byte[] buffer=new byte[1024*1024*5];
                int r = fsRead.Read(buffer, 0, buffer.Length);
                textBox1.Text = Encoding.Default.GetString(buffer, 0, r);
            }
        }
    }
}
