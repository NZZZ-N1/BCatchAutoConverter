using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BCatchAutoConverter
{
    internal static class FileBinarySetter
    {
        //返回null表示没有问题，非null则是错误信息
        public static string DecryptFile(string path)
        {
            if (!PathManager.OriginalFilePathVaild(path))
                return "目标文件路径不合法";

            LinkedList<byte> list = new LinkedList<byte>(GetFileBinary(path) ?? new byte[0]);
            if (list.Count <= 0)
                return "读取路径失败";


            for (int i = 0; i < 9; i++)
            {
                foreach (var c in list)
                {
                    if (c != 48)
                        return "不可行的解密方式";
                    break;
                }
                list.RemoveFirst();
            }
            if (!WriteBinary(path, list.ToArray()))
            {
                list = null;
                return "写入失败";
            }
            list = null;//释放内存，虽然我也不知道这么做有没有必要
            return null;
        }

        static byte[] GetFileBinary(string path)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(path);
                return fileBytes;
            }
            catch
            {
                return null;
            }
        }
        static bool WriteBinary(string path, byte[] bytes)
        {
            if (!File.Exists(path))
                return false;
            File.WriteAllBytes(path, bytes);
            return true;
        }
    }
}