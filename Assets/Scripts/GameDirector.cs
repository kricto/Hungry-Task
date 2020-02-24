using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    private static GameDirector instance;

    public static GameDirector Instance => instance;

    [SerializeField] private CameraHandler cameraHandler;
    [SerializeField] private Transform playerPosition;

    [Space]
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private float leftXBorder;
    [SerializeField] private float downYBorder;
    [SerializeField] private float rightXBorder;
    [SerializeField] private float upYBorder;

    [Space]
    [SerializeField] private Text scoreText;
    private int score = 0;
    [SerializeField] private Text targetsCounter;
    private int targetsCount = 0;

    private bool shouldStop = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        cameraHandler.Setup(() => playerPosition.position);

        StartCoroutine(nameof(SpawnTarget));
    }

    private void OnDestroy()
    {
        shouldStop = true;
    }

    public void IncreaseScore(int number)
    {
        score += number;
        scoreText.text = score.ToString();

        targetsCount--;
        targetsCounter.text = targetsCount.ToString();
    }

    private IEnumerator SpawnTarget()
    {
        while (!shouldStop)
        {
            yield return new WaitForSecondsRealtime(1f);

            if (targetsCount <= 40)
            {
                float positionY = Random.Range(downYBorder, upYBorder);
                float positionX = Random.Range(leftXBorder, rightXBorder);

                Instantiate(targetPrefab, new Vector3(positionX, positionY, 0f), transform.rotation);

                targetsCount++;
                targetsCounter.text = targetsCount.ToString();
            }
        }
    }
}
