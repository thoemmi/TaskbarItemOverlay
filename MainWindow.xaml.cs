using System.ComponentModel;
using System.Windows;
using System.Threading;

namespace WpfApplication1 {
    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            var viewModel = new ViewModel();
            DataContext = viewModel;

            Loaded += delegate {
                var thread = new Thread(() => {
                    while (true) {
                        Thread.Sleep(1000);
                        viewModel.Count++;
                    };
                }) {
                    IsBackground = true
                };
                thread.Start();
            };
        }
    }

    public class ViewModel : INotifyPropertyChanged {
        private int _count;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Count {
            get { return _count; }
            set {
                if (_count != value) {
                    _count = value;
                    var handler = PropertyChanged;
                    if (handler != null) {
                        handler(this, new PropertyChangedEventArgs("Count"));
                    }
                }
            }
        }
    }
}