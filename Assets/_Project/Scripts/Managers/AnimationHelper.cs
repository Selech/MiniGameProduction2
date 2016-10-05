using UnityEngine;
using System.Collections;

public class AnimationHelper : StateMachineBehaviour
{
    public bool PlayingSound;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (PlayingSound)
        {
            EventManager.Instance.TriggerEvent(new IntroVO2event());
        }
        else
        {
            EventManager.Instance.TriggerEvent(new IntroVO3event());
        }
    }

}
