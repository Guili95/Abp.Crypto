using static GuiLi.Abp.Crypto.NationalStandard.SM4.Sm4Crypto;

namespace GuiLi.Abp.Crypto.NationalStandard.SM4
{
    public class Sm4Options
    {
        public string Key { get; set; }

        public string Iv { get; set; }
        public Sm4CryptoEnum CryptoMode { get; set; }
    }
}
