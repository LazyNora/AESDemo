using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using static AES_Demo.AES_Demo;

namespace AES_Demo
{
    public partial class Form1 : Form
    {
        public Form1() // constructor
        {
            InitializeComponent();
            //test();
        }

        // ! Default values
        private string history_filename = "history.json";
        private string key_filename = "key.txt";
        private string iv_filename = "iv.txt";
        private AES_Demo.ChainMode mode = AES_Demo.ChainMode.ECB; // 0 = ECB, 1 = CBC, 2 = CFB, 3 = OFB, 4 = CTR
        private AES_Demo.KeySize keySize = AES_Demo.KeySize.Bits128;
        private int blockSize = AES_Demo.blockSize;
        private bool Base64 = false;
        byte[] salt = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        string encrypted_extension = ".aes";
        List<HistoryItem> history = new List<HistoryItem>();

        // AES Initialization
        private void Form1_Load(object sender, EventArgs e)
        {
            history = HistoryManager.LoadFromFile(history_filename);
            PopulateHistory();

            // Set default values
            radioKey128.Checked = true;
            radioECB.Checked = true;
            radioBase64.Checked = false;
            IVInput.Enabled = false;
            Base64 = false;

            // Set default settings for AES
            mode = AES_Demo.ChainMode.ECB;
            keySize = AES_Demo.KeySize.Bits128;
        }

        // Handle Radio Group Key Size
        private void radioKey128_CheckedChanged(object sender, System.EventArgs e)
        {
            keySize = AES_Demo.KeySize.Bits128;
            setStatusStrip("Changed key size to 128");

        }

        private void radioKey192_CheckedChanged(object sender, System.EventArgs e)
        {
            keySize = AES_Demo.KeySize.Bits192;
            setStatusStrip("Changed key size to 192");

        }

        private void radioKey256_CheckedChanged(object sender, System.EventArgs e)
        {
            keySize = AES_Demo.KeySize.Bits256;
            setStatusStrip("Changed key size to 256");

        }

        // Handle Radio Group Chaining Mode
        private void radioECB_CheckedChanged(object sender, System.EventArgs e)
        {
            mode = AES_Demo.ChainMode.ECB;
            setStatusStrip("Changed Chaining Mode to ECB");
            IVInput.Enabled = false;
            IVInput.Text = "";
        }

        private void radioCBC_CheckedChanged(object sender, System.EventArgs e)
        {
            if (mode == AES_Demo.ChainMode.ECB)
            {
                IVInput.Enabled = true;
                IVInput.Text = "";
            }
            mode = AES_Demo.ChainMode.CBC;
            setStatusStrip("Changed Chaining Mode to CBC");
        }

        private void radioCFB_CheckedChanged(object sender, System.EventArgs e)
        {
            if (mode == AES_Demo.ChainMode.ECB)
            {
                IVInput.Enabled = true;
                IVInput.Text = "";
            }
            mode = AES_Demo.ChainMode.CFB;
            setStatusStrip("Changed Chaining Mode to CFB");
        }

        private void radioOFB_CheckedChanged(object sender, System.EventArgs e)
        {
            if (mode == AES_Demo.ChainMode.ECB)
            {
                IVInput.Enabled = true;
                IVInput.Text = "";
            }
            mode = AES_Demo.ChainMode.OFB;
            // OFB somehow not working, internal error
            setStatusStrip("Changed Chaining Mode to OFB");
        }

        private void radioCTR_CheckedChanged(object sender, System.EventArgs e)
        {
            if (mode == AES_Demo.ChainMode.ECB)
            {
                IVInput.Enabled = true;
                IVInput.Text = "";
            }
            mode = AES_Demo.ChainMode.CTR;
            // CTR is not supported by .NET
            setStatusStrip("CTR is not supported by .NET, using BouncyCastle AES Engine for CTR mode");
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
            KeyInput.Text = AES_Demo.randomKey((int)keySize);
        }

