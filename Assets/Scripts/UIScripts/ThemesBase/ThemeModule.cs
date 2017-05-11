using UnityEngine;

public class ThemeModule : MonoBehaviour
{
    public bool IsActive
    {
        get
        {
            return gameObject.activeSelf;
        }
    }

    public void SetActive(bool status)
    {
        gameObject.SetActive(status);
    }

    public Vector2 GetPositon()
    {
        return transform.position;
    }
}
