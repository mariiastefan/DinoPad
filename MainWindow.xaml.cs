using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using tema1;
using Path = System.IO.Path;

namespace temanpd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        new int TabIndex = 1;
        ObservableCollection<TabVM> Tabs = new ObservableCollection<TabVM>();
        public MainWindow()
        {
            InitializeComponent();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo driveInfo in drives)
                treeView.Items.Add(CreateTreeItem(driveInfo));
            var tab1 = new TabVM()
            {
                Header = $"Tab {TabIndex}",
                Text = ""
            };
            Tabs.Add(tab1);

            MyTabControl.ItemsSource = Tabs;
            MyTabControl.SelectionChanged += MyTabControl_SelectionChanged;

        }

        private void MyTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                var pos = MyTabControl.SelectedIndex;
            }
        }


        private void onClose_Click(object sender, RoutedEventArgs e)
        {
            var tab = (sender as Button).DataContext as TabVM;
            var index = Tabs.IndexOf(tab);
            Tabs.RemoveAt(index);
        }

        private void addNEW_Click(object sender, RoutedEventArgs e)
        {
            TabIndex++;
            var tab1 = new TabVM()
            {
                Header = $"Tab {TabIndex}",
                Text = ""
            };
            Tabs.Add(tab1);

        }
        private void btnSaveFileAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            var pos = MyTabControl.SelectedIndex;
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, Tabs[pos].Text);
                Tabs[pos].Header = Path.GetFileName(saveFileDialog.FileName);
                Tabs[pos].Path = saveFileDialog.FileName;
                Tabs[pos].Colour = "Black";
            }



        }

        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            var pos = MyTabControl.SelectedIndex;

            if (Tabs[pos].Path != null)
                File.WriteAllText(Tabs[pos].Path, Tabs[pos].Text);
            else
                btnSaveFileAs_Click(sender, e);
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            var pos = MyTabControl.SelectedIndex;
            if (openFileDialog.ShowDialog() == true)
            {
                Tabs[pos].Header = Path.GetFileName(openFileDialog.FileName);
                Tabs[pos].Text = File.ReadAllText(openFileDialog.FileName);
                Tabs[pos].Path = openFileDialog.FileName;
            }
        }
        private void btnAbout_Click(object sender, RoutedEventArgs e)
        {
            help win2 = new help();
            win2.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            win2.Show();

        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Find find = new Find();
            find.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            find.Show();
            var pos = MyTabControl.SelectedIndex;

        }
        #region TreeView Implementation
        public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if ((item.Items.Count == 1) && (item.Items[0] is string) && !(item.Tag is FileInfo))
            {
                item.Items.Clear();

                DirectoryInfo expandedDir = null;

                if (item.Tag is DriveInfo)
                    expandedDir = (item.Tag as DriveInfo).RootDirectory;

                if (item.Tag is DirectoryInfo)
                    expandedDir = (item.Tag as DirectoryInfo);

                try
                {
                    foreach (DirectoryInfo subDir in expandedDir.GetDirectories())
                    {
                        item.Items.Add(CreateTreeItem(subDir));
                    }
                    foreach (FileInfo file in expandedDir.GetFiles())
                    {
                        item.Items.Add(CreateTreeItem(file));
                    }
                }
                catch { }


            }
        }

        private TreeViewItem CreateTreeItem(object o)
        {
            TreeViewItem item = new TreeViewItem();
            if (o is FileInfo)
            {
                item.Header = o.ToString();
                item.Tag = o;
                item.MouseDoubleClick += new MouseButtonEventHandler(AddTabFromTreeView);

            }
            else
            {
                item.Header = o.ToString();
                item.Tag = o;
                item.Items.Add("Loading...");
            }

            return item;
        }

        private void AddTabFromTreeView(object sender, RoutedEventArgs e)
        {
            
            var path = (((TreeViewItem)sender).Tag as FileInfo).FullName;
            var tab = new TabVM()
            {
                Header = Path.GetFileName(path),
                Text = File.ReadAllText(path),
                Path = path,
                Colour = "LightGreen"
        };
            Tabs.Add(tab);
        }
        #endregion

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }



}
