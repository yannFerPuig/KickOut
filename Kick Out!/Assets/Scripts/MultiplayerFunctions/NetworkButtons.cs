using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkButtons : NetworkBehaviour
{
    public string sceneToLoad = "YourSceneName";

    private void ChangeScene()
    {
        NetworkManager.SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUILayout.Button("Host")) 
            {
                NetworkManager.Singleton.StartHost();
                ChangeScene();
            }
            if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        }
        GUILayout.EndArea();
    }
}
