using UnityEngine;

public class InputTools : MonoBehaviour {


    /// <summary>
    /// Get the mouse click object
    /// </summary>
    /// <returns>Gameobject of mouse clicked on and null if not click on object</returns>
    public static GameObject GetMouseClickObj()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }




}
