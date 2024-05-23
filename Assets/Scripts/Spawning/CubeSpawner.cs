using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject CTemplateObject;
    [SerializeField] private List<GameObject> aObjectList;

    public const string SPAWN_CUBE_VALUE = "SPAWN_CUBE_VALUE";

    // Start is called before the first frame update
    void Start()
    {
        this.CTemplateObject.SetActive(false);
        EventBroadcaster.Instance.AddObserver(EventNames.X22_Events.ON_SPAWN_CUBE, this.SpawnCubeEvent);

    }

    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(EventNames.X22_Events.ON_SPAWN_CUBE);
    }

    // Update is called once per frame
    void Update()
    {
        //this.SpawnCubeEvent();
    }

    private void SpawnCubeEvent(Parameters CParam)
    {
        int nSpawnTotal = CParam.GetIntExtra(SPAWN_CUBE_VALUE, 1);
        for (int i = 0; i < nSpawnTotal; i++)
        {
            GameObject CInstance = GameObject.Instantiate(this.CTemplateObject, this.transform);
            CInstance.SetActive(true);
            this.aObjectList.Add(CInstance);
        }
       
    }
}
