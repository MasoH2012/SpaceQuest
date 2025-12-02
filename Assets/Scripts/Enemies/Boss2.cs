using UnityEngine;

public class Boss2 : Enemy
{
    private Animator animator;
    public bool charging = true;

    public override void OnEnable() {
        base.OnEnable();
        EnterIdleState();
    }

    public override void Start()
    {
        base.Start();
        destroyEffectPool = GameObject.Find("Boom3Pool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.hitArmor;
        destroySound = AudioManager.Instance.boom2;
    }

    public override void Update() {
        base.Update();
        if (transform.position.y > 4 || transform.position.y < -4) {
            speedY *= -1;
        }

        if (transform.position.x > 7.5) {
            EnterIdleState();
        } else if (transform.position.x < -5) {
            EnterChargeState();
        }
    }

    private void EnterIdleState() {
        if (charging) {
            speedX = 0.2f;
            speedY = Random.Range(-1.2f, 1.2f);
            charging = false;
        }
    }

    private void EnterChargeState() {
        if (!charging) {
            speedX = Random.Range(3.5f, 4);
            speedY = 0;
            charging = true;
        }
    }
}
