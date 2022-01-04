using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingBehavior : MonoBehaviour {
	private Tilemap tilemapFloors;
	private Tilemap tilemapWalls1;
	private Tilemap tilemapWalls2;
	private Tilemap tilemapRoofs;
	private Transform characterTransform;

	// Start is called before the first frame update
	void Start() {
		tilemapFloors = this.transform.Find("Tilemap Floors").GetComponent<Tilemap>();
		tilemapRoofs = this.transform.Find("Tilemap Roofs").GetComponent<Tilemap>();
		tilemapWalls1 = this.transform.Find("Walls").transform.Find("Tilemap Walls 1").GetComponent<Tilemap>();
		tilemapWalls2 = this.transform.Find("Walls").transform.Find("Tilemap Walls 2").GetComponent<Tilemap>();
		characterTransform = GameObject.FindGameObjectWithTag("Player").transform;
	}

    // Update is called once per frame
    void Update() {
		//Gets the character position in cells coordinates
		Vector3Int characterCellCoordinates = tilemapFloors.LocalToCell(tilemapFloors.WorldToLocal(characterTransform.position));
		//Hides the roof if the character is inside the building
		HideRoof(characterCellCoordinates);
	}

	//Hides the roof if the character is inside the building
	private void HideRoof(Vector3Int characterCellCoordinates) {
		tilemapRoofs.gameObject.SetActive(!tilemapFloors.HasTile(characterCellCoordinates));
	}
}
