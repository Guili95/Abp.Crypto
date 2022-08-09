using Microsoft.EntityFrameworkCore.DataEncryption;

namespace GuiLi.Abp.Crypto.EntityFrameworkCore.EncryptionFactory
{
    public interface IEncryptionFactory
    {
        IEncryptionProvider Create(string type);
    }
}
