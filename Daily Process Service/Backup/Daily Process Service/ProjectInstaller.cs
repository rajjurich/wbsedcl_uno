using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace Daily_Process_Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}