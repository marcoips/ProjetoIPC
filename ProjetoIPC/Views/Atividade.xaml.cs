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
            // Hide all dropdowns
            Drive1Dropdown.IsVisible = false;
            Drive2Dropdown.IsVisible = false;
            Drive3Dropdown.IsVisible = false;

            // If the tapped frame is already expanded, collapse it
            if (_lastTappedFrame == tappedFrame)
            {
                _lastTappedFrame = null; // Clear the last tapped frame
                return;
            }

            // Show the dropdown for the tapped frame
            if (tappedFrame == Drive1Frame)
                Drive1Dropdown.IsVisible = true;
            else if (tappedFrame == Drive2Frame)
                Drive2Dropdown.IsVisible = true;
            else if (tappedFrame == Drive3Frame)
                Drive3Dropdown.IsVisible = true;

            _lastTappedFrame = tappedFrame; // Track the tapped frame
        }
    }
}