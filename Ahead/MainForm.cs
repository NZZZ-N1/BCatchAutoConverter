using BCatchAutoConverter.HeadINFO;
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
            Init_ComboBox_ExtractMode();
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
        //Init
        void Init_ComboBox_ExtractMode()
        {
            string[] strs = 
            { 
                CmINFO_ExtractMode.Default,
                CmINFO_ExtractMode.OnlyVedio,
                CmINFO_ExtractMode.OnlyAudio,
                CmINFO_ExtractMode.All
            };
            Combo_ExtractMode.Items.AddRange(strs);
            Combo_ExtractMode.SelectedIndex = 0;
        }

        //UiSet
        public void SetUI_PathText()
        {
            Text_Path_OriginalVedio.Text = PathManager.Path_OriginalVedio ?? "无有效视频路径";
            Text_Path_OriginalAudio.Text = PathManager.Path_OriginalAudio ?? "无有效音频路径";
        }
        public static bool ExtractModeAndPathOK(string path, ExtractFileType fileType)
        {
            if (path == null)
                return false;
            string s = (string)Instance.Combo_ExtractMode.SelectedItem;
            if (s == CmINFO_ExtractMode.Default)
            {
                return true;
            }
            else if (s == CmINFO_ExtractMode.OnlyVedio)
            {
                if (fileType == ExtractFileType.Vedio)
                    return true;
                return false;
            }
            else if (s == CmINFO_ExtractMode.OnlyAudio)
            {
                if (fileType == ExtractFileType.Audio)
                    return true;
                return false;
            }
            else if (s == CmINFO_ExtractMode.All)
            {
                return true;
            }
            else
            {
                MessageBox.Show("不可行的提取模式，请重新设置", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw new System.ArgumentException();
            }
        }

        public static string InputFieldText_FileName => Instance.InputField_OutputFileName.Text;
        public static bool Checked_DeleteAfterMerge
            => Instance.CheckBox_DeleteAfterMerge.Checked;
        public static bool Checked_DeleteAfterFormatConvert
            => Instance.CheckBox_DeleteAfterFormatConvert.Checked;

        //ButtonAction
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
        #region Button_复制文件
        string TryCopy(ref string path, string fileName)
        {
            if (path == null)
                return "未选中";
            if (!File.Exists(path))
                return "所选路径不存在";
            string basePath = Path.GetDirectoryName(path);
            string ext = Path.GetExtension(path);
            string p = Path.Combine(basePath, fileName + ext);
            if (File.Exists(p))
                return "已存在复制文件";
            File.Copy(path, p);
            path = p;
            SetUI_PathText();
            return null;
        }

        public const string FILENAME_COPY_VEDIO = "c_vedio";
        public const string FILENAME_COPY_AUDIO = "c_audio";
        private void Button_CopyFile_Click(object sender, EventArgs e)
        {
            string s1 = TryCopy(ref PathManager.Path_OriginalVedio, FILENAME_COPY_VEDIO);
            string s2 = TryCopy(ref PathManager.Path_OriginalAudio, FILENAME_COPY_AUDIO);

            string str = (s1 != null ? "视频未复制:" + s1 + "\n": "视频:复制完成\n");
            str += (s2 != null ? "音频未复制:" + s2 + "\n" : "音频:复制完成\n");
            if (!sender.IsIgnoreSigh())
                MessageBox.Show(str, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Button_解密文件
        string Decrypt(string path, ExtractFileType t)
        {
            if (path == null)
                return null;
            if (!ExtractModeAndPathOK(path, t))
                return null;
            string str = FileBinarySetter.DecryptFile(path);
            return str;
        }

        private void Button_Decrypt_Click(object sender, EventArgs e)
        {
            string b_vedio = Decrypt(PathManager.Path_OriginalVedio, ExtractFileType.Vedio);
            string b_audio = Decrypt(PathManager.Path_OriginalAudio, ExtractFileType.Audio);

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
        public string ChangeFileExtension(ref string filePath, string newExtension)
        {
            if (!PathManager.OriginalFilePathVaild(filePath))
                return "路径不合法";

            string directory = Path.GetDirectoryName(filePath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string newFilePath = Path.Combine(directory, fileNameWithoutExtension + newExtension);
            File.Move(filePath, newFilePath);
            filePath = newFilePath;
            SetUI_PathText();
            return null;
        }
        string TryChangeExt(ref string path, string ext, ExtractFileType t)
        {
            if (path == null)
                return null;
            if (!ExtractModeAndPathOK(path, t))
                return null;

            string str = ChangeFileExtension(ref path, ext);
            if (str != null)
                MessageBox.Show("在尝试将文件转换为" + ext + "时出错",
                    "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return str;
        }

        private void Button_ChangeExt_Click(object sender, EventArgs e)
        {
            string s1 = TryChangeExt(ref PathManager.Path_OriginalVedio, ".mp4", ExtractFileType.Vedio);
            string s2 = TryChangeExt(ref PathManager.Path_OriginalAudio, ".mp3", ExtractFileType.Audio);
            if (s1 == null && s2 == null)
                if (!sender.IsIgnoreSigh())
                    MessageBox.Show("更改完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
        #region Button_格式转换
        string ConvertFormat(string path, string ext, ExtractFileType t)
        {
            if (path == null)
                return null;
            if (!ExtractModeAndPathOK(path, t))
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
            string s1 = ConvertFormat(PathManager.Path_OriginalVedio, ".mp4", ExtractFileType.Vedio);
            string s2 = ConvertFormat(PathManager.Path_OriginalAudio, ".mp3", ExtractFileType.Audio);
            if (s1 == null && s2 == null)
            {
                if (!sender.IsIgnoreSigh())
                    MessageBox.Show("转换完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show(s1 + "\n" + s2, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
        #region Button_一键转换
        private void Button_AutoStart_Click(object sender, EventArgs e)
        {
            Button_CopyFile_Click(IIS_TRUE, null);
            Button_Decrypt_Click(IIS_TRUE, null);
            Button_ChangeExt_Click(IIS_TRUE, null);
            Button_ConvertFormat_Click(IIS_TRUE, null);
            ChangeFileNameAfterConvert.OnOneFileConvertEnd(false);

            if (ExtractModeAndPathOK(PathManager.Path_OriginalVedio, ExtractFileType.Vedio)
                && ExtractModeAndPathOK(PathManager.Path_OriginalAudio, ExtractFileType.Audio))
            {
                DialogResult r = MessageBox.Show("转换完成，是否需要合并", "询问",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (r == DialogResult.Yes)
                    Button_Combine_Click(IIS_TRUE, null);
            }
            else
            {
                MessageBox.Show("转换完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
        #region Button_合并文件
        private void Button_Combine_Click(object sender, EventArgs e)
        {
            string str = FormatConvert.MergeToMp4();
            if (str != null)
                MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show("合并完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ChangeFileNameAfterConvert.OnOneFileConvertEnd(true);
            PathManager.ClearPath();
            SetUI_PathText();
        }
        #endregion
    }
    #region IgnoreButtonEx拓展
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
    #endregion
}