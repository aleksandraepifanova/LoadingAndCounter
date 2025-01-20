using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar;          // Прогресс-бар
    public TMP_Text progressText;       // Текст прогресса

    private const string settingsFileName = "Settings.json";
    private const string messageFileName = "Message.json";
    private const string assetBundleName = "example.bundle";

    public static AssetBundle loadedBundle; // Глобальная ссылка на Asset Bundle

    void Start()
    {
        StartCoroutine(LoadContent());
    }

    private IEnumerator LoadContent()
    {
        // Искусственная задержка
        yield return new WaitForSeconds(1f);

        // Загрузка JSON-файлов
        string settingsPath = Path.Combine(Application.streamingAssetsPath, settingsFileName);
        string messagePath = Path.Combine(Application.streamingAssetsPath, messageFileName);

        if (File.Exists(settingsPath) && File.Exists(messagePath))
        {
            string settingsJson = File.ReadAllText(settingsPath);
            string messageJson = File.ReadAllText(messagePath);

            Debug.Log($"Settings: {settingsJson}");
            Debug.Log($"Message: {messageJson}");
        }
        else
        {
            Debug.LogError("JSON files not found in StreamingAssets!");
        }

        // Загрузка Asset Bundle
        string assetBundlePath = Path.Combine(Application.streamingAssetsPath, assetBundleName);
        if (File.Exists(assetBundlePath))
        {
            loadedBundle = AssetBundle.LoadFromFile(assetBundlePath);
            if (loadedBundle != null)
            {
                Debug.Log("Asset Bundle loaded successfully!");
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogError("Failed to load Asset Bundle!");
            }
        }
        else
        {
            Debug.LogError("Asset Bundle not found in StreamingAssets!");
        }

        // Обновляем прогресс-бар
        progressBar.value = 1f;
        progressText.text = "Loading... 100%";

        // Искусственная задержка
        yield return new WaitForSeconds(1f);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
