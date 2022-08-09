using GuiLi.Abp.Crypto.NationalStandard.SM4;
using Microsoft.EntityFrameworkCore.DataEncryption;
using System;
using System.IO;

namespace GuiLi.Abp.Crypto.EntityFrameworkCore.NationalStandard.SM4
{
    public class Sm4EncryptionProvider : IEncryptionProvider
    {
        private Sm4Crypto Sm4Crypto { get; set; }
        private Sm4Options Options { get; set; }
        public Sm4EncryptionProvider(
            Sm4Options options)
        {
            Options = options;
            Sm4Crypto = new Sm4Crypto(Options.Key, Options.Iv, Options.CryptoMode);
        }

        public TModel Decrypt<TStore, TModel>(TStore dataToDecrypt, Func<TStore, byte[]> decoder, Func<Stream, TModel> converter)
        {
            Sm4Crypto.Data = dataToDecrypt.ToString();
            var crypto = Sm4Crypto.Decrypt(Sm4Crypto, 2);
            using var memoryStream = new MemoryStream(System.Text.Encoding.Default.GetBytes(crypto.ToString()));
            return converter(memoryStream);
        }

        public TStore Encrypt<TStore, TModel>(TModel dataToEncrypt, Func<TModel, byte[]> converter, Func<Stream, TStore> encoder)
        {
            Sm4Crypto.Data = dataToEncrypt.ToString();
            var crypto = Sm4Crypto.Encrypt(Sm4Crypto);
            using var memoryStream = new MemoryStream(crypto);
            return encoder(memoryStream);
        }
    }
}
