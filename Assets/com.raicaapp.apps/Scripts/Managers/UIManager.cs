using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance 
    { 
        get => FindObjectOfType<UIManager>(); 
    }

    private float count;
    private GameObject _gameRef;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject game;
    [SerializeField] GameObject settings;

    [Space(10)]
    [SerializeField] GameObject pause;
    [SerializeField] GameObject gameover;

    [Space(10)]
    [SerializeField] Text countText;
    [SerializeField] GameObject pauseGo;


    private void Update()
    {
        count += Time.deltaTime;
        countText.text = $"{count:N}m";
    }

    private void Start()
    {
        OpenMenu();
    }

    public void SetPause(bool IsPause)
    {
        Time.timeScale = IsPause ? 0.0f : 1.0f;
        pause.SetActive(IsPause);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameover.SetActive(true);
        pauseGo.SetActive(false);

        if(SettingsManager.VibraEnbled)
        {
            Handheld.Vibrate();
        }

        GameObject.Find("SFXSource").GetComponent<AudioSource>().Play();
    }

    public void OpenSttings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void StartGame()
    {
        count = 0;

        Time.timeScale = 1.0f;

        gameover.SetActive(false);
        pauseGo.SetActive(true);

        if (_gameRef)
        {
            Destroy(_gameRef);
        }

        var _parent = GameObject.Find("Environment").transform;
        var _prefab = Resources.Load<GameObject>("level");

        _gameRef = Instantiate(_prefab, _parent);

        menu.SetActive(false);
        game.SetActive(true);
    }

    public void OpenMenu()
    {
        Time.timeScale = 1.0f;
        pause.SetActive(false);

        if(_gameRef)
        {
            Destroy(_gameRef);
        }

        settings.SetActive(false);
        game.SetActive(false);
        menu.SetActive(true);
    }
}
