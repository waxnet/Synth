using NLua;
using System.Reflection;

namespace Synth.Api
{
    public static class Runtime
    {
        private static readonly List<string> blacklist = [
            "package",
            "os",
            "dofile",
            "loadfile",
            "io",
            "debug",
            "csharp",
            "print",
            "include",
            "require",
            "collectgarbage",
            "_G"
        ];

        // methods
        public static Lua Create()
        {
            Lua runtime = new();

            // disable blacklisted functions
            foreach (string function in blacklist)
                runtime[function] = null;

            // add functions from modules
            List<Type> modules = [
                typeof(Core),
                typeof(Data),
                typeof(FileSystem),
                typeof(Screen),
                typeof(Other)
            ];

            foreach (var module in modules)
                foreach (var method in module.GetMethods(BindingFlags.Static | BindingFlags.Public))
                    runtime.RegisterFunction(method.Name, method);

            // add data types
            List<Type> dataTypes = [
                typeof(Vector2)
            ];

            foreach (var dataType in dataTypes)
            {
                runtime.DoString(dataType.Name + " = {}");

                foreach (var method in dataType.GetMethods(BindingFlags.Static | BindingFlags.Public))
                    runtime.RegisterFunction($"{dataType.Name}.{method.Name}", method);
            }

            return runtime;
        }
    }
}
