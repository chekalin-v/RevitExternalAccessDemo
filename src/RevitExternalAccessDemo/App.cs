/* 
 * Copyright 2012 © Victor Chekalin IVC
 * 
 * THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 * PARTICULAR PURPOSE.
 * 
 */

#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Description;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;

#endregion

namespace RevitExternalAccessDemo
{
    class App : IExternalApplication
    {
        private const string serviceUrl =
            "http://localhost:56789/RevitExternalService";


        private ServiceHost serviceHost;

        public Result OnStartup(UIControlledApplication a)
        {
            a.Idling += OnIdling;

            Uri uri = new Uri(serviceUrl);

            serviceHost = 
                new ServiceHost(typeof(RevitExternalService), uri);


            try
            {
                serviceHost.AddServiceEndpoint(typeof (IRevitExternalService),
                                               new WSHttpBinding(),
                                               "RevitExternalService");

                ServiceMetadataBehavior smb =
                    new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                serviceHost.Description.Behaviors.Add(smb);

                serviceHost.Open();
            }
            catch (Exception)
            {
                
                throw;
            }
            
            return Result.Succeeded;
        }

        private void OnIdling(object sender, IdlingEventArgs e)
        {
            var uiApp = sender as UIApplication;

            Debug.Print("OnIdling: {0}", DateTime.Now.ToString("HH:mm:ss.fff"));

            // be carefull. It loads CPU 
            e.SetRaiseWithoutDelay();

            if (!TaskContainer.Instance.HasTaskToPerform)
                return;

            try
            {
                Debug.Print("Start execute task: {0}", DateTime.Now.ToString("HH:mm:ss.fff"));

                var task = TaskContainer.Instance.DequeueTask();
                task(uiApp);

                Debug.Print("Ending execute task: {0}", DateTime.Now.ToString("HH:mm:ss.fff"));
            }
            catch (Exception ex)
            {
                uiApp.Application.WriteJournalComment("FamilyBrowserCommand. An error occurs while execute command OnIdling event\r\n" +
                    ex.ToString(), true);
                Debug.WriteLine(ex);
            }

            //e.SetRaiseWithoutDelay();
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            a.Idling -= OnIdling;

            if (serviceHost != null)
            {
                serviceHost.Close();
            }

            return Result.Succeeded;
        }
    }
}
