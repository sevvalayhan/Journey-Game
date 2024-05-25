using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [field:SerializeField] public Animator WatermelonAnimator { get; private set;}    
    public static AnimationController Instance { get; }

}
