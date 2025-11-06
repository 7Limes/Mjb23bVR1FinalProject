using UnityEngine;

public class SparkBolt : LifetimeProjectile {
    protected override void OnExpire() {
        Debug.Log("Spark bolt expired, summon small explosion");
        base.OnExpire();
    }
}

