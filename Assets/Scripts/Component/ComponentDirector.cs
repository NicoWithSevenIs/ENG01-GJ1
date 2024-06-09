using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDirector : MonoBehaviour
{   
    public static ComponentDirector instance;
    [SerializeField] private ComponentBuilder builder;
    public ComponentBuilder Builder { get { return builder; } }

    void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
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
