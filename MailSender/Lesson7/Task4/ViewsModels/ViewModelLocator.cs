using Microsoft.Extensions.DependencyInjection;

namespace Task4.ViewsModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Services
            .GetRequiredService<MainWindowViewModel>();
    }
}
