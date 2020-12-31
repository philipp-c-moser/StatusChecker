﻿using System;
using Xamarin.Essentials;

namespace StatusChecker.Helper
{
    public static class AppHelper
    {
        /// <summary>
        /// Returns the AppName with AppVersion and AppBuildNumber, if defined
        /// </summary>
        /// <returns></returns>
        public static string GetAppVersionInformation()
        {
            string appBuildNumber = AppSettingsManager.Settings["AppBuildNumber"];
            string appBuildNumberString = "";

            if (!string.IsNullOrEmpty(appBuildNumber)) appBuildNumberString = $"({ appBuildNumber })";

            VersionTracking.Track();

            return $"StatusChecker v{ VersionTracking.CurrentVersion } { appBuildNumberString }";
        }
    }
}
