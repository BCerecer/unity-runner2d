using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetReady : MonoBehaviour
{
    public delegate void GetReadyFinished();
    public static event GetReadyFinished OnGetReadyFinished;

    void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("right"))
        {
            OnGetReadyFinished();
        }
    }

}
