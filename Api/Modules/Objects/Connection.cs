using NLua;
using OpenQA.Selenium;

namespace Synth.Api
{
    public class Connection(LuaFunction function, string traffic)
    {
        private readonly LuaFunction _function = function;
        private readonly object _lock = new();

        // properties
        public string Traffic { get; } = traffic;

        // methods
        public void Call(object? _, NetworkRequestSentEventArgs args)
        {
            if (args == null) return;

            lock (_lock)
                _function.Call(new Request(args));
        }
        public void Call(object? _, NetworkResponseReceivedEventArgs args)
        {
            if (args == null) return;

            lock (_lock)
                _function.Call(new Response(args));
        }
    }
}
