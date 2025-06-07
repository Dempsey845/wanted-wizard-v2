using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }

    public List<EnemyBulletSO> enemyBulletSOs = new();

    [SerializeField] TMP_Text bulletCountText;
    [SerializeField] int maxBullets = 8;
    [SerializeField] float firebackRadius = 3f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void AddBullet(EnemyBulletSO enemyBulletSO)
    {
        if (enemyBulletSOs.Count >= maxBullets) return;

        enemyBulletSOs.Add(enemyBulletSO);
        UpdateCountText();
    }

    public void UpdateCountText()
    {
        bulletCountText.text = enemyBulletSOs.Count.ToString() + "/" + maxBullets.ToString();
    }

    public int GetMaxBulletCount() { return maxBullets; }
    public float GetFirebackRadius() { return firebackRadius; }
}
