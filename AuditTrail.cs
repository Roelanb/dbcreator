
namespace AuditTrail.Core;


public interface IAuditTrailer
{
    Task Commit();
        Task LogCreate(string application, string username, object newObject, string objectType, string objectName);

    Task LogUpdate(string application1, string username, object? oldObject, object application2, string v1, string v2);

    Task LogDelete(string application, string username, object oldObject, string objectType, string objectName);

    Task LogCreateRelation(string application, string username, string firstObjectName, string firstObjectType, string secondObjectName,
        string secondObjectType);

    Task LogDeleteRelation(string application, string username, string firstObjectName, string firstObjectType, string secondObjectName,
        string secondObjectType);

}

public class AuditTrail : IAuditTrailer
{
    public Task Commit()
    {
        return Task.CompletedTask;
    }

    public Task LogCreate(string application, string username, object newObject, string objectType, string objectName)
    {
        return Task.CompletedTask;
    }

    public Task LogCreateRelation(string application, string username, string firstObjectName, string firstObjectType, string secondObjectName, string secondObjectType)
    {
        return Task.CompletedTask;
    }

    public Task LogDelete(string application, string username, object oldObject, string objectType, string objectName)
    {
        return Task.CompletedTask;
    }

    public Task LogDeleteRelation(string application, string username, string firstObjectName, string firstObjectType, string secondObjectName, string secondObjectType)
    {
        return Task.CompletedTask;
    }

    public Task LogUpdate(string application1, string username, object? oldObject, object application2, string v1, string v2)
    {
        return Task.CompletedTask;
    }



}

