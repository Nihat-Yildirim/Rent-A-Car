using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Helpers.GuidHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelperManager
{
    public class FileHelperManager : IFileHelper
    {
        public void Delete(string filePath)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public string Update(IFormFile file, string filePath, string root)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Upload(file, root);
        }

        public string Upload(IFormFile file, string root)
        {
            if(file.Length > 0)
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }

                string extension = Path.GetExtension(file.FileName); // uzantı .jpg .png
                string guid = GuidHelperr.CreateGuid(); // ismi
                string fileName = guid + extension; // dosyanın tam ismi (guid + uzantı)

                using(FileStream fileStream = File.Create(root + fileName))
                {
                    file.CopyTo(fileStream); // arabellekte tutulan resmi oluşturduğumuz yeni yola kopyalar.
                    fileStream.Flush();//arabellekten bu yol değerini siler
                    return fileName;
                }
            }
            return null;
        }
    }
}
