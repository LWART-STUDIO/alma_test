using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Panel:MonoBehaviour,IPanel
    {
        private CanvasGroup _canvasGroup;
        private Tween _tween;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        public void Show()
        {
            _tween.Kill();
            _tween=_canvasGroup.DOFade(1, 0.2f);
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _tween.Kill();
            _tween=_canvasGroup.DOFade(0, 0.2f).OnComplete(()=>gameObject.SetActive(false));
        }
    }
}