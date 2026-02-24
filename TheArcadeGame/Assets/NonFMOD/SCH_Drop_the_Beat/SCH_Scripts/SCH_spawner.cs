using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SCH_spawner : MonoBehaviour
{
    //Notes spawning
    public Rigidbody2D redNote;
    public Rigidbody2D yellowNote;
    public Rigidbody2D greenNote;
    public Rigidbody2D blueNote;
    public float notesFallingSpeed { get; set; }

    //Strings spawning
    public Rigidbody2D stringsPfb;
    private float stringYCoor = 30f;
    public float stringMovementSpeed { get; set; }

    private float stringSpawnerVerticalSpeed = 0.05f;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void SCH_SpawnRedNote()
    {
        //Debug.Log("redNote");
        Rigidbody2D redNotePfb = Instantiate(redNote, new Vector3(transform.position.x - 40, transform.position.y, transform.position.z), Quaternion.identity) as Rigidbody2D;
        redNotePfb.GetComponent<Rigidbody2D>().AddForce(new Vector3(-20, -notesFallingSpeed, 0));
    }

    public void SCH_SpawnYellowNote()
    {
        Rigidbody2D yellowNotePfb = Instantiate(yellowNote, new Vector3(transform.position.x - 12, transform.position.y, transform.position.z), Quaternion.identity) as Rigidbody2D;
        yellowNotePfb.GetComponent<Rigidbody2D>().AddForce(new Vector3(-20, -notesFallingSpeed, 0));
    }

    public void SCH_SpawnGreenNote()
    {
        Rigidbody2D greenNotePfb = Instantiate(greenNote, new Vector3(transform.position.x + 15, transform.position.y, transform.position.z), Quaternion.identity) as Rigidbody2D;
        greenNotePfb.GetComponent<Rigidbody2D>().AddForce(new Vector3(-20, -notesFallingSpeed, 0));
    }

    public void SCH_SpawnBlueNote()
    {
        Rigidbody2D blueNotePfb = Instantiate(blueNote, new Vector3(transform.position.x + 44, transform.position.y, transform.position.z), Quaternion.identity) as Rigidbody2D;
        blueNotePfb.GetComponent<Rigidbody2D>().AddForce(new Vector3(-20, -notesFallingSpeed, 0));
    }

    //String spawner functions

    public void SCH_SpawnString()
    {
        //Debug.Log("Spawn");
        Rigidbody2D redNotePfb = Instantiate(stringsPfb, new Vector3(0, 
        stringYCoor, -50), Quaternion.identity) as Rigidbody2D;
        redNotePfb.GetComponent<Rigidbody2D>().AddForce(new Vector3(stringMovementSpeed, 0, 0));
    }

    public void SCH_MoveUpwards()
    {
        //Debug.Log("Up");
        stringYCoor += stringSpawnerVerticalSpeed;
    }

    public void SCH_MoveDownwards()
    {
        //Debug.Log("Down");
        stringYCoor -= stringSpawnerVerticalSpeed;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
