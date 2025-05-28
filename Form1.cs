namespace Pokemon_save_editor
{
    public partial class Form1 : Form
    {
        private string loadedFilePath = string.Empty;
        private string[] saveValues;
        private string rawSaveData = "";

        private System.Windows.Forms.Label lblCoins;
        private System.Windows.Forms.Label lblPokeballs;
        private System.Windows.Forms.Label lblItem1;
        private System.Windows.Forms.Label lblItem2;
        private System.Windows.Forms.Label lblItem3;
        private System.Windows.Forms.Label lblItem4;
        private System.Windows.Forms.Button btnUnlockAllPokemon;
        private System.Windows.Forms.Button btnUnlockAllFigures;
        private System.Windows.Forms.Button btnUnlockAllBackgrounds;
        private System.Windows.Forms.Button btnUnlockAllAchievements;
     

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Pokemon Save Files (*.sav;*.pkmth)|*.sav;*.pkmth|All Files (*.*)|*.*";
                openFileDialog.Title = "Select a Pokemon Save File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    loadedFilePath = openFileDialog.FileName;
                    string base64 = File.ReadAllText(loadedFilePath);
                    byte[] data = Convert.FromBase64String(base64);
                    string decoded = System.Text.Encoding.UTF8.GetString(data);

                    // Example: values separated by commas
                    saveValues = decoded.Split(',');

                    // Populate textboxes (adjust indices as needed)
                    txtCoins.Text = saveValues[0];
                    txtPokeballs.Text = saveValues[1];
                    txtItem1.Text = saveValues[2];
                    txtItem2.Text = saveValues[3];
                    txtItem3.Text = saveValues[4];
                    txtItem4.Text = saveValues[5];

                    MessageBox.Show("Loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loadedFilePath))
            {
                MessageBox.Show("No file loaded. Please load a file before saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update saveValues from textboxes
            saveValues[0] = txtCoins.Text;
            saveValues[1] = txtPokeballs.Text;
            saveValues[2] = txtItem1.Text;
            saveValues[3] = txtItem2.Text;
            saveValues[4] = txtItem3.Text;
            saveValues[5] = txtItem4.Text;

            // Re-encode and save
            string updated = string.Join(",", saveValues);
            string base64 = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(updated));
            File.WriteAllText(loadedFilePath, base64);

            MessageBox.Show("Saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Example unlock all button
        private void btnUnlockAllPokemon_Click(object sender, EventArgs e)
        {
            // Set the relevant value(s) to max/unlocked
            saveValues[4] = "999"; // Example: unlock all pokemon
            txtItem3.Text = "999";
            MessageBox.Show("All Pokémon unlocked!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Pokemon Save Files (*.sav;*.pkmth)|*.sav;*.pkmth|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    loadedFilePath = openFileDialog.FileName;
                    string base64 = File.ReadAllText(loadedFilePath);
                    byte[] data = Convert.FromBase64String(base64);
                    rawSaveData = System.Text.Encoding.UTF8.GetString(data);

                    // Fill textboxes with values from the decoded data
                    txtUsername.Text = GetValue(rawSaveData, "username");
                    txtMoney.Text = GetValue(rawSaveData, "money");
                    txtPkmnCaught.Text = GetValue(rawSaveData, "pkmncaught");
                    txtShinyLevel.Text = GetValue(rawSaveData, "shinyLevel");
                    txtLegendLevel.Text = GetValue(rawSaveData, "legendLevel");
                    txtMythicLevel.Text = GetValue(rawSaveData, "mythicLevel");

                    MessageBox.Show("Loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Update rawSaveData with new values from textboxes
            rawSaveData = SetValue(rawSaveData, "username", txtUsername.Text);
        }

        private string GetValue(string data, string key)
        {
            var match = System.Text.RegularExpressions.Regex.Match(data, key + @"([A-Za-z0-9\.]+)");
            return match.Success ? match.Groups[1].Value : "";
        }

        private string SetValue(string data, string key, string value)
        {
            var regex = new System.Text.RegularExpressions.Regex(key + @"([A-Za-z0-9\.]+)");
            return regex.Replace(data, key + value);
        }
    }
}
