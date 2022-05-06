using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    // todo: ai (ml agents?)
    // todo: ai physics (unity?)
    // todo: ai movement
    // todo: ai consumption
    public float boostPower = 10f;
    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // some push force that sends it towards an object
        body.AddForce(Vector3.up * boostPower, ForceMode.Impulse);
    }
}
