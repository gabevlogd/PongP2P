namespace GV.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster))]
    [RequireComponent(typeof(Animator))]
    public class UIWindow : MonoBehaviour
    {
        protected UIManager _relatedUIManager;
        protected Animator _animator;

        private void Awake()
        {
            _relatedUIManager = GetComponentInParent<UIManager>();
            _animator = GetComponent<Animator>();
        }

        public void OpenWindow() => gameObject.SetActive(true); 
        public void CloseWindow() => gameObject.SetActive(false);
    }
}

