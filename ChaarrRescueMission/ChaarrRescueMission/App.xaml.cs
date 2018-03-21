using ChaarrRescueMission.ViewModel;
using System;
using System.Diagnostics;
using System.Windows;

namespace ChaarrRescueMission
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var context = new RescueMissionViewModel();
                var app = new ApplicationView();                
                app.Show();
                app.DataContext = context;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
