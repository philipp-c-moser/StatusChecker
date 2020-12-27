﻿using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace StatusChecker.Views.LegalPages
{
    public class PrivacyPolicyPage : ContentPage
    {
        public PrivacyPolicyPage()
        {
            var browser = new WebView();
            var htmlSource = new HtmlWebViewSource();

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ImprintPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("StatusChecker.Content.Static.Legal.privacypolicy_de.html");


            string htmlFileContent = "";
            using (var reader = new StreamReader(stream))
            {
                htmlFileContent = reader.ReadToEnd();
            }


            htmlSource.Html = htmlFileContent;

            browser.Source = htmlSource;
            Content = browser;

            Content.BackgroundColor = Color.FromHex("#1C1C1E");
        }
    }
}

