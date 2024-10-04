using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    const int k_startSceneIndex = 0;
    const int k_mainSceneIndex = 1;

    [SerializeField] TMP_Text m_bestScoreInfoText;
    [SerializeField] TMP_InputField m_nameInput;
    [SerializeField] TMP_Text m_noNameErrorText;
    [SerializeField] Button m_startButton;
    [SerializeField] Button m_exitButton;

    void Awake()
    {
        m_startButton.onClick.AddListener(LoadMainScene);
        m_exitButton.onClick.AddListener(ExitApplication);
    }

    void Start()
    {
        m_bestScoreInfoText.text = DataManager.Instance.BestScore.ToString();
        m_nameInput.text = DataManager.Instance.Name;
    }

    public void LoadMainScene()
    {
        if (m_nameInput.text == string.Empty)
        {
            if (!m_noNameErrorText.gameObject.activeInHierarchy)
            {
                m_noNameErrorText.gameObject.SetActive(true);
            }

            return;
        }
        else if (m_noNameErrorText.gameObject.activeInHierarchy)
        {
            m_noNameErrorText.gameObject.SetActive(false);
        }

        DataManager.Instance.Name = m_nameInput.text;
        DataManager.Instance.SaveName();

        SceneManager.LoadScene(k_mainSceneIndex);
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
