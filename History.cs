using System.Collections.Generic;
using System.Security.Cryptography;

namespace AES_Demo
{
    public class HistoryItem
    {
        public bool isFile { get; set; } // true = file, false = text
        public int Mode { get; set; } // 0 = ECB, 1 = CBC, 2 = CFB, 3 = OFB, 4 = CTR
        public int KeySize { get; set; } // 128, 192, 256
        public string Key { get; set; }
        public string IV { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }

        public HistoryItem(bool isFile, int Mode, int KeySize, string Key, string IV, string Input, string Output)
        {
            this.isFile = isFile;
            this.Mode = Mode;
            this.KeySize = KeySize;
            this.Key = Key;
            this.IV = IV;
            this.Input = Input;
            this.Output = Output;
        }

        public static int ConvertMode(CipherMode mode = 0)
        {
            switch (mode)
            {

                case CipherMode.ECB:
                    return 0;
                case CipherMode.CBC:
                    return 1;
                case CipherMode.CFB:
                    return 2;
                case CipherMode.OFB:
                    return 3;
                default:
                    return 0;
            }
        }

        public static CipherMode ConvertMode(int mode = 0)
        {
            switch (mode)
            {
                case 0:
                    return CipherMode.ECB;
                case 1:
                    return CipherMode.CBC;
                case 2:
                    return CipherMode.CFB;
                case 3:
                    return CipherMode.OFB;
                default:
                    return CipherMode.ECB;
            }
        }

        public override string ToString()
        {
            return string.Format("Mode: {0}, KeySize: {1}, Key: {2}, IV: {3}, Input: {4}, Output: {5}", Mode, KeySize, Key, IV, Input, Output);
        }
    }

    public class HistoryManager
    {
        public static void SaveToFile(List<HistoryItem> list, string filePath)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            System.IO.File.WriteAllText(filePath, json);
        }

        public static List<HistoryItem> LoadFromFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<HistoryItem>>(json);
            }
            return new List<HistoryItem>();
        }
    }
}
