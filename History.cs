using System.Collections.Generic;

namespace AES_Demo
{
    public class HistoryItem
    {
        public bool Encrypt { get; set; } // true = encrypt, false = decrypt
        public bool isFile { get; set; } // true = file, false = text
        public int Mode { get; set; } // 0 = ECB, 1 = CBC, 2 = CFB, 3 = OFB, 4 = CTR
        public int KeySize { get; set; } // 128, 192, 256
        public string Key { get; set; }
        public string IV { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public bool Base64 { get; set; }

        public HistoryItem(bool Encrypt, bool isFile, int Mode, int KeySize, string Key, string IV, string Input, string Output, bool Base64)
        {
            this.Encrypt = Encrypt;
            this.isFile = isFile;
            this.Mode = Mode;
            this.KeySize = KeySize;
            this.Key = Key;
            this.IV = IV;
            this.Input = Input;
            this.Output = Output;
            this.Base64 = Base64;
        }

        public override string ToString()
        {
            return string.Format("{0} Mode: {1}, KeySize: {2}, Key: {3}, IV: {4}, Input: {5}, Output: {6}", Encrypt ? "EN" : "DE", Mode, KeySize, Key, IV, Input, Output);
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
