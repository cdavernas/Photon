using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Input;
using Photon.Media;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;

namespace Photon
{

    /// <summary>
    /// Provides the ability to create, configure, show, and manage the lifetime of windows and dialog boxes
    /// </summary>
    public class Window
        : DependencyObject, Controls.IDecorator
    {

        /// <summary>
        /// The default width of a window
        /// </summary>
        public const int DEFAULT_WIDTH = 1024;
        /// <summary>
        /// The default height of a window
        /// </summary>
        public const int DEFAULT_HEIGHT = 768;
        /// <summary>
        /// The default updates per second of a window
        /// </summary>
        public const double DEFAULT_UDATES_PER_SECOND = 30.0;
        /// <summary>
        /// The default fps of a window
        /// </summary>
        public const double DEFAULT_FRAMES_PER_SECOND = 30.0;

        /// <summary>
        /// This event is fired whenever the window is loaded
        /// </summary>
        public event EventHandler Loaded;
        /// <summary>
        /// This event is fired when the window is closing
        /// </summary>
        public event EventHandler Closing;
        /// <summary>
        /// This event is fired whenever the window is closed
        /// </summary>
        public event EventHandler Closed;
        /// <summary>
        /// This event is fired every time the mouse moves
        /// </summary>
        public event EventHandler<MouseMoveEventArgs> MouseMove;
        /// <summary>
        /// This event is fired every time a mouse button is down
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseButtonDown;
        /// <summary>
        /// This event is fired every time a mouse button is up
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseButtonUp;
        /// <summary>
        /// This event is fired every time the left mouse button is down
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseLeftButtonDown;
        /// <summary>
        /// This event is fired every time the left mouse button is up
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseLeftButtonUp;
        /// <summary>
        /// This event is fired every time the middle mouse button is down
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseMiddleButtonDown;
        /// <summary>
        /// This event is fired every time the middle mouse button is up
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseMiddleButtonUp;
        /// <summary>
        /// This event is fired every time the right mouse button is down
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseRightButtonDown;
        /// <summary>
        /// This event is fired every time the right mouse button is up
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseRightButtonUp;
        /// <summary>
        /// This event is fired every time the mouse wheel is used
        /// </summary>
        public event EventHandler<MouseWheelEventArgs> MouseWheel;
        /// <summary>
        /// This event is fired every time a key is down
        /// </summary>
        public event EventHandler<KeyboardKeyEventArgs> KeyDown;
        /// <summary>
        /// This event is fired every time a key is up
        /// </summary>
        public event EventHandler<KeyboardKeyEventArgs> KeyUp;
        /// <summary>
        /// This event is fired every time a key is pressed
        /// </summary>
        public event EventHandler<KeyPressEventArgs> KeyPressed;
        /// <summary>
        /// This event is fired every time the window's visibility has changed
        /// </summary>
        public event EventHandler VisibilityChanged;
        /// <summary>
        /// This event is fired every time the window gets focus
        /// </summary>
        public event EventHandler GotFocus;
        /// <summary>
        /// This event is fired every time the window looses focus
        /// </summary>
        public event EventHandler LostFocus;

        /// <summary>
        /// The default constructor for the <see cref="Window"/> class
        /// </summary>
        public Window()
        {
            this.Initialize();
        }

        /// <summary>
        /// Gets the window's <see cref="Photon.DrawingContext"/>
        /// </summary>
        internal DrawingContext DrawingContext { get; private set; }

        /// <summary>
        /// Gets the window's underlying <see cref="GameWindow"/>
        /// </summary>
        internal GameWindow Hwnd { get; private set; }

        public static DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(Window));
        /// <summary>
        /// Gets/sets the window's title
        /// </summary>
        public string Title

        {
            get
            {
                return this.GetValue<string>(Window.TitleProperty);
            }
            set
            {
                this.SetValue(Window.TitleProperty, value);
            }
        }

        public static DependencyProperty WidthProperty = DependencyProperty.Register("Width", typeof(Window));
        /// <summary>
        /// Gets/sets the window's width
        /// </summary>
        public double? Width
        {
            get
            {
                return this.GetValue<double?>(Window.WidthProperty);
            }
            set
            {
                this.SetValue(Window.WidthProperty, value);
            }
        }

        public static DependencyProperty HeightProperty = DependencyProperty.Register("Height", typeof(Window));
        /// <summary>
        /// Gets/sets the window's height
        /// </summary>
        public double? Height
        {
            get
            {
                return this.GetValue<double?>(Window.HeightProperty);
            }
            set
            {
                this.SetValue(Window.HeightProperty, value);
            }
        }

        public static DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof(Window));
        /// <summary>
        /// Gets/sets the <see cref="Media.Brush"/> used to paint the window's background
        /// </summary>
        public Brush Background
        {
            get
            {
                return this.GetValue<Brush>(Window.BackgroundProperty);
            }
            set
            {
                this.SetValue(Window.BackgroundProperty, value);
            }
        }

        public static DependencyProperty IsHitTestVisibleProperty = DependencyProperty.Register("IsHitTestVisible", typeof(Window));
        /// <summary>
        /// Gets/sets a boolean indicating whether or not the window is hit test visible
        /// </summary>
        public bool IsHitTestVisible
        {
            get
            {
                return this.GetValue<bool>(Window.IsHitTestVisibleProperty);
            }
            set
            {
                this.SetValue(Window.IsHitTestVisibleProperty, value);
            }
        }

        public static DependencyProperty ChildProperty = DependencyProperty.Register("Child", typeof(Window));
        /// <summary>
        /// Gets/sets the window's child <see cref="UIElement"/>
        /// </summary>
        public UIElement Child
        {
            get
            {
                return this.GetValue<UIElement>(Window.ChildProperty);
            }
            set
            {
                this.SetValue(Window.ChildProperty, value);
            }
        }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not the window's contents affect its layout
        /// </summary>
        public bool ContentsAffectsLayout
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the window's contents <see cref="Media.Size"/>
        /// </summary>
        public Size ContentsSize
        {
            get
            {
                return new Size();
            }
        }

        /// <summary>
        /// Gets a <see cref="Media.Rectangle"/> representing the position and size of the window's layout
        /// </summary>
        public Rectangle LayoutTarget
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets a <see cref="Media.Rectangle"/> representing the position and size of the window's render target
        /// </summary>
        public Rectangle RenderTarget
        {
            get
            {
                return new Rectangle(0, 0, this.Hwnd.Width, this.Hwnd.Height);
            }
        }

        /// <summary>
        /// Initializes the <see cref="Window"/>
        /// </summary>
        private void Initialize()
        {
            Input.FocusManager.SetIsFocusScope(this, true);
            this.DrawingContext = new DrawingContext(this);
            this.OnInitialized();
        }

        /// <summary>
        /// Shows the window
        /// </summary>
        public void Show()
        {
            double width, height;
            string title;
            Task.Run(() =>
            {
                if (this.Width.HasValue)
                {
                    width = this.Width.Value;
                }
                else
                {
                    width = Window.DEFAULT_WIDTH;
                }
                if (this.Height.HasValue)
                {
                    height = this.Height.Value;
                }
                else
                {
                    height = Window.DEFAULT_HEIGHT;
                }
                title = this.Title;
                if (string.IsNullOrWhiteSpace(title))
                {
                    title = "Amacrine: Untitled";
                }
                this.Hwnd = new GameWindow((int)width, (int)height, new GraphicsMode(32, 24, 8, 4));
                this.Hwnd.Title = title;
                //this.Hwnd.Width = (int)width;
                //this.Hwnd.Height = (int)height;
                this.Hwnd.Location = new System.Drawing.Point((int)(SystemParameters.WorkArea.Width / 2) - (this.Hwnd.Width / 2), (int)(SystemParameters.WorkArea.Height / 2) - (this.Hwnd.Height / 2));
                //this.Hwnd.WindowBorder = WindowBorder.Hidden;
                //this.Hwnd.WindowState = WindowState.Fullscreen;
                this.Hwnd.Load += this.OnHwndLoad;
                this.Hwnd.UpdateFrame += this.OnHwndUpdateFrame;
                this.Hwnd.RenderFrame += this.OnHwndRenderFrame;
                this.Hwnd.Closing += this.OnHwndClosing;
                this.Hwnd.Closed += this.OnHwndClosed;
                this.Hwnd.MouseMove += this.OnHwndMouseMove;
                this.Hwnd.MouseDown += this.OnHwndMouseButtonDown;
                this.Hwnd.MouseUp += this.OnHwndMouseButtonUp;
                this.Hwnd.MouseWheel += this.OnHwndMouseWheel;
                this.Hwnd.KeyDown += this.OnHwndKeyDown;
                this.Hwnd.KeyUp += this.OnHwndKeyUp;
                this.Hwnd.KeyPress += this.OnHwndKeyPress;
                Application.Current.RegisterWindow(this);
                this.Hwnd.Run(Window.DEFAULT_UDATES_PER_SECOND, Window.DEFAULT_FRAMES_PER_SECOND);
            });
        }

        /// <summary>
        /// Show the window as a dialog, meaning the UI will block until the dialog closes or returns a value
        /// </summary>
        public void ShowDialog()
        {

        }

        /// <summary>
        /// Hides the window
        /// </summary>
        public void Hide()
        {
            this.Hwnd.Visible = false;
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        public void Close()
        {
            if(this.Hwnd == null)
            {
                return;
            }
            this.Hwnd.Close();
        }

        /// <summary>
        /// Computes the x and y offset of the specified window's child <see cref="UIElement"/>
        /// </summary>
        /// <param name="child">The window's child <see cref="UIElement"/> for whihc to compute the offset</param>
        /// <returns>A <see cref="Media.Point"/> representing the window's child x and y offset</returns>
        public Point ComputeChildOffset(UIElement child)
        {
            return new Point();
        }

        /// <summary>
        /// Processes the <see cref="UIEvent"/> specified by the <see cref="UIEventArgs"/> passed as parameter
        /// </summary>
        /// <param name="e">The <see cref="UIEventArgs"/> associated with the <see cref="UIEvent"/> to process</param>
        public void ProcessUIEvent(UIEventArgs e)
        {
            if (this.Child != null)
            {
                this.Child.ProcessUIEvent(e);
            }
            if (e.IsHandled)
            {
                return;
            }
            switch (e.SourceEvent)
            {
                case UIEvent.MouseMove:
                    if (this.MouseMove != null)
                    {
                        this.MouseMove(this, (MouseMoveEventArgs)e.SourceEventArgs);
                    }
                    break;
                case UIEvent.MouseButtonDown:
                    if (this.MouseButtonDown != null)
                    {
                        this.MouseButtonDown(this, (MouseButtonEventArgs)e.SourceEventArgs);
                    }
                    break;
                case UIEvent.MouseLeftButtonDown:
                    if (this.MouseLeftButtonDown != null)
                    {
                        this.MouseLeftButtonDown(this, (MouseButtonEventArgs)e.SourceEventArgs);
                    }
                    break;
                case UIEvent.MouseMiddleButtonDown:
                    if (this.MouseMiddleButtonDown != null)
                    {
                        this.MouseMiddleButtonDown(this, (MouseButtonEventArgs)e.SourceEventArgs);
                    }
                    break;
                case UIEvent.MouseRightButtonDown:
                    if (this.MouseRightButtonDown != null)
                    {
                        this.MouseRightButtonDown(this, (MouseButtonEventArgs)e.SourceEventArgs);
                    }
                    break;
            }
        }

        /// <summary>
        /// Sets the mouse cursor
        /// </summary>
        /// <param name="cursor">The <see cref="Media.MouseCursor"/> to set</param>
        public void SetCursor(Media.MouseCursor cursor)
        {
            if (this.Hwnd == null)
            {
                return;
            }
            if (cursor == null)
            {
                cursor = Media.MouseCursor.Default;
            }
            this.Hwnd.Cursor = cursor.CursorObject;
        }

        /// <summary>
        /// Invalidates the window's layout
        /// </summary>
        public void InvalidateLayout()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invalidates the window's visual, forcing it to redraw
        /// </summary>
        public void InvalidateVisual()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the underlying <see cref="GameWindow.Load"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndLoad(object sender, EventArgs e)
        {
            if(this.Child != null)
            {
                this.Child.Load();
            }
            if(this.Loaded != null)
            {
                this.Loaded(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the underlying <see cref="GameWindow.UpdateFrame"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndUpdateFrame(object sender, FrameEventArgs e)
        {

        }

        /// <summary>
        /// Handles the underlying <see cref="GameWindow.RenderFrame"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndRenderFrame(object sender, FrameEventArgs e)
        {
            foreach (Media.Animations.AnimationClock clock in this.AnimationClocks.Where(c => c.IsRunning))
            {
                clock.Render();
            }
            this.DrawingContext.BeginRenderPass();
            this.OnRender(this.DrawingContext);
            if (this.Child != null)
            {
                this.Child.Render(this.DrawingContext);
            }
            this.DrawingContext.EndRenderPass();
        }

        /// <summary>
        /// Handles the underlying <see cref="GameWindow.MouseMove"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndMouseMove(object sender, MouseMoveEventArgs e)
        {
            this.ProcessUIEvent(new UIEventArgs(UIEvent.MouseMove, e));
        }

        /// <summary>
        /// Handles the underlying <see cref="NativeWindow.MouseDown"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ProcessUIEvent(new UIEventArgs(UIEvent.MouseButtonDown, e));
            switch (e.Button)
            {
                case MouseButton.Left:
                    this.ProcessUIEvent(new UIEventArgs(UIEvent.MouseLeftButtonDown, e));
                    break;
                case MouseButton.Middle:
                    this.ProcessUIEvent(new UIEventArgs(UIEvent.MouseMiddleButtonDown, e));
                    break;
                case MouseButton.Right:
                    this.ProcessUIEvent(new UIEventArgs(UIEvent.MouseRightButtonDown, e));
                    break;
            }
        }

        /// <summary>
        /// Handles the underlying <see cref="NativeWindow.MouseUp"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndMouseButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.ProcessUIEvent(new UIEventArgs(UIEvent.MouseButtonUp, e));
            switch (e.Button)
            {
                case MouseButton.Left:
                    this.ProcessUIEvent(new UIEventArgs(UIEvent.MouseLeftButtonUp, e));
                    break;
                case MouseButton.Middle:
                    this.ProcessUIEvent(new UIEventArgs(UIEvent.MouseMiddleButtonUp, e));
                    break;
                case MouseButton.Right:
                    this.ProcessUIEvent(new UIEventArgs(UIEvent.MouseRightButtonUp, e));
                    break;
            }
        }

        /// <summary>
        /// Handles the underlying <see cref="NativeWindow.MouseWheel"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndMouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.ProcessUIEvent(new UIEventArgs(UIEvent.MouseWheel, e));
        }

        /// <summary>
        /// Handles the underlying <see cref="NativeWindow.KeyDown"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Tab:
                    Input.KeyboardNavigation.NavigateToNextElement(this);
                    break;
                default:
                    UIElement focusedElement;
                    focusedElement = Input.FocusManager.GetFocusedElement(this);
                    if(focusedElement != null)
                    {
                        focusedElement.OnKeyDown(e);
                    }
                    break;
            }
        }

        /// <summary>
        /// Handles the underlying <see cref="NativeWindow.KeyUp"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                default:
                    UIElement focusedElement;
                    focusedElement = Input.FocusManager.GetFocusedElement(this);
                    if (focusedElement != null)
                    {
                        focusedElement.OnKeyUp(e);
                    }
                    break;
            }
        }

        /// <summary>
        /// Handles the underlying <see cref="NativeWindow.KeyPress"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndKeyPress(object sender, KeyPressEventArgs e)
        {
            UIElement focusedElement;
            focusedElement = Input.FocusManager.GetFocusedElement(this);
            if (focusedElement != null)
            {
                focusedElement.OnKeyPressed(e);
            }
        }

        /// <summary>
        /// Handles the underlying <see cref="NativeWindow.Closing"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndClosing(object sender, EventArgs e)
        {
            if (this.Closing != null)
            {
                this.Closing(this, new EventArgs());
            }
        }

        /// <summary>
        /// Handles the underlying <see cref="NativeWindow.Closed"/> event
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's arguments</param>
        private void OnHwndClosed(object sender, EventArgs e)
        {
            if (this.Closed != null)
            {
                this.Closed(this, new EventArgs());
            }
        }

        /// <summary>
        /// When overriden in a class, this method provides means to run code whenever the <see cref="Window"/> has been initialized
        /// </summary>
        protected virtual void OnInitialized()
        {

        }

        /// <summary>
        /// Handles the <see cref="DependencyObject.PropertyChanged"/> event
        /// </summary>
        /// <param name="propertyName">The dependency property's name</param>
        /// <param name="originalValue">The dependency property's original value</param>
        /// <param name="value">The dependency property's new value</param>
        protected override void OnPropertyChanged(string propertyName, object originalValue, object value)
        {
            if(propertyName == Window.ChildProperty.Name)
            {
                if(value != null)
                {
                    ((UIElement)value).Parent = this;
                }
                return;
            }
            base.OnPropertyChanged(propertyName, originalValue, value);
        }

        /// <summary>
        /// When overriden in a class, renders the window in the specified <see cref="Photon.DrawingContext"/>
        /// </summary>
        /// <param name="drawingContext">The <see cref="Photon.DrawingContext"/> in which to render the window</param>
        protected virtual void OnRender(DrawingContext drawingContext)
        {
            if(this.Background != null)
            {
                drawingContext.DrawRectangle(new Rectangle(0, 0, this.Hwnd.Width, this.Hwnd.Height), Thickness.Empty, this.Background, null);
            }
        }

        /// <summary>
        /// Retrieves the <see cref="Window"/> to which the specified <see cref="UIElement"/> belongs
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> to retrieve the parent <see cref="Window"/> of</param>
        /// <returns>The <see cref="Window"/> the specified <see cref="UIElement"/> belongs to</returns>
        public static Window GetWindow(UIElement element)
        {
            IUIElement parent;
            parent = element.Parent;
            while(parent != null)
            {
                if (typeof(Window).IsAssignableFrom(parent.GetType()))
                {
                    return (Window)parent;
                }
                else
                {
                    parent = ((UIElement)parent).Parent;
                }
            }
            return null;
        }

    }

}
