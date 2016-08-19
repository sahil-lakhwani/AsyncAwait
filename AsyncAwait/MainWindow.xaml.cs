using MSNetwork4Demo2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace AsyncAwait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ManualResetEvent evento = new ManualResetEvent(false);
        private PauseTokenSource m_pauseTokeSource;
        private Action _cancelwork;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.m_pauseTokeSource = new PauseTokenSource();
            this.start.IsEnabled = false;
            try
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                this._cancelwork = (Action)(() =>
                {
                    this.cancel.IsEnabled = false;
                    this.start.IsEnabled = true;
                    this.pause.IsEnabled = false;
                    this.myBar.Value = 0.0;
                    this.myLabel.Content = (object)0;

                    
                    cancellationTokenSource.Cancel();
                });
                this.myBar.Minimum = 0.0;
                this.myBar.Maximum = 100;
                Progress<int> progressReport = new Progress<int>((Action<int>)(i =>
                {
                    this.myBar.Value = (double)i;
                    this.myLabel.Content = (object)i;
                }));
                CancellationToken token = cancellationTokenSource.Token;
                this.cancel.IsEnabled = true;
                this.pause.IsEnabled = true;
                await Task.Run(() => this.count(token,progressReport, this.m_pauseTokeSource.Token), token);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.ToString());
            }
            this.start.IsEnabled = true;
            this.pause.IsEnabled = false;
            this.cancel.IsEnabled = false;
            this._cancelwork = (Action)null;
        }

        public async Task count(CancellationToken token,Progress<int> progressHandler, PauseToken pauseToken)
        {
            IProgress<int> progress = (IProgress<int>)progressHandler;
           

          
            List<int> li3 = Enumerable.Range(0,101).ToList();
            try
            {
                for (int i = 0; i < li3.Count; i++)
                {
                    Thread.Sleep(200);
                    await pauseToken.WaitWhilePausedAsync();
                    if (progress != null)
                        progress.Report(i);
                    if (token.IsCancellationRequested)
                    {
                        this.m_pauseTokeSource.IsPaused = !this.m_pauseTokeSource.IsPaused;
                       
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
           


        }

        private  void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (this._cancelwork == null)
                return;
            this._cancelwork();

            //Try adding the async keyword ToleranceType this method
            //You'll have a warning that because there is no await, the method will run just synchronously
        }

        private  void Button_Click_3(object sender, RoutedEventArgs e)
         {
             
            this.m_pauseTokeSource.IsPaused = !this.m_pauseTokeSource.IsPaused;
            if (this.pause.Content as string == "Pause")
                this.pause.Content = (object)"Resume";
            else
                this.pause.Content = (object)"Pause";

            return;
        }

        private  void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.pause.Content = (object)"Pause";
            this.cancel.IsEnabled = false;
            this.pause.IsEnabled = false;
        }
    }
}
