using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFirePlace : MonoBehaviour
{
    public GameObject firePlace;
    private List<float> tempX, tempZ;
    private int count = 0;
    private List<Vector3> tempList;

    public void SpawnSequence()
    {
        tempList = GetComponentInParent<PointsGenerator>().outSide;
        tempX = new List<float>();
        tempZ = new List<float>();

        foreach (Vector3 pos in tempList)
        {
            tempX.Add(pos.x);
            tempZ.Add(pos.z);
            count++;
        }

        //Debug.Log(RecurAddX(0.0f, 0));
        //Debug.Log(RecurAddZ(0.0f, 0));

        Instantiate(firePlace, new Vector3(RecurAddX(0.0f, 0), 1.0f, RecurAddZ(0.0f, 0)), Quaternion.identity);

    }

    float RecurAddX(float A, int count)
    {
        if (count == 0)
        {
            return (tempX[count] + RecurAddX(tempX[count], count + 1)) / tempX.Count;
        }
        else if (count < tempX.Count)
        {
            //Debug.Log(tempX[count]);
            return tempX[count] + RecurAddX(tempX[count], count + 1);
        }
        else
        {
            return 0.0f;
        }
    }

    float RecurAddZ(float A, int count)
    {
        if (count == 0)
        {
            return (tempZ[count] + RecurAddZ(tempZ[count], count + 1)) / tempZ.Count;
        }
        else if (count < tempZ.Count)
        {
            //Debug.Log(tempY[count]);
            return tempZ[count] + RecurAddZ(tempZ[count], count + 1);
        }
        else
        {
            return 0.0f;
        }
    }
}
