using GuiLi.Abp.Crypto.EntityFrameworkCore.NationalStandard.SM4;
using GuiLi.Abp.Crypto.NationalStandard.SM4;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace GuiLi.Abp.Crypto.EntityFrameworkCore.EncryptionFactory
{
    public class EncryptionFactory : IEncryptionFactory, ITransientDependency
    {
        protected Sm4Options Options { get; }
        public EncryptionFactory(
            IOptionsMonitor<Sm4Options> options)
        {
            Options = options.CurrentValue;
        }
        public IEncryptionProvider Create(string type)
        {
            switch (type)
            {
                case "SM4":
                    return new Sm4EncryptionProvider(Options);
                default:
                    return new Sm4EncryptionProvider(Options);
            }

        }
    }
}
