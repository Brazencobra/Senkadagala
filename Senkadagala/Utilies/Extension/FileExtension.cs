using Microsoft.AspNetCore.Routing.Constraints;

namespace Senkadagala.Utilies.Extension
{
    public static class FileExtension
    {
        public static string ChangeFileName(string oldname)
        {
            string extension = oldname.Substring(oldname.LastIndexOf("."));
            if (oldname.Length < 32)
            {
                oldname = oldname.Substring(0,oldname.LastIndexOf("."));
            }
            else
            {
                oldname = oldname.Substring(0,32);
            }
            return Guid.NewGuid() + oldname+ extension;
        }
        public static string SaveFile(this IFormFile file,string path)
        {
            string filename = ChangeFileName(file.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path,filename),FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filename;
        }
        public static void DeleteFile(this string filename,string root,string folder)
        {
            string path = Path.Combine(root,folder,filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
