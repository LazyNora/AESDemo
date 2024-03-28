using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace AES_Demo
{
    public partial class Form1 : Form
    {
        public Form1() // constructor
        {
            InitializeComponent();
        }

        // ! Default values
        private string history_filename = "history.json";
        private string key_filename = "key.txt";
        private string iv_filename = "iv.txt";
        private int mode = 0; // 0 = ECB, 1 = CBC, 2 = CFB, 3 = OFB, 4 = CTR
        private int keySize = 128;
        private int blockSize = 128;
        byte[] salt = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        string encrypted_extension = ".aes";
        List<HistoryItem> history = new List<HistoryItem>();

        SymmetricAlgorithm aes;
        // AesEngine engine = new AesEngine(); // BouncyCastle AES Engine for CTR mode

        // AES Initialization
        private void Form1_Load(object sender, EventArgs e)
        {
            history = HistoryManager.LoadFromFile(history_filename);
            PopulateHistory();

            // Set default values
            radioKey128.Checked = true;
            radioCBC.Checked = true;

            // Set default settings for AES
            mode = 0;
            keySize = 128;
            blockSize = 128;
        }

        // Handle Radio Group Key Size
        private void radioKey128_CheckedChanged(object sender, System.EventArgs e)
        {
            keySize = 128;
            setStatusStrip("Changed key size to 128");
        }

        private void radioKey192_CheckedChanged(object sender, System.EventArgs e)
        {
            keySize = 192;
            setStatusStrip("Changed key size to 192");
        }

        private void radioKey256_CheckedChanged(object sender, System.EventArgs e)
        {
            keySize = 256;
            setStatusStrip("Changed key size to 256");
        }

        // Handle Radio Group Chaining Mode
        private void radioCBC_CheckedChanged(object sender, System.EventArgs e)
        {
            mode = 1;
            setStatusStrip("Changed Chaining Mode to CBC");
        }

        private void radioECB_CheckedChanged(object sender, System.EventArgs e)
        {
            mode = 0;
            setStatusStrip("Changed Chaining Mode to ECB");
        }

        private void radioCFB_CheckedChanged(object sender, System.EventArgs e)
        {
            mode = 2;
            setStatusStrip("Changed Chaining Mode to CFB");
        }

        private void radioCTR_CheckedChanged(object sender, System.EventArgs e)
        {
            mode = 4;
            // CTR is not supported by .NET
            setStatusStrip("CTR is not supported by .NET, using BouncyCastle AES Engine for CTR mode");
        }

        private void radioOFB_CheckedChanged(object sender, System.EventArgs e)
        {
            mode = 3;
            // OFB somehow not working, internal error
            setStatusStrip("Changed Chaining Mode to OFB");
        }

        // Status Strip msg Handler
        public void setStatusStrip(string msg)
        {
            statusStrip1.Items[0].Text = msg;
        }

        // Select File Button
        private void btnSelectFile_Click(object sender, System.EventArgs e)
        {
            try
            {
                setStatusStrip("Selecting file...");
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFileBox.Text = openFileDialog.FileName;
                    setStatusStrip("File selected: " + openFileDialog.FileName);

                    if (selectedSaveBox.Text == "" || selectedSaveBox.Text == "Select save location...")
                    {
                        selectedSaveBox.Text = openFileDialog.FileName + encrypted_extension;
                    }
                }
                else
                {
                    setStatusStrip("No file selected.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnKeyRandom_Click(object sender, EventArgs e)
        {
            aes = Aes.Create();
            aes.Mode = HistoryItem.ConvertMode(mode);
            aes.KeySize = keySize;
            aes.BlockSize = blockSize;
            aes.GenerateKey();
            KeyInput.Text = Convert.ToBase64String(aes.Key);
        }

        private void btnIVRandom_Click(object sender, EventArgs e)
        {
            aes = Aes.Create();
            aes.Mode = HistoryItem.ConvertMode(mode);
            aes.KeySize = keySize;
            aes.BlockSize = blockSize;
            aes.GenerateIV();
            IVInput.Text = Convert.ToBase64String(aes.IV);
        }

        private void genKeyIV_Click(object sender, EventArgs e)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(PassInput.Text, salt); // Create a PasswordDeriveBytes object with the password and salt
            KeyInput.Text = Convert.ToBase64String(pdb.GetBytes(keySize / 8)); // Generate a key from the password
            IVInput.Text = Convert.ToBase64String(pdb.GetBytes(blockSize / 8)); // Generate an IV from the password
            setStatusStrip("Key and IV generated.");
        }

        private string EncryptText(string plaintext)
        {
            // Get Key and IV from Base64 string
            byte[] key = Convert.FromBase64String(KeyInput.Text);
            byte[] iv = Convert.FromBase64String(IVInput.Text);

            // Create AES object
            aes = Aes.Create();
            aes.Mode = HistoryItem.ConvertMode(mode);
            aes.KeySize = keySize;
            aes.BlockSize = blockSize;
            aes.Key = key;
            aes.IV = iv;

            // Create Encryptor
            ICryptoTransform transform = aes.CreateEncryptor();
            MemoryStream ms = new MemoryStream(); // Create a memory stream to write the encrypted data to
            CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write); // Create a CryptoStream to encrypt the data
            StreamWriter sw = new StreamWriter(cs); // Create a StreamWriter to write the data to the CryptoStream

            sw.Write(plaintext); // Write the data to the CryptoStream
            sw.Close(); // Close the StreamWriter
            cs.Close(); // Close the CryptoStream
            byte[] buffer = ms.ToArray(); // Get the encrypted data from the MemoryStream
            ms.Close(); // Close the MemoryStream

            return Convert.ToBase64String(buffer); // Return the encrypted data as a Base64 string
        }

        private string EncryptText_BC(string plaintext)
        {
            // BouncyCastle AES Engine for CTR & OFB mode
            AesEngine engine = new AesEngine(); // BouncyCastle AES Engine for CTR mode
            BufferedBlockCipher cipher;
            if (mode == 4)
            {
                cipher = new BufferedBlockCipher(new SicBlockCipher(engine)); // CTR mode cipher with AES engine in SIC mode (CTR mode)
            }
            else
            {
                cipher = new BufferedBlockCipher(new OfbBlockCipher(engine, blockSize / 8)); // OFB mode cipher with AES engine
            }

            KeyParameter keyParam = new KeyParameter(Convert.FromBase64String(KeyInput.Text), 0, keySize / 8); // Create a key parameter from the key
            ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, Convert.FromBase64String(IVInput.Text), 0, blockSize / 8); // Create a parameter with the key and IV

            cipher.Init(true, keyParamWithIV); // Initialize the cipher with the key and IV

            byte[] input = System.Text.Encoding.UTF8.GetBytes(plaintext); // Convert the plaintext to a byte array
            byte[] output = new byte[cipher.GetOutputSize(input.Length)]; // Create a byte array to hold the encrypted data

            int length = cipher.ProcessBytes(input, 0, input.Length, output, 0); // Encrypt the data
            cipher.DoFinal(output, length); // Finalize the encryption

            return Convert.ToBase64String(output); // Return the encrypted data as a Base64 string
        }

        private string DecryptText(string encrypted)
        {
            // Get Key and IV from Base64 string
            byte[] key = Convert.FromBase64String(KeyInput.Text);
            byte[] iv = Convert.FromBase64String(IVInput.Text);

            // Create AES object
            aes = Aes.Create();
            aes.Mode = HistoryItem.ConvertMode(mode);
            aes.KeySize = keySize;
            aes.BlockSize = blockSize;
            aes.Key = key;
            aes.IV = iv;

            // Create Decryptor
            ICryptoTransform transform = aes.CreateDecryptor();
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(encrypted)); // Create a memory stream to read the encrypted data from
            CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Read); // Create a CryptoStream to decrypt the data
            StreamReader sr = new StreamReader(cs); // Create a StreamReader to read the data from the CryptoStream

            string plaintext = sr.ReadToEnd(); // Read the decrypted data from the CryptoStream
            sr.Close();
            cs.Close();
            ms.Close();

            return plaintext; // Return the decrypted data
        }

        private string DecryptText_BC(string encrypted)
        {
            // BouncyCastle AES Engine for CTR mode
            AesEngine engine = new AesEngine(); // BouncyCastle AES Engine for CTR mode
            BufferedBlockCipher cipher;

            if (mode == 4)
            {
                cipher = new BufferedBlockCipher(new SicBlockCipher(engine)); // CTR mode cipher with AES engine in SIC mode (CTR mode)
            }
            else
            {
                cipher = new BufferedBlockCipher(new OfbBlockCipher(engine, blockSize / 8)); // OFB mode cipher with AES engine
            }

            KeyParameter keyParam = new KeyParameter(Convert.FromBase64String(KeyInput.Text), 0, keySize / 8); // Create a key parameter from the key
            ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, Convert.FromBase64String(IVInput.Text), 0, blockSize / 8); // Create a parameter with the key and IV

            cipher.Init(false, keyParamWithIV); // Initialize the cipher with the key and IV

            byte[] input = Convert.FromBase64String(encrypted); // Convert the encrypted data to a byte array
            byte[] output = new byte[cipher.GetOutputSize(input.Length)]; // Create a byte array to hold the decrypted data

            int length = cipher.ProcessBytes(input, 0, input.Length, output, 0); // Decrypt the data
            cipher.DoFinal(output, length); // Finalize the decryption

            return System.Text.Encoding.UTF8.GetString(output); // Return the decrypted data as a string
        }

        private void EncryptFile(string inputPath, string outputPath)
        {
            // Get Key and IV from Base64 string
            byte[] key = Convert.FromBase64String(KeyInput.Text);
            byte[] iv = Convert.FromBase64String(IVInput.Text);

            // Create AES object
            aes = Aes.Create();
            aes.Mode = HistoryItem.ConvertMode(mode);
            aes.KeySize = keySize;
            aes.BlockSize = blockSize;
            aes.Key = key;
            aes.IV = iv;

            // Create Encryptor
            ICryptoTransform transform = aes.CreateEncryptor();
            FileStream fsInput = new FileStream(inputPath, FileMode.Open, FileAccess.Read); // Open the input file for reading
            FileStream fsOutput = new FileStream(outputPath, FileMode.Create, FileAccess.Write); // Create the output file for writing
            CryptoStream cs = new CryptoStream(fsOutput, transform, CryptoStreamMode.Write); // Create a CryptoStream to encrypt the data

            toolStripProgressBar1.Maximum = (int)fsInput.Length; // Set the maximum value of the progress bar to the length of the input file

            byte[] buffer = new byte[4096]; // Create a buffer to hold the data read from the input file
            int bytesRead;
            while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0) // Read the data from the input file
            {
                cs.Write(buffer, 0, bytesRead); // Encrypt the data and write it to the output file

                toolStripProgressBar1.Value += bytesRead; // Update the progress bar
            }

            // Close the streams
            fsInput.Close();
            cs.Close();
            fsOutput.Close();

            toolStripProgressBar1.Value = 0; // Reset the progress bar
        }

        private void EncryptFile_BC(string inputPath, string outputPath)
        {
            // BouncyCastle AES Engine for CTR mode
            AesEngine engine = new AesEngine(); // BouncyCastle AES Engine for CTR mode
            BufferedBlockCipher cipher;

            if (mode == 4)
            {
                cipher = new BufferedBlockCipher(new SicBlockCipher(engine)); // CTR mode cipher with AES engine in SIC mode (CTR mode)
            }
            else
            {
                cipher = new BufferedBlockCipher(new OfbBlockCipher(engine, blockSize / 8)); // OFB mode cipher with AES engine
            }

            KeyParameter keyParam = new KeyParameter(Convert.FromBase64String(KeyInput.Text), 0, keySize / 8); // Create a key parameter from the key
            ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, Convert.FromBase64String(IVInput.Text), 0, blockSize / 8); // Create a parameter with the key and IV

            cipher.Init(true, keyParamWithIV); // Initialize the cipher with the key and IV

            FileStream fsInput = new FileStream(inputPath, FileMode.Open, FileAccess.Read); // Open the input file for reading
            FileStream fsOutput = new FileStream(outputPath, FileMode.Create, FileAccess.Write); // Create the output file for writing

            toolStripProgressBar1.Maximum = (int)fsInput.Length; // Set the maximum value of the progress bar to the length of the input file

            byte[] buffer = new byte[4096]; // Create a buffer to hold the data read from the input file
            int bytesRead;
            while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0) // Read the data from the input file
            {
                byte[] output = new byte[cipher.GetOutputSize(bytesRead)]; // Create a byte array to hold the encrypted data
                int length = cipher.ProcessBytes(buffer, 0, bytesRead, output, 0); // Encrypt the data
                fsOutput.Write(output, 0, length); // Write the encrypted data to the output file

                toolStripProgressBar1.Value += bytesRead; // Update the progress bar
            }

            cipher.DoFinal(); // Finalize the encryption

            // Close the streams
            fsInput.Close();
            fsOutput.Close();

            toolStripProgressBar1.Value = 0; // Reset the progress bar
        }

        private void DecryptFile(string inputPath, string outputPath)
        {
            // Get Key and IV from Base64 string
            byte[] key = Convert.FromBase64String(KeyInput.Text);
            byte[] iv = Convert.FromBase64String(IVInput.Text);

            aes.Key = key;
            aes.IV = iv;

            // Create Decryptor
            ICryptoTransform transform = aes.CreateDecryptor();
            FileStream fsInput = new FileStream(inputPath, FileMode.Open, FileAccess.Read); // Open the input file for reading
            FileStream fsOutput = new FileStream(outputPath, FileMode.Create, FileAccess.Write); // Create the output file for writing
            CryptoStream cs = new CryptoStream(fsInput, transform, CryptoStreamMode.Read); // Create a CryptoStream to decrypt the data

            toolStripProgressBar1.Maximum = (int)fsInput.Length; // Set the maximum value of the progress bar to the length of the input file

            byte[] buffer = new byte[4096]; // Create a buffer to hold the data read from the input file
            int bytesRead;
            while ((bytesRead = cs.Read(buffer, 0, buffer.Length)) > 0) // Read the data from the input file
            {
                fsOutput.Write(buffer, 0, bytesRead); // Decrypt the data and write it to the output file

                toolStripProgressBar1.Value += bytesRead; // Update the progress bar
            }

            // Close the streams
            fsInput.Close();
            cs.Close();
            fsOutput.Close();

            toolStripProgressBar1.Value = 0; // Reset the progress bar
        }

        private void DecryptFile_BC(string inputPath, string outputPath)
        {
            // BouncyCastle AES Engine for CTR mode
            AesEngine engine = new AesEngine(); // BouncyCastle AES Engine for CTR mode
            BufferedBlockCipher cipher;

            if (mode == 4)
            {
                cipher = new BufferedBlockCipher(new SicBlockCipher(engine)); // CTR mode cipher with AES engine in SIC mode (CTR mode)
            }
            else
            {
                cipher = new BufferedBlockCipher(new OfbBlockCipher(engine, blockSize / 8)); // OFB mode cipher with AES engine
            }

            KeyParameter keyParam = new KeyParameter(Convert.FromBase64String(KeyInput.Text), 0, keySize / 8); // Create a key parameter from the key
            ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, Convert.FromBase64String(IVInput.Text), 0, blockSize / 8); // Create a parameter with the key and IV

            cipher.Init(false, keyParamWithIV); // Initialize the cipher with the key and IV

            FileStream fsInput = new FileStream(inputPath, FileMode.Open, FileAccess.Read); // Open the input file for reading
            FileStream fsOutput = new FileStream(outputPath, FileMode.Create, FileAccess.Write); // Create the output file for writing

            toolStripProgressBar1.Maximum = (int)fsInput.Length; // Set the maximum value of the progress bar to the length of the input file

            byte[] buffer = new byte[4096]; // Create a buffer to hold the data read from the input file
            int bytesRead;
            while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0) // Read the data from the input file
            {
                byte[] output = new byte[cipher.GetOutputSize(bytesRead)]; // Create a byte array to hold the decrypted data
                int length = cipher.ProcessBytes(buffer, 0, bytesRead, output, 0); // Decrypt the data
                fsOutput.Write(output, 0, length); // Write the decrypted data to the output file

                toolStripProgressBar1.Value += bytesRead; // Update the progress bar
            }

            cipher.DoFinal(); // Finalize the decryption

            // Close the streams
            fsInput.Close();
            fsOutput.Close();

            toolStripProgressBar1.Value = 0; // Reset the progress bar
        }

        private void btnSelectLocation_Click(object sender, EventArgs e)
        {
            try
            {
                setStatusStrip("Selecting location...");
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.OverwritePrompt = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedSaveBox.Text = saveFileDialog.FileName;
                    setStatusStrip("Location selected: " + saveFileDialog.FileName);
                }
                else
                {
                    setStatusStrip("No location selected.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            // Check for key
            if (KeyInput.Text == "")
            {
                MessageBox.Show("Key must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ECB mode only needs key
            if (mode != 0 && IVInput.Text == "")
            {
                MessageBox.Show("IV must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tabControl1.SelectedIndex == 0)
            {
                if (InputTextBox.Text == "")
                {
                    MessageBox.Show("Input text must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    string plaintext = InputTextBox.Text;
                    string encrypted = (mode == 3 || mode == 4) ? EncryptText_BC(plaintext) : EncryptText(plaintext);
                    OutputTextBox.Text = encrypted;
                    setStatusStrip("Encryption successful.");
                    history.Add(new HistoryItem(false, mode, keySize, KeyInput.Text, IVInput.Text, plaintext, encrypted));
                    PopulateHistory();
                }
                catch (Exception ex)
                {
                    setStatusStrip("Error: " + ex.Message);
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (selectedFileBox.Text == "" || selectedSaveBox.Text == "")
                {
                    MessageBox.Show("Input and output file path must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                try
                {
                    if (mode == 3 || mode == 4)
                    {
                        EncryptFile_BC(selectedFileBox.Text, selectedSaveBox.Text);
                    }
                    else
                    {
                        EncryptFile(selectedFileBox.Text, selectedSaveBox.Text);
                    }
                    setStatusStrip("File encryption successful.");
                    history.Add(new HistoryItem(true, mode, keySize, KeyInput.Text, IVInput.Text, selectedFileBox.Text, selectedSaveBox.Text));
                    PopulateHistory();
                }
                catch (Exception ex)
                {
                    setStatusStrip("Error: " + ex.Message);
                }
            }

            // Save Key & IV when success
            File.WriteAllText(key_filename, KeyInput.Text);
            File.WriteAllText(iv_filename, IVInput.Text);
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            // Check for key
            if (KeyInput.Text == "")
            {
                MessageBox.Show("Key must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ECB mode only needs key
            if (mode != 0 && IVInput.Text == "")
            {
                MessageBox.Show("IV must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tabControl1.SelectedIndex == 0)
            {
                if (InputTextBox.Text == "")
                {
                    MessageBox.Show("Input text must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    string encrypted = InputTextBox.Text;
                    string decrypted = (mode == 3 || mode == 4) ? DecryptText_BC(encrypted) : DecryptText(encrypted);
                    OutputTextBox.Text = decrypted;
                    setStatusStrip("Decryption successful.");
                }
                catch (Exception ex)
                {
                    setStatusStrip("Error: " + ex.Message);
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (selectedFileBox.Text == "" || selectedSaveBox.Text == "")
                {
                    MessageBox.Show("Input and output file path must be set.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    if (mode == 3 || mode == 4)
                    {
                        DecryptFile_BC(selectedFileBox.Text, selectedSaveBox.Text);
                    }
                    else
                    {
                        DecryptFile(selectedFileBox.Text, selectedSaveBox.Text);
                    }
                    setStatusStrip("File decryption successful.");
                }
                catch (Exception ex)
                {
                    setStatusStrip("Error: " + ex.Message);
                }

            }
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            InputTextBox.Text = OutputTextBox.Text;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            HistoryManager.SaveToFile(history, history_filename);
        }

        private void btnSelectKey_Click(object sender, EventArgs e)
        {
            try
            {
                setStatusStrip("Selecting key file...");
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text Files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    KeyInput.Text = File.ReadAllText(openFileDialog.FileName);
                    setStatusStrip("Key file selected: " + openFileDialog.FileName);
                }
                else
                {
                    setStatusStrip("No key file selected.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSelectIV_Click(object sender, EventArgs e)
        {
            try
            {
                setStatusStrip("Selecting IV file...");
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text Files (*.txt)|*.txt";
                openFileDialog.FilterIndex = 1;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    IVInput.Text = File.ReadAllText(openFileDialog.FileName);
                    setStatusStrip("IV file selected: " + openFileDialog.FileName);
                }
                else
                {
                    setStatusStrip("No IV file selected.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void selectedFileBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectedFileBox.Text == "" || selectedFileBox.Text == "Select a file...")
                btnSelectFile_Click(sender, e);
        }

        private void selectedSaveBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (selectedSaveBox.Text == "" || selectedSaveBox.Text == "Select save location...")
                btnSelectLocation_Click(sender, e);
        }

        private void PopulateHistory()
        {
            historyListBox.Items.Clear();
            foreach (HistoryItem item in history)
            {
                historyListBox.Items.Add((item.Mode == 4 ? "CTR" : HistoryItem.ConvertMode(item.Mode).ToString()) + ": " + (item.isFile ? Path.GetFileName(item.Input) : item.Input));
            }
        }

        private void historyListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (historyListBox.SelectedIndex != -1)
            {
                HistoryItem item = history[historyListBox.SelectedIndex];
                if (item == null)
                {
                    return;
                }

                if (item.isFile)
                {
                    tabControl1.SelectedIndex = 1;
                    selectedFileBox.Text = item.Input;
                    selectedSaveBox.Text = item.Output;
                }
                else
                {
                    tabControl1.SelectedIndex = 0;
                    InputTextBox.Text = item.Input;
                    OutputTextBox.Text = item.Output;
                }

                KeyInput.Text = item.Key;
                IVInput.Text = item.IV;

                switch (item.Mode)
                {
                    case 0:
                        radioECB.Checked = true;
                        break;
                    case 1:
                        radioCBC.Checked = true;
                        break;
                    case 2:
                        radioCFB.Checked = true;
                        break;
                    case 3:
                        radioOFB.Checked = true;
                        break;
                    case 4:
                        radioCTR.Checked = true;
                        break;
                }
                switch (item.KeySize)
                {
                    case 128:
                        radioKey128.Checked = true;
                        break;
                    case 192:
                        radioKey192.Checked = true;
                        break;
                    case 256:
                        radioKey256.Checked = true;
                        break;
                }
            }
        }
    }
}
