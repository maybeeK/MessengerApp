using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp.Client.Sevices.Interfaces
{
    public interface IEncryptionService
    {
        Task<string> EncryptAsync(string clearText, string passphrase);
        Task<string> DecryptAsync(string encrypted, string passphrase);
    }
}
