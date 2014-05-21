/* 
 * Copyright 2014 © Victor Chekalin
 * 
 * THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
 * KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 * PARTICULAR PURPOSE.
 * 
 */

using System.ServiceModel;

namespace RevitExternalAccessDemo
{
    [ServiceContract]
    public interface IRevitExternalService
    {
        [OperationContract]
        string GetCurrentDocumentPath();

        [OperationContract]
        bool CreateWall(XYZ startPoint, XYZ endPoint);
    }
}