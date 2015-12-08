using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Input
{

    /// <summary>
    /// This class defines static members for handling the focus of <see cref="IUIElement"/>s
    /// </summary>
    public static class FocusManager
    {

        /// <summary>
        /// Describes the <see cref="FocusManager"/>'s FocusedElement attached <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty FocusedElementProperty = DependencyProperty.RegisterAttached("FocusedElement", typeof(UIElement), typeof(FocusManager));
        /// <summary>
        /// Gets the focused <see cref="UIElement"/>, if any, within the specified focus scope
        /// </summary>
        /// <param name="focusScope">The <see cref="IUIElement"/> that represents the scope within which to find the focused element</param>
        /// <returns>A <see cref="UIElement"/> representing the focused element of the specified focus scope</returns>
        public static UIElement GetFocusedElement(IUIElement focusScope)
        {
            if (!focusScope.DependencyProperties.ContainsKey(FocusManager.FocusedElementProperty))
            {
                return null;
            }
            return focusScope.GetValue<UIElement>(FocusManager.FocusedElementProperty);
        }
        /// <summary>
        /// Sets the focused <see cref="UIElement"/> within the specified focus scope
        /// </summary>
        /// <param name="focusScope">The <see cref="IUIElement"/> that represents the scope for which to set the focused element</param>
        /// <param name="focusedElement">The <see cref="UIElement"/> to set the focus to</param>
        public static void SetFocusedElement(IUIElement focusScope, UIElement focusedElement)
        {
            UIElement toUnfocus;
            if (!focusScope.DependencyProperties.ContainsKey(FocusManager.FocusedElementProperty))
            {
               FocusManager.AppendFocusProperties(focusScope);
            }
            toUnfocus = focusScope.GetValue<UIElement>(FocusManager.FocusedElementProperty);
            if(toUnfocus != null)
            {
                toUnfocus.Unfocus();
            }
            focusScope.SetValue(FocusManager.FocusedElementProperty, focusedElement);
            focusedElement.Focus();
        }

        /// <summary>
        /// Describes the <see cref="FocusManager"/>'s IsFocusScope attached <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty IsFocusScopeProperty = DependencyProperty.RegisterAttached("IsFocusScope", typeof(bool), typeof(FocusManager));
        /// <summary>
        /// Gets a boolean indicating whether or not the specified <see cref="IUIElement"/> is a focus scope element
        /// </summary>
        /// <param name="element">The <see cref="IUIElement"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="IUIElement"/> is a focus scope element</returns>
        public static bool GetIsFocusScope(IUIElement element)
        {
            if (!element.DependencyProperties.ContainsKey(FocusManager.IsFocusScopeProperty))
            {
                return false;
            }
            return element.GetValue<bool>(FocusManager.IsFocusScopeProperty);
        }
        /// <summary>
        /// Sets a boolean indicating whether or not the specified <see cref="IUIElement"/> is a focus scope element
        /// </summary>
        /// <param name="element">The <see cref="IUIElement"/> to set</param>
        /// <param name="isFocusScope">A boolean indicating whether or not the specified <see cref="IUIElement"/> is a focus scope element</param>
        public static void SetIsFocusScope(IUIElement element, bool isFocusScope)
        {
            if (!element.DependencyProperties.ContainsKey(FocusManager.IsFocusScopeProperty))
            {
                FocusManager.AppendFocusProperties(element);
            }
            element.SetValue(FocusManager.IsFocusScopeProperty, isFocusScope);
        }

        /// <summary>
        /// Describes the <see cref="FocusManager"/>'s FocusableElements attached <see cref="DependencyProperty"/>
        /// </summary>
        internal static DependencyProperty FocusableElementsProperty = DependencyProperty.RegisterAttached("FocusableElements", typeof(HashSet<UIElement>), typeof(FocusManager));
        /// <summary>
        /// Gets a list of all the focusable <see cref="UIElement"/> contained within the specified focus scope
        /// </summary>
        /// <param name="focusScope">The focus scope <see cref="IUIElement"/></param>
        /// <returns>A list of all the focusable <see cref="UIElement"/> contained within the specified focus scope</returns>
        public static HashSet<UIElement> GetFocusableElements(IUIElement focusScope)
        {
            if (!focusScope.DependencyProperties.ContainsKey(FocusManager.FocusableElementsProperty))
            {
                return null;
            }
            return focusScope.GetValue<HashSet<UIElement>>(FocusManager.FocusableElementsProperty);
        }
        /// <summary>
        /// Sets a list of all the focusable <see cref="UIElement"/> contained within the specified focus scope
        /// </summary>
        /// <param name="focusScope">The focus scope <see cref="IUIElement"/></param>
        /// <param name="focusables">A list of all the focusable <see cref="UIElement"/> to add to the specified focus scope</param>
        public static void SetFocusableElements(IUIElement focusScope, HashSet<UIElement> focusables)
        {
            if (!focusScope.DependencyProperties.ContainsKey(FocusManager.IsFocusScopeProperty))
            {
                FocusManager.AppendFocusProperties(focusScope);
            }
            focusScope.SetValue(FocusManager.FocusableElementsProperty, focusables);
        }

        /// <summary>
        /// Describes the <see cref="FocusManager"/>'s IsFocusable attached <see cref="DependencyProperty"/>
        /// </summary>
        public static DependencyProperty IsFocusableProperty = DependencyProperty.RegisterAttached("IsFocusable", typeof(bool), typeof(FocusManager));
        /// <summary>
        /// Gets a boolean indicating whether or not the specified <see cref="UIElement"/> is focusable
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> to check</param>
        /// <returns>A boolean indicating whether or not the specified <see cref="UIElement"/> is focusable</returns>
        public static bool GetIsFocusable(UIElement element)
        {
            if (!element.DependencyProperties.ContainsKey(FocusManager.IsFocusableProperty))
            {
                return false;
            }
            return element.GetValue<bool>(FocusManager.IsFocusableProperty);
        }
        /// <summary>
        /// Sets a boolean indicating whether or not the specified <see cref="UIElement"/> is focusable
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> to set</param>
        /// <param name="isFocusable">A boolean indicating whether or not the specified <see cref="UIElement"/> is focusable</param>
        public static void SetIsFocusable(UIElement element, bool isFocusable)
        {
            IUIElement focusScopeElement;
            HashSet<UIElement> focusables;
            focusScopeElement = FocusManager.GetFocusScopeElement(element);
            focusables = FocusManager.GetFocusableElements(focusScopeElement);
            if (isFocusable)
            {
                if (!focusables.Contains(element))
                {
                    focusables.Add(element);
                }
            }
            else
            {
                if (focusables.Contains(element))
                {
                    focusables.Remove(element);
                }
            }
            if (!element.DependencyProperties.ContainsKey(FocusManager.IsFocusableProperty))
            {
                element.DependencyProperties.Add(FocusManager.IsFocusableProperty, isFocusable);
            }
            else
            {
                element.SetValue(FocusManager.IsFocusableProperty, isFocusable);
            }
        }

        /// <summary>
        /// This method is used to append all the <see cref="DependencyProperty"/> required by the <see cref="FocusManager"/>
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> to which to append the <see cref="DependencyProperty"/> to</param>
        internal static void AppendFocusProperties(IUIElement element)
        {
            HashSet<UIElement> observables;
            if (!element.DependencyProperties.ContainsKey(FocusManager.IsFocusScopeProperty))
            {
                element.DependencyProperties.Add(FocusManager.IsFocusScopeProperty, null);
            }
            if (!element.DependencyProperties.ContainsKey(FocusManager.FocusedElementProperty))
            {
                element.DependencyProperties.Add(FocusManager.FocusedElementProperty, null);
            }
            if (!element.DependencyProperties.ContainsKey(FocusManager.FocusableElementsProperty))
            {
                observables = new HashSet<UIElement>();
                element.DependencyProperties.Add(FocusManager.FocusableElementsProperty, observables);
            }
        }

        /// <summary>
        /// Retrieves the focus scope element within which is contained the specified <see cref="UIElement"/>
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> for which to retrieve the focus scope</param>
        /// <returns>A <see cref="IUIElement"/> representing the specified <see cref="UIElement"/>'s focus scope</returns>
        public static IUIElement GetFocusScopeElement(UIElement element)
        {
            IUIElement parent;
            parent = element.Parent;
            while(parent != null)
            {
                if (FocusManager.GetIsFocusScope(parent))
                {
                    return parent;
                }
                if (typeof(Window).IsAssignableFrom(parent.GetType()))
                {
                    return null;
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
