using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingBehavior : MonoBehaviour {
	public DoorBehavior[] mainDoors;
	private Tilemap tilemapFloors;
	private Tilemap tilemapRoofs;
	private Tilemap tilemapStairs1;
	private TilemapCollider2D firstLevelFurnitureCollider;
	private TilemapCollider2D firstLevelWallsCollider;
	private TilemapCollider2D secondLevelWallsCollider;
	private GameObject secondLevel;
	private Transform characterTransform;
	private SpriteRenderer characterSpriteRenderer;
	private bool hasBeenOnStairs = false;
	private Vector3Int lastStairTileCoordinates;

	// Start is called before the first frame update
	void Start() {
		tilemapFloors = this.transform.Find("Levels").Find("Level 1").Find("Tilemap Floors").GetComponent<Tilemap>();
		tilemapRoofs = this.transform.Find("Tilemap Roofs").GetComponent<Tilemap>();
		if (this.transform.Find("Levels")) {
			if (this.transform.Find("Levels").Find("Level 2")) {
				tilemapStairs1 = this.transform.Find("Stairs").Find("Tilemap Stairs 1").GetComponent<Tilemap>();
				firstLevelFurnitureCollider = this.transform.Find("Levels").Find("Level 1").Find("Furnitures").Find("Furniture 1").GetComponent<TilemapCollider2D>();
				firstLevelWallsCollider = this.transform.Find("Levels").Find("Level 1").Find("Walls").Find("Tilemap Walls 1").GetComponent<TilemapCollider2D>();
				secondLevelWallsCollider = this.transform.Find("Levels").Find("Level 2").Find("Walls").Find("Tilemap Walls 1").GetComponent<TilemapCollider2D>();
				secondLevel = this.transform.Find("Levels").Find("Level 2").gameObject;
				characterSpriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
			}
		}
		characterTransform = GameObject.FindGameObjectWithTag("Player").transform;
		foreach (DoorBehavior db in mainDoors) {
			db.passingDoorEvent.AddListener(SetEntry);
		}
	}

	// Update is called once per frame
	void Update() {
		//Gets the character position in cells coordinates
		Vector3Int characterCellCoordinates = tilemapFloors.LocalToCell(tilemapFloors.WorldToLocal(characterTransform.position));
		CheckLevel(characterCellCoordinates);
	}

	//Makes it so the character is indoor or outdoor
	private void SetEntry(bool value) {
		if (secondLevel) {
			secondLevel.SetActive(!value);
		}
		tilemapRoofs.gameObject.SetActive(!value);
	}

	//Checks which story of the building the character is on
	private void CheckLevel(Vector3Int characterCellCoordinates) {
		if (tilemapStairs1) {
			if (tilemapStairs1.HasTile(characterCellCoordinates)) {
				if (hasBeenOnStairs) {
					if (characterCellCoordinates.y > lastStairTileCoordinates.y) {
						//Character is ascending
						AscendStairs();
					} else if (characterCellCoordinates.y < lastStairTileCoordinates.y) {
						//Character is descending
						DescendStairs();
					}
				} else {
					//Character has stepped on stairs for the first time
					hasBeenOnStairs = true;
				}
				lastStairTileCoordinates = characterCellCoordinates;
			}
		}
	}

	//Sets the building level to 2
	private void AscendStairs() {
		firstLevelFurnitureCollider.enabled = false;
		secondLevelWallsCollider.enabled = true;
		firstLevelWallsCollider.enabled = false;
		secondLevel.SetActive(true);
		characterSpriteRenderer.sortingOrder = 17;
	}

	//Sets the building level to 1
	private void DescendStairs() {
		firstLevelFurnitureCollider.enabled = true;
		firstLevelWallsCollider.enabled = true;
		secondLevelWallsCollider.enabled = false;
		secondLevel.SetActive(false);
		characterSpriteRenderer.sortingOrder = 7;
	}
}