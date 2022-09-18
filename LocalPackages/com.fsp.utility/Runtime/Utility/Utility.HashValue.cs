using System;
using System.Security.Cryptography;
using System.Text;
using fsp.assetbundlecore;
using UnityEngine;

namespace fsp.utility
{
    public static partial class Utility
    {
        // TODO:哈希值算法
        public static int GetStringHashCode(string value)
        {
            int num1 = 5381;
            int num2 = num1;
            GetStringHashCode(value, ref num1, ref num2);
            return num1 + num2 * 1566083941;
        }

        private static void GetStringHashCode(string value, ref int num1, ref int num2)
        {
            unsafe
            {
                fixed (char* chPtr1 = value)
                {
                    int num3;
                    for (char* chPtr2 = chPtr1; (num3 = (int) *chPtr2) != 0; chPtr2 += 2)
                    {
                        num1 = (num1 << 5) + num1 ^ num3;
                        int num4 = (int) chPtr2[1];
                        if (num4 != 0)
                        {
                            num2 = (num2 << 5) + num2 ^ num4;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        // public static int GetHashCodeByAbsoluteAssetPath(string absoluteAssetPath, bool bRepChar = true)
        // {
        //     string path = absoluteAssetPath.Substring(absoluteAssetPath.IndexOf("Assets", StringComparison.Ordinal));
        //     return GetHashCodeByAssetPath(path, bRepChar);
        // }
        
        public static int LocalDataAssetsPathLength
        {
            get
            {
                if (localDataAssetsPathLength == -1)
                {
                    localDataAssetsPathLength = Application.dataPath.Length - "Assets".Length;
                }
                return localDataAssetsPathLength;
            }
        }
        private static int localDataAssetsPathLength = -1;
        
        public static int GetHashCodeByFullAssetPath(string assetPath, bool bRepChar = true)
        {
            assetPath = assetPath.Remove(0, LocalDataAssetsPathLength);
            return GetHashCodeByAssetPath(assetPath, bRepChar);
        }
        
        public static int GetHashCodeUnderUnknowPackagesPath(string assetPath, bool bRepChar = true)
        {
            assetPath = assetPath.Replace("LocalPackages", "Packages");
            assetPath = assetPath.Remove(0, LocalDataAssetsPathLength);
            return GetHashCodeByAssetPath(assetPath, bRepChar);
        }
        
        public static int GetHashCodeByAssetPath(string assetPath, bool bRepChar = true)
        {
            if (bRepChar)
            {
                assetPath = assetPath.Replace('\\', '/');
            }

            MD5 md5Hash = MD5.Create();
            // 将输入字符串转换为字节数组并计算哈希数据  
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(assetPath));
            // 创建一个 Stringbuilder 来收集字节并创建字符串  
            StringBuilder str = new StringBuilder();
            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串  
            for (int i = 0; i < data.Length; i++)
            {
                str.Append(data[i].ToString("x2")); // 加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
            }
            return GetGuidHashCode(str.ToString());
        }
        
        public static int GetGuidHashCode(string guid)
        {
            ulong height = Convert.ToUInt64(guid.Substring(0, 16), 16);
            ulong low = Convert.ToUInt64(guid.Substring(16, 16), 16);
            MarkleTree mt = new MarkleTree(2);
            mt.Add(height.GetHashCode());
            mt.Add(low.GetHashCode());
            int hash = mt.HashCode;
            mt.Dispose();
            return hash;
        }
    }
}