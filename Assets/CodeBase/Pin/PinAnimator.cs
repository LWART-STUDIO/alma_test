using DG.Tweening;
using UnityEngine;

namespace CodeBase
{
    public class PinAnimator : MonoBehaviour
    {
        private float _time=1f;
        private float _value=1.2f;
        
        private Tween _upScaleTween;
        private Tween _downScaleTween;
        private Tween _scaleTween;
        public void DoScaleUp()
        {
            if (_upScaleTween!=null)
                return;
            _scaleTween?.Kill();
            _downScaleTween?.Kill();
            _upScaleTween=transform.DOScale(_value, _time).OnKill(()=>_upScaleTween=null);

        }

        public void DoScaleDown()
        {
            
            if(_downScaleTween!=null)
                return;
            _scaleTween?.Kill();
            _upScaleTween?.Kill();
            _downScaleTween=transform.DOScale(1f, _time/2).OnKill(()=>_downScaleTween=null);

        }

        public void DoScaleUpAndDown()
        {
            if (_scaleTween!=null)
                return;
            _upScaleTween?.Kill();
            _downScaleTween?.Kill();
            _scaleTween=transform.DOScale(1,_time/2).SetLoops(-1, LoopType.Yoyo).OnKill(()=>_scaleTween=null);

        }

      
    }
}
