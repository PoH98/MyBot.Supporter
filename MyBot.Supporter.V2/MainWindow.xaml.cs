using MaterialDesignThemes.Wpf;
using MyBot.Supporter.V2.Helper;
using MyBot.Supporter.V2.Models;
using MyBot.Supporter.V2.Service;
using Newtonsoft.Json;
using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MyBot.Supporter.V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SupporterSettings settings = new SupporterSettings();
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private readonly Computer me = new Computer();
        private string SelectedCPUV, SelectedCPUF, SelectedCPUT, SelectedRAML, SelectedCPUL, CPUN;
        private double CPUTM, CPUF, CPUV, CPUT, CPUL, RAML;
        private bool Network = false;
        private double Receive, Send, OldSend, OldReceive, USpeed, DSpeed;
        private NetworkInterface nics;
        private Worker Worker;

        private void mode_Unchecked(object sender, RoutedEventArgs e)
        {
            ToggleBaseColour(false);
        }

        private void mode_Checked(object sender, RoutedEventArgs e)
        {
            ToggleBaseColour(true);
        }

        private async void compile_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("Compiler.exe"))
            {
                MessageBox.Show("Start Downloading Required files","Compiling Started");
                AutoITDownloader downloader = new AutoITDownloader();
                await downloader.DownloadAutoIT();
            }
            ProcessStartInfo compiler = new ProcessStartInfo("Compiler.exe");
            compiler.Arguments = "/in \"" + Environment.CurrentDirectory + "\\MyBot.run.au3\" /out \"" + Environment.CurrentDirectory + "\\MyBot.run.exe\" /icon \"" + Environment.CurrentDirectory + "\\images\\MyBot.ico\"";
            Process com = Process.Start(compiler);
            while (!com.HasExited)
            {
                await Task.Delay(100);
            }
            MessageBox.Show("Compile Completed","Compile Result");
        }

        private readonly PaletteHelper _paletteHelper = new PaletteHelper();
        private readonly Dictionary<Emulator, string> downloadEmulator = new Dictionary<Emulator, string>
        {
            {
                Emulator.Bluestacks,
                "https://mega.nz/#!GFVilDAL!Wkyp2xpxFOx8J_Gz8wIf0jGSxTT3IiT6xthvrHhRbME"
            },
            {
                Emulator.Bluestacks2,
                "https://mega.nz/#!BpdEUBbZ!4unxWMPzA5rESONTVgNrxlNxSj8H2wwicx4Q15PmBo4"
            },
            {
                Emulator.MEmu,
                "https://mega.nz/file/hhI0RbRQ#ztf3bVkOwfBB7nMfmK_kihmW9py7PhyLSE-Zcy3Aq0o"
            },
            {
                Emulator.ITools,
                "https://mega.nz/file/Gc9QlI5R#FCrXIpi-27Ja9kqwkcyzbeu9HoMlxR2NGjb3KS0m6NM"
            },
            {
                Emulator.Nox,
                "https://mega.nz/file/fJsCnABZ#SGrKLMow7NPgC0y_Xl5nMXJUtWQZ8Z9FJ0zcJ0DIIBQ"
            }
        };

        private void UpdateBot_Click(object sender, RoutedEventArgs e)
        {
            if (Worker.IsRunning)
            {
                StartBot_Click(sender, e);
            }
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, ev) =>
            {
                var version = "v0.0";
                if (File.Exists("MyBot.run.version.au3"))
                {
                    var au3 = File.ReadAllText("MyBot.run.version.au3");
                    version = Regex.Match(au3, "\\$g_sBotVersion = \"(.+)\"").Groups[0].Value.Replace("$g_sBotVersion =", "").Replace("\"", "").Trim().Split(' ')[0];
                }
                var wc = new WebClient();
                wc.Headers.Add("User-Agent", "MyBot.Supporter.UpdateChecker");
                var api = JsonConvert.DeserializeObject<GithubAPI>(wc.DownloadString("https://api.github.com/repos/MyBotRun/MyBot/releases/latest"));
                var apiver = api.TagName.Replace("MBR_", "");
                var compare = string.Compare(version, apiver, true);
                if (compare < 0)
                {
                    MessageBox.Show("Start downloading latest version...");
                    wc.Headers.Add("User-Agent", "MyBot.Supporter.UpdateChecker");
                    wc.DownloadFile(api.Assets[0].BrowserDownloadUrl, api.Assets[0].Name);
                    ZipExtract ex = new ZipExtract();
                    ex.Extract(api.Assets[0].Name);
                    MessageBox.Show("Job done!");
                }
                else if (compare > 0)
                {
                    MessageBox.Show("You are using future version of MyBot!");
                }
                else
                {
                    MessageBox.Show("You are using latest version of MyBot!");
                }
                wc.Dispose();
            };
            var frame = new DispatcherFrame();
            worker.RunWorkerCompleted += (s2, args) =>
            {
                frame.Continue = false;
            };
            worker.RunWorkerAsync();
            Dispatcher.PushFrame(frame);
        }


        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var newItem = e.Row.DataContext as BotSetting;
                if (!settings.Bots.Contains(newItem))
                {
                    settings.Bots.Add(newItem);
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += (s, e) => LogUnhandledException((Exception)e.ExceptionObject);
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                LogUnhandledException(e.Exception);
                e.SetObserved();
            };
            me.CPUEnabled = true;
            me.RAMEnabled = true;
            if (File.Exists("conf.json"))
            {
                settings = JsonConvert.DeserializeObject<SupporterSettings>(File.ReadAllText("conf.json"));
            }
            if (settings.Bots == null)
            {
                settings.Bots = new BotSettings();
            }
            dataGrid.ItemsSource = settings.Bots;
            mini.DataContext = settings;
            mini.SetBinding(ToggleButton.IsCheckedProperty, new Binding(nameof(settings.Mini)));
            hideandroid.DataContext = settings;
            hideandroid.SetBinding(ToggleButton.IsCheckedProperty, new Binding(nameof(settings.HideAndroid)));
            dock.DataContext = settings;
            dock.SetBinding(ToggleButton.IsCheckedProperty, new Binding(nameof(settings.Dock)));
            mode.DataContext = settings;
            mode.SetBinding(ToggleButton.IsCheckedProperty, new Binding(nameof(settings.DarkMode)));
            if (settings.DarkMode)
            {
                ToggleBaseColour(true);
            }
            else
            {
                ToggleBaseColour(false);
            }
            Worker = new Worker();
            foreach (var e in downloadEmulator)
            {
                var btn = new Button
                {
                    Content = e.Key.ToString()
                };
                btn.Click += (o, s) =>
                {
                    Process.Start(e.Value);
                };
                downloadPanel.Children.Add(btn);
            }
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void LogUnhandledException(Exception exception)
        {
            File.WriteAllText("Supporter.error", exception.ToString());
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (Worker != null && Worker.IsRunning)
            {
                Worker.Stop();
            }
            me.Close();
            File.WriteAllText("conf.json", JsonConvert.SerializeObject(settings));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            me.Open();
            double highestV = 0;
            double highestT = 0;
            double highestC = 0;
            double highestL = 0;
            double highestLR = 0;
            foreach (var h in me.Hardware)
            {
                if (h.HardwareType == HardwareType.CPU)
                {
                    h.Update();
                    CPUN = h.Name;
                    foreach (var s in h.Sensors)
                    {
                        if (s.SensorType == SensorType.Power)
                        {
                            if (highestV < Convert.ToDouble(s.Value))
                            {
                                highestV = Convert.ToDouble(s.Value);
                                SelectedCPUV = s.Name;
                            }
                        }
                        else if (s.SensorType == SensorType.Clock)
                        {
                            if (highestC < Convert.ToDouble(s.Value))
                            {
                                highestC = Convert.ToDouble(s.Value);
                                SelectedCPUF = s.Name;
                            }
                        }
                        else if (s.SensorType == SensorType.Temperature)
                        {
                            if (highestT < Convert.ToDouble(s.Value))
                            {
                                highestT = Convert.ToDouble(s.Value);
                                SelectedCPUT = s.Name;
                            }
                        }
                        else if (s.SensorType == SensorType.Load)
                        {
                            if (highestL < Convert.ToDouble(s.Value))
                            {
                                highestL = Convert.ToDouble(s.Value);
                                SelectedCPUL = s.Name;
                            }
                        }
                    }
                }
                else if (h.HardwareType == HardwareType.RAM)
                {
                    h.Update();
                    foreach (var s in h.Sensors)
                    {
                        if (s.SensorType == SensorType.Load)
                        {
                            if (highestLR < Convert.ToDouble(s.Value))
                            {
                                highestLR = Convert.ToDouble(s.Value);
                                SelectedRAML = s.Name;
                            }
                        }
                    }
                }
            }
            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    if (nic.Name != "Loopback Pseudo-Interface 1")
                    {
                        Network = true;
                        Receive = nic.GetIPStatistics().BytesReceived; //Get Received nework data volume
                        Send = nic.GetIPStatistics().BytesSent; //Get Sended network data volume
                        NetName.Text = nic.Name;
                        nics = nic;
                        break;
                    }
                }
            }
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;
        }

        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            try
            {
                foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        if (nic.Name != "Loopback Pseudo-Interface 1")
                        {
                            Network = true;
                            Receive = nic.GetIPStatistics().BytesReceived; //Get Received nework data volume
                            Send = nic.GetIPStatistics().BytesSent; //Get Sended network data volume
                            NetName.Text = nic.Name;
                            nics = nic;
                            break;
                        }
                    }
                }
            }
            catch
            {
                Network = false;
            }
        }

        private string Calc(double data)
        {
            string[] sizes = new string[] { "KB", "MB", "GB", "TB", "PB" };
            string size = "B";
            int loopCount = 0;
            while (data > 1024)
            {
                size = sizes[loopCount];
                data /= 1024;
                loopCount++;
                if (loopCount > sizes.Length - 1)
                {
                    break;
                }
            }
            return data.ToString("N3") + " " + size;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var hardware in me.Hardware)
            {
                if (hardware.HardwareType == HardwareType.CPU)
                {
                    hardware.Update();
                    foreach (ISensor sensor in hardware.Sensors)
                    {
                        switch (sensor.SensorType)
                        {
                            case SensorType.Temperature:
                                if (sensor.Name == SelectedCPUT)
                                {
                                    CPUT = Convert.ToDouble(sensor.Value);
                                    if (CPUTM < CPUT)
                                    {
                                        CPUTM = CPUT;
                                    }
                                }
                                break;
                            case SensorType.Clock:
                                if (sensor.Name == SelectedCPUF)
                                {
                                    CPUF = Convert.ToDouble(sensor.Value / 1024);
                                }
                                break;
                            case SensorType.Power:
                                if (sensor.Name == SelectedCPUV)
                                {
                                    CPUV = Convert.ToDouble(sensor.Value);
                                }
                                break;
                            case SensorType.Load:
                                if (sensor.Name == SelectedCPUL)
                                {
                                    CPUL = Convert.ToDouble(sensor.Value);
                                }
                                break;
                        }
                    }
                }
                else if (hardware.HardwareType == HardwareType.RAM)
                {
                    hardware.Update();
                    foreach (ISensor sensor in hardware.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Load)
                        {
                            if (sensor.Name == SelectedRAML)
                            {
                                RAML = Convert.ToDouble(sensor.Value);
                            }
                        }
                    }
                }
            }
            if (Network)
            {
                Receive = nics.GetIPStatistics().BytesReceived;
                Send = nics.GetIPStatistics().BytesSent;
                USpeed = Send - OldSend;
                DSpeed = Receive - OldReceive;
                OldSend = Send;
                OldReceive = Receive;
            }
            CPULoad.Value = 100 - CPUL;
            CPUName.Text = CPUN;
            CPUTemp.Text = CPUT.ToString("N3") + " °C";
            CPUMaxTemp.Text = CPUTM.ToString("N3") + " °C";
            CPUFreq.Text = CPUF.ToString("N2") + " Ghz";
            RAMLoad.Value = 100 - RAML;
            CPUPower.Text = CPUV.ToString("N2") + " W";
            Time.Text = DateTime.Now.ToString("hh:mm:ss tt");
            NetR.Text = Calc(Receive);
            NetS.Text = Calc(Send);
            NetU.Text = Calc(USpeed) + "/s";
            NetD.Text = Calc(DSpeed) + "/s";
            for (int i = 0; i < settings.Bots.Count; i++)
            {
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                //unable to fetch row
                if (row == null)
                {
                    break;
                }
                if (settings.Bots[i].Id != null && Worker.IsRunning)
                {
                    try
                    {
                        Process p = Process.GetProcessById(settings.Bots[i].Id.Value);
                        if (p != null)
                        {
                            if (settings.DarkMode)
                            {
                                row.Background = new SolidColorBrush(Color.FromRgb(0, 110, 0));
                            }
                            else
                            {
                                row.Background = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        if (settings.DarkMode)
                        {
                            row.Background = new SolidColorBrush(Color.FromRgb(110, 110, 0));
                        }
                        else
                        {
                            row.Background = new SolidColorBrush(Color.FromRgb(225, 255, 0));
                        }
                    }
                }
                else
                {
                    row.Background = Brushes.Transparent;
                }
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void StartBot_Click(object sender, RoutedEventArgs e)
        {
            if (Worker == null)
            {
                Worker = new Worker();
            }
            if (!File.Exists("MyBot.run.exe") && !File.Exists("MyBot.run.au3"))
            {
                MessageBox.Show("MyBot not found! Please press the Update MyBot for auto download latest version!");
                return;
            }
            if (Worker.IsRunning)
            {
                StartBot.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
                StartBot.Content = "Start Botting";
                Worker.Stop();
            }
            else
            {
                StartBot.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                StartBot.Content = "Stop Botting";
                Worker.Run(settings);
            }
        }

        private void ToggleBaseColour(bool isDark)
        {
            ITheme theme = _paletteHelper.GetTheme();
            IBaseTheme baseTheme = isDark ? new MaterialDesignDarkTheme() : (IBaseTheme)new MaterialDesignLightTheme();
            theme.SetBaseTheme(baseTheme);
            _paletteHelper.SetTheme(theme);
        }
    }
}
