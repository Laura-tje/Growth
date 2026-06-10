using UnityEngine;

public class _Jump_End_Sound : StateMachineBehaviour
{

    [SerializeField] private SoundType _Jump_Sound;

    [SerializeField, Range(0, 1f)] private float _Volume = 1.0f;


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Sound_Manage_III._Play_Sound(_Jump_Sound, _Volume);

    }

}
