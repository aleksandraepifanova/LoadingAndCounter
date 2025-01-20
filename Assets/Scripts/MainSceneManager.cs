using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MainSceneManager : MonoBehaviour
{
    public TMP_Text welcomeText;        // ����� �����������
    public TMP_Text counterText;        // ����� �� ������ ��������
    public Button incrementButton;     // ������ "��������� �������"
    public Button updateContentButton; // ������ "�������� �������"

    private int counter = 0;           // �������� ��������
    private const string counterFileName = "counter.txt";
    private const string settingsFileName = "Settings.json";
    private const string messageFileName = "Message.json";

    void Start()
    {
        LoadCounter();
        LoadWelcomeMessage();

        // ��������� ����� ������ ��������
        UpdateCounterText();

        // ������������� �� ������� ������
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
            // ��������� ��������� �������� �� Settings.json
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
        // ����� ����� ������ ��� ���������� JSON � Asset Bundle
        Debug.Log("Update content triggered!");
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
