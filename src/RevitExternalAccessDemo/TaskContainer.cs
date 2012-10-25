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
using System.Collections.Generic;
using Autodesk.Revit.UI;

namespace RevitExternalAccessDemo
{
    public class TaskContainer
    {
        private static readonly object LockObj = new object();
        private volatile static TaskContainer _instance;

        private readonly Queue<Action<UIApplication>> _tasks;

        private TaskContainer()
        {
            _tasks = new Queue<Action<UIApplication>>();
        }

        public static TaskContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new TaskContainer();
                        }
                    }
                }

                return _instance;
            }
        }

        public void EnqueueTask(Action<UIApplication> task)
        {
            _tasks.Enqueue(task);
        }

        public bool HasTaskToPerform
        {
            get { return _tasks.Count > 0; }
        }

        public Action<UIApplication> DequeueTask()
        {
            return _tasks.Dequeue();
        }
    }
}