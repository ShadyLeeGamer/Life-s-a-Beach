using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Collections;
using TMPro;
using Bundos.WaterSystem;

public class WaveManager : MonoBehaviour
{
    [Serializable]
    public struct EnemyDefines
    {
#if UNITY_EDITOR
        [HideInInspector] public string name;
#endif
        public Enemy prefab;
        [ColorUsage(false)] public Color colour;
    }

    [Serializable]
    public struct Wave
    {
        public Gradient probablities;

        public int numSpawns;
        public float spawnDelay;
    }

    [SerializeField] EnemyDefines[] enemies;

    [SerializeField] Wave[] waves;
    int currentWaveIndex;
    int currentEnemyCount;
    [SerializeField] Water water;

    [Header("UI")]
    [SerializeField] TextMeshProUGUI waveCounter;
    [SerializeField] TextMeshProUGUI enemiesLeftCounter;
    [SerializeField] Canvas endWaveCanvas;

    Dictionary<string, Enemy> enemyPrefabDict;

    public event Action OnWaveStart;
    public event Action OnWaveEnd;

    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < enemies.Length; i++)
        {
            string hexString = enemies[i].colour.ToHexString();
            hexString = hexString.Substring(0, hexString.Length - 2) + "FF";
            enemyPrefabDict.Add(hexString, enemies[i].prefab);
        }

        ResetWaves();
    }

    [ContextMenu("NextWave")]
    public void NextWave()
    {
        currentWaveIndex++;
        OnWaveStart?.Invoke();
        StartCoroutine(RunWave(waves[currentWaveIndex]));
    }

    IEnumerator RunWave(Wave wave)
    {
        waveCounter.text = $"Wave {currentWaveIndex + 1}";
        currentEnemyCount = wave.numSpawns;
        RefreshEnemiesLeftCounter();

        // Spawn Enemies
        for (int spawnCount = 0; spawnCount < wave.numSpawns; spawnCount++)
        {
            Color randomColour = wave.probablities.Evaluate(UnityEngine.Random.value);
            SpawnEnemy(enemyPrefabDict[randomColour.ToHexString()]);

            yield return new WaitForSeconds(wave.spawnDelay);
        }
    }

    void SpawnEnemy(Enemy prefab)
    {
        Enemy enemy = Instantiate(prefab, water.GetRandomPoint(), Quaternion.identity);
        enemy.OnKill += OnEnemyDeath;
    }

    public void OnEnemyDeath()
    {
        currentEnemyCount--;
        RefreshEnemiesLeftCounter();
        if (currentEnemyCount <= 0)
        {
            endWaveCanvas.gameObject.SetActive(true);
            OnWaveEnd?.Invoke();
        }
    }

    void RefreshEnemiesLeftCounter()
    {
        enemiesLeftCounter.text = $"{currentEnemyCount} Enemies Left";
    }

    public void ResetWaves()
    {
        currentWaveIndex = -1;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].prefab)
            {
                enemies[i].name = enemies[i].prefab.name;
            }
        }
    }
#endif
}