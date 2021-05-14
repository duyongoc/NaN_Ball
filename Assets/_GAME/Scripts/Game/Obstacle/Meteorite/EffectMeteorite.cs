﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMeteorite : MonoBehaviour
{
    [Header("Crazy boom effect")]
    public GameObject boomEffect;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Contains("Enemy"))
        {
            var temp = other.GetComponent<IDamage>();
            temp?.TakeDamage(0);

            Instantiate(boomEffect, transform.localPosition, Quaternion.identity);
        }
        else if(other.tag == "Player")
        {
            Instantiate(boomEffect, other.transform.position, Quaternion.identity);

            other.gameObject.SetActive(false);
            // SceneMgr.GetInstance().ChangeState(SceneMgr.GetInstance().m_sceneGameOver);
        }
        else if (other.tag == "Elastic" || other.tag == "Tornado" || other.tag == "Obstacle")
        {
            Instantiate(boomEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject); 
        }     
    }
}