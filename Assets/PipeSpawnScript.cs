using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    public GameObject pipe;
    public float baseSpawnRate = 1.5f; // Le spawn rate initial
    public float spawnRate;
    private float timer = 0;
    public float heightOffset = 10;
    [SerializeField] private LogicScript logic;
    public float minSpawnRate = 0.8f; // Limite minimale

    void Start()
    {
        spawnRate = baseSpawnRate;
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        int currentScore = logic.playerScore;
        int multiplierSteps = currentScore / 10; // 1 step tous les 10 points
        float rateMultiplier = 1f - (0.1f * multiplierSteps); // -10% par palier
        spawnRate = Mathf.Max(minSpawnRate, baseSpawnRate * rateMultiplier);
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnPipe();
            timer = 0;
        }
    }


    void spawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;

        Instantiate(pipe, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), transform.rotation);

    }
}
