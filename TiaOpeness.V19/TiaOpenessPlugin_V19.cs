using ApplicationUtilities.DI;
using System;
using System.Reflection;
using TiaOpeness.V19.Internal;

namespace TiaOpeness.V19
{
    public class TiaOpenessPlugin_V19 : TiaOpenessPlugin
    {
		private const string DOMAINNAME = "TiaV19";
        private const string PLUGINNAME = "TIA Openess Plugin";
        private const string VERSION = "19.0.0.0";
        private const string CMDOPTION = "v19";
        private const string DESCRIPTION = "TIA Openess Plugin (v19.0)";

        public TiaOpenessPlugin_V19(Context context) : base(context)
        {
            this.name = PLUGINNAME;
            this.version = new Version(VERSION);
            this.cmdOption = CMDOPTION;
            this.description = DESCRIPTION;
            this.domainName = DOMAINNAME;
        }

        public override bool IsTiaOpenessInstalled()
        {
            return TiaOpenessHelper_V19.IsInstalled();
        }

        public override void AllowFirewallAccess(Assembly assembly)
        {
            if (IsTiaOpenessInstalled())
                TiaOpenessFirewall_V19.AllowAccess(assembly);
        }

        public override bool Initialize()
        {
            string tiaInstallationPath = TiaOpenessHelper_V19.GetInstallationPath();
            domain = CreateDomain(tiaInstallationPath);
            AppDomain.CurrentDomain.AssemblyResolve += TiaOpenessApiResolver_V19.AssemblyResolver;

            isInitialized = true;
            return isInitialized;
        }

        protected override ITiaOpeness CreateTiaOpenessInstance()
        {
            return new TiaOpeness_V19();
        }

    }
}
