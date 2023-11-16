using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace HundeKennel.Services.Helpers
{
    public interface IFilePickerService
    {
        Task<string> PickAFileAsync();
        Task<string> PickAFileSync();

    }
    public class FilePickerService : IFilePickerService
    {
        public async Task<string> PickAFileAsync()
        {
        
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Select a file",
                    Filter = "All files (*.*)|*.*" // You can specify specific file types here
                };

                bool? result = openFileDialog.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    return openFileDialog.FileName;
                }
                else
                {
                    return "Operation cancelled.";
                }

        }

        public Task<string> PickAFileSync()
        {
            throw new NotImplementedException();
        }
    }
}
