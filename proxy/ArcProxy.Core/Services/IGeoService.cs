namespace ArcProxy.Core.Services
{
    public interface IGeoService
    {
        Task<bool> TryAccessAsync(string servicePath);
    }
}