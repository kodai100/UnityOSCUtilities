using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OSCManager))]
public class OSCManagerEditor : Editor {

    SerializedProperty mode;

    SerializedProperty receiverId;
    SerializedProperty receiverPort;

    SerializedProperty senderId;
    SerializedProperty targetIp;
    SerializedProperty targetPort;

    OSCManager script;
    
    private void OnEnable()
    {
        script = target as OSCManager;

        mode = serializedObject.FindProperty("mode");

        receiverId = serializedObject.FindProperty("receiverId");
        receiverPort = serializedObject.FindProperty("receiverPort");

        senderId = serializedObject.FindProperty("senderId");
        targetIp = serializedObject.FindProperty("targetIp");
        targetPort = serializedObject.FindProperty("targetPort");
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(mode);


        switch (script.mode)
        {
            case OSCMode.Receive:
                MakeReceiverGUI();
                break;
            case OSCMode.Send:
                MakeSenderGUI();
                break;
            case OSCMode.SendAndReceive:
                MakeReceiverGUI();
                MakeSenderGUI();
                break;
            default:
                break;
        }


        serializedObject.ApplyModifiedProperties();
    }

    void MakeSenderGUI()
    {
        EditorGUILayout.LabelField("Sender", EditorStyles.boldLabel);

        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(senderId);
        EditorGUILayout.PropertyField(targetIp);
        EditorGUILayout.PropertyField(targetPort);
        EditorGUI.indentLevel--;
    }

    void MakeReceiverGUI()
    {
        EditorGUILayout.LabelField("Receiver", EditorStyles.boldLabel);

        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(receiverId);
        EditorGUILayout.PropertyField(receiverPort);
        EditorGUI.indentLevel--;
    }
}
