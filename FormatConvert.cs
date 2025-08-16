using FFMpegCore.Arguments;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCatchAutoConverter
{
    internal static class FormatConvert
    {
        public static string FfmPath => Application.StartupPath + @"\" + "ffmpeg.exe";
        public static bool FfmExist => File.Exists(FfmPath);

        const string FILENAME_OUTPUT_VEDIO = "n_vedio";
        const string FILENAME_OUTPUT_AUDIO = "n_audio";

        /// <summary>
        /// ext= .mp3 or .mp4
        /// </summary>
        public static string Convert(string path, string ext)
        {
            if (ext != ".mp3" && ext != ".mp4")
                return "无效后缀";

            string fileName;
            if (ext == ".mp3")
                fileName = FILENAME_OUTPUT_AUDIO;
            else if (ext == ".mp4")
                fileName = FILENAME_OUTPUT_VEDIO;
            else
                throw new System.ArgumentException();

            string outputFile = Path.Combine(
                Path.GetDirectoryName(path),
                Path.GetFileNameWithoutExtension(fileName) + ext);

            if (!path.EndsWith(".mp3") && !path.EndsWith(".mp4"))
                path = Path.Combine(
                Path.GetDirectoryName(path),
                Path.GetFileNameWithoutExtension(path) + ext);

            try
            {
                if (ext == ".mp3")
                    ConvertToMp3(FfmPath, path, outputFile);
                if (ext == ".mp4")
                    ConvertToMp4(FfmPath, path, outputFile);
            }
            catch (Exception ex)
            {
                return "转换失败:\n" + ex.Message;
            }

            return null;
        }

        static void ConvertFormat(string ffmpegPath, string arg)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = arg,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            Process process = new Process { StartInfo = startInfo };

            process.EnableRaisingEvents = true;
            process.Start();

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();

            int exitCode = process.ExitCode;
            process.Close();

            if (exitCode != 0)
                throw new Exception("Error:" + exitCode);;
        }
        static void ConvertToMp3(string ffmpegPath, string inputFile, string outputFile)
        {
            string arg = $"-i \"{inputFile}\" -q:a 0 -map a \"{outputFile}\"";
            ConvertFormat(ffmpegPath, arg);
        }
        static void ConvertToMp4(string ffmpegPath, string inputFile, string outputFile)
        {
            string arguments =
                $"-i \"{inputFile}\" -c:v libx264 -preset fast -crf 28 -c:a aac -b:a 128k -movflags +faststart \"{outputFile}\"";
            ConvertFormat(ffmpegPath, arguments);
        }
        public static string MergeToMp4()
        {
            string path_oriMp4 = PathManager.Path_OriginalVedio;
            string path_oriMp3 = PathManager.Path_OriginalAudio;

            if (path_oriMp4 == null && path_oriMp3 == null)
                return "无可用路径";
            if (path_oriMp4 != null && path_oriMp3 != null &&
                Path.GetDirectoryName(path_oriMp4) != Path.GetDirectoryName(path_oriMp3))
                return "选中的两个文件根目录不同";
            string basePath = Path.GetDirectoryName(path_oriMp4 ?? path_oriMp3);

            string path_mp4 = Path.Combine(basePath, FILENAME_OUTPUT_VEDIO + ".mp4");
            string path_mp3 = Path.Combine(basePath, FILENAME_OUTPUT_AUDIO + ".mp3");
            if (!File.Exists(path_mp4) || !File.Exists(path_mp3))
                return "缺失视频或音频文件";

            string arg = $"-i \"{path_mp4}\" -i \"{path_mp3}\" -c:v copy -c:a aac -strict experimental -map 0:v:0 -map 1:a:0 -shortest \"{Path.Combine(basePath, "merge.mp4")}\"";
            try
            {
                ConvertFormat(FfmPath, arg);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}