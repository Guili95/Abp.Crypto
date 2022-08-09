using Volo.Abp.Modularity;

namespace GuiLi.Abp.Crypto.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpCryptoModule)
    )]
    public class AbpCryptoEntityFrameworkCoreModule : AbpModule
    {
    }
}
