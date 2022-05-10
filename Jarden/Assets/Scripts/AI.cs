using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AI : MonoBehaviour
{
    // todo: ai (ml agents? | likely just basic ai)
    // todo: ai physics (unity? | rigid bodys)
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
        // Check radius for enemy (assign target | circular/potentially only if "aggrod")
        // Jump at enemy (animation+force | play animation, then jump, then landing animation)
        // Fix position (animation+ralign | play animation to readjust self, procedural ani?)
        // Wait (arbitrarytime | maybe randomized for fun)
        // Jump is ready (repeat | clear flags/jump lock)
    }

    public void OnFire()
    {
        JumpAtTarget();
    }

    public void JumpAtTarget() {
        var heading = target.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        // some push force that sends it towards an object
        body.AddForce(direction * boostPower, ForceMode.Impulse);
    }
}
