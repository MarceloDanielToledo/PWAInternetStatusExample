namespace PWA_Hosted.Client.Services.Interfaces
{
    public interface INetworkState
    {
        bool IsOnline { get; set; }
        void OnStatusChanged(bool isOnline);
        event Action StatusChanged;
    }
}
