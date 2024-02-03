using UnityEngine;

public class AnimatorControl : MonoBehaviour
{
    private PlayerControl _playerControl;
    private Animator _animator;

    private void Start()
    {
        _playerControl = PlayerControl.Instance;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_playerControl.Death) _animator.SetBool("Death", true);

        _animator.SetBool("Xmove", _playerControl.MoveX != 0);

        int Yvector = 0;
        if (Mathf.Abs(_playerControl.NowYVelocity) > 0.1f)
        {
            Yvector = _playerControl.NowYVelocity > 0 ? 1 : -1;
        }
        _animator.SetInteger("Yvector", Yvector);
    }

    private void LateUpdate()
    {
        _animator.SetBool("DoubleJump", !_playerControl.DoubleJump && _playerControl.LestJump);//!playerController.doubleJump && 
    }
}
