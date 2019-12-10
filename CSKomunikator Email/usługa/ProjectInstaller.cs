using System.ComponentModel;
using System.Configuration.Install;

namespace gra
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            serviceInstaller1.Description = "_graŻabkaDesc";
            serviceInstaller1.DisplayName = "_graŻabka";
        }

        private void serviceInstaller1_AfterInstall(object sender, InstallEventArgs e)
        {

        }
    }
}
