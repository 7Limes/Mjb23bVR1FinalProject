using UnityEngine;

public class CapsuleTrigger : PayloadProjectile {
    private float breakForce;

    public void SetBreakForce(float newBreakForce) {
        breakForce = newBreakForce;
    }

    protected override void OnCollisionEnter(Collision collision) {
        Debug.Log("Impulse: " + collision.impulse.magnitude);
        if (collision.impulse.magnitude >= breakForce) {
            base.OnCollisionEnter(collision); 
        }
    }
}