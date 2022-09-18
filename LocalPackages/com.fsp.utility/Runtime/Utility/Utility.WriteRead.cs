using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace fsp.utility
{
    public static partial class Utility
    {
        public static void WriteStringByUTF8Byte(BinaryWriter writer, string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            writer.Write(bytes.Length);
            writer.Write(bytes);
        }
        
        public static void WriteListByte(BinaryWriter writer, List<int> list)
        {
            writer.Write(list.Count);
            foreach (var item in list)
            {
                writer.Write(item);
            }
        }
        
        public static string ReadStringFromUTF8Byte(BinaryReader reader)
        {
            var len = reader.ReadInt32();
            return System.Text.Encoding.UTF8.GetString(reader.ReadBytes(len));
        }

        public static List<int> ReadListFromByte(BinaryReader reader)
        {
            var len = reader.ReadInt32();
            var ret = new List<int>(len);
            for (int i = 0; i < len; i++)
            {
                ret.Add(reader.ReadInt32());
            }

            return ret;
        }

        public static void WriteObjectToJson(object soE, string savePath)
        {
            var jsonInfo = JsonUtility.ToJson(soE);
            File.Delete(savePath);
            File.WriteAllText(savePath, jsonInfo);
        }
    }
}