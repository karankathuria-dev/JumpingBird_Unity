using UnityEngine;

public class ResetAnimation : MonoBehaviour
{
    private Animator mAnimator;

    private void Start()
    {
        //  mAnimator = GetComponent<Animator>();
        mAnimator = GetComponent<Animator>();
        mAnimator.Play("birdhome" , -1 , 0f);
    }
    private void OnEnable()
    {
      //  mAnimator = GetComponent<Animator>();
      //  mAnimator.Play("birdhome",-1,0f);
    }
}
