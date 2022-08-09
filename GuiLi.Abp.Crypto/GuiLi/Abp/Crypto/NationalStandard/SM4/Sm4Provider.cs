using Microsoft.Extensions.Options;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace GuiLi.Abp.Crypto.NationalStandard.SM4
{
    public class Sm4Provider : ISm4Provider, ITransientDependency
    {
        protected Sm4Options Options { get; }
        public Sm4Provider(
            IOptionsMonitor<Sm4Options> options)
        {
            Options = options.CurrentValue;
        }

        public string Encrypt(string dataToEncrypt, int needEnCode = 1)
        {
            var Sm4Crypto = CreateSm4();
            Sm4Crypto.Data = dataToEncrypt;
            var crypto = Sm4Crypto.Encrypt(Sm4Crypto);
            string str = "";
            if (needEnCode == 1)
            {
                str = Encoding.Default.GetString(Hex.Encode(crypto));
            }
            else if (needEnCode == 2)
            {
                str = Encoding.Default.GetString(Base64.Encode(crypto));
            }
            return str;
        }

        public string Decrypt(string dataToDecrypt, int needEnCode = 1)
        {
            var Sm4Crypto = CreateSm4();
            Sm4Crypto.Data = dataToDecrypt;
            var crypto = Sm4Crypto.Decrypt(Sm4Crypto, needEnCode);
            return crypto.ToString();
        }

        protected virtual Sm4Crypto CreateSm4()
        {
            return new Sm4Crypto(Options.Key, Options.Iv, Options.CryptoMode);
        }
    }
}
