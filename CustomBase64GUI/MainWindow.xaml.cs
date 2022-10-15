using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomBase64GUI
{
    public partial class MainWindow : Window
    {
        private CustomBase64 base64 = new();

        private Color EthicalBlue = new() { R = 0x00, G = 0xAA, B = 0xFF, A = 0xFF };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonEncodeDecodeText_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButtonCustomAlphabet.IsChecked == true)
                base64.CustomAlphabet = TextBoxCustomAlphabet.Text;
            else if (RadioButtonDefaultAlphabet.IsChecked == true)
                base64.CustomAlphabet = base64.DefaultAlphabet;

            if (base64.CustomAlphabet.Length != 64)
            {
                MessageBox.Show("The Base64 alphabet length should be 64 characters.", 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            object? content = (sender as Button)?.Content;

            if (content?.ToString()?.ToLower().Contains("decode") == true)
            {
                var bytes = Encoding.UTF8.GetBytes(TextBoxEncodedDecodedText.Text);
                string text = Encoding.UTF8.GetString(bytes);
                TextBoxEncodedDecodedText.Text = base64.DecodeText(text);
                TextBoxEncodedDecodedText.Foreground = new SolidColorBrush() { Color = EthicalBlue };
            }
            else
            {
                var bytes = Encoding.UTF8.GetBytes(TextBoxEncodedDecodedText.Text);
                string text = Encoding.UTF8.GetString(bytes);
                TextBoxEncodedDecodedText.Text = base64.EncodeText(text);
                TextBoxEncodedDecodedText.Foreground = new SolidColorBrush() { Color = Colors.Red };
            }
        }

        private void ButtonEncodeDecodeFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                FileName = "",
                DefaultExt = ".*",
                Filter = "All files (.*)|*.*",
                Title = "Open file for Base64 encoding or decoding"
            };

            string filename = string.Empty;

            if (dialog.ShowDialog() == true)
            {
                filename = dialog.FileName;

                if (File.Exists(filename))
                {
                    var bytes = new FileInfo(filename).Length;

                    if (bytes > 1073741824)
                    {
                        MessageBox.Show("This program was not tested on files with size over 1 GB.", 
                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Selected file does not exist.", 
                        "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            List<byte> processed = new List<byte>();
            var buffer = File.ReadAllBytes(filename);

            object? content = (sender as Button)?.Content;

            string processedFilename = string.Empty;

            if (content?.ToString()?.ToLower().Contains("decode") == true)
            {
                processed = base64.DecodeBytes(buffer.ToList());
                processedFilename = "base64_decoded.bin";
            }
            else
            {
                processed = base64.EncodeBytes(buffer.ToList());
                processedFilename = "base64_encoded.bin";
            }

            var saveDialog = new Microsoft.Win32.SaveFileDialog()
            {
                FileName = processedFilename,
                DefaultExt = ".*",
                Filter = "All files (.*)|*.*",
                Title = "Save processed file"
            };
            
            if (saveDialog.ShowDialog() == true)
            {
                File.WriteAllBytes(saveDialog.FileName, processed.ToArray());
                MessageBox.Show("Processed data was saved to file " + Path.GetFileName(saveDialog.FileName), 
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }
    }
}
