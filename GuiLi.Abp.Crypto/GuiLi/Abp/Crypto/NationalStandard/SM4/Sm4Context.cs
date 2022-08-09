namespace GuiLi.Abp.Crypto.NationalStandard.SM4
{
    public class Sm4Context
    {
        public Sm4Context()
        {
            mode = 1;
            isPadding = true;
            sk = new long[32];
        }
        /// <summary>
        /// 1表示加密，0表示解密
        /// </summary>
        public int mode;
        /// <summary>
        /// 密钥
        /// </summary>
        public long[] sk;
        /// <summary>
        /// 是否补足16进制字符串
        /// </summary>
        public bool isPadding;
    }
}
