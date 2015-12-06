using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photon.Input
{

    /// <summary>
    /// This class defines static members for handling keyboard navigation
    /// </summary>
    public static class KeyboardNavigation
    {

        public static DependencyProperty IsTabStopProperty = DependencyProperty.RegisterAttached("IsTabStop", typeof(bool), typeof(KeyboardNavigation));
        /// <summary>
        /// Gets a boolean indicating whether or not the specified element is a Tab stop
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> to check</param>
        /// <returns>A boolean</returns>
        public static bool GetIsTabStop(UIElement element)
        {
            if (!element.DependencyProperties.ContainsKey(KeyboardNavigation.IsTabStopProperty))
            {
                return false;
            }
            return element.GetValue<bool>(KeyboardNavigation.IsTabStopProperty);
        }
        /// <summary>
        /// Sets a boolean indicating whether or not the specified element is a Tab stop
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> to set</param>
        /// <param name="isTabStop">A boolean indicating whether or not the element is a Tab stop element</param>
        public static void SetIsTabStop(UIElement element, bool isTabStop)
        {
            IUIElement focusScopeElement;
            HashSet<UIElement> focusables;
            int index;
            if (!element.DependencyProperties.ContainsKey(KeyboardNavigation.IsTabStopProperty))
            {
                KeyboardNavigation.AppendKeyboardNavigationProperties(element);
            }
            focusScopeElement = FocusManager.GetFocusScopeElement(element);
            if (focusScopeElement == null)
            {
                return;
            }
            element.SetValue(KeyboardNavigation.IsTabStopProperty, isTabStop);
            focusables = focusScopeElement.GetValue<HashSet<UIElement>>(FocusManager.FocusableElementsProperty);
            if (focusables.Contains(element))
            {
                return;
            }
            if(focusables.Count > 0)
            {
                index = focusables.Max(elem => elem.GetValue<int>(KeyboardNavigation.TabIndexProperty));
            }
            else
            {
                index = 0;
            }
            element.SetValue(KeyboardNavigation.TabIndexProperty, index);
        }

        public static DependencyProperty TabIndexProperty = DependencyProperty.RegisterAttached("TabIndex", typeof(int), typeof(KeyboardNavigation));
        /// <summary>
        /// Gets an integer representing the specified <see cref="UIElement"/>'s Tab index
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> who's Tab index is to return</param>
        /// <returns>An integer</returns>
        public static int GetTabIndex(UIElement element)
        {
            if (!element.DependencyProperties.ContainsKey(KeyboardNavigation.TabIndexProperty))
            {
                return -1;
            }
            return element.GetValue<int>(KeyboardNavigation.TabIndexProperty);
        }
        /// <summary>
        /// Sets an integer representing the specified <see cref="UIElement"/>'s Tab index
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> who's Tab index is to set</param>
        /// <param name="index">An integer representing the <see cref="UIElement"/>'s Tab index</param>
        public static void SetTabIndex(UIElement element, int index)
        {
            IUIElement focusScopeElement;
            ObservableHashSet<UIElement> focusables;
            if (!element.DependencyProperties.ContainsKey(KeyboardNavigation.TabIndexProperty))
            {
                KeyboardNavigation.AppendKeyboardNavigationProperties(element);
            }
            if (index < 0)
            {
                index = 0;
            }
            focusScopeElement = FocusManager.GetFocusScopeElement(element);
            if(focusScopeElement == null)
            {
                return;
            }
            focusables = focusScopeElement.GetValue<ObservableHashSet<UIElement>>(FocusManager.FocusableElementsProperty);
            if (!focusables.Contains(element))
            {
                focusables.Add(element);
            }
            element.SetValue(KeyboardNavigation.IsTabStopProperty, true);
            element.SetValue(KeyboardNavigation.TabIndexProperty, index);
        }

        /// <summary>
        /// This method is used to append all the <see cref="DependencyProperty"/> required for <see cref="KeyboardNavigation"/>
        /// </summary>
        /// <param name="element">The <see cref="UIElement"/> to which to append the <see cref="DependencyProperty"/> to</param>
        private static void AppendKeyboardNavigationProperties(UIElement element)
        {
            if (!element.DependencyProperties.ContainsKey(KeyboardNavigation.IsTabStopProperty))
            {
                element.DependencyProperties.Add(KeyboardNavigation.IsTabStopProperty, null);
            }
            if (!element.DependencyProperties.ContainsKey(KeyboardNavigation.TabIndexProperty))
            {
                element.DependencyProperties.Add(KeyboardNavigation.TabIndexProperty, null);
            }
        }

        /// <summary>
        /// Sets the focus to the next focusable <see cref="UIElement"/> available
        /// </summary>
        /// <param name="focusScope">The <see cref="IUIElement"/> within which to navigate</param>
        public static void NavigateToNextElement(IUIElement focusScope)
        {
            IEnumerable<UIElement> focusables;
            UIElement focusedElement;
            int focusedElementIndex;
            if (!FocusManager.GetIsFocusScope(focusScope))
            {
                return;
            }
            focusables = FocusManager.GetFocusableElements(focusScope);
            focusables = focusables.Where(f => f.GetValue<bool>(KeyboardNavigation.IsTabStopProperty)).OrderBy(f => f.GetValue<int>(KeyboardNavigation.TabIndexProperty));
            focusedElement = FocusManager.GetFocusedElement(focusScope);
            if(focusedElement == null)
            {
                focusedElement = focusables.First();
            }
            else
            {
                focusedElementIndex = focusables.ToList().IndexOf(focusedElement);
                if(focusedElementIndex + 1 < focusables.Count())
                {
                    focusedElementIndex++;
                    focusedElement = focusables.ElementAt(focusedElementIndex);
                }
            }
            FocusManager.SetFocusedElement(focusScope, focusedElement);
        }

    }

}
