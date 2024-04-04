using Microsoft.JSInterop;
using PWA_Hosted.Client.Services.Interfaces;

namespace PWA_Hosted.Client.Services
{
    public class NetworkState : INetworkState
    {
        public event Action StatusChanged;
        public bool IsOnline { get; set; } = true;

        public NetworkState(IJSRuntime jsRuntime)
            => jsRuntime.InvokeVoidAsync("network.initialize", DotNetObjectReference.Create(this));

        [JSInvokable("Network.StatusChanged")]
        public void OnStatusChanged(bool isOnline)
        {
            if (IsOnline != isOnline)
            {
                IsOnline = isOnline;
                StatusChanged?.Invoke();
            }
        }

    }
}
