using UnityEngine;
using System.Collections.Generic;

public static class GameObjectFinder {

    public static GameObject inChildByName(Transform trans, string withName) {
        Transform[] ts = trans.GetComponentsInChildren<Transform>();
        foreach (Transform t in ts) {
            if (t.gameObject.name.Contains(withName)) {
                return t.gameObject;
            }
        }
        return null;
    }

    public static GameObject playerByOwnerId(int ownerId) {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in players) {
            int playerLoopId = go.GetComponent<PhotonView>().ownerId;
            if (playerLoopId == ownerId) {
                return go;
            }
        }
        return null;
    }

    public static List<GameObject> otherPlayers(GameObject self) {
        List<GameObject> otherPlayers = new List<GameObject>();
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Player")) {
            if (go.GetComponent<PhotonView>() != null && go.GetComponent<PhotonView>().ownerId != self.GetComponent<PhotonView>().ownerId) {
                otherPlayers.Add(go);
            }
        }
        return otherPlayers;
    }
}
