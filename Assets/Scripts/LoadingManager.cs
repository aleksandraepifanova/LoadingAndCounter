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

    void Start()
    {
        StartCoroutine(LoadContent());
    }

    private IEnumerator LoadContent()
    {
        // Искусственная задержка для демонстрации загрузки
        yield return new WaitForSeconds(1f);

        // Загружаем JSON-файлы
        string settingsPath = Path.Combine(Application.streamingAssetsPath, settingsFileName);
        string messagePath = Path.Combine(Application.streamingAssetsPath, messageFileName);

        if (File.Exists(settingsPath) && File.Exists(messagePath))
        {
            // Читаем файлы
            string settingsJson = File.ReadAllText(settingsPath);
            string messageJson = File.ReadAllText(messagePath);

            // Обрабатываем файлы (распарсим в будущем)
            Debug.Log($"Settings: {settingsJson}");
            Debug.Log($"Message: {messageJson}");
        }
        else
        {
            Debug.LogError("JSON files not found in StreamingAssets!");
        }

        // Загружаем Asset Bundle
        string assetBundlePath = Path.Combine(Application.streamingAssetsPath, "example.bundle");
        if (File.Exists(assetBundlePath))
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(assetBundlePath);
            if (bundle != null)
            {
                Debug.Log("Asset Bundle loaded successfully!");
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

        // Обновляем прогресс-бар и текст
        progressBar.value = 1f;
        progressText.text = "Loading... 100%";

        // Искусственная задержка перед переходом на основной экран
        yield return new WaitForSeconds(1f);

        // Переход на основную сцену
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
