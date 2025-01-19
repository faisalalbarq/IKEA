using Microsoft.AspNetCore.Http;

namespace LinkDev.IKEA.BusinesLogicLayer.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly List<string> _allowedExtensions = new List<string>() { ".jpg", ".png", ".jpeg" };
        private const int _allowedMaxSize = 2_097_152; // 2 MegaByte
       
        /* Byte System
         * 1 kiloByte = 1024
         * 1 megaByte = 1024 * 1024
         * 1 gigaByte = 1024 * 1024 * 1024
         */

        public string? Upload(IFormFile file, string folderName)
        {
            var extension = Path.GetExtension(file.FileName);// when i send hamada.jpg, the GetExtension give me the .jpg
            // file.Name ==> extension Name 
            // file.FileName ==> The extension ex(.jpg)

            if (!_allowedExtensions.Contains(extension))
            {
                return null;
            }
            if(file.Length > _allowedMaxSize)
            {
                return null;
            }

            // var folderPath = $"{Directory.GetCurrentDirectory}\\wwwroot\\files\\{folderName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);


            // if folderPath is no exsist 
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }


            var fileName = $"{Guid.NewGuid()} {extension}";// must be unique, because i will store the fileName in the emp column in the database  

            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);// create empty file
            /// Different between the Create & CreateNew 
            /// Create ==> when i send the file that same name, will happen the written 
            /// CreateNew ==> when i send the file that same name, will happen the throw Exception
            /// in CreateNew i can't make change the picture, because the file name i same 
            
            /// Other way in fileStream
            /// using var fileStream = file.Create(filePath);
            
            /// i will using the using keyword for dispose the streaming
            /// try
            /// {
            /// 
            /// }
            /// finally
            /// {
            ///     fileStream.Dispose();
            /// }  


            file.CopyTo(fileStream);// uploading the file data per time, that's mean the file i will put in the stream because go to the filePath, why? to make the Create

            return fileName; // i will return the fileName becaue the filePath is repeated
        }
        public bool Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
