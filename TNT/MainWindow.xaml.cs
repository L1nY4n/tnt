using MossbauerLab.TinyTcpServer.Core.Server;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System;
using MossbauerLab.TinyTcpServer.Core.Client;
using MossbauerLab.TinyTcpServer.Core.Handlers;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TNT
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
       

        private void updateUI(byte[] data,TcpClientContext cli)
        {
            IPEndPoint remote= ((IPEndPoint)cli.Client.Client.RemoteEndPoint);
            string ip = remote.Address.ToString();
            string port = remote.Port.ToString();
            string str = ToHexString(data);
            Action del =()=>
            {
                this.Items3.Add(

                 new SelectableViewModel
                 {
                     Code = 'R',
                     Name = $"{ip}:{port}",
                     Description = str,
                     Food = "apple"
                 }

             );
                this.Items1.Add(cli);
            
            };

            Dispatcher.Invoke(del);
        }

        public static string ToHexString(byte[] bytes) // 0xae00cf => "AE00CF "

        {
            string hexString = string.Empty;

            if (bytes != null)

            {

                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)

                {

                    strB.Append(bytes[i].ToString("X2"));

                }

                hexString = strB.ToString();

            }
            return hexString;

        }

        private ObservableCollection<TcpClientContext> deviceList =new ObservableCollection<TcpClientContext> { };
      
        private  ObservableCollection<SelectableViewModel> _items3;
        private bool? _isAllItems3Selected;

       

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            _items3 = CreateData();
            TcpServer  tc = new TcpServer();
            tc.test += updateUI;
            tc.Start("0.0.0.0",6000);
            tc.AddConnectionHandler(Guid.NewGuid(),new Action<TcpClientContext, Boolean>(handleConnet));
            //tc.AddHandler(null,new Func<Byte[], TcpClientHandlerInfo, Byte[]>(Handle));
            while (tc.ConnectedClients>0)
            {
                
            }
            OnPropertyChanged();
        }

      

        public  void handleConnet(TcpClientContext c, Boolean b)
        {


        }
        public Byte[] Handle(Byte[] b, TcpClientHandlerInfo hf) {

            return new Byte[0];

        }

        public bool? IsAllItems3Selected
        {
            get { return _isAllItems3Selected; }
            set
            {
                if (_isAllItems3Selected == value) return;

                _isAllItems3Selected = value;

                if (_isAllItems3Selected.HasValue)
                    SelectAll(_isAllItems3Selected.Value, Items3);

                OnPropertyChanged();
            }
        }

        private static void SelectAll(bool select, IEnumerable<SelectableViewModel> models)
        {
            foreach (var model in models)
            {
                model.IsSelected = select;
            }
        }

        private static ObservableCollection<SelectableViewModel> CreateData()
        {
            return new ObservableCollection<SelectableViewModel> { };
           
        }

        public ObservableCollection<TcpClientContext> Items1 => deviceList;

        public ObservableCollection<SelectableViewModel> Items3 {
            get { return _items3;
            }
            set {
                _items3 = value;
                OnPropertyChanged();
            }
           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
           
        }

        public IEnumerable<string> Foods
        {
            get
            {
                yield return "Burger";
                yield return "Fries";
                yield return "Shake";
                yield return "Lettuce";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Items3.Add ( 
               
                new SelectableViewModel
                {
                    Code = 'F',
                    Name = "Dragablz",
                    Description = "Dragablz Tab Control",
                    Food = "Fries"
                }
           
            );
        }
    }
}
