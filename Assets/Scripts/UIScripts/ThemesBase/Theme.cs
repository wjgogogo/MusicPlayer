using UnityEngine;

public class Theme : MonoBehaviour
{
    protected ThemeModule[] m_modules;

    protected void Start()
    {
        m_modules = gameObject.GetComponentsInChildren<ThemeModule>();
    }

    /// <summary>
    /// set module status --- active or not
    /// </summary>
    /// <param name="i"></param>
    /// <param name="status"></param>
    /// <returns>return true if succeed</returns>
    protected bool SetActiveModule(int i, bool status)
    {
        if (i >= m_modules.Length)
            return false;

        m_modules[i].SetActive(status);
        return true;
    }

    /// <summary>
    /// set index one's active is status and other's is contrary
    /// </summary>
    /// <param name="index"></param>
    /// <param name="status"></param>
    /// <returns>return true if succeed</returns>
    protected bool ActiveOneModule(int index, bool status)
    {
        if (index >= m_modules.Length)
            return false;

        for (int i = 0; i < m_modules.Length; i++)
        {
            if (i == index)
            {
                if (m_modules[i].IsActive != status)
                    SetActiveModule(i, status);
            }
            else if (m_modules[i].IsActive == status)
                SetActiveModule(i, !status);
        }

        return true;
    }

    /// <summary>
    /// set all the modules status
    /// </summary>
    /// <param name="status"></param>
    protected void SetAllModulesActive(bool status)
    {
        for (int i = 0; i < m_modules.Length; i++)
        {
            SetActiveModule(i, status);
        }
    }
}
