namespace AdminPanel.Services
{
    public class RemoveImageFromWwwRootService : IRemoveImageFromFileSystemService
    {
        private readonly IWebHostEnvironment _env;

        public RemoveImageFromWwwRootService(IWebHostEnvironment env)
        {
            _env = env;
        }


        public void RemoveImage(string imageUrl)
        {
            string fullPath = Path.Combine(_env.WebRootPath, imageUrl);
            if (fullPath == imageUrl) fullPath = _env.WebRootPath + imageUrl;

            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
