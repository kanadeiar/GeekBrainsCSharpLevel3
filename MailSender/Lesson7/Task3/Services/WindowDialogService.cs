using System.Windows;
using Task3.Interfaces;

namespace Task3.Services
{
    class WindowDialogService : IDialogService
    {
        public void ShowInfo(string message) => MessageBox.Show(message);
    }
}