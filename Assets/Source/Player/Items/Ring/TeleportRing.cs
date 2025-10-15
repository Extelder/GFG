using UnityEngine;
using UniRx;
using UnityEngine.InputSystem;

public class TeleportRing : PlayerRing
{
    [SerializeField] private Transform _blinkPoint;

    [SerializeField] private Blink _blinkEffect;

    [SerializeField] private Transform _camera;
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _teleportLayerMask;

    private CompositeDisposable _disposable = new CompositeDisposable();

    private RaycastHit _hit;

    private bool _ableToTeleport;

    protected override void OnRingAbilityBindStarted(InputAction.CallbackContext obj)
    {
        _disposable.Clear();
        _blinkEffect.gameObject.SetActive(true);

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Physics.Raycast(_camera.position, _camera.forward, out _hit, _range, _teleportLayerMask))
            {
                _blinkEffect.transform.position = _hit.point;
                _blinkEffect.transform.up = _hit.normal;
                _ableToTeleport = true;
                return;
            }

            _ableToTeleport = false;
        }).AddTo(_disposable);
    }

    protected override void CancelAction()
    {
        _blinkEffect.gameObject.SetActive(false);
        _disposable.Clear();
        _ableToTeleport = false;
    }

    protected override void OnRingAbilityBindCanceled(InputAction.CallbackContext obj)
    {
        _disposable.Clear();
        if (_ableToTeleport)
        {
            _blinkEffect.Blinked();
        }
    }
}