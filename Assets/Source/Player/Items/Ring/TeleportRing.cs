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

    public RaycastHit Hit;

    private bool _ableToTeleport;

    private float _defaultGravitiy;


    protected override void OnRingAbilityBindStarted(InputAction.CallbackContext obj)
    {
        _defaultGravitiy = PlayerCharacter.Instance.PlayerController.gravity;
        PlayerCharacter.Instance.TimeStop.SetTimeValue(0.5f, true);
        _disposable.Clear();
        _blinkEffect.gameObject.SetActive(true);

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (Physics.Raycast(_camera.position, _camera.forward, out Hit, _range, _teleportLayerMask))
            {
                _blinkEffect.transform.position = Hit.point;
                _blinkEffect.transform.up = Hit.normal;
                _ableToTeleport = true;
                return;
            }

            _ableToTeleport = false;
        }).AddTo(_disposable);
    }

    protected override void CancelAction()
    {
        PlayerCharacter.Instance.TimeStop.SetTimeValue(1, false);
        PlayerCharacter.Instance.PlayerController.gravity = _defaultGravitiy;
        _blinkEffect.gameObject.SetActive(false);
        _disposable.Clear();
        _ableToTeleport = false;
    }

    protected override void OnRingAbilityBindCanceled(InputAction.CallbackContext obj)
    {
        PlayerCharacter.Instance.TimeStop.SetTimeValue(1, false);

        _disposable.Clear();
        if (_ableToTeleport)
        {
            _blinkEffect.Blinked();
        }

        _blinkEffect.gameObject.SetActive(false);
    }
}