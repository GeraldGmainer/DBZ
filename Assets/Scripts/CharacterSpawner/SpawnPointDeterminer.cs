using UnityEngine;
using System.Collections.Generic;

public class SpawnPointDeterminer : MonoBehaviour {

    private List<Transform> spawnPoints = new List<Transform>();

    void Awake() {
        findAllSpawnPoints();
        if (spawnPoints.Count == 0) {
            Debug.LogError("CharacterSpawner: no spawn points found");
        }
    }

    private void findAllSpawnPoints() {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Respawn")) {
            spawnPoints.Add(go.transform);
        }
    }

    public Transform determine() {
        List<Transform> emptySpawnPoints = determineEmptySpawnPoints();
        if (emptySpawnPoints.Count == 0) {
            return determineRandomSpawnPoint(spawnPoints);
        }
        else {
            return determineRandomSpawnPoint(emptySpawnPoints);
        }
    }

    private List<Transform> determineEmptySpawnPoints() {
        List<Transform> emptySpawnPoints = new List<Transform>();
        foreach (Transform trans in spawnPoints) {
            if (trans.GetComponent<SpawnPoint>().isEmpty()) {
                emptySpawnPoints.Add(trans);
            }
        }
        return emptySpawnPoints;
    }

    private Transform determineRandomSpawnPoint(List<Transform> transformPoints) {
        int random = Random.Range(0, transformPoints.Count);
        return transformPoints[random].transform;
    }


}
