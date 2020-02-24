using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroyer : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
