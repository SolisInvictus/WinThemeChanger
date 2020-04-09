using Quartz;
using Quartz.Impl;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using WinThemeChangerLib;
using WinThemeChangerSvc.Comparers;
using WinThemeChangerSvc.Jobs;
using WinThemeChangerSvc.Types;

namespace WinThemeChangerSvc
{
    class JobScheduler
    {
        private IScheduler scheduler;

        public async void Schedule()
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            scheduler = await factory.GetScheduler();
            await scheduler.Start();

            PrepareTimes();
        }

        private async void PrepareTimes()
        {
            DateTime lightTime = DateTime.ParseExact(
                Settings.GetInstance().LightScheduledTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime darkTime = DateTime.ParseExact(
                Settings.GetInstance().DarkScheduledTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime now = DateTime.ParseExact(DateTime.Now.ToString("HH:mm"), "HH:mm", CultureInfo.InvariantCulture);
            now = now.AddSeconds(1d);

            SortTimesAndVerify(lightTime, darkTime, now);

            IJobDetail lightThemeJob = JobBuilder.Create<ChangeToLightThemeJob>()
                .Build();
            ITrigger lightThemeTrigger = TriggerBuilder.Create()
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(lightTime.Hour, lightTime.Minute))
                .Build();

            IJobDetail darkThemeJob = JobBuilder.Create<ChangeToDarkThemeJob>()
                .Build();
            ITrigger darkThemeTrigger = TriggerBuilder.Create()
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(darkTime.Hour, darkTime.Minute))
                .Build();

            await scheduler.ScheduleJob(lightThemeJob, lightThemeTrigger);
            await scheduler.ScheduleJob(darkThemeJob, darkThemeTrigger);
        }

        private void SortTimesAndVerify(DateTime lightTime, DateTime darkTime, DateTime nowTime)
        {
            ThemeTime light = new ThemeTime(Consts.LIGHT_THEME_KEY_VALUE, lightTime.ToString("HH:mm:ss"));
            ThemeTime dark = new ThemeTime(Consts.DARK_THEME_KEY_VALUE, darkTime.ToString("HH:mm:ss"));
            ThemeTime now = new ThemeTime("now", nowTime.ToString("HH:mm:ss"));

            ArrayList times = new ArrayList(3);
            times.Add(light);
            times.Add(dark);
            times.Add(now);

            times.Sort(new ThemeTimeComparer());

            VerifyTimes(times, now);
        }

        private void VerifyTimes(ArrayList times, ThemeTime now)
        {
            int nowIndex = times.IndexOf(now);
            if (nowIndex > 0)
            {
                if (((ThemeTime)times[nowIndex - 1]).ThemeKey == Consts.LIGHT_THEME_KEY_VALUE)
                {
                    changeToLightThemeNow();
                }
                else
                {
                    changeToDarkThemeNow();
                }
            }
        }

        private async void changeToLightThemeNow()
        {
            IJobDetail job = JobBuilder.Create<ChangeToLightThemeJob>()
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        private async void changeToDarkThemeNow()
        {
            IJobDetail job = JobBuilder.Create<ChangeToDarkThemeJob>()
                .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
