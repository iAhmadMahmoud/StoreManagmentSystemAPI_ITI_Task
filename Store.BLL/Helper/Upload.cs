using Microsoft.AspNetCore.Http;

namespace Store.BLL
{
    public static class Upload
    {
        public static string UploadFile(string FolderName, IFormFile File)
        {
            try
            {
                // 1 ) catch the folder path and the file name in the server
                string FolderPath = Directory.GetCurrentDirectory() + "/wwwroot/" + FolderName;

                // 2 ) Get file name 
                string FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);

                // 3 ) Merge path with file name 
                string FinalPath = Path.Combine(FolderPath, FileName);

                // 4 ) Save file as streams "Data Overtime"
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    File.CopyTo(Stream);
                }
                return FileName;
            }
            catch (Exception ex)
            {
                {
                    return ex.Message;
                }
            }
        }

        public static string RemoveFile(string FolderName, string FileName)
        {
            try
            {
                var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FolderName, FileName);
                if (File.Exists(directory))
                {
                    File.Delete(directory);
                    return "File Deleted";
                }
                return "File Not Deleted";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
