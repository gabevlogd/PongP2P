namespace GV.Patterns
{
    using UnityEngine;

    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T This { get => _this; }
        private static T _this;

        protected virtual void Awake()
        {
            if (_this == null)
            {
                if (!TryGetComponent<T>(out _this))
                {
                    _this = gameObject.AddComponent<T>();
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

