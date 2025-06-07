using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }

    public List<EnemyBulletSO> enemyBulletSOs = new();

    [SerializeField] TMP_Text bulletCountText;
    [SerializeField] int maxBullets = 8;

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
        enemyBulletSOs.Add(enemyBulletSO);
        bulletCountText.text = enemyBulletSOs.Count.ToString();
    }
}
