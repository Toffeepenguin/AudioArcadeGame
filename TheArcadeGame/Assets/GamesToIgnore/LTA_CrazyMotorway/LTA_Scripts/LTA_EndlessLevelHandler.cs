using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LTA_EndlessLevelHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] sectionPrefabs;

    GameObject[] sectionsPool = new GameObject[20];

    GameObject[] sections = new GameObject[10];

    Transform playerCarTransfrom;



    WaitForSeconds waitFor100ms = new WaitForSeconds(0.1f);

    const float sectionLength = 26;
    // Start is called before the first frame update
    void Start()
    {
        playerCarTransfrom = GameObject.FindGameObjectWithTag("Player").transform;

        int prefabIndex = 0;

        //create a pool for the endless sections
        for(int i = 0; i < sectionsPool.Length; i++)
        {
            sectionsPool[i] = Instantiate(sectionPrefabs[prefabIndex]);
            sectionsPool[i].SetActive(false);

            prefabIndex++;

            //Loop the prefab index if we run out of prefabs
            if (prefabIndex > sectionPrefabs.Length - 1)
            {
                prefabIndex = 0;
            }
        }

        //Add the first sections to the road
        for (int i = 0;i < sections.Length; i++)
        {
            //Get a random section
            GameObject randomSection = GetRandomSectionFromPool();

            //Move it into position and set it to active
            randomSection.transform.position = new Vector3(sectionsPool[i].transform.position.x, 0, i * sectionLength);
            randomSection.SetActive(true);

            //Set the section in the array
            sections[i] = randomSection;
        }

        StartCoroutine(UpdateLessOftenCO());
    }

    IEnumerator UpdateLessOftenCO()
    {
        while (true)
        {
            UpdateSectionPosition();
            yield return waitFor100ms;
        }
    }

    void UpdateSectionPosition()
    {
        for (int i = 0; i < sections.Length ; i++)
        {
            //Check if section is to far behind
            if (sections[i].transform.position.z - playerCarTransfrom.position.z < -sectionLength)
            {
                //Store the position of the section and disable it
                Vector3 lastSectionPosition = sections[i].transform.position;
                sections[i].SetActive (false);

                //Get new section & enable it and move it forward
                sections[i] = GetRandomSectionFromPool();

                //Move the new section infront of player and activate it
                sections[i].transform.position = new Vector3(lastSectionPosition.x, 0, lastSectionPosition.z + sectionLength * sections.Length);
                sections[i].SetActive(true);
            }
        }
    }
    GameObject GetRandomSectionFromPool()
    {
        int randomIndex = Random.Range(0, sectionsPool.Length);

        // Iterate through the pool to find an inactive section
        for (int i = 0; i < sectionsPool.Length; i++)
        {
            int index = (randomIndex + i) % sectionsPool.Length; // Loop through the array
            if (!sectionsPool[index].activeInHierarchy)
            {
                return sectionsPool[index];
            }
        }

        // If no inactive sections are found, log an error (this shouldn't happen if the pool is correctly managed)
        Debug.LogError("No inactive sections found in the pool!");
        return null;
    }
}
