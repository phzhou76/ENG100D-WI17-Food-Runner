using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour {

    // Reference to the player's animator object.
    public Animator playerAnim;

	void SetAnimator(bool isFemale)
    {
        if(isFemale == true)
        {
            playerAnim.runtimeAnimatorController = Resources.Load("Assets/Falling Food Minigame/Animation/Samantha") as RuntimeAnimatorController;
        }
        else
        {
            playerAnim.runtimeAnimatorController = Resources.Load("Assets/Falling Food Minigame/Animation/Sam") as RuntimeAnimatorController;
        }
    }
}
