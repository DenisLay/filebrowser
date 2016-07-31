using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace filebrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<DirectoryEntry> entries = new ObservableCollection<DirectoryEntry>();
        ObservableCollection<DirectoryEntry> subEntries = new ObservableCollection<DirectoryEntry>();
        ObservableCollection<DirectoryEntry> subEntries2 = new ObservableCollection<DirectoryEntry>();

        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string s in Directory.GetLogicalDrives())
            {
                DirectoryEntry d = new DirectoryEntry(s, s, "<Driver>", "<DIR>", Directory.GetLastWriteTime(s), "C:/Users/Robert/Documents/dotnet/filebrowser/images/hdd.ico", EntryType.Dir);
                entries.Add(d);
            }
            this.listView1.ItemsSource = entries;
            this.listView2.ItemsSource = entries;

        }
        int i1 = 1;
        int i2 = 1;
        string[] last1 = new string[100];
        string[] last2 = new string[100];

        void listViewItem1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = e.Source as ListViewItem;

            DirectoryEntry entry = item.DataContext as DirectoryEntry;

            if (entry.Type == EntryType.Dir)
            {
                if (entry.Name == "...")
                {
                    i1--;
                    if (last1[i1] == null)
                    {
                        subEntries.Clear();

                        foreach (string s in Directory.GetLogicalDrives())
                        {
                            DirectoryEntry d = new DirectoryEntry(s, s, "<Driver>", "<DIR>", Directory.GetLastWriteTime(s), "C:/Users/Robert/Documents/dotnet/filebrowser/images/hdd.ico", EntryType.Dir);
                            subEntries.Add(d);
                        }
                    }
                    else
                    {
                        subEntries.Clear();

                        DirectoryEntry dd = new DirectoryEntry("...", last1[i1], "<Folder>", "<DIR>", entry.Date, "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);
                        subEntries.Add(dd);

                        foreach (string s in Directory.GetDirectories(last1[i1]))
                        {
                            DirectoryInfo dir = new DirectoryInfo(s);
                            DirectoryEntry d = new DirectoryEntry(dir.Name, dir.FullName, "<Folder>", "<DIR>", Directory.GetLastWriteTime(s), "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);
                            d.Lastpath = entry.Fullpath;
                            subEntries.Add(d);
                        }

                        foreach (string f in Directory.GetFiles(last1[i1]))
                        {
                            FileInfo file = new FileInfo(f);
                            DirectoryEntry d = new DirectoryEntry(Path.GetFileNameWithoutExtension(file.Name), file.FullName, file.Extension, file.Length.ToString(), file.LastWriteTime, "C:/Users/Robert/Documents/dotnet/filebrowser/images/file.ico", EntryType.File);
                            subEntries.Add(d);
                        }

                    }

                }
                else
                {
                    subEntries.Clear();

                    DirectoryEntry dd = new DirectoryEntry(
                            "...", entry.Lastpath, "", "<DIR>",
                            entry.Date,
                            "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);

                    subEntries.Add(dd);

                    try
                    {
                        foreach (string s in Directory.GetDirectories(entry.Fullpath))
                        {

                            DirectoryInfo dir = new DirectoryInfo(s);
                            DirectoryEntry d = new DirectoryEntry(
                                dir.Name, dir.FullName, "", "<DIR>",
                                Directory.GetLastWriteTime(s),
                                "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);
                            d.Lastpath = entry.Fullpath;
                            subEntries.Add(d);

                        }
                        foreach (string f in Directory.GetFiles(entry.Fullpath))
                        {
                            FileInfo file = new FileInfo(f);
                            DirectoryEntry d = new DirectoryEntry(
                                Path.GetFileNameWithoutExtension(file.Name), file.FullName, file.Extension, file.Length.ToString(),
                                file.LastWriteTime,
                                "C:/Users/Robert/Documents/dotnet/filebrowser/images/file.ico", EntryType.File);
                            subEntries.Add(d);
                        }
                        last1[i1] = entry.Lastpath;
                        last1[i1 + 1] = entry.Fullpath;
                        i1++;
                    }
                    catch (Exception ex)
                    {   //hozzáférés lekezelése
                        if (ex is UnauthorizedAccessException)
                        {
                            MessageBox.Show("Hozzáférés megtagadva, kérem kérjen rendszergazdai jogosultságot!");
                        }
                        else
                            MessageBox.Show(ex.ToString());
                    }
                }

                listView1.ItemsSource = subEntries;

            }
            else if (entry.Type == EntryType.File)
            {
                try
                {
                    Process.Start(entry.Fullpath.ToString());
                }
                catch
                {
                    ;
                }
            }
        }
        void listViewItem2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = e.Source as ListViewItem;
        
            DirectoryEntry entry = item.DataContext as DirectoryEntry;

            if (entry.Type == EntryType.Dir)
            {
                if (entry.Name == "...")
                {
                    i2--;
                    if (last2[i2] == null)
                    {
                        subEntries2.Clear();

                        foreach (string s in Directory.GetLogicalDrives())
                        {
                            DirectoryEntry d = new DirectoryEntry(s, s, "<Driver>", "<DIR>", Directory.GetLastWriteTime(s), "C:/Users/Robert/Documents/dotnet/filebrowser/images/hdd.ico", EntryType.Dir);
                            subEntries2.Add(d);
                        }
                    }
                    else
                    {
                        subEntries2.Clear();

                        DirectoryEntry dd = new DirectoryEntry("...", last2[i2], "<Folder>", "<DIR>", entry.Date, "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);
                        subEntries2.Add(dd);

                        foreach (string s in Directory.GetDirectories(last2[i2]))
                        {
                            DirectoryInfo dir = new DirectoryInfo(s);
                            DirectoryEntry d = new DirectoryEntry(dir.Name, dir.FullName, "<Folder>", "<DIR>", Directory.GetLastWriteTime(s), "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);
                            d.Lastpath = entry.Fullpath;
                            subEntries2.Add(d);
                        }

                        foreach (string f in Directory.GetFiles(last2[i2]))
                        {
                            FileInfo file = new FileInfo(f);
                            DirectoryEntry d = new DirectoryEntry(Path.GetFileNameWithoutExtension(file.Name), file.FullName, file.Extension, file.Length.ToString(), file.LastWriteTime, "C:/Users/Robert/Documents/dotnet/filebrowser/images/file.ico", EntryType.File);
                            subEntries2.Add(d);
                        }

                    }

                }
                else
                {
                    subEntries2.Clear();

                    DirectoryEntry dd = new DirectoryEntry(
                            "...", entry.Lastpath, "", "<DIR>",
                            entry.Date,
                            "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);

                    subEntries2.Add(dd);

                    try
                    {
                        foreach (string s in Directory.GetDirectories(entry.Fullpath))
                        {

                            DirectoryInfo dir = new DirectoryInfo(s);
                            DirectoryEntry d = new DirectoryEntry(
                                dir.Name, dir.FullName, "", "<DIR>",
                                Directory.GetLastWriteTime(s),
                                "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);
                            d.Lastpath = entry.Fullpath;
                            subEntries2.Add(d);

                        }
                        foreach (string f in Directory.GetFiles(entry.Fullpath))
                        {
                            FileInfo file = new FileInfo(f);
                            DirectoryEntry d = new DirectoryEntry(
                                Path.GetFileNameWithoutExtension(file.Name), file.FullName, file.Extension, file.Length.ToString(),
                                file.LastWriteTime,
                                "C:/Users/Robert/Documents/dotnet/filebrowser/images/file.ico", EntryType.File);
                            subEntries2.Add(d);
                        }
                        last2[i2] = entry.Lastpath;
                        last2[i2 + 1] = entry.Fullpath;
                        i2++;
                    }
                    catch (Exception ex)
                    {   //hozzáférés lekezelése
                        if (ex is UnauthorizedAccessException)
                        {
                            MessageBox.Show("Hozzáférés megtagadva, kérem kérjen rendszergazdai jogosultságot!");
                        }
                        else
                            MessageBox.Show(ex.ToString());
                    }
                }

                listView2.ItemsSource = subEntries2;

            }
            else if (entry.Type == EntryType.File)
            {
                try
                {
                    Process.Start(entry.Fullpath.ToString());
                }
                catch
                {
                    ;
                }
            }
        }



        private delegate void dUpdateProgressBar(int pValue);

        private void UpdateProgressBar(int pValue)
        {
            this.progressBar1.Value = pValue;
        }

        public Stream celStream;
        public int prValue;

        public class MyAsyncInfo
        {
            public Byte[] ByteArray { get; set; }
            public Stream MyStream { get; set; }

            public MyAsyncInfo(Byte[] array, Stream stream)
            {
                ByteArray = array;
                MyStream = stream;
            }
        }


   
        private void Button_Click_Copy(object sender, RoutedEventArgs e)
        {
            string celnev;
            string start;
            bool first = true;

            try
            {
                if (listView1.SelectedItem != null)
                {
                    start = ((DirectoryEntry)listView1.SelectedItem).Fullpath;
                    celnev = last2[i2];
                    
                }
                else if (listView2.SelectedItem != null)
                {
                    start = ((DirectoryEntry)listView2.SelectedItem).Fullpath;
                    celnev = last1[i1];
                    
                }
                else
                    return;
            }
            catch
            {
                return;
            }

            FileStream stream = null;
            try
            {
                celnev = celnev + "\\" + Path.GetFileName(start);
                stream = File.OpenRead(start);
                celStream = File.Create(celnev);
                this.progressBar1.Maximum = (int)stream.Length;
                prValue = 0;
                this.progressBar1.Value = 0;

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("A fájlt nem lehet megnyitni.");
                return;
            }

            Byte[] myByteArray = new Byte[1000];

            try
            {
                stream.BeginRead(myByteArray, 0, 1000,
                  new AsyncCallback(OnRead), new MyAsyncInfo(myByteArray, stream));
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Nem sikerült az olvasás.");
                stream.Close();
            }

            
            
        }

             private void OnRead(IAsyncResult ar)
             {
            MyAsyncInfo info = (MyAsyncInfo)ar.AsyncState;

            int amountRead = 0;
            try
            {
                amountRead = info.MyStream.EndRead(ar);
                prValue += amountRead;

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Nem sikerült a fájl olvasása.");
                info.MyStream.Close();
                return;
            }

            string text = Encoding.UTF8.GetString(info.ByteArray, 0, amountRead);

            dUpdateProgressBar mUpdateProgressBar = new dUpdateProgressBar(UpdateProgressBar);
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Send, mUpdateProgressBar, prValue);

            celStream.Write(info.ByteArray, 0, amountRead);
            if (info.MyStream.Position < info.MyStream.Length)
            {
                try
                {

                    info.MyStream.BeginRead(info.ByteArray, 0,
                      1000, new AsyncCallback(OnRead), info);
                }
                catch (Exception)
                {
                    System.Windows.MessageBox.Show("Nem sikerült az olvasás folytatása.");
                    info.MyStream.Close();
                }
            }
            else
            {
                info.MyStream.Close();
                celStream.Close();
                System.Windows.MessageBox.Show("A fájl másolása megtörtént.");
            }
        }

           private void Button_Click_Delete(object sender, RoutedEventArgs e)
             {
                 string delete;
                 int i;
                 try
                 {
                     if (listView1.SelectedItem != null )
                     {
                         delete = ((DirectoryEntry)listView1.SelectedItem).Fullpath;
                         File.Delete(delete);
                         

                         string folder = Directory.GetParent(delete).FullName;
                         subEntries.Clear();

                         DirectoryEntry dd = new DirectoryEntry(
                                 "...", last1[i1], "", "<DIR>",
                                 new DateTime(),
                                 "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);

                         subEntries.Add(dd);

                         try
                         {
                             foreach (string s in Directory.GetDirectories(folder))
                             {

                                 DirectoryInfo dir = new DirectoryInfo(s);
                                 DirectoryEntry d = new DirectoryEntry(
                                     dir.Name, dir.FullName, "", "<DIR>",
                                     Directory.GetLastWriteTime(s),
                                     "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);
                        
                                 subEntries.Add(d);

                             }
                             foreach (string f in Directory.GetFiles(folder))
                             {
                                 FileInfo file = new FileInfo(f);
                                 DirectoryEntry d = new DirectoryEntry(
                                     Path.GetFileNameWithoutExtension(file.Name), file.FullName, file.Extension, file.Length.ToString(),
                                     file.LastWriteTime,
                                     "C:/Users/Robert/Documents/dotnet/filebrowser/images/file.ico", EntryType.File);
                                 subEntries.Add(d);
                             }
                         }
                         catch
                         {
                             ;
                         }
                         MessageBox.Show(delete + " fájl törlése sikeres.");
                     }
                     else if (listView2.SelectedItem != null)
                     {
                         delete = ((DirectoryEntry)listView2.SelectedItem).Fullpath;
                         File.Delete(delete);

                         string folder = Directory.GetParent(delete).FullName;
                         subEntries2.Clear();

                         DirectoryEntry dd = new DirectoryEntry(
                                 "...", last2[i2], "", "<DIR>",
                                 new DateTime(),
                                 "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);

                         subEntries2.Add(dd);

                         try
                         {
                             foreach (string s in Directory.GetDirectories(folder))
                             {

                                 DirectoryInfo dir = new DirectoryInfo(s);
                                 DirectoryEntry d = new DirectoryEntry(
                                     dir.Name, dir.FullName, "", "<DIR>",
                                     Directory.GetLastWriteTime(s),
                                     "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);

                                 subEntries2.Add(d);

                             }
                             foreach (string f in Directory.GetFiles(folder))
                             {
                                 FileInfo file = new FileInfo(f);
                                 DirectoryEntry d = new DirectoryEntry(
                                     Path.GetFileNameWithoutExtension(file.Name), file.FullName, file.Extension, file.Length.ToString(),
                                     file.LastWriteTime,
                                     "C:/Users/Robert/Documents/dotnet/filebrowser/images/file.ico", EntryType.File);
                                 subEntries2.Add(d);
                             }
                         }
                         catch
                         {
                             ;
                         }
                         MessageBox.Show(delete + " fájl törlése sikeres.");
                     }
                     else
                         return;
                 }
                 catch
                 {
                     return;
                 }

                
             }

             private void Button_Click_New_Folder(object sender, RoutedEventArgs e)
             {
                 string path;
                 try
                 {
                     if (listView1.SelectedItem != null && listView1.IsFocused)
                     {
                         path = ((DirectoryEntry)listView1.SelectedItem).Fullpath;
                         path = Directory.GetParent(path).FullName;
                         Directory.CreateDirectory(path + "\\" + textbox1.Text);
                     
                         string folder = path;

                         subEntries.Clear();

                         DirectoryEntry dd = new DirectoryEntry(
                                 "...", last1[i1-1], "", "<DIR>",
                                 new DateTime(),
                                 "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);

                         subEntries.Add(dd);

                         try
                         {
                             foreach (string s in Directory.GetDirectories(folder))
                             {

                                 DirectoryInfo dir = new DirectoryInfo(s);
                                 DirectoryEntry d = new DirectoryEntry(
                                     dir.Name, dir.FullName, "", "<DIR>",
                                     Directory.GetLastWriteTime(s),
                                     "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);

                                 subEntries.Add(d);

                             }
                             foreach (string f in Directory.GetFiles(folder))
                             {
                                 FileInfo file = new FileInfo(f);
                                 DirectoryEntry d = new DirectoryEntry(
                                     Path.GetFileNameWithoutExtension(file.Name), file.FullName, file.Extension, file.Length.ToString(),
                                     file.LastWriteTime,
                                     "C:/Users/Robert/Documents/dotnet/filebrowser/images/file.ico", EntryType.File);
                                 subEntries.Add(d);
                             }
                         }
                         catch
                         {
                             ;
                         }
                         MessageBox.Show(textbox1.Text + " könyvtár létrehozása sikeres.");
                     }
                     else if (listView2.SelectedItem != null)
                     {
                         path = ((DirectoryEntry)listView2.SelectedItem).Fullpath;
                         path = Directory.GetParent(path).FullName;
                         Directory.CreateDirectory(path + "\\" + textbox1.Text);
                         
                         string folder = path;
                         subEntries2.Clear();

                         DirectoryEntry dd = new DirectoryEntry(
                                 "...", last2[i2-1], "", "<DIR>",
                                 new DateTime(),
                                 "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);

                         subEntries2.Add(dd);

                         try
                         {
                             foreach (string s in Directory.GetDirectories(folder))
                             {

                                 DirectoryInfo dir = new DirectoryInfo(s);
                                 DirectoryEntry d = new DirectoryEntry(
                                     dir.Name, dir.FullName, "", "<DIR>",
                                     Directory.GetLastWriteTime(s),
                                     "C:/Users/Robert/Documents/dotnet/filebrowser/images/folder.ico", EntryType.Dir);

                                 subEntries2.Add(d);

                             }
                             foreach (string f in Directory.GetFiles(folder))
                             {
                                 FileInfo file = new FileInfo(f);
                                 DirectoryEntry d = new DirectoryEntry(
                                     Path.GetFileNameWithoutExtension(file.Name), file.FullName, file.Extension, file.Length.ToString(),
                                     file.LastWriteTime,
                                     "C:/Users/Robert/Documents/dotnet/filebrowser/images/file.ico", EntryType.File);
                                 subEntries2.Add(d);
                             }
                         }
                         catch
                         {
                             ;
                         }
                         MessageBox.Show(textbox1.Text + " könyvtár létrehozása sikeres.");
                         
                     }
                     else
                         return;
                 }
                 catch
                 {
                     MessageBox.Show("Könyvtár létrehozása nem sikerült");
                     return;
                 }
                 
             }

        
    }

    public enum EntryType
    {
        Dir,
        File
    }

    public class DirectoryEntry
    {
        private string _name;
        private string _fullpath;
        private string _lastpath;
        private string _ext;
        private string _size;
        private DateTime _date;
        private string _imagepath;
        private EntryType _type;

        public DirectoryEntry(string name,string fullname, string ext, string size, DateTime date, string imagepath, EntryType type)
        {
            _name = name;
            _fullpath = fullname;
            _ext = ext;
            _size = size;
            _date = date;
            _imagepath = imagepath;
            _type = type;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        public string Ext
        {
            get { return _ext; }
            set { _ext = value; }
        }

        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }
        
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        
        public string Imagepath
        {
            get { return _imagepath; }
            set { _imagepath = value; }
        }

        public EntryType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public string Fullpath
        {
            get { return _fullpath; }
            set { _fullpath = value; }
        }

        public string Lastpath
        {
            get { return _lastpath; }
            set { _lastpath = value; }
        }
    }
}
