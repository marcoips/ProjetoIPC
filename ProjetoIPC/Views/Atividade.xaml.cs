using System.Windows.Input;

namespace ProjetoIPC.Views
{
    public partial class Atividade : ContentPage
    {
        public ICommand DriveTappedCommand { get; }

        private Frame _lastTappedFrame; // Track the last tapped frame

        public Atividade()
        {
            InitializeComponent();

            // Define the command logic
            DriveTappedCommand = new Command<Frame>(OnDriveTapped);

            // Bind the command to the page's BindingContext
            BindingContext = this;
        }

        private void OnDriveTapped(Frame tappedFrame)
        {

            // If the tapped frame is already expanded, reset it
            if (_lastTappedFrame == tappedFrame && tappedFrame.Scale > 1)
            {
                tappedFrame.Scale = 1; // Reset to default size
                _lastTappedFrame = null; // Clear the last tapped frame
                return;
            }

            // Reset all frames to default size
            Drive1Frame.Scale = 1;
            Drive2Frame.Scale = 1;
            Drive3Frame.Scale = 1;

            // Scale the tapped frame
            tappedFrame.Scale = 1.2; // Increase size by 20%
            _lastTappedFrame = tappedFrame; // Track the tapped frame
        }
    }
}