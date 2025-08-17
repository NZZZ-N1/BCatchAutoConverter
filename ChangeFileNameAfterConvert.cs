using System;
using System.IO;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BCatchAutoConverter
{
    internal static class ChangeFileNameAfterConvert
    {
        public static void OnOneFileConvertEnd(bool isMerge)
        {
            string basePath = Path.GetDirectoryName(PathManager.Path_OriginalAudio ?? PathManager.Path_OriginalVedio);
            string path_mp4 = Path.Combine(basePath, FormatConvert.FILENAME_OUTPUT_VEDIO + ".mp4");
            string path_mp3 = Path.Combine(basePath, FormatConvert.FILENAME_OUTPUT_AUDIO + ".mp3");
            string str = MainForm.InputFieldText_FileName;
            if (str == null || str.Trim() == "")
                return;

            bool e_mp4 = File.Exists(path_mp4);
            bool e_mp3 = File.Exists(path_mp3);
            if (!isMerge && e_mp4 && e_mp3)
                return;
            if (!isMerge)
            {
                if (e_mp4)
                    ChangeFileName(path_mp4, str);
                if (e_mp3)
                    ChangeFileName(path_mp3, str);
            }
            else
            {
                string path_merge = Path.Combine(basePath, FormatConvert.FILENAME_OUTPUT_MERGE + ".mp4");
                ChangeFileName(path_merge, str);
            }
        }

        static void ChangeFileName(string path, string fileName)
        {
            if (!File.Exists(path))
                throw new System.MissingFieldException();
            string ext = Path.GetExtension(path);
            string newPath = Path.Combine(
                Path.GetDirectoryName(path),
                fileName + ext);
            File.Move(path, newPath);
        }
    }
}