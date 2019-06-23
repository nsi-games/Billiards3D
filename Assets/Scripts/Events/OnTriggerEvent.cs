﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class OnTriggerEvent : MonoBehaviour
{
    public string hitTag = "Ball";
    public UnityEvent onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == hitTag)
        {
            onTriggerEnter.Invoke();
            Destroy(other.gameObject);
        }
    }
}
