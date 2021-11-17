using MyBot.Supporter.V2.Models;
using Newtonsoft.Json;
using OpenHardwareMonitor.Hardware;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
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
        private Brush DefaultBrush;

        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var newItem = e.Row.DataContext as BotSetting;
                if(!settings.Bots.Contains(newItem)){
                    settings.Bots.Add(newItem);
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DefaultBrush = (Brush)FindResource("MaterialDesignDarkBackground");
            me.CPUEnabled = true;
            me.RAMEnabled = true;
            if (File.Exists("conf.json"))
            {
                settings = JsonConvert.DeserializeObject<SupporterSettings>(File.ReadAllText("conf.json"));
            }
            if(settings.Bots == null)
            {
                settings.Bots = new BotSettings();
            }
            dataGrid.ItemsSource = settings.Bots;
            Worker = new Worker(settings);
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Worker != null && Worker.IsRunning)
            {
                Worker.Stop();
            }
            me.Close();
            File.WriteAllText("conf.json",JsonConvert.SerializeObject(settings));
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
            while(data > 1024)
            {
                size = sizes[loopCount];
                data = data / 1024;
                loopCount++;
                if(loopCount > sizes.Length - 1)
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
                else if (hardware.HardwareType == HardwareType.RAM)
                {
                    hardware.Update();
                    foreach (ISensor sensor in hardware.Sensors)

                        if (sensor.SensorType == SensorType.Load)
                        {
                            if (sensor.Name == SelectedRAML)
                            {
                                RAML = Convert.ToDouble(sensor.Value);
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
            CPULoad.Value = CPUL;
            CPUName.Text = CPUN;
            CPUTemp.Text = CPUT.ToString("N3") + " °C";
            CPUMaxTemp.Text = CPUTM.ToString("N3") + " °C";
            CPUFreq.Text = CPUF.ToString("N2") + " Ghz";
            RAMLoad.Value = RAML;
            CPUPower.Text = CPUV.ToString("N2") + " W";
            Time.Text = DateTime.Now.ToString("hh:mm:ss tt");
            NetR.Text = Calc(Receive);
            NetS.Text = Calc(Send);
            NetU.Text = Calc(USpeed) + "/s";
            NetD.Text = Calc(DSpeed) + "/s";
            for(int i = 0; i < settings.Bots.Count; i++)
            {
                DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(i);
                if(settings.Bots[i].Id != null && Worker.IsRunning)
                {
                    try
                    {
                        Process p = Process.GetProcessById(settings.Bots[i].Id.Value);
                        if(p != null)
                        {
                            row.Background = new SolidColorBrush(Color.FromRgb(0, 110, 0));
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    catch
                    {
                        row.Background = new SolidColorBrush(Color.FromRgb(110, 110, 0));
                    }
                }
                else
                {
                    row.Background = DefaultBrush;
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
                this.DragMove();
        }

        private void StartBot_Click(object sender, RoutedEventArgs e)
        {
            if(Worker == null)
            {
                Worker = new Worker(settings);
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
                Worker.Run();
            }
        }
    }
}
