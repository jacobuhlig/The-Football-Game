using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public AudioSource audioSource;
    public AudioClip whistleClip;

    // References to the prefab
    public GameObject sceneTextPrefab;
    private TextMeshProUGUI sceneText;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(3);

    public GameObject timerTextPrefab;
    private TextMeshProUGUI timerText;
    private GameObject timerInstance;
    private float levelStartTime;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Menu")
        {
            SetupIntroductionToLevel(scene);
            SetupTimer();
        }
    }

    void SetupIntroductionToLevel(Scene scene)
    {
        GameObject instance = Instantiate(sceneTextPrefab);

        sceneText = instance.GetComponentInChildren<TextMeshProUGUI>();

        DontDestroyOnLoad(instance);

        sceneText.text = scene.name.Replace('_', ' ');

        StartCoroutine(ClearSceneTextAfterDelay());
    }

    void SetupTimer()
    {
        audioSource.clip = whistleClip;
        audioSource.Play();

        if (timerInstance != null)
        {
            Destroy(timerInstance);
        }

        timerInstance = Instantiate(timerTextPrefab);
        timerText = timerInstance.GetComponentInChildren<TextMeshProUGUI>();
        DontDestroyOnLoad(timerInstance);

        levelStartTime = Time.time;
    }

    IEnumerator ClearSceneTextAfterDelay()
    {
        yield return waitForSeconds;
        sceneText.text = string.Empty;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

        if (timerText != null)
        {
            float elapsed = Time.time - levelStartTime;
            int minutes = Mathf.FloorToInt(elapsed / 60F);
            int seconds = Mathf.FloorToInt(elapsed - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            timerText.text = niceTime;
        }
        else
        {
            Debug.Log("something happened");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
