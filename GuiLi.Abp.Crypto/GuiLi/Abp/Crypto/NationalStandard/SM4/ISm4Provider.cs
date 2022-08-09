namespace GuiLi.Abp.Crypto.NationalStandard.SM4
{
    public interface ISm4Provider
    {
        string Decrypt(string dataToDecrypt, int needEnCode = 1);
        string Encrypt(string dataToEncrypt, int needEnCode = 1);
    }
}
