using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Seeker.Other
{
    public interface IFile
    {
        Task<bool> ExistsAsync(string filename);
        Task<string> LoadTextAsync(string filename);
        Task<IEnumerable<string>> GetFilesAsync();
    }
}
