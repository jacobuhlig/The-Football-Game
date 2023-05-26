using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Reference to the prefab
    public GameObject sceneTextPrefab;
    private TextMeshProUGUI sceneText;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(3);

    public AudioSource audioSource;
    public AudioClip whistleClip;

    public GameObject timerTextPrefab; // Assign in Inspector
    private TextMeshProUGUI timerText;
    private GameObject timerInstance; // keep track of the instance
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
        // Instantiate the sceneTextPrefab and get the reference to the TextMeshProUGUI component
        GameObject instance = Instantiate(sceneTextPrefab);

        sceneText = instance.GetComponentInChildren<TextMeshProUGUI>();

        // Make sure the instantiated prefab doesn't get destroyed when loading a new scene
        DontDestroyOnLoad(instance);

        // Display the scene name
        sceneText.text = scene.name.Replace('_', ' ');

        // Start the coroutine to clear the text after 3 seconds
        StartCoroutine(ClearSceneTextAfterDelay());
    }

    void SetupTimer()
    {
        // Play the whistle sound
        audioSource.clip = whistleClip;
        audioSource.Play();

        // Destroy the old timer instance if it exists
        if (timerInstance != null)
        {
            Destroy(timerInstance);
        }

        // Instantiate a new timer instance
        timerInstance = Instantiate(timerTextPrefab);
        timerText = timerInstance.GetComponentInChildren<TextMeshProUGUI>();
        DontDestroyOnLoad(timerInstance);

        // Reset the start time
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

    // Unsubscribe when the game object is destroyed
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
