namespace MultiShop.Extensions
{
    public static class FileExtension
    {
        public static bool IsValidType(this IFormFile file, string type)
            => file.ContentType.Contains(type);
        public static bool IsValidSize(this IFormFile file, int KByte)
            => file.Length <= 1024 * KByte;

        public static async Task<string> SaveFileAsync(this IFormFile formFile, string path)
        {
            string extension = Path.GetExtension(formFile.FileName);
            string newName = Path.GetRandomFileName();
            await using FileStream fileStream = new FileStream(Path.Combine(path, newName + extension), FileMode.Create);
            await formFile.CopyToAsync(fileStream);
            return newName + extension;
        } 
    }
}
