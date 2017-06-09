using UnityEngine;
using UnityEngine.UI;

public class MusicItemControl : MonoBehaviour
{
    private string text;

    public string FilePath;

    public float DeltaTime = 0f;

    public string Text
    {
        get
        {
            return text;
        }

        set
        {
            text = value;
            GetComponentInChildren<Text>().text = value;
        }
    }

    [SerializeField]
    private Image image;

    public void SetImageEnabled(bool enabled)
    {
        image.enabled = enabled;
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowClick);
    }

    private void ShowClick()
    {
        Debug.Log("Click: " + text);
    }
}
