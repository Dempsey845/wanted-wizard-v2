using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] EnemyBulletSO bulletSO;

    Rigidbody rb;

    float lifetime;

    const string PLAYER_TAG = "Player";

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * bulletSO.moveSpeed;
    }

    void Update()
    {
        lifetime += Time.deltaTime;

        if (lifetime > bulletSO.maxLifetime) Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            // TODO
        }
        Instantiate(bulletSO.BulletHitFXPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
