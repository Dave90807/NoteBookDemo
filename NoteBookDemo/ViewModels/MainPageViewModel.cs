using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;

namespace NoteBookDemo.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged // Derive from required
    {
        public event PropertyChangedEventHandler PropertyChanged; // Define event handler

        // Implementation of ProperyChanged give two way updating of UI and data
        private Models.FileInfo _File; // create a field of type FileInfo
        public Models.FileInfo File // Call getter and setter for the field
        {
            get { return _File; }
            set
            {
                _File = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(File)));
            }
        }

        Services.FileService _FileService = new Services.FileService(); // news-up a FileService

        public async void Save() // method that uses the service created above
        {
            await _FileService.SaveAsync(File);
        }

        public async void Open()
        {
            // prompt a picker
            var picker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.List,
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            };
            picker.FileTypeFilter.Add(".txt");
            var file = await picker.PickSingleFileAsync();
            if (file == null)
                await new Windows.UI.Popups.MessageDialog("No file selected.").ShowAsync();
            else
                File = await _FileService.LoadAsync(file);
        }
    }
}
