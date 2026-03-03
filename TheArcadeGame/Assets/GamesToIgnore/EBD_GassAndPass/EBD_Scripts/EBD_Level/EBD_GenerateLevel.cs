using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBD_GenerateLevel : MonoBehaviour
{
    public GameObject[] section;
    public GameObject carPrefab;
    public int zPos = 50;
    public bool SectionCreation = false;
    public int secNum;
    public GameObject Bike;
    public List<GameObject> spawnedSection = new List<GameObject>();
    public float destroyDistance = 100;

    InputSubscription GetInput;

    private void Awake()
    {
        GetInput = GetComponent<InputSubscription>();
    }
    // Update is called once per frame
    void Update()
    {
        if (SectionCreation == false)
        {
            SectionCreation = true;
            StartCoroutine(GenerateSection());
        }

        DestroyOldSection();
    }

    IEnumerator GenerateSection()
    {
        secNum = Random.Range(0, section.Length);
        GameObject newSection = Instantiate(section[secNum], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 50;

        spawnedSection.Add(newSection);

        yield return new WaitForSeconds(0.1f);
        SectionCreation = false;
    }


    void DestroyOldSection()
    {
        for (int i = spawnedSection.Count - 1; i >= 0; i--)
        {
            if (Bike.transform.position.z - spawnedSection[i].transform.position.z > destroyDistance)
            {
                Destroy(spawnedSection[i]);
                spawnedSection.RemoveAt(i);
            }
        }
    }
}