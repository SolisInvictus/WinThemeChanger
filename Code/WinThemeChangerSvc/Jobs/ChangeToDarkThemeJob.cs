using Microsoft.Win32;
using Quartz;
using System;
using System.Threading.Tasks;
using WinThemeChangerLib;

namespace WinThemeChangerSvc.Jobs
{
    class ChangeToDarkThemeJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Change();

            return Task.CompletedTask;
        }

        private void Change()
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(Consts.PERSONALIZE_REGISTRY_KEY);
                if (registryKey == null)
                    throw new Exception();

                Console.WriteLine(registryKey.GetValue(Consts.SYSTEM_SIDE_LIGHT_THEME_KEY));
                if (Settings.GetInstance().LightChangeWindowMode)
                    registryKey.SetValue(Consts.SYSTEM_SIDE_LIGHT_THEME_KEY, 0);
                if (Settings.GetInstance().LightChangeApplicationMode)
                    registryKey.SetValue(Consts.APPLICATION_SIZE_LIGHT_THEME_KEY, 0);
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine(e.Message);
#endif
            }
        }
    }
}
