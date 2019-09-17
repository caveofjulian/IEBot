using System.IO;
using System.Threading.Tasks;

namespace IEBot.Services
{
    internal interface ITokenService
    {
        Task<string> GetTokenAsync();
    }

    /// <summary>
    /// Implementation of <see cref="ITokenService"/> using a text file. Change the constant located in this class to change the location of the 
    /// </summary>
    internal class TokenFileService : ITokenService
    {
        private readonly string _tokenFilePath = "D:/workspace/token.txt";

        public TokenFileService(string tokenFilePath)
        {
            _tokenFilePath = tokenFilePath;
        }

        public async Task<string> GetTokenAsync()
        {
            if (!await (Task.FromResult(File.Exists(_tokenFilePath)))) return null;

            string line = null;

            using (FileStream fileStream = new FileStream(_tokenFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    while (streamReader.Peek() != -1)
                    {
                        line = await streamReader.ReadLineAsync();
                        break;
                    }
                }
            }

            return line;
        }
    }
}
