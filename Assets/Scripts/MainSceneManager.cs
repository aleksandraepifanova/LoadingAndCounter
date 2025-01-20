using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Collections;

public class MainSceneManager : MonoBehaviour
{
    public TMP_Text welcomeText;        // Текст приветствия
    public TMP_Text counterText;        // Текст на кнопке счётчика
    public Button incrementButton;     // Кнопка "Увеличить счётчик"
    public Button updateContentButton; // Кнопка "Обновить контент"

    private int counter = 0;           // Значение счётчика
    private const string counterFileName = "counter.txt";
    private const string settingsFileName = "Settings.json";
    private const string messageFileName = "Message.json";
    private const string assetBundleName = "example.bundle";

    void Start()
    {
        LoadCounter();
        LoadWelcomeMessage();
        LoadInitialButtonBackground();
        UpdateCounterText();

        incrementButton.onClick.AddListener(IncrementCounter);
        updateContentButton.onClick.AddListener(UpdateContent);
    }

    private void LoadCounter()
    {
        string counterPath = Path.Combine(Application.persistentDataPath, counterFileName);
        if (File.Exists(counterPath))
        {
            counter = int.Parse(File.ReadAllText(counterPath));
        }
        else
        {
            string settingsPath = Path.Combine(Application.streamingAssetsPath, settingsFileName);
            if (File.Exists(settingsPath))
            {
                string settingsJson = File.ReadAllText(settingsPath);
                counter = JsonUtility.FromJson<Settings>(settingsJson).startingNumber;
            }
            else
            {
                Debug.LogError("Settings.json not found!");
            }
        }
    }

    private void LoadWelcomeMessage()
    {
        string messagePath = Path.Combine(Application.streamingAssetsPath, messageFileName);
        if (File.Exists(messagePath))
        {
            string messageJson = File.ReadAllText(messagePath);
            welcomeText.text = JsonUtility.FromJson<Message>(messageJson).welcomeMessage;
        }
        else
        {
            Debug.LogError("Message.json not found!");
        }
    }

    private void LoadInitialButtonBackground()
    {
        if (LoadingManager.loadedBundle != null)
        {
            Sprite initialSprite = LoadingManager.loadedBundle.LoadAsset<Sprite>("ExampleSprite");
            if (initialSprite != null)
            {
                incrementButton.GetComponent<Image>().sprite = initialSprite;
                Debug.Log("Initial button background loaded!");
            }
            else
            {
                Debug.LogError("Sprite not found in Asset Bundle!");
            }
        }
        else
        {
            Debug.LogError("No Asset Bundle loaded from LoadingManager!");
        }
    }

    private void IncrementCounter()
    {
        counter++;
        UpdateCounterText();
        SaveCounter();
    }

    private void UpdateCounterText()
    {
        counterText.text = $"Increase Counter: {counter}";
    }

    private void SaveCounter()
    {
        string counterPath = Path.Combine(Application.persistentDataPath, counterFileName);
        File.WriteAllText(counterPath, counter.ToString());
    }

    private void UpdateContent()
    {
        StartCoroutine(UpdateContentCoroutine());
    }

    private IEnumerator UpdateContentCoroutine()
    {
        // Искусственная задержка
        yield return new WaitForSeconds(1f);

        // Загружаем обновлённые JSON-файлы
        string settingsPath = Path.Combine(Application.streamingAssetsPath, settingsFileName);
        string messagePath = Path.Combine(Application.streamingAssetsPath, messageFileName);

        if (File.Exists(settingsPath) && File.Exists(messagePath))
        {
            string settingsJson = File.ReadAllText(settingsPath);
            string messageJson = File.ReadAllText(messagePath);

            counter = JsonUtility.FromJson<Settings>(settingsJson).startingNumber;
            UpdateCounterText();

            welcomeText.text = JsonUtility.FromJson<Message>(messageJson).welcomeMessage;

            Debug.Log("Settings and welcome message updated!");
        }
        else
        {
            Debug.LogError("Updated JSON files not found in StreamingAssets!");
        }

        if (LoadingManager.loadedBundle != null)
        {
            Debug.Log("Unloading existing Asset Bundle...");
            LoadingManager.loadedBundle.Unload(true);
            LoadingManager.loadedBundle = null;
        }

        // Загружаем новый Asset Bundle
        string assetBundlePath = Path.Combine(Application.streamingAssetsPath, assetBundleName);
        if (File.Exists(assetBundlePath))
        {
            AssetBundle newBundle = AssetBundle.LoadFromFile(assetBundlePath);
            if (newBundle != null)
            {
                LoadingManager.loadedBundle = newBundle;

                Sprite newSprite = newBundle.LoadAsset<Sprite>("ExampleSprite");
                if (newSprite != null)
                {
                    incrementButton.GetComponent<Image>().sprite = newSprite;
                    Debug.Log("Button background updated!");
                }
                else
                {
                    Debug.LogError("Sprite not found in updated Asset Bundle!");
                }
            }
            else
            {
                Debug.LogError("Failed to load updated Asset Bundle!");
            }
        }
        else
        {
            Debug.LogError("Updated Asset Bundle not found in StreamingAssets!");
        }
    }

    [System.Serializable]
    private class Settings
    {
        public int startingNumber;
    }

    [System.Serializable]
    private class Message
    {
        public string welcomeMessage;
    }
}
