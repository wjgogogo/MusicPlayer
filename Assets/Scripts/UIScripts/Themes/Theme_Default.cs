using UnityEngine;

public class Theme_Default : Theme
{
    private new void Start()
    {
        base.Start();
        Debug.Log("module length:  " + m_modules.Length);

        Debug.Log("Not succeed" + ActiveOneModule(2, true));
    }
}
