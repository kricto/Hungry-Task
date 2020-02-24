using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject bulletHitEffect;
    [SerializeField] private GameObject targetHitEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GameDirector.Instance.IncreaseScore(20);

            Instantiate(bulletHitEffect, collision.transform.position, collision.transform.rotation);

            Destroy(collision.gameObject);

            Instantiate(targetHitEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
