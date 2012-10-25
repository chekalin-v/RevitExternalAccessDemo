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
using System.ServiceModel;
using RevitExternalAccessDemo;

namespace RevitExternalAccessClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConsoleKeyInfo keyInfo;

            ConsoleKey command;

            Console.WriteLine("Press any key when Revit has launched..");
            Console.ReadKey();

            IRevitExternalService service;
            try
            {
                System.ServiceModel.ChannelFactory<IRevitExternalService> channelFactory =
                new ChannelFactory<IRevitExternalService>("WSHttpBinding_IRevitExternalService");

                service = channelFactory.CreateChannel();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.WriteLine("Select command:");
                Console.WriteLine("\tP - Get project file path");
                Console.WriteLine("\tW - Create walls");
                Console.WriteLine("\tEsc - Exit");
                var key = Console.ReadKey();
                Console.WriteLine();

                command = key.Key;

                try
                {                    
                    switch (command)
                    {
                        case ConsoleKey.P:
                            var path = service.GetCurrentDocumentPath();

                            Console.WriteLine(path);
                            break;

                        case ConsoleKey.W:
                            

                            // H
                            CreateWallAndWriteResult(XYZ.Create(-60, 50),
                                XYZ.Create(-60, 38),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-60, 44),
                                XYZ.Create(-53, 44),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-53, 50),
                                XYZ.Create(-53, 38),
                                service);

                            // E
                            CreateWallAndWriteResult(XYZ.Create(-48, 50),
                                XYZ.Create(-48, 38),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-48, 50),
                                XYZ.Create(-41, 50),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-48, 44),
                                XYZ.Create(-41, 44),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-48, 38),
                                XYZ.Create(-41, 38),
                                service);
                       

                            // L
                            CreateWallAndWriteResult(XYZ.Create(-36, 50),
                                XYZ.Create(-36, 38),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-36, 38),
                                XYZ.Create(-29, 38),
                                service);

                            // L
                            CreateWallAndWriteResult(XYZ.Create(-24, 50),
                                XYZ.Create(-24, 38),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-24, 38),
                                XYZ.Create(-17, 38),
                                service);

                            // O
                            CreateWallAndWriteResult(XYZ.Create(-12, 50),
                                XYZ.Create(-12, 38),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-12, 50),
                                XYZ.Create(-5, 50),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-5, 50),
                                XYZ.Create(-5, 38),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-12, 38),
                                XYZ.Create(-5, 38),
                                service);

                            // R
                            CreateWallAndWriteResult(XYZ.Create(-55, 30),
                                XYZ.Create(-55, 18),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-55, 30),
                                XYZ.Create(-50, 30),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-50, 30),
                                XYZ.Create(-48, 29),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-48, 29),
                                XYZ.Create(-48, 25),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-48, 25),
                                XYZ.Create(-50, 24),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-50, 24),
                                XYZ.Create(-55, 24),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-55, 24),
                                XYZ.Create(-48, 18),
                                service);

                            // E
                            CreateWallAndWriteResult(XYZ.Create(-41, 30),
                                XYZ.Create(-41, 18),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-41, 30),
                                XYZ.Create(-34, 30),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-41, 24),
                                XYZ.Create(-34, 24),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-41, 18),
                                XYZ.Create(-34, 18),
                                service);

                            // V
                            CreateWallAndWriteResult(XYZ.Create(-29, 30),
                                XYZ.Create(-26, 18),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-26, 18),
                                XYZ.Create(-23, 30),
                                service);

                            // I
                            CreateWallAndWriteResult(XYZ.Create(-17, 30),
                                XYZ.Create(-11, 30),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-14, 30),
                                XYZ.Create(-14, 18),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-17, 18),
                                XYZ.Create(-11, 18),
                                service);

                            // T
                            CreateWallAndWriteResult(XYZ.Create(-6, 30),
                                XYZ.Create(-0, 30),
                                service);

                            CreateWallAndWriteResult(XYZ.Create(-3, 30),
                                XYZ.Create(-3, 18),
                                service);

                            Console.WriteLine("Walls created...");
                            break;

                        case ConsoleKey.Escape:                            
                            return;

                        default:
                            Console.WriteLine("!!!Wrong command!!!");
                            continue;
                    }

                
                    
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex);
                }

               
            }
        }

        private static void CreateWallAndWriteResult(XYZ point1, XYZ point2, IRevitExternalService service)
        {
            bool res = service.CreateWall(point1, point2);
            Console.WriteLine("Create wall result: {0}", res);
        }
    }
}