﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;
using StatusChecker.Helper;

namespace StatusChecker
{
    public class AppSettingsManager
    {
        #region Fields
        private static AppSettingsManager _instance;
        private readonly JObject _settings;

        private const string Namespace = "StatusChecker";
        private const string FileName = "appsettings.json";


        #region Singleton
        /// <summary>
        /// Singleton for AppSettingsManager Instance
        /// </summary>
        public static AppSettingsManager Settings
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppSettingsManager();
                }

                return _instance;
            }
        }
        #endregion


        /// <summary>
        /// Search and return Setting by Name in Settings-JSON-File
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                try
                {
                    var path = name.Split(':');

                    var tempPath = path[0];

                    JToken node = _settings[tempPath];

                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception ex)
                {
                    var properties = new Dictionary<string, string> {
                        { "Method", "AppSettingsManager" },
                        { "Event", "Could not find AppSetting" }
                    };

                    AppHelper.TrackError(ex, properties);


                    return string.Empty;
                }
            }
        }
        #endregion


        #region Construction
        private AppSettingsManager()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppSettingsManager)).Assembly;
                var stream = assembly.GetManifestResourceStream($"{ Namespace }.{ FileName }");
                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    _settings = JObject.Parse(json);
                }
            }
            catch (Exception ex)
            {
                var properties = new Dictionary<string, string> {
                    { "Method", "AppSettingsManager" },
                    { "Event", "Unable to load AppSettings File" }
                };

                AppHelper.TrackError(ex, properties);
            }
        }
        #endregion
    }
}