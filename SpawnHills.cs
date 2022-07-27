using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHills : MonoBehaviour
{
    //public GameObject spawnManager;
    public GameObject[] hillPrefab;
    private int count;
    //public List<Transform> spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        count = 1;
        for (int i = 1; i <= 3; i++)
        {
            GetComponentInParent<PointsGenerator>().GeneratePoints(25*i, 50*i, 30*i);
            count++;
            SummonHills();
        }
        GetComponentInParent<PointsGenerator>().GeneratePoints(100, 200, 90);
        GetComponentInParent<SpawnMountains>().SummonMountains();
        GetComponentInParent<SpawnFirePlace>().SpawnSequence();
    }

    //void PopulateList()
    //{
    //    List<Transform> tempList = new List<Transform>(spawnManager.GetComponentsInChildren<Transform>());
    //    foreach (Transform ChildT in tempList)
    //    {
    //        if (ChildT.gameObject.transform.parent != null)
    //        {
    //            spawnLocations.Add(ChildT);
    //        }
    //    }
    //}

    public void SummonHills()
    {
        int modifierX, modifierY, modifierZ;
        List<Vector3> summonAreas = GetComponentInParent<PointsGenerator>().outSide;
        foreach (Vector3 hillPosition in summonAreas)
        {
            modifierX = Random.Range(-12, 12);
            modifierY = Random.Range(1, 2+(count/2));
            modifierZ = Random.Range(-12, 12);
            //Debug.Log(count);
            Instantiate(hillPrefab[count-2], new Vector3(hillPosition.x + modifierX, hillPosition.y + modifierY, hillPosition.z + modifierX), rotation());
        }
    }

    Quaternion rotation()
    {
        Quaternion rotAm = new Quaternion();
        rotAm.x = 0;
        rotAm.z = 0;
        int tempY = Random.Range(0, 360);
        //Debug.Log(tempY);
        rotAm.y = tempY;
        return rotAm;
    }
}
