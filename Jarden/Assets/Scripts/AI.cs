using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AI : MonoBehaviour
{
    // todo: ai (ml agents?)
    // todo: ai physics (unity?)
    // todo: ai movement
    // todo: ai consumption
    public float boostPower = 10f;
    Rigidbody body;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        
    }

    public void OnFire()
    {
        var heading = target.position - this.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        // some push force that sends it towards an object
        body.AddForce(direction * boostPower, ForceMode.Impulse);
    }
}
