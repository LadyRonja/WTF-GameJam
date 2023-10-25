using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    private static int Idle;// = Animator.StringToHash("temp_p_idle");
    private static int Run;// = Animator.StringToHash("temp_p_walk");
    private static int Jump;// = Animator.StringToHash("temp_p_jump");
    private static int Fall;// = Animator.StringToHash("temp_p_fall");

    private void Awake()
    {
        Idle = Animator.StringToHash("temp_p_idle");
        Run = Animator.StringToHash("temp_p_walk");
        Jump = Animator.StringToHash("temp_p_jump");
        Fall = Animator.StringToHash("temp_p_fall");
    }

    public void SetAnimation(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                animator.CrossFade(Idle, 0, 0);
                break;
            case PlayerState.Run:
                animator.CrossFade(Run, 0, 0);
                break;
            case PlayerState.Jump:
                animator.CrossFade(Jump, 0, 0);
                break;
            case PlayerState.Fall:
                animator.CrossFade(Fall, 0, 0);
                break;
            default:
                Debug.LogError("Reached end of switch-state-machine, new cases not added?");
                Debug.Log("Forcing Idle state");
                SetAnimation(PlayerState.Idle);
                break;
        }
    }

    public void SetFlip(bool setLeft)
    {
        spriteRenderer.flipX = setLeft;
    }

}
