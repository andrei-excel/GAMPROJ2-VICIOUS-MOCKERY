using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialWaveSpawner : MonoBehaviour
{
    [SerializeField] private TutorialWave[] tutorialWaves;

    [SerializeField] private LevelDataManager lvlDataManager;

    [Header("Wave Indicator")]
    [SerializeField] private GameObject waveIndicatorBanner;
    [SerializeField] private TextMeshProUGUI waveIndicatorText;
    [SerializeField] private float waveIndicatorTimer = 1.0f;

    [Header("SpawnPoints")]
    [SerializeField] private Transform tutorialSpawnPoint;

    [Header("Time Between Wave")]
    [SerializeField] private float timeBetweenWaves;
    private float waveCountdown;
    public int nextWave = 0;

    private float enemySearchCountdown = 1f;

    private TutorialSpawnState state = TutorialSpawnState.Counting;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        waveIndicatorText.enabled = false;
        waveIndicatorBanner.SetActive(false);
    }

    private void Update()
    {
        if (state == TutorialSpawnState.Waiting)
        {
            if (!lvlDataManager.IsGameOver)
            {
                if (!EnemyIsAlive())
                {
                    // If all enemies from last wave is dead, start next wave
                    StartNextWave();
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != TutorialSpawnState.Spawning)
            {
                StartCoroutine(WaveIndicator(waveIndicatorTimer));
                StartCoroutine(SpawnWave(tutorialWaves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void StartNextWave()
    {
        state = TutorialSpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > tutorialWaves.Length - 1)
        {
            state = TutorialSpawnState.Spawning;
            lvlDataManager.GameOverWin();

            Debug.Log("All waves complete");
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        enemySearchCountdown -= Time.deltaTime;

        if (enemySearchCountdown <= 0f)
        {
            enemySearchCountdown = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator WaveIndicator(float timer)
    {
        FindObjectOfType<AudioManager>().Play("Wave_Indicator_SFX");
        waveIndicatorText.text = tutorialWaves[nextWave].name;
        waveIndicatorBanner.SetActive(true);
        waveIndicatorText.enabled = true;

        yield return new WaitForSeconds(timer);

        waveIndicatorBanner.SetActive(false);
        waveIndicatorText.enabled = false;
    }

    IEnumerator SpawnWave(TutorialWave _wave)
    {
        state = TutorialSpawnState.Spawning;

        foreach (TutorialWaveContent TWC in _wave.waveContent)
        {
            for (int i = 0; i < TWC.count; i++)
            {
                SpawnEnemy(TWC.enemyPrefab);
                yield return new WaitForSeconds(_wave.timeBetweenSpawn);
            }
        }

        state = TutorialSpawnState.Waiting;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        EnemyData type;

        type = _enemy.GetComponent<Enemy>().enemyData;

        Instantiate(_enemy, tutorialSpawnPoint.position, Quaternion.Euler(30, 0, 0));
    }
}

[System.Serializable]
public class TutorialWave
{
    public string name;
    public float timeBetweenSpawn;
    public List<TutorialWaveContent> waveContent;
}

[System.Serializable]
public class TutorialWaveContent
{
    public GameObject enemyPrefab;
    public int count;
}

public enum TutorialSpawnState
{
    Spawning,
    Waiting,
    Counting
};
