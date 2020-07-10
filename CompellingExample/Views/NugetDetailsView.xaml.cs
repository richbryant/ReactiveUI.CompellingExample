using System.Reactive.Disposables;
using System.Windows.Media.Imaging;
using ReactiveUI;

namespace CompellingExample.Views
{
    public partial class NugetDetailsView
    {
        public NugetDetailsView()
        {
            InitializeComponent();
            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(ViewModel, 
                        viewModel => viewModel.IconUrl, 
                        view => view.IconImage.Source, 
                        url => url == null ? null : new BitmapImage(url))
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(ViewModel, 
                        viewModel => viewModel.Title, 
                        view => view.TitleRun.Text)
                    .DisposeWith(disposableRegistration);
                
                this.OneWayBind(ViewModel, 
                        viewModel => viewModel.Description, 
                        view => view.DescriptionRun.Text)
                    .DisposeWith(disposableRegistration);
            
                this.BindCommand(ViewModel, 
                        viewModel => viewModel.OpenPage, 
                        view => view.OpenButton)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}
