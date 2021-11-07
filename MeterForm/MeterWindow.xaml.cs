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
using System.Windows.Shapes;
using System.Threading;
using MeterForm.MeterTests.MeterServer;
using MeterForm.MeterTests.HES;
using System.Net.Sockets;

namespace MeterForm
{
    /// <summary>
    /// Логика взаимодействия для MeterWindow.xaml
    /// </summary>
    public partial class MeterWindow : Window
    {
        //Поток для запуска tcp сервера прибора учета
        Thread _tMeterTcpServer = null;
        static List<System.Threading.Thread> _tHESRequests = new List<Thread>();

        static AsynchronousSocketListener _startMeterServer = new AsynchronousSocketListener();
        static AsynchronousClient _startHesRequest = new AsynchronousClient();

        static bool bReady = false; //признак успешной инициализации
        static System.Windows.Threading.Dispatcher dispObj; //диспетчер UI-потока, через него обращения к элементам UI из других потоков
        static TextBox txtConsole, txtCommand; //окна сообщений и комманд
        static TextBox _txtActivePower, _txtReactivePower, _txtApparentPower, _txtDateTime;
        static Label _txtMeterServerState;

        static int _tcpipPort = 11000;
        public MeterWindow()
        {
            InitializeComponent();

            txtConsole = txtData;
            _txtActivePower = txtActivePower;
            _txtReactivePower = txtReactivePower;
            _txtApparentPower = txtApparentPower;
            _txtDateTime = txtDateTime;
            _txtMeterServerState = lblMeterServerState;

            dispObj = Dispatcher;
            //программа успешно стартовала
            bReady = true;
            this.Closing += OnBeforeClosing; //обработчик для остановки потоков
        }

        public static void ThreadProc()
        {
            //AsynchronousSocketListener _startMeterServer = new AsynchronousSocketListener();
            _startMeterServer.MeterServerPort = _tcpipPort;
            _startMeterServer.StartListening();
        }
        private void btnStartMeterServer_Click(object sender, RoutedEventArgs e)
        {
            //int _tcpipPort = 11000;
            bool bResult = int.TryParse(txtMeterServerPort.Text, out _tcpipPort);
            if (!bResult)
            {
                MessageBox.Show("Wrong TCP/IP port. Please check it.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_tMeterTcpServer == null || !_tMeterTcpServer.IsAlive)
            {
                // The constructor for the Thread class requires a ThreadStart
                // delegate that represents the method to be executed on the
                // thread.  C# simplifies the creation of this delegate.
                _tMeterTcpServer = new Thread(new ThreadStart(ThreadProc));

                // Start ThreadProc.  Note that on a uniprocessor, the new
                // thread does not get any processor time until the main thread
                // is preempted or yields.  Uncomment the Thread.Sleep that
                // follows t.Start() to see the difference.
                _tMeterTcpServer.Start();

                lblMeterServerState.Content = _startMeterServer.MeterSereverState;// "Meter TCP Server started.";
            }
            else
            {
                lblMeterServerState.Content = "Meter TCP Server has already started.";
            }
        }

        void OnBeforeClosing(object sender, EventArgs e)
        {
            bReady = false;
            //остановка потоков со скриптами
            //foreach (var thr in lstThr)
            //{
            //    if (thr.IsAlive) thr.Abort();
            //}

            //остановка потока tcp сервера прибора учета
            if (_tMeterTcpServer == null)
                return;

            if (_tMeterTcpServer.IsAlive)
            {
                //_startMeterServer.listener.Shutdown(SocketShutdown.Both);//.StopServer();
                //_startMeterServer.listener.Close();
                _tMeterTcpServer.Abort();
                //_tMeterTcpServer.Join();
                lblMeterServerState.Content = "Meter TCP Server stopped.";
            }
            //остановка потоков запросов верхнего уровня к прибору учета
            foreach (var _tHESRequest in _tHESRequests)
            {
                if (_tHESRequest.IsAlive) _tHESRequest.Abort();
            }
        }

        public static void ThreadHESRequestProc()
        {
            //AsynchronousClient _startHesRequest = new AsynchronousClient();
            _startHesRequest.StartClient();

        }
        private void btnHesRequest_Click(object sender, RoutedEventArgs e)
        {
            Thread _tHESRequest = null;

            // The constructor for the Thread class requires a ThreadStart
            // delegate that represents the method to be executed on the
            // thread.  C# simplifies the creation of this delegate.
            _tHESRequest = new Thread(new ThreadStart(ThreadHESRequestProc));

            // Start ThreadProc.  Note that on a uniprocessor, the new
            // thread does not get any processor time until the main thread
            // is preempted or yields.  Uncomment the Thread.Sleep that
            // follows t.Start() to see the difference.
            _tHESRequest.Start();

            _tHESRequests.Add(_tHESRequest);
            lblHesClientState.Content = "HES request started.";
        }

        //запускаем циклические процессы в отдельном потоке
        //чтобы обновлять интерфейс пользователя, используем запуск кода в UI потоке как орисано по ссылке ниже
        //https://ru.stackoverflow.com/questions/615113/%D0%9F%D0%BE%D1%87%D0%B5%D0%BC%D1%83-thread-sleep-%D0%B2%D0%B5%D0%B4%D1%91%D1%82-%D1%81%D0%B5%D0%B1%D1%8F-%D0%BD%D0%B5%D0%BF%D1%80%D0%B0%D0%B2%D0%B8%D0%BB%D1%8C%D0%BD%D0%BE-%D0%9A%D0%B0%D0%BA-%D0%BC%D0%BD%D0%B5-%D1%81%D0%B4%D0%B5%D0%BB%D0%B0%D1%82%D1%8C-%D0%B7%D0%B0%D0%B4%D0%B5%D1%80%D0%B6%D0%BA%D1%83-%D0%B8%D0%BB%D0%B8-%D0%B4%D0%BB%D0%B8%D0%BD%D0%BD%D1%8B%D0%B5
        /// <summary>
        /// добавить текст в окно сообщений
        /// </summary>
        /// <param name="s">текст</param>
        public static void Console(string s, bool bNewLine = true)
        {
            //txtConsole.Text += (s + "\r\n");
            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bReady) txtConsole.Text += (s + (bNewLine ? "\r\n" : ""));
            });
        }
        public static void SetActivePower(string s, bool bNewLine = true)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bReady) _txtActivePower.Text += s;// (s + (bNewLine ? "\r\n" : ""));
            });
        }
        public static void SetReactivePower(string s, bool bNewLine = true)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bReady) _txtReactivePower.Text += s;// (s + (bNewLine ? "\r\n" : ""));
            });
        }
        public static void SetApparentPower(string s, bool bNewLine = true)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bReady) _txtApparentPower.Text += s;// (s + (bNewLine ? "\r\n" : ""));
            });
        }
        public static void SetDateTime(string s, bool bNewLine = true)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bReady) _txtDateTime.Text += s;// (s + (bNewLine ? "\r\n" : ""));
            });
        }
        public static void SetMeterState(string s, bool bNewLine = true)
        {
            if (!bReady || dispObj.HasShutdownStarted) return;
            //мы запускаем код в UI потоке
            dispObj.Invoke(delegate
            {
                if (bReady) _txtMeterServerState.Content = s;// (s + (bNewLine ? "\r\n" : ""));
            });
        }
    }
}
