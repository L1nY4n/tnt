using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Sockets;
using System.Threading;


namespace MossbauerLab.TinyTcpServer.Core.Client

{
    //todo: umv: add dispose
    public class TcpClientContext: INotifyPropertyChanged
    {
        private bool _isSelected;
        private string _name;
        private string _description;
        private char _code ='a';
        private double _numeric;
        private string _food;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public char Code
        {
            get { return _code; }
            set
            {
                if (_code == value) return;
                _code = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value) return;
                _description = value;
                OnPropertyChanged();
            }
        }

        public double Numeric
        {
            get { return _numeric; }
            set
            {
                if (_numeric == value) return;
                _numeric = value;
                OnPropertyChanged();
            }
        }

        public string Food
        {
            get { return _food; }
            set
            {
                if (_food == value) return;
                _food = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));

        }
    
    public TcpClientContext(TcpClient client)
        {
            if(client == null)
                throw new ArgumentNullException("client");
            Id = Guid.NewGuid();
            Client = client;
            BytesRead = 0;
            ReadDataEvent = new ManualResetEventSlim(false, 100);
            WriteDataEvent = new ManualResetEventSlim(false, 100);
            IsProcessing = false;
            SynchObject = new Object();
            OnPropertyChanged();
        }

        public Guid Id { get; set; }
        public Object SynchObject { get; set; }
        public Boolean IsProcessing { get; set; }
        public Int32 BytesRead { get; set; }
        public TcpClient Client { get; private set; }
        public ManualResetEventSlim ReadDataEvent { get; set; }
        public ManualResetEventSlim WriteDataEvent { get; set; }
        public Boolean Inactive { get; set; }
        public DateTime InactiveTimeMark { get; set; }
    }
}
