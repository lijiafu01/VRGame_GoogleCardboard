using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class testScene : MonoBehaviour
{
    //�ഫ����
    public void ChangeScene()
    {
        SceneManager.LoadScene("Scene_01");
    }
}
