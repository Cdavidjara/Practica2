using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform objective;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(objective);
    }
}
