using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBookDemo.Services
{
    public class FileService
    {
        public async Task SaveAsync(Models.FileInfo model)
        {
            if (model != null)
                await Windows.Storage.FileIO.WriteTextAsync(model.Ref, model.Text);
        }

        public async Task<Models.FileInfo> LoadAsync(Windows.Storage.StorageFile file)
        {
            return new Models.FileInfo
            {
                Text = await Windows.Storage.FileIO.ReadTextAsync(file),
                Name = file.DisplayName,
                Ref = file,
            };
        }
    }
}
