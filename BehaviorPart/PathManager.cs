using System;
using System.IO;

namespace BCatchAutoConverter
{
    internal static class PathManager
    {
        //原视频音频的路径
        //null表示没有设置
        public static string Path_OriginalVedio = null;
        public static string Path_OriginalAudio = null;

        public static bool PathVaild_OriginalVedio => OriginalFilePathVaild(Path_OriginalVedio);
        public static bool PathVaild_OriginalAudio => OriginalFilePathVaild(Path_OriginalAudio);

        //路径是否有效
        public static bool OriginalFilePathVaild(string path)
        {
            if (path == null)
                return false;
            if(!File.Exists(path)) 
                return false;
            if (!path.EndsWith(".m4s") && !path.EndsWith(".mp3") && !path.EndsWith(".mp4"))
                return false;
            return true;
        }

        //移除路径
        public static void ClearPath()
        {
            Path_OriginalVedio = null;
            Path_OriginalAudio = null;
        }
    }
}