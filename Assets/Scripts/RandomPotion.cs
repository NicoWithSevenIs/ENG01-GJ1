using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPotion : MonoBehaviour
{
    [SerializeField]
    bool hasCork = false;

    [SerializeField]
    GameObject corkPrefab;

    [SerializeField]
    List<Material> potionMaterials = new List<Material>();

    [SerializeField]
    Material glassMaterial;

    [SerializeField]
    List<Mesh> potionBodies = new List<Mesh>();

    [SerializeField]
    List<Mesh> potionHoles = new List<Mesh>();
    private GameObject body;
    private GameObject hole;
    public int bottleShape = 0;

    // Start is called before the first frame update
    void Start()
    {
        body = transform.Find("Body").gameObject;
        hole = transform.Find("Hole").gameObject;

        // Set Bottle
        bottleShape = Random.Range(0, potionBodies.Count);
        body.GetComponent<MeshFilter>().sharedMesh = potionBodies[bottleShape];
        hole.GetComponent<MeshFilter>().sharedMesh = potionHoles[bottleShape];
        hole.GetComponent<Renderer>().material = potionMaterials[
            Random.Range(0, potionMaterials.Count)
        ];
        hole.GetComponent<Renderer>().material.SetFloat("_Fill", Random.Range(0.5f, 1f));

        if (hasCork)
        {
            Instantiate(corkPrefab, transform);
        }
    }
}
