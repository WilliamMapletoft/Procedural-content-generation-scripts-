using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsGenerator : MonoBehaviour
{
    public Vector3[] randomPos;
    public List<Vector3> outSide;
    // Start is called before the first frame update
    public void GeneratePoints(int lowerBound,int upperBound, int boundarySize)
    {
        outSide.Clear();
        outSide = new List<Vector3>();
        int noPoints = Random.Range(lowerBound, upperBound);
        randomPos = new Vector3[noPoints];
        for (int i = 0; i < noPoints; i++)
        {
            randomPos[i] = RandomPosition(boundarySize);
            while (CheckValidPosition(randomPos[i])== false)
            {
                randomPos[i] = RandomPosition(boundarySize);
            }
        }

        outSideStuff();
        //GetComponentInParent<SpawnHills>().SummonHills();
        //GetComponentInParent<SpawnFirePlace>().SpawnSequence();
    }

    void outSideStuff()
    {
        int leftMost = 0;
        //Debug.Log(randomPos.Length);
        for (int i = 0; i < randomPos.Length; i++)
        {
            if (randomPos[i].x < randomPos[leftMost].x)
            {
                leftMost = i;
            }
        }
        outSide.Add(randomPos[leftMost]);
        List<Vector3> collinearPoints = new List<Vector3>();
        Vector3 current = randomPos[leftMost];

        while (true)
        {
            Vector3 nextTarget = randomPos[0];
            for (int i = 1; i < randomPos.Length; i++)
            {
                if (randomPos[i] == current)
                    continue;
                float x1, x2, z1, z2;
                x1 = current.x - nextTarget.x;
                x2 = current.x - randomPos[i].x;
                z1 = current.z - nextTarget.z;
                z2 = current.z - randomPos[i].z;
                float val = (z2 * x1) - (z1 * x2);
                if (val > 0)
                {
                    nextTarget = randomPos[i];
                    collinearPoints = new List<Vector3>();
                }
                else if (val == 0)
                {
                    if (Vector2.Distance(current, nextTarget) < Vector2.Distance(current, randomPos[i]))
                    {
                        collinearPoints.Add(nextTarget);
                        nextTarget = randomPos[i];
                    }
                    else
                        collinearPoints.Add(randomPos[i]);
                }
            }
            int count = 0;
            foreach (Vector3 t in collinearPoints)
            {
                count++;
                //Debug.Log(count);
                outSide.Add(t);
            }
            if (nextTarget == randomPos[leftMost])
                break;
            outSide.Add(nextTarget);
            current = nextTarget;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if(Application.isPlaying)
        {
            //for (int i = 0; i < randomPos.Length; i++)
            //{
            //    Gizmos.DrawSphere(randomPos[i], 0.5f);
            //}
            for (int i = 0; i < outSide.Count-1; i++)
            {
                Gizmos.DrawSphere(outSide[i], 2);
                Gizmos.DrawLine(outSide[i], outSide[i + 1]);
            }
            Gizmos.DrawLine(outSide[0], outSide[outSide.Count - 1]);
            Gizmos.DrawSphere(outSide[outSide.Count-1], 2);
        }
    }

    bool CheckValidPosition(Vector3 checkPoint)
    {
        for (int i = 0; i < randomPos.Length; i++)
        {
            if (checkPoint == randomPos[i])
            {
                return true;
            }
        }
        return false;
    }

    Vector3 RandomPosition(int range)
    {
        return new Vector3((Random.Range(transform.position.x -range, transform.position.x + range) / Random.Range(1,3)), 0.5f, (Random.Range(transform.position.z - range, transform.position.z + range)/Random.Range(1,3)));
    }


}
