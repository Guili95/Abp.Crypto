using Org.BouncyCastle.Utilities.Encoders;
using System.ComponentModel;
using System.Text;

namespace GuiLi.Abp.Crypto.NationalStandard.SM4
{
    /// <summary>
    /// Sm4算法  
    /// 对标国际DES算法
    /// </summary>
    public class Sm4Crypto
    {
        public Sm4Crypto(string key, string iv, Sm4CryptoEnum cryptoMode = Sm4CryptoEnum.ECB)
        {
            Key = key;
            Iv = iv;
            HexString = false;
            CryptoMode = cryptoMode;
        }
        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 向量
        /// </summary>
        public string Iv { get; set; }
        /// <summary>
        /// 明文是否是十六进制
        /// </summary>
        public bool HexString { get; set; }
        /// <summary>
        /// 加密模式(默认ECB)
        /// </summary>
        public Sm4CryptoEnum CryptoMode { get; set; }

        #region 加密
        public byte[] Encrypt(Sm4Crypto entity)
        {
            return entity.CryptoMode == Sm4CryptoEnum.CBC ? EncryptCBC(entity) : EncryptECB(entity);
        }
        /// <summary>
        /// ECB加密
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static byte[] EncryptECB(Sm4Crypto entity)
        {
            Sm4Context ctx = new Sm4Context();
            ctx.isPadding = true;
            ctx.mode = SM4.SM4_ENCRYPT;
            byte[] keyBytes;
            if (entity.HexString)
            {
                keyBytes = Hex.Decode(entity.Key);
            }
            else
            {
                keyBytes = Encoding.Default.GetBytes(entity.Key);
            }
            SM4 sm4 = new SM4();
            sm4.sm4_setkey_enc(ctx, keyBytes);

            byte[] contentBytes = Encoding.Default.GetBytes(entity.Data);

            byte[] encrypted = sm4.sm4_crypt_ecb(ctx, contentBytes);

            return encrypted;
        }
        /// <summary>
        /// CBC加密
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static byte[] EncryptCBC(Sm4Crypto entity)
        {
            Sm4Context ctx = new Sm4Context();
            ctx.isPadding = true;
            ctx.mode = SM4.SM4_ENCRYPT;
            byte[] keyBytes;
            byte[] ivBytes;
            if (entity.HexString)
            {
                keyBytes = Hex.Decode(entity.Key);
                ivBytes = Hex.Decode(entity.Iv);
            }
            else
            {
                keyBytes = Encoding.Default.GetBytes(entity.Key);
                ivBytes = Encoding.Default.GetBytes(entity.Iv);
            }
            SM4 sm4 = new SM4();
            sm4.sm4_setkey_enc(ctx, keyBytes);
            byte[] encrypted = sm4.sm4_crypt_cbc(ctx, ivBytes, Encoding.Default.GetBytes(entity.Data));

            return encrypted;
        }
        #endregion


        #region 解密
        public string Decrypt(Sm4Crypto entity, int needEnCode = 1)
        {
            return entity.CryptoMode == Sm4CryptoEnum.CBC ? DecryptCBC(entity, needEnCode) : DecryptECB(entity, needEnCode);
        }
        /// <summary>
        ///  ECB解密
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="needEnCode">1、哈希加密；2、base64加密</param>
        /// <returns></returns>
        public static string DecryptECB(Sm4Crypto entity, int needEnCode = 1)
        {
            Sm4Context ctx = new Sm4Context();
            ctx.isPadding = true;
            ctx.mode = SM4.SM4_DECRYPT;

            byte[] keyBytes;
            if (entity.HexString)
            {
                keyBytes = Hex.Decode(entity.Key);
            }
            else
            {
                keyBytes = Encoding.Default.GetBytes(entity.Key);
            }

            SM4 sm4 = new SM4();
            sm4.sm4_setkey_dec(ctx, keyBytes);

            byte[] decrypted;
            if (needEnCode == 1)
            {
                decrypted = sm4.sm4_crypt_ecb(ctx, Hex.Decode(entity.Data));
            }
            else
            {
                decrypted = sm4.sm4_crypt_ecb(ctx, Base64.Decode(entity.Data));
            }
           
            return Encoding.Default.GetString(decrypted);
        }
        /// <summary>
        /// CBC解密
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="needEnCode">1、哈希加密；2、base64加密</param>
        /// <returns></returns>
        public static string DecryptCBC(Sm4Crypto entity, int needEnCode = 1)
        {
            Sm4Context ctx = new Sm4Context();
            ctx.isPadding = true;
            ctx.mode = SM4.SM4_DECRYPT;
            byte[] keyBytes;
            byte[] ivBytes;
            if (entity.HexString)
            {
                keyBytes = Hex.Decode(entity.Key);
                ivBytes = Hex.Decode(entity.Iv);
            }
            else
            {
                keyBytes = Encoding.Default.GetBytes(entity.Key);
                ivBytes = Encoding.Default.GetBytes(entity.Iv);
            }
            SM4 sm4 = new SM4();
            sm4.sm4_setkey_dec(ctx, keyBytes);

            byte[] decrypted;
            if (needEnCode == 1)
            {
                decrypted = sm4.sm4_crypt_cbc(ctx, ivBytes, Hex.Decode(entity.Data));
            }
            else
            {
                decrypted = sm4.sm4_crypt_cbc(ctx, ivBytes, Base64.Decode(entity.Data));
            }

            return Encoding.Default.GetString(decrypted);
        }
        #endregion

        /// <summary>
        /// 加密类型
        /// </summary>
        public enum Sm4CryptoEnum
        {
            /// <summary>
            /// ECB(电码本模式)
            /// </summary>
            [Description("ECB模式")]
            ECB = 0,
            /// <summary>
            /// CBC(密码分组链接模式)
            /// </summary>
            [Description("CBC模式")]
            CBC = 1
        }
    }
}
