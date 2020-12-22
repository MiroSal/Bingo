using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevPreload : MonoBehaviour
{
    private void Awake()
    {
        GameObject check = GameObject.Find("Directors");
        if (check == null)
        {
            Debug.Log("Loading _preload scene"+ check);
            UnityEngine.SceneManagement.SceneManager.LoadScene("_Preload");
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
