using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Configuration;

using Newtonsoft.Json;

/// <summary>
/// HostFileEditor - Written by Andrew Delicata
/// https://github.com/Delie
/// </summary>
namespace HostFileEditor
{
	public partial class MainWindow : Window
	{
		string strHostFileLocation;
		string strHostFileLocationCloud;

		public MainWindow() {
			InitializeComponent();
			try {
				string filePath = Environment.CurrentDirectory + @"\hostfileconfig.json";
				if (File.Exists(filePath)) {
					string strJson = File.ReadAllText(filePath);
					HostFileConfig config = JsonConvert.DeserializeObject<HostFileConfig>(strJson);
					strHostFileLocation = config.HostFileLocation;
					strHostFileLocationCloud = config.HostFileLocationCloud;
					txtHostsFile.Text = LoadFile(strHostFileLocation);
				}
				else {
					MessageBox.Show("Please create config file \"HostFileConfig.json\" in the app root folder.  See HostFileConfig.Example.json for reference", "Error");
					Close();
				}
			}
			catch (Exception ex) {
				MessageBox.Show("HostFileConfig.json file is invalid!  Please see HostFileConfig.Example.json for reference", "Error");
				Close();
			}
		}

		private string LoadFile(string strFilename) {
			string str = "";
			try {
				str = File.ReadAllText(strFilename);
			}
			catch (Exception ex) {
				MessageBox.Show("Error: " + ex.Message);
			}
			return str;
		}

		private void SaveFile(string strFilename, string strContents) {
			try {
				if (!string.IsNullOrWhiteSpace(strFilename))
					File.WriteAllText(strFilename, strContents);

				lblStatus.Content = "File saved (" + DateTime.Now.ToLongTimeString() + ")";
			}
			catch (Exception ex) {
				MessageBox.Show("Error: " + ex.Message);
			}
		}

		#region Hosts File
		private void btnHostLoad_Click(object sender, RoutedEventArgs e) {
			txtHostsFile.Text = LoadFile(strHostFileLocation);
		}

		private void btnHostSave_Click(object sender, RoutedEventArgs e) {
			SaveFile(strHostFileLocation, txtHostsFile.Text);
		}

		private void btnHostLoadCloud_Click(object sender, RoutedEventArgs e) {
			txtHostsFile.Text = LoadFile(strHostFileLocationCloud);
		}

		private void btnHostSaveCloud_Click(object sender, RoutedEventArgs e) {
			SaveFile(strHostFileLocationCloud, txtHostsFile.Text);
		}
		#endregion
	}
}
