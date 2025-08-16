using System;
using System.IO;
using System.Windows.Forms;
using static BCatchAutoConverter.IgnoreButtonEx;

namespace BCatchAutoConverter
{
    public partial class MainForm : Form
    {
        public static MainForm Instance { get; private set; } = null;
        public MainForm()
        {
            Instance = this;
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            SetUI_PathText(); 
            if (!FormatConvert.FfmExist)
            {
                MessageBox.Show("ffmpeg.exe不存在，请检查程序目录完整性或在根目录下放置ffmpeg.exe",
                    "文件缺失",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                Instance.Close();
                return;
            }
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        //UiSet
        public void SetUI_PathText()
        {
            Text_Path_OriginalVedio.Text = PathManager.Path_OriginalVedio ?? "无有效视频路径";
            Text_Path_OriginalAudio.Text = PathManager.Path_OriginalAudio ?? "无有效音频路径";
        }

        #region Button_设置路径
        #region ButtonClick
        private void Button_SetPath_OriginalVedio_Click(object sender, EventArgs e)
        {
            SetPath(ref PathManager.Path_OriginalVedio, true);
            SetUI_PathText();
        }
        private void Button_SetPath_SetOriginalAudio_Click(object sender, EventArgs e)
        {
            SetPath(ref PathManager.Path_OriginalAudio, true);
            SetUI_PathText();
        }
        #endregion
        void SetPath(ref string v, bool isFile)
        {
            if (isFile)//找m4s
            {
                var f = new OpenFileDialog();
                f.SupportMultiDottedExtensions = false;
                f.CheckPathExists = true;
                f.CheckPathExists = true;
                DialogResult r = f.ShowDialog();
                if (r == DialogResult.OK)
                {
                    string str = f.FileName;
                    if (!PathManager.OriginalFilePathVaild(str))
                    {
                        MessageBox.Show(
                            "错误的文件格式"
                            , "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        goto FAILD;
                    }
                    v = str;
                }
                else
                    goto FAILD;
            }
            else//找文件夹
            {
                FolderBrowserDialog f = new FolderBrowserDialog();
                f.Description = "选择输出文件夹目录";
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string str = f.SelectedPath;
                    v = str;
                }
                else
                    goto FAILD;
            }

            return;
        FAILD:;
            v = null;
        }
        #endregion
        #region Button_解密文件
        string Decrypt(string path)
        {
            if (path == null)
                return null;
            string str = FileBinarySetter.DecryptFile(path);
            return str;
        }

        private void Button_Decrypt_Click(object sender, EventArgs e)
        {
            string b_vedio = Decrypt(PathManager.Path_OriginalVedio);
            string b_audio = Decrypt(PathManager.Path_OriginalAudio);

            string str = "";
            str += "解密结果:\n";
            str += PathManager.Path_OriginalVedio == null ? "无视频" : 
                "视频:" + (b_vedio ?? "解码完成");
            str += "\n";
            str += PathManager.Path_OriginalAudio == null ? "无音频" :
                "音频:" + (b_vedio ?? "解码完成");
            if (!sender.IsIgnoreSigh())
                MessageBox.Show(str, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Button_后缀更改
        public string ChangeFileExtension(string filePath, string newExtension)
        {
            if (!PathManager.OriginalFilePathVaild(filePath))
                return "路径不合法";

            string directory = Path.GetDirectoryName(filePath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string newFilePath = Path.Combine(directory, fileNameWithoutExtension + newExtension);
            File.Move(filePath, newFilePath);
            return null;
        }
        string TryChangeExt(string path, string ext)
        {
            if (path == null)
                return null;

            string str = ChangeFileExtension(path, ext);
            if (str != null)
                MessageBox.Show("在尝试将文件转换为" + ext + "时出错",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return str;
        }

        private void Button_ChangeExt_Click(object sender, EventArgs e)
        {
            string s1 = TryChangeExt(PathManager.Path_OriginalVedio, ".mp4");
            string s2 = TryChangeExt(PathManager.Path_OriginalAudio, ".mp3");
            if (s1 == null && s2 == null)
                if (!sender.IsIgnoreSigh())
                    MessageBox.Show("更改完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Button_格式转换
        string ConvertFormat(string path, string ext)
        {
            if (path == null)
                return null;
            string str = FormatConvert.Convert(path, ext);
            if (str != null)
                MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return str;
        }

        private void Button_ConvertFormat_Click(object sender, EventArgs e)
        {
            if (!FormatConvert.FfmExist)
            {
                MessageBox.Show("ffmpeg.exe丢失!",
                    "文件缺失",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
                return;
            }
            string s1 = ConvertFormat(PathManager.Path_OriginalVedio, ".mp4");
            string s2 = ConvertFormat(PathManager.Path_OriginalAudio, ".mp3");
            if (s1 == null && s2 == null)
                if (!sender.IsIgnoreSigh())
                    MessageBox.Show("转换完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Button_一键转换
        private void Button_AutoStart_Click(object sender, EventArgs e)
        {
            Button_Decrypt_Click(IIS_TRUE, null);
            Button_ChangeExt_Click(IIS_TRUE, null);
            Button_ConvertFormat_Click(IIS_TRUE, null);
        }
        #endregion
        #region Button_合并
        private void Button_Combine_Click(object sender, EventArgs e)
        {
            string str = FormatConvert.MergeToMp4();
            if (str != null)
                MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("合并完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

    }
    public static class IgnoreButtonEx
    {
        //是否允许调用按钮时忽略弹窗
        public const byte IIS_TRUE = 48;
        public static bool IsIgnoreSigh(this object v)
        {
            if (v == null)
                return false;
            if (!(v is byte))
                return false;
            byte b = (byte)v;
            if (b == IIS_TRUE)
                return true;
            return false;
        }
    }
}
