using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class ControllerData
{
    public List<ControllerDataPoint> Data;
}

[System.Serializable]
public class ControllerDataPoint
{
    public double time;
    public double positionX;
    public double positionY;
    public double positionZ;
    public double rotationX;
    public double rotationY;
    public double rotationZ;
    public double rotationW;
    public bool selectActive;
    public bool activateActive;
    public bool pressActive;

    public ControllerDataPoint(XRControllerState value)
    {
        time = value.time;
        positionX = value.position.x;
        positionY = value.position.y;
        positionZ = value.position.z;
        rotationX = value.rotation.x;
        rotationY = value.rotation.y;
        rotationZ = value.rotation.z;
        rotationW = value.rotation.w;
        selectActive = value.selectInteractionState.activatedThisFrame;
        activateActive = value.activateInteractionState.activatedThisFrame;
        pressActive = value.uiPressInteractionState.activatedThisFrame;
    }
}

public class Record : MonoBehaviour
{
    public ControllerData Right;
    public ControllerData Left;

    public GameObject[] Controllers;
    public XRControllerRecorder[] Recorders;
    public XRControllerRecording[] Recordings;
    public ControllerData[] Loaded=new ControllerData[2];
    public XRControllerRecording[] MotionCapture;
    public XRControllerRecording[] Inputs;
    public bool load=false;
    public bool record = false;
    public bool play = false;


    public void StartRecord()
    {
        foreach (XRControllerRecorder controller in Recorders)
        {
            controller.isRecording = true;
        }  
    }

    public void StopRecording()
    {
        Debug.Log("Stop Recording");
        foreach (XRControllerRecorder controller in Recorders)
        {
            controller.isRecording = false;
        }
        SaveData(Right, "/Right.dat",Recordings[0]);
        SaveData(Left, "/Left.dat", Recordings[1]);
    }

    void SaveData(ControllerData controller, string Location,XRControllerRecording recording)
    {
        foreach (XRControllerState State in recording.frames)
        {
            controller.Data.Add(new ControllerDataPoint(State));
        }
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + Location;
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, controller);
        stream.Close();
    }

    public static ControllerData LoadRecording(string name)
    {
        //string path = Application.persistentDataPath + name;
        string path = "C:\\Users\\ciara\\Documents\\Unity\\VR-Snooker\\VR Snooker\\Assets\\Recording" + name;
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        ControllerData data = formatter.Deserialize(stream) as ControllerData;
        stream.Close();
        return data;
    }

    public void ConvertData(ControllerData data,XRControllerRecording Recording,bool Transform)
    {
        double lasttime=-0.1;
        Recording.InitRecording();
        foreach(ControllerDataPoint dataPoint in data.Data)
        {
            double time = dataPoint.time;
            if (lasttime>=time)
            {
                Recording.InitRecording();
                Debug.LogError("Cleared");
            }
            Vector3 Position;
            Quaternion Rotation;
            if (Transform)
            {
                Position = new Vector3((float)dataPoint.positionX, (float)dataPoint.positionY, (float)dataPoint.positionZ);
                Rotation = new Quaternion((float)dataPoint.rotationX, (float)dataPoint.rotationY, (float)dataPoint.rotationZ, (float)dataPoint.rotationW);
            }
            else
            {
                Position = Vector3.zero;
                Rotation = Quaternion.identity;
            }
            Recording.AddRecordingFrame(dataPoint.time, Position, Rotation, dataPoint.selectActive, dataPoint.activateActive, dataPoint.pressActive);
            lasttime = time;
        }
    }

    public void StartPlayback()
    {
        Debug.Log("Play");
        foreach (GameObject controller in Controllers)
        {
            /*XRController[] XRcontrollers =controller.GetComponentsInChildren<XRController>();
            foreach(XRController xRController in XRcontrollers)
            {
                xRController.enableInputTracking = false;
                xRController.enableInputActions = false;
            }*/
            XRControllerRecorder[] children =controller.GetComponentsInChildren<XRControllerRecorder>();
            foreach (XRControllerRecorder child in children)
            {
                child.isPlaying = true;
            }
            controller.GetComponent<XRControllerRecorder>().isPlaying = true;
        }

    }

    void Update ()
    {
        if (load)
        {
            load = false;
            Loaded[0]= LoadRecording("/Right.dat");
            Loaded[1] = LoadRecording("/Left.dat");
            ConvertData(Loaded[0], MotionCapture[0],true);
            ConvertData(Loaded[1], MotionCapture[1],true);
            ConvertData(Loaded[0], Inputs[0],false);
            //ConvertData(Loaded[1], MotionCapture[1],false);
            Debug.Log("Loaded");
        }
        if(record)
        {
            record = false;
            StartRecord();
        }
        if (play)
        {
            play = false;
            StartPlayback();
        }
    }
}
