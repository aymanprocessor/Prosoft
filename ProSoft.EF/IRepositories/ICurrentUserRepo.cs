public interface ICurrentUserService
{
    int UserId { get; }
    string UserName { get; }
    string BranchName { get; }
    int BranchId { get; }
}