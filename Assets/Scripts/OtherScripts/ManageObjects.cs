using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (manager != null)
        {
            Destroy(manager.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
