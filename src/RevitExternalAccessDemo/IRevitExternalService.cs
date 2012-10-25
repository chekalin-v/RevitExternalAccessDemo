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