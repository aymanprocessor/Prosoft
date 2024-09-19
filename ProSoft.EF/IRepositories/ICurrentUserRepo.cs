public interface ICurrentUserService
{
    string UserId { get; }
    string UserName { get; }
    string BranchName { get; }
    string BranchId { get; }
}