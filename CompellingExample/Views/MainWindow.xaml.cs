using System.Reactive.Disposables;
using CompellingExample.ViewModels;
using ReactiveUI;

namespace CompellingExample.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new AppViewModel();

            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(ViewModel, 
                        viewModel => viewModel.IsAvailable, 
                        view => view.SearchResultsListBox.Visibility)
                    .DisposeWith(disposableRegistration); 
                
                this.OneWayBind(ViewModel, 
                        viewModel => viewModel.SearchResults, 
                        view => view.SearchResultsListBox.ItemsSource)
                    .DisposeWith(disposableRegistration); 
                
                this.Bind(ViewModel, 
                        viewModel => viewModel.SearchTerm, 
                        view => view.SearchTextBox.Text)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}
