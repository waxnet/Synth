using NLua;
using OpenQA.Selenium;

namespace Synth.Api
{
    public class Network(INetwork? network)
    {
        private readonly INetwork? _network = network;
        private bool _monitoring = false;

        // methods
        public Connection? Connect(LuaFunction function, string traffic)
        {
            if (_network == null) return null;

            Connection connection = new(function, traffic);
            switch (traffic)
            {
                case "requests":
                    _network.NetworkRequestSent += connection.Call;
                    break;
                case "responses":
                    _network.NetworkResponseReceived += connection.Call;
                    break;
                default:
                    return null;
            }

            return connection;
        }
        public void Disconnect(Connection connection)
        {
            if (_network == null) return;

            switch (connection.Traffic)
            {
                case "requests":
                    _network.NetworkRequestSent -= connection.Call;
                    break;
                case "responses":
                    _network.NetworkResponseReceived -= connection.Call;
                    break;
            }
        }
        public void StartMonitoring()
        {
            if (_monitoring) return;
            Task.Run(() => _network?.StartMonitoring()).Wait();
            _monitoring = true;
        }
        public void StopMonitoring()
        {
            if (!_monitoring) return;
            Task.Run(() => _network?.StopMonitoring()).Wait();
            _monitoring = false;
        }
    }
}
