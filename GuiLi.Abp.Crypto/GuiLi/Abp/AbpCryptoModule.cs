using GuiLi.Abp.Crypto.NationalStandard.SM4;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace GuiLi.Abp.Crypto
{
    public class AbpCryptoModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<Sm4Options>(configuration.GetSection("Sm4"));
        }
    }
}
