using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SqrtHelper : MonoBehaviour 
{
    public bool updateSqrtTables;
    public int tableLength = 100000;

    public SqrtData sqrtData;
    public SqrtData sqrtDataBelow1;
    public SqrtData sqrtDataBelow10;

    public static SqrtHelper helper;

    public float sqrtTen;
    public float sqrtHundred;
    public float sqrtThousand;
    public float sqrtTenThousand;

    void Awake()
    {
        helper = this;
    }
	
	void Update () 
    {
        if (updateSqrtTables)
        {
            updateSqrtTables = false;
            int i = 0;

            sqrtTen = Mathf.Sqrt(10f);
            sqrtHundred = 10f;
            sqrtThousand = Mathf.Sqrt(1000f);
            sqrtTenThousand =100f;

            //creating main table data
            sqrtData = new SqrtData();
            sqrtData.sqrtNumberData = new List<float>();
            for (i = 0; i < tableLength; i++)
            {
                sqrtData.sqrtNumberData.Add(Mathf.Sqrt(i));
            }

            //creating table data for sqrtDataBelow1
            sqrtDataBelow1 = new SqrtData();
            sqrtDataBelow1.sqrtNumberData = new List<float>();
            for (i = 0; i < 101; i++)
            {
                sqrtDataBelow1.sqrtNumberData.Add(Mathf.Sqrt(i/100f));
            }

            //creating table data for sqrtDataBelow10
            sqrtDataBelow10 = new SqrtData();
            sqrtDataBelow10.sqrtNumberData = new List<float>();
            for (i = 0; i < 101; i++)
            {
                sqrtDataBelow10.sqrtNumberData.Add(Mathf.Sqrt(i/10f));
            }

//            for (i = 0; i < 5; i++)
//            {
//                float rNum = Random.Range(0f, 1f);
//                print("sqrt of "+rNum +" is "+ GetSqrt(rNum));
//
//                rNum = Random.Range(0f, 10f);
//                print("sqrt of "+rNum +" is "+ GetSqrt(rNum));
//
//                rNum = Random.Range(10f, 100f);
//                print("sqrt of "+rNum +" is "+ GetSqrt(rNum));
//            }
        }
	}

    public float Vector3Distance(Vector3 start, Vector3 end)
    {
        return(Vector3Magnitude(end-start));
    }

    public float Vector3Magnitude(Vector3 vec)
    {
        return(GetSqrt(vec.sqrMagnitude));
    }

    public float GetSqrt(float value)
    {
        if (value <1f)
        {
            int lowerLimit = Mathf.RoundToInt(value*100f - 0.5f);
            int upperLimit = Mathf.RoundToInt(value*100f + 0.5f);
            float lerpFactor = Mathf.InverseLerp(lowerLimit, upperLimit, value*100f);
            return(Mathf.Lerp(sqrtDataBelow1.sqrtNumberData[lowerLimit], sqrtDataBelow1.sqrtNumberData[upperLimit], lerpFactor));
        }
        else if (value < 10f)
        {
            int lowerLimit = Mathf.RoundToInt(value*10f - 0.5f);
            int upperLimit = Mathf.RoundToInt(value*10f + 0.5f);
            float lerpFactor = Mathf.InverseLerp(lowerLimit, upperLimit, value*10f);
            return(Mathf.Lerp(sqrtDataBelow10.sqrtNumberData[lowerLimit], sqrtDataBelow10.sqrtNumberData[upperLimit], lerpFactor));
        }
        else
        {
            int lowerLimit = Mathf.RoundToInt(value - 0.5f);
            int upperLimit = Mathf.RoundToInt(value + 0.5f);
            if (upperLimit < tableLength)
            {
                float lerpFactor = Mathf.InverseLerp(lowerLimit, upperLimit, value);
                return(Mathf.Lerp(sqrtData.sqrtNumberData[lowerLimit], sqrtData.sqrtNumberData[upperLimit], lerpFactor));
            }
            else
            {
                if (upperLimit < tableLength*10)
                {
                    return(GetSqrt(value/10f) * sqrtTen);
                }
                else if (upperLimit < tableLength*100)
                {
                    return(GetSqrt(value/100f) * sqrtHundred);
                }
                else if (upperLimit < tableLength*1000)
                {
                    return(GetSqrt(value/1000f) * sqrtThousand);
                }
                else if (upperLimit < tableLength*10000)
                {
                    return(GetSqrt(value/10000f) * sqrtTenThousand);
                }
                else
                {
                    //print("no sqrt data for this value " + value);
                    return(Mathf.Sqrt(value));
                }
            }
        }
    }
}
