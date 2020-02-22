﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GameDirector.Instance.IncreaseScore(20);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
