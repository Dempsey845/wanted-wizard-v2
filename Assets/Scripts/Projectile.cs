using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;

    float maxLifetime;
    float lifetime;
    float moveSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Start()
    {
        rb.linearVelocity = transform.forward * moveSpeed;
    }

    void Update()
    {
        lifetime += Time.deltaTime;

        if (lifetime > maxLifetime) Destroy(this.gameObject);
    }

    public void Init(float moveSpeed, float maxLifetime)
    {
        this.moveSpeed = moveSpeed;
        this.maxLifetime = maxLifetime;
    }
}
