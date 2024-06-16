using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the object's behaviour for scene transitions when gazed at.
/// </summary>
public class SceneObject : MonoBehaviour
{
   /* public Material InactiveMaterial;
    public Material GazedAtMaterial;*/

    //private Renderer _myRenderer;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
   /* void Start()
    {
        _myRenderer = GetComponent<Renderer>();
        SetMaterial(false);
    }*/

    /// <summary>
    /// This method is called by the Main Camera when it starts gazing at this GameObject.
    /// </summary>
    public void OnPointerEnter()
    {
        
       
    }

    /// <summary>
    /// This method is called by the Main Camera when it stops gazing at this GameObject.
    /// </summary>
    public void OnPointerExit()
    {
        CancelInvoke("clicked");
        //SetMaterial(false);
    }

    /// <summary>
    /// This method is called by the Main Camera when it is gazing at this GameObject and the screen is touched.
    /// </summary>
    public void OnPointerClick()
    {
        Debug.Log("co nhan");
        //SetMaterial(true);
        Invoke("clicked", 3f);
    }

    /// <summary>
    /// Sets this instance's material according to gazedAt status.
    /// </summary>
    ///
    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
   /* private void SetMaterial(bool gazedAt)
    {
        _myRenderer.material = gazedAt ? GazedAtMaterial : InactiveMaterial;
    }*/

    /// <summary>
    /// Changes the scene to the specified next scene.
    /// </summary>
    private void ChangeScene()
    {
        SceneManager.LoadScene("Scene_03");
    }
}
