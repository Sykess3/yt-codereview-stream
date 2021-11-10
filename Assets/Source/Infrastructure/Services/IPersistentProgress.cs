namespace Source.Infrastructure.Services
{
    public interface IPersistentProgress
    {
        PlayerProgress Player { get; set; }
    }
}