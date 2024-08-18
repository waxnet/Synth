using OpenQA.Selenium;

namespace Synth.Api
{
    public class Window(IWindow? window)
    {
        private readonly IWindow? _window = window;

        // properties
        public Vector2? Position
        {
            get { return new Vector2(_window?.Position.X, _window?.Position.Y); }
            set
            {
                if (_window == null || value == null)
                    return;
                _window.Position = new System.Drawing.Point(value.X, value.Y);
            }
        }
        public Vector2? Size
        {
            
            get { return new Vector2(_window?.Size.Width, _window?.Size.Height); }
            set
            {
                if (_window == null || value == null)
                    return;
                _window.Size = new System.Drawing.Size(value.X, value.Y);
            }
        }

        // methods
        public void Fullscreen()
        {
            _window?.FullScreen();
        }
        public void Maximize()
        {
            _window?.Maximize();
        }
        public void Minimize()
        {
            _window?.Minimize();
        }
    }
}
