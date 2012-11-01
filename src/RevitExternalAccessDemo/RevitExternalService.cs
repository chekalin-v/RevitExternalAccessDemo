/* 
 * Copyright 2012 © Victor Chekalin IVC
 * 
 * THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 * PARTICULAR PURPOSE.
 * 
 */

using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitExternalAccessDemo
{
    class RevitExternalService : IRevitExternalService
    {
        private string currentDocumentPath;

        private static readonly object _locker =
            new object();

        private const int WAIT_TIMEOUT = 10000; // 10 seconds timeout

        #region Implementation of IRevitExternalService

        public string GetCurrentDocumentPath()
        {
            Debug.Print("Push task to the container: {0}", DateTime.Now.ToString("HH:mm:ss.fff"));

            lock (_locker)
            {
                TaskContainer.Instance.EnqueueTask(GetDocumentPath);

                // Wait when the task is completed
                Monitor.Wait(_locker, WAIT_TIMEOUT);
            }

            Debug.Print("Finish task: {0}", DateTime.Now.ToString("HH:mm:ss.fff"));
            return currentDocumentPath;
        }

        private void GetDocumentPath(UIApplication uiapp)
        {
            try
            {
                currentDocumentPath = uiapp.ActiveUIDocument.Document.PathName;
            }
            // Always release locker in finally block
            // to ensure to unlock locker object.
            finally
            {
                lock (_locker)
                {
                    Monitor.Pulse(_locker);
                }
            }
        }

        public bool CreateWall(XYZ startPoint, XYZ endPoint)
        {

            Wall wall = null;

            lock (_locker)
            {

                TaskContainer.Instance.EnqueueTask(uiapp =>
                    {
                        try
                        {
                            var doc = uiapp.ActiveUIDocument.Document;

                            using (Transaction t = new Transaction(doc, "Create wall"))
                            {
                                t.Start();

                                Curve curve = Line.get_Bound(
                                    new Autodesk.Revit.DB.XYZ(startPoint.X, startPoint.Y, startPoint.Z),
                                    new Autodesk.Revit.DB.XYZ(endPoint.X, endPoint.Y, endPoint.Z));
                                FilteredElementCollector collector = new FilteredElementCollector(doc);
                                var level =
                                    collector
                                        .OfClass(typeof(Level))
                                        .ToElements()
                                        .OfType<Level>()
                                        .FirstOrDefault();

                                #if REVIT2012
                                wall = doc.Create.NewWall(curve, level, false);
                                #else
                                wall = Wall.Create(doc, curve, level.Id, true);
                                #endif

                                t.Commit();
                            }
                        }
                        finally
                        {

                            lock (_locker)
                            {
                                Monitor.Pulse(_locker);
                            }
                        }

                    });

                Monitor.Wait(_locker);
            }
            return wall != null;
        }

        #endregion
    }
}