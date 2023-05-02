using UnityEngine;

public class AnimationLogic : MonoBehaviour
{
    [SerializeField] string inJumpBoolName;
    [SerializeField] string runBoolName;

    [SerializeField] Animator animator;

    private Control _control;

    private bool _isRun
    {
        get {
            return _control._state == Control.State.Move && _control.isGrounded() == true;
        }
    }
    private bool _isJump
    {
        get {
            return _control._state == Control.State.inJump;
        }
    }

    private void Start()
    {
        _control = GetComponent<Control>();
    }

    private void Update()
    {
        animator.SetBool(inJumpBoolName, _isJump);
        animator.SetBool(runBoolName, _isRun);

        if (_control.getMoveDirection() > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (_control.getMoveDirection() < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
