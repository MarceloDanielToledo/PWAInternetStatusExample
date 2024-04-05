# Internet Status Checker in Blazor PWA  ⚡

This is an example of how to check the internet connection status in a Blazor Progressive Web Application (PWA).


![Alt Text](https://s9.gifyu.com/images/SV5bk.gif)





## How it Works
It's quite simple. We'll use a JavaScript script and then define `NetworkState` as a state container to use it in any component.

#### network.js
```js
    window.network = {
        initialize: function (interop) {
            function handler() {
                interop.invokeMethodAsync("Network.StatusChanged", navigator.onLine);
            }
    
            window.addEventListener('online', handler);
            window.addEventListener('offline', handler);
    
            if (!navigator.onLine) {
                handler(navigator.onLine);
            }
        }
    };
```

#### NetworkState.cs


```csharp
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
```

```sh
Regarding the state container, you should define the service as a singleton if the application is WebAssembly, or scoped if it's server-side.
```

You should subscribe in any component you desire. In this example, I've subscribed in the "NetworkStatus" component.


```csharp
@code {
    protected bool IsOnline => _networkState.IsOnline;
    protected override void OnInitialized()
    {
        base.OnInitialized();
        _networkState.StatusChanged += NetworkStatusChanged;

    }

    private void NetworkStatusChanged()
    {

        StateHasChanged();

    }
    public void Dispose()
    {
        _networkState.StatusChanged -= NetworkStatusChanged;
    }
}
```







Consider giving a star ⭐, forking the repository, and staying tuned for updates!