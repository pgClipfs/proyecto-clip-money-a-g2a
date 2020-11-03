using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Optimization;

namespace clip_money.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Script/Bundles").Include(
                "~/bundles/inline.*",
                "~/bundles/polyfills.*",
                "~/bundles/scripts.*",
                "~/bundles/vendor.*",
                "~/bundles/runtime.*",
                "~/bundles/zone.*",
                "~/bundles/main.*"));
            bundles.Add(new StyleBundle("~/Content/Styles").Include("~/bundles/styles.*"));
        }
    }
}