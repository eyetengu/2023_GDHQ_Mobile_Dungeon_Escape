using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    //handle to the spider
    private Spider _spiderScript;

    private void Start()
    {
        _spiderScript = transform.parent.GetComponent<Spider>();
    }

    public void Fire()
    {
        _spiderScript.Attack();
    }
}
