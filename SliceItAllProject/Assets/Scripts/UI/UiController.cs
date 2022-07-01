using UnityEngine;

namespace UI
{
    public class UiController
    {
        protected static T LoadView<T>(string name) where T : MonoBehaviour
        {
            var view = Object.Instantiate(Resources.Load<T>(name));
            Object.DontDestroyOnLoad(view);

            return view;
        }
    }
}