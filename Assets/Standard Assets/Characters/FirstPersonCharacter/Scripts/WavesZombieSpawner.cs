using UnityEngine;

public class WavesZombieSpawner : MonoBehaviour
{
    [Header("Стартанут ли волны сразу при запуске")]
    public bool StartSpawned;
    public static WavesZombieSpawner Instance;
    public GameObject Prefabzombie;

    [System.Serializable]
    public class WaveZombi
    {
        public float DelayStartSpawned;
        public Transform[] SpawnPoints;
        public float DelaySpawnZombi;
        public int MaxCountZombi;
        [Header("Количество заспавненных")]
        public int CountSpawnedZombi;
    }
    public WaveZombi[] WavesZombi;
    public int KilledZombi;

    public int CountStartZombi;
    [Header("Устанавливается сама при старте")]
    public int AllCountZombi;

    [Header("Лучше не трогать")]
    public int CurIndexWave;
    public float tmDelayWave, tmDelaySpawn;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        for (int i = 0; i < WavesZombi.Length; i++)
        {
            AllCountZombi += WavesZombi[i].MaxCountZombi;
        }
        AllCountZombi += CountStartZombi;
    }

    private void Update()
    {
        if(StartSpawned)
        {
            if(CurIndexWave < WavesZombi.Length)
            {
                if(tmDelayWave >= WavesZombi[CurIndexWave].DelayStartSpawned)
                {
                    tmDelaySpawn += Time.deltaTime;
                    if(tmDelaySpawn >= WavesZombi[CurIndexWave].DelaySpawnZombi)
                    {
                        GameObject zombi = Instantiate(Prefabzombie, WavesZombi[CurIndexWave].SpawnPoints[Random.Range(0, WavesZombi[CurIndexWave].SpawnPoints.Length)].position,
                            Prefabzombie.transform.rotation);
                        zombi.GetComponent<ZombieControl>().State = ZombieControl.States.Run;
                        WavesZombi[CurIndexWave].CountSpawnedZombi++;
                        if (WavesZombi[CurIndexWave].CountSpawnedZombi >= WavesZombi[CurIndexWave].MaxCountZombi)
                        {
                            tmDelayWave = 0;
                            CurIndexWave++;
                        }
                        tmDelaySpawn = 0;
                    }
                }
                else
                {
                    tmDelayWave += Time.deltaTime;
                }
            }
        }
    }

    public void StartWavesZombi ()
    {
        StartSpawned = true;
    }

    public void AddKilledZombie ()
    {
        KilledZombi++;

        if(KilledZombi >= AllCountZombi)
        {
            GameManager.Instance.ShowWin();
        }
    }
}