        private void btnIVRandom_Click(object sender, EventArgs e)
        {
            if (mode == AES_Demo.ChainMode.ECB)
            {
                MessageBox.Show("IV is not needed for ECB mode.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IVInput.Text = AES_Demo.randomKey(blockSize);
        }

        private void genKeyIV_Click(object sender, EventArgs e)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(PassInput.Text, salt); // Create a PasswordDeriveBytes object with the password and salt
            KeyInput.Text = AES_Demo.bytesArrayToString(pdb.GetBytes((int)keySize / (8 * 2))); // Generate a key from the password
            if (mode != 0)
            {
                IVInput.Text = AES_Demo.bytesArrayToString(pdb.GetBytes(blockSize / (8 * 2))); // Generate an IV from the password
                setStatusStrip("Key and IV generated.");
            }
            else
            {
                setStatusStrip("Key generated.");
            }
        }

        private void EncryptFile(string inputPath, string outputPath)
        {
            if (mode >= ChainMode.ECB && mode <= ChainMode.CFB)
            {
                EncryptFile_Helper(inputPath, outputPath);
            }
            else
            {
                EncryptFile_BouncyCastle(inputPath, outputPath);
            }
        }

        private void EncryptFile_Helper(string inputPath, string outputPath)
        {
            // Get Key and IV from Base64 string
            byte[] key = getBytes_Key(KeyInput.Text);
            byte[] iv = getBytes_Key(IVInput.Text);

            // Create AES object
            Aes aes = Aes.Create();
            aes.Mode = mapCipherMode(mode);
            aes.KeySize = (int)keySize;
            aes.BlockSize = blockSize;
            aes.Padding = paddingMode;
            aes.Key = key;
            if (mode != ChainMode.ECB)
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

        private void EncryptFile_BouncyCastle(string inputPath, string outputPath)
        {
            // BouncyCastle AES Engine for CTR mode
            AesEngine engine = new AesEngine(); // BouncyCastle AES Engine for CTR mode
            BufferedBlockCipher cipher;

            switch (mode)
            {
                case ChainMode.OFB:
                    cipher = new BufferedBlockCipher(new OfbBlockCipher(engine, blockSize)); // OFB, block size is in bit
                    break;
                case ChainMode.CTR:
                    cipher = new BufferedBlockCipher(new SicBlockCipher(engine)); // CTR
                    break;
                default:
                    throw new Exception("How did you even get here?!!");
            }

            KeyParameter keyParam = new KeyParameter(getBytes_Key(KeyInput.Text), 0, (int)keySize / 8); // Create a key parameter from the key
            ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, getBytes_Key(IVInput.Text), 0, blockSize / 8); // Create a parameter with the key and IV

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
            if (mode >= ChainMode.ECB && mode <= ChainMode.CFB)
            {
                DecryptFile_Helper(inputPath, outputPath);
            }
            else
            {
                DecryptFile_BouncyCastle(inputPath, outputPath);
            }
        }

        private void DecryptFile_Helper(string inputPath, string outputPath)
        {
            // Get Key and IV from Base64 string
            byte[] key = AES_Demo.getBytes_Key(KeyInput.Text);
            byte[] iv = AES_Demo.getBytes_Key(IVInput.Text);

            // Create AES object
            Aes aes = Aes.Create();
            aes.Mode = AES_Demo.mapCipherMode(mode);
            aes.KeySize = (int)keySize;
            aes.BlockSize = blockSize;
            aes.Padding = AES_Demo.paddingMode;
            aes.Key = key;
            if (mode != AES_Demo.ChainMode.ECB)
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

        private void DecryptFile_BouncyCastle(string inputPath, string outputPath)
        {
            // BouncyCastle AES Engine for CTR mode
            AesEngine engine = new AesEngine(); // BouncyCastle AES Engine for CTR mode
            BufferedBlockCipher cipher;

            switch (mode)
            {
                case ChainMode.OFB:
                    cipher = new BufferedBlockCipher(new OfbBlockCipher(engine, blockSize)); // OFB, block size is in bit
                    break;
                case ChainMode.CTR:
                    cipher = new BufferedBlockCipher(new SicBlockCipher(engine)); // CTR
                    break;
                default:
                    throw new Exception("How did you even get here?!!");
            }

            KeyParameter keyParam = new KeyParameter(getBytes_Key(KeyInput.Text), 0, (int)keySize / 8); // Create a key parameter from the key
            ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, getBytes_Key(IVInput.Text), 0, blockSize / 8); // Create a parameter with the key and IV

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
                    byte[] key = AES_Demo.getBytes_Key(KeyInput.Text);
                    byte[] iv = AES_Demo.getBytes_Key(IVInput.Text);
                    byte[] plainText = AES_Demo.getBytes_Text(InputTextBox.Text);

                    if (!Base64)
                    {
                        OutputTextBox.Text = AES_Demo.bytesArrayToString(AES_Demo.Encrypt(mode, keySize, plainText, key, iv));

                    }
                    else
                    {
                        OutputTextBox.Text = Convert.ToBase64String(AES_Demo.Encrypt(mode, keySize, plainText, key, iv));
                    }
                    setStatusStrip("Encryption successful.");

                    AddItem(true, false, (int)mode, (int)keySize, KeyInput.Text, IVInput.Text, InputTextBox.Text, OutputTextBox.Text, Base64);
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
                    EncryptFile(selectedFileBox.Text, selectedSaveBox.Text);
                    setStatusStrip("File encryption successful.");
                    AddItem(true, true, (int)mode, (int)keySize, KeyInput.Text, IVInput.Text, selectedFileBox.Text, selectedSaveBox.Text, Base64);
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
                    byte[] key = AES_Demo.getBytes_Key(KeyInput.Text); // ASCII 8bit
                    byte[] iv = AES_Demo.getBytes_Key(IVInput.Text); // ASCII 8bit
                    byte[] ciphertext;
                    if (!Base64)
                    {
                        ciphertext = AES_Demo.stringToBytesArray(InputTextBox.Text); // Byte array
                    }
                    else
                    {
                        ciphertext = Convert.FromBase64String(InputTextBox.Text); // Byte array
                    }

                    OutputTextBox.Text = AES_Demo.getString_Text(AES_Demo.Decrypt(mode, keySize, ciphertext, key, iv));
                    setStatusStrip("Decryption successful.");

                    AddItem(false, false, (int)mode, (int)keySize, KeyInput.Text, IVInput.Text, InputTextBox.Text, OutputTextBox.Text, Base64);
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
                    DecryptFile(selectedFileBox.Text, selectedSaveBox.Text);
                    setStatusStrip("File decryption successful.");
                    AddItem(false, true, (int)mode, (int)keySize, KeyInput.Text, IVInput.Text, selectedFileBox.Text, selectedSaveBox.Text, Base64);
                    PopulateHistory();
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
            if (mode == ChainMode.ECB)
            {
                MessageBox.Show("IV is not needed for ECB mode.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                historyListBox.Items.Add(AES_Demo.modeToString((AES_Demo.ChainMode)item.Mode) + (item.Encrypt ? "-Encrypt" : "-Decrypt") + ": " + (item.isFile ? Path.GetFileName(item.Input) : item.Input)); ;
            }
        }

        private void AddItem(bool Encrypt, bool isFile, int Mode, int KeySize, string Key, string IV, string Input, string Output, bool Base64)
        {
            history.Add(new HistoryItem(Encrypt, isFile, Mode, KeySize, Key, IV, Input, Output, Base64));
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

                    switch (item.Base64)
                    {
                        case true:
                            radioBase64.Checked = true;
                            break;
                        case false:
                            radioHex.Checked = true;
                            break;
                    }
                }

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

                KeyInput.Text = item.Key;
                IVInput.Text = item.IV;
            }
        }

        private void radioBase64_CheckedChanged(object sender, EventArgs e)
        {
            Base64 = true;
            setStatusStrip("Using cipher text format: Base64");
        }

        private void radioHex_CheckedChanged(object sender, EventArgs e)
        {
            Base64 = false;
            setStatusStrip("Using cipher text format: Hex");
        }
    }
}
