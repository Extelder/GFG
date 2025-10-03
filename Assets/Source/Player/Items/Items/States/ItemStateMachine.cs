using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ItemStateMachine : StateMachine
{
    [Header("States")] [SerializeField] private State _idle;
    [SerializeField] private State _move;

    [Space(15)] [SerializeField] private CharacterController _groundChecker;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public override void OnEnable()
    {
        base.OnEnable();

        Observable.EveryUpdate().Subscribe(_ =>
        {
            float playerHorizontal = Mathf.Abs(PlayerCharacter.Instance.Binds.Character.Horizontal.ReadValue<float>());
            float playerVertical = Mathf.Abs(PlayerCharacter.Instance.Binds.Character.Vertical.ReadValue<float>());
            bool movingOnGround =
                (playerHorizontal > 0 || playerVertical > 0) && _groundChecker.isGrounded;

            if (movingOnGround)
            {
                ChangeState(_move);
                return;
            }

            ChangeState(_idle);
        }).AddTo(_disposable);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}