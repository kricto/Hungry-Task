using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(DestroySelf), 1f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
