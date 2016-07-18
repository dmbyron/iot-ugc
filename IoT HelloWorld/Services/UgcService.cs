using StudioUgc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;


namespace StudioUgc.Services
{
    public class UgcService
	{

        //attempt at allowing user to select network folder
        //public async Task<StorageFolder> GetFolder()
        //{
        //    var folderPicker = new Windows.Storage.Pickers.FolderPicker();
        //    folderPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;

        //    StorageFolder folder = await folderPicker.PickSingleFolderAsync();
        //    if (folder != null)
        //    {
        //        // Application now has read/write access to all contents in the picked folder
        //        // (including other sub-folder contents)
        //        Windows.Storage.AccessCache.StorageApplicationPermissions.
        //        FutureAccessList.AddOrReplace("PickedFolderToken", folder);
        //        //this.textBlock.Text = "Picked folder: " + folder.Name;
        //    }
        //    else
        //    {
        //        //this.textBlock.Text = "Operation cancelled.";
        //    }

        //    return folder;

        //}

       
            
            
        //Attempt printing names of external devices attached to remote media
        public async void getFolders()
        {
            StorageFolder externalDevices = KnownFolders.RemovableDevices;
            IReadOnlyList<StorageFolder> externalDrives = await externalDevices.GetFoldersAsync();

            foreach (StorageFolder s in externalDrives) {
                System.Diagnostics.Debug.WriteLine(s + ", ");
            }
        }
       
       

   


        public async Task<IList<IUgc>> GetUgc()
		{
            //Paths

            //path to Jared's original folder - not working
            //var path = "\\\\WIN81 -BZVECSJS\\StudioUgc";

            //path to locally synched OneDrive - not working
            //var path = "C:\\Users\\dillon.byron\\OneDrive - Avanade\\testLib";

            //path to generic local folder - working
            var path = (@"C:\Users\dillon.byron\Pictures\PresentationPhotos");

            //path to sharepoint lib - not working
            //var path = "\\\\avanade-my.sharepoint.com\\personal\\dillon_byron_avanade_com\\Documents\\testLib";

            //path to generic public network folder - unknown
            //var path = (@"\\WIN-XBE0K4YKHC\PresentationPhotosNet");

            //path to generic public network folder - unknown
            //var path = ("Z:\\");

            //path to generic public hosted folder - unknown
            //var path = ("Z:\\");


            //folder pointing attempts
            var folder = await StorageFolder.GetFolderFromPathAsync(path);
            //var folder = KnownFolders.PicturesLibrary;
            // var folder = StorageFolder.GetFolderFromPathAsync();


            var fileList = await folder.GetFilesAsync();
            //var fileList = await folder.GetFilesAsync();

			var ugc = fileList.Select(CreateUgc).Where(x => x != null).ToList();

            getFolders();

			return ugc;
		}



    private IUgc CreateUgc(StorageFile file)
		{
			switch (file.FileType.ToLower())
			{
				case ".jpg":
				case ".png":
				case ".bmp":
                case ".gif":
					return new Image(file);
				case ".mp4":
                case ".mov":
					return new Video(file);
			}
			return null;
		}

		//private Image CreatePhotoUgc(StorageFile file)
		//{
		//	return new Image(file);
		//}

		//private Video CreateVideoUgc(StorageFile file)
		//{
		//	return new Video(file);
		//}
	}
}
