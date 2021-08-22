using System;
using UnityEngine;

public class DynamicTable : MonoBehaviour
{
    public GameObject[][] Elements;
    public GameObject[] Boundary;
    public GameObject[] Felt;
    public GameObject[] Cushion;
    public float ReqSize;
    public float fullSize=12f;
    public float scaleFactor;
    int total;
    GameObject instance;
    TableElement thisedge;

    TableElement longedge;
    TableElement shortedge;
    TableElement cornerhole;
    TableElement edgehole;

    TableElement FeltShort;
    TableElement FeltLong;

    Vector3 rot;
    float up;
    float right;

    public float Build(float size)
    {
        ReqSize = size;
        Elements = new GameObject[][] { Boundary, Felt, Cushion };
        scaleFactor = ReqSize / fullSize;
        for (int j = 0; j < Elements.GetLength(0) - 1; j++)
        {
            foreach (GameObject part in Elements[j])
            {
                TableElement info = part.GetComponent<TableElement>();
                if (info.Type == "Long" || info.Type == "Short")
                {
                    info.realsize = info.FullSize * scaleFactor;
                    part.transform.localScale = new Vector3(1, scaleFactor, 1);
                }
                else if (info.Type == "Centre")
                {
                    part.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
                }
                else
                {
                    info.realsize = info.FullSize;
                }
                switch (info.Type)
                {
                    case "Short":
                        shortedge = info;
                        if (j == 1)
                        {
                            FeltShort = shortedge;
                        }
                        break;
                    case "Long":
                        longedge = info;
                        if (j == 1)
                        {
                            FeltLong = longedge;
                        }
                        break;
                    case "Edge":
                        edgehole = info;
                        break;
                    case "Corner":
                        cornerhole = info;
                        break;
                }
            }

            foreach (GameObject part in Elements[j])
            {
                up = 0;
                right = 0;
                thisedge = part.GetComponent<TableElement>();
                total = thisedge.number;
                int[] order = { 0, -90, 90, 180 };
                for (int i = 0; i < total; i++)
                {
                    rot = new Vector3(0, 0, 0);
                    rot.z = (i % 2) * 180;
                    instance = Instantiate(part, gameObject.transform);
                    switch (thisedge.Type)
                    {
                        case "Short":
                            SetPos(i, shortedge, longedge, total, instance);
                            break;
                        case "Long":
                            SetPos(i, longedge, shortedge, total, instance);
                            break;
                        case "Edge":
                            SetPos(i, shortedge, longedge, total, instance);
                            break;
                        case "Corner":
                            SetPos(i, longedge, shortedge, total, instance);
                            rot.z = order[i];
                            break;
                    }
                    instance.transform.Rotate(rot, Space.Self);
                }
            }
        }
        GameObject cushionlong = null;
        GameObject cushionshort = null;
        float cushionlonglength = 0;
        float cushionshortlength = 0;
        float cushionno=0;
        foreach (GameObject cushion in Elements[Elements.GetLength(0)-1])
        {
            TableElement info = cushion.GetComponent<TableElement>();
            if (info.Type == "Long")
            {
                cushionlonglength = info.FullSize;
                cushionno = info.number;
                cushion.transform.localScale = new Vector3(1, scaleFactor, 1);
                cushion.transform.localPosition = new Vector3(0, cushion.transform.localPosition.y, 0);
                cushionlong = cushion;
            }
            else if (info.Type == "Short")
            {
                cushionshortlength = info.FullSize;
                cushionno = info.number;
                cushion.transform.localScale = new Vector3(1, scaleFactor, 1);
                cushion.transform.localPosition = new Vector3(0, cushion.transform.localPosition.y, 0);
                cushionshort = cushion;
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    instance = Instantiate(cushion, cushionlong.transform);
                    int j = (i * 2) - 1;
                    instance.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    instance.transform.localPosition = new Vector3(0, j * cushionlonglength, 0);
                    instance.transform.localScale = new Vector3(1, j, 1);
                }
                for (int i = 0; i < 2; i++)
                {
                    instance = Instantiate(cushion, cushionshort.transform);
                    int j = (i * 2) - 1;
                    instance.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    instance.transform.localPosition = new Vector3(0, j * cushionshortlength, 0);
                    instance.transform.localScale = new Vector3(1, j, 1);
                }
            }
        }
        cornerhole.smalledge = 0;
        rot = new Vector3(0, 0, 0);
        for (int i=0; i<2;i++)
        {
            instance=Instantiate(cushionlong, gameObject.transform);
            SetPos(i, FeltLong, FeltShort, 2, instance);
            rot.z = (i % 2) * 180;
            instance.transform.Rotate(rot, Space.Self);
        }
        for (int i = 0; i < 4; i++)
        {
            instance = Instantiate(cushionshort, gameObject.transform);
            SetPos(i,FeltShort, FeltLong, 4, instance);
            rot.z = (i % 2) * 180;
            instance.transform.Rotate(rot, Space.Self);
        }
        return scaleFactor;
    }

    void SetPos(int i,TableElement thisedge,TableElement otheredge,int total,GameObject instance)
    {
        float dist;
        instance.transform.localPosition = new Vector3(0, instance.transform.localPosition.y, 0);
        if (total>2)
        {
            up = (shortedge.realsize + edgehole.realsize) / 2;
        }
        if (otheredge.number>2)
        {
            dist = otheredge.realsize + (edgehole.realsize / 2);
        }
        else
        {
            dist = otheredge.realsize / 2;
        }

        right = dist + cornerhole.realsize - (cornerhole.smalledge / 2);
        if(instance.name== "Corner(Clone)")
        {
            up = longedge.realsize / 2 + (cornerhole.realsize / 2);
            right = shortedge.realsize + (edgehole.realsize / 2) + (cornerhole.realsize / 2);
        }
        if (i > 1)
        {
            up = -up;
        }

        if (i % 2 == 0)
        {
            right = -right;
        }
        instance.transform.localPosition += instance.transform.up * up;
        instance.transform.localPosition += instance.transform.right * right;
    }
}
