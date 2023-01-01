namespace Binding
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            _mWindow = new MainWindow();
            _mWindow.Activate();
        }

        private Window _mWindow;
    }
}
