using System.Collections.Generic;
using System.Linq;
using Ui.View;
using UnityEngine;

namespace Ui
{
    public interface IPopupService
    {
        T ShowPopup<T>() where T : AbstractView;
        bool CloseFirstPopup<T>() where T : AbstractView;
        bool CloseLastPopup<T>() where T : AbstractView;
    }

    public class PopupService : IPopupService
    {
        private readonly ViewFactory _viewFactory;

        private readonly List<AbstractView> _activeViews;

        public PopupService(ViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
            _activeViews = new List<AbstractView>();
        }

        public T ShowPopup<T>() where T : AbstractView
        {
            T view = _viewFactory.CreatePopup<T>();
            _activeViews.Add(view);
            return view;
        }

        public bool CloseFirstPopup<T>() where T : AbstractView
        {
            T popup = _activeViews.OfType<T>().FirstOrDefault();
            if (popup == null)
            {
                return false;
            }

            _activeViews.Remove(popup);
            Object.Destroy(popup.gameObject);
            return true;
        }

        public bool CloseLastPopup<T>() where T : AbstractView
        {
            T popup = _activeViews.OfType<T>().LastOrDefault();
            if (popup == null)
            {
                return false;
            }

            _activeViews.Remove(popup);
            Object.Destroy(popup.gameObject);
            return true;
        }
    }
}