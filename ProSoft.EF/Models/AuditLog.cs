using ProSoft.Core.Enums;

public class AuditLog
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? CreatorId { get; set; }

    public string? CorrelationId { get; set; }
   public AuditLogEventTypes AuditLogEventType { get; set; }
    public string? EntityId { get; set; }
    public string? EntityName { get; set; }
    public string? UserName { get; set; }
    public string? UserRole { get; set; }
    public string? SessionId { get; set; }
    public string? ClientIpAddress { get; set; }
    public string? BrowserInfo { get; set; }
    public string? ApplicationName { get; set; }
    public string? MachineName { get; set; }
    public string? MachineVersion { get; set; }
    public string? MachineOsVersion { get; set; }
    public string? ServiceName { get; set; }
    public string? MethodName { get; set; }
    public string? HttpMethod { get; set; }
    public string? RequestUrl { get; set; }
    public string? ResponseStatus { get; set; }
    public DateTime? ExecutionTime { get; set; }
    public string? ExecutionDuration { get; set; }
    public string? RequestHeaders { get; set; }
    public string? QueryParameters { get; set; }
    public string? BodyParameters { get; set; }
    public string? Exception { get; set; }

    public ICollection<EntityPropertyChange> EntityPropertyChanges { get; set; } = new List<EntityPropertyChange>();
}
public class EntityPropertyChange
{
    public Guid Id { get; set; }
    public string? PropertyName { get; set; }
    public string? OriginalValue { get; set; }
    public string? NewValue { get; set; }
    public string? PropertyTypeFullName { get; set; }
    public Guid AuditLogId { get; set; }
}
public class AuditScope
{
    public List<AuditLog> Logs { get; set; } = new List<AuditLog>();
}