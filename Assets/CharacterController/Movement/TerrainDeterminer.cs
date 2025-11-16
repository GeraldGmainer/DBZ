using UnityEngine;

public class TerrainDeterminer : MonoBehaviour {
    private static string TERRAIN_NAME = "Terrain";

    private bool terrain;

    void OnCollisionEnter(Collision other) {
        terrain = other.gameObject.name == TERRAIN_NAME;
    }
	
    public bool isTerrain() {
        return terrain;
    }
}
