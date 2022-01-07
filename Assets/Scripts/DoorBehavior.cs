using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class DoorBehavior : MonoBehaviour {


	public bool isMainDoor;
	public PassingDoorEvent passingDoorEvent;
	[SerializeField]
	private TileBase doorClosed2;
	[SerializeField]
	private TileBase doorClosed1;
	[SerializeField]
	private TileBase doorClosed0;
	[SerializeField]
	private TileBase doorOpen2;
	[SerializeField]
	private TileBase doorOpen1;
	[SerializeField]
	private TileBase doorOpen0;
	[SerializeField]
	private AudioClip doorOpen;
	[SerializeField]
	private AudioClip doorClose;
	private bool doorIsOpen = false;
	private Tilemap tilemapDoors0;
	private Tilemap tilemapDoors1;
	private Tilemap tilemapDoors2;
	private Transform characterTransform;
	private AudioSource audioSource;
	private bool hasBeenOnDoor = false;
	private Vector3Int lastDoorTileCoordinates;

	private void Awake() {
		audioSource = this.GetComponent<AudioSource>();
	}

	// Start is called before the first frame update
	void Start() {
		tilemapDoors0 = this.transform.Find("Tilemap Doors 0").GetComponent<Tilemap>();
		tilemapDoors1 = this.transform.Find("Tilemap Doors 1").GetComponent<Tilemap>();
		tilemapDoors2 = this.transform.Find("Tilemap Doors 2").GetComponent<Tilemap>();
		characterTransform = GameObject.FindGameObjectWithTag("Player").transform;
		passingDoorEvent = new PassingDoorEvent();
	}

	// Update is called once per frame
	void Update() {
		//Gets the character position in cells coordinates
		Vector3Int characterCellCoordinates = tilemapDoors1.LocalToCell(tilemapDoors1.WorldToLocal(characterTransform.position));
		//Opens or closes the door if needed
		if (doorIsOpen) {
			if (!IsInFrontOfDoor(characterCellCoordinates)) {
				CloseDoor();
			}
		} else {
			if (IsInFrontOfDoor(characterCellCoordinates)) {
				OpenDoor();
			}
		}
		CheckEntry(characterCellCoordinates);
	}

	//Checks if the playe is entering or exiting the building
	private void CheckEntry(Vector3Int characterCellCoordinates) {
		if (isMainDoor) {
			if (tilemapDoors0.HasTile(characterCellCoordinates) || tilemapDoors1.HasTile(characterCellCoordinates) || tilemapDoors2.HasTile(characterCellCoordinates)) {
				if (hasBeenOnDoor) {
					if (characterCellCoordinates.y > lastDoorTileCoordinates.y) {
						//Character is entering
						passingDoorEvent.Invoke(true);
					} else if (characterCellCoordinates.y < lastDoorTileCoordinates.y) {
						//Character is exiting
						passingDoorEvent.Invoke(false);
					}
				} else {
					hasBeenOnDoor = true;
				}
				lastDoorTileCoordinates = characterCellCoordinates;
			}
		}
	}

	//Returns true if the given cell is on, below or above the door
	private bool IsInFrontOfDoor(Vector3Int cellPosition) {
		return (tilemapDoors1.HasTile(cellPosition) || tilemapDoors1.HasTile(cellPosition + Vector3Int.down) || tilemapDoors1.HasTile(cellPosition + Vector3Int.up));
	}

	//Opens the door
	private void OpenDoor() {
		tilemapDoors0.SwapTile(doorClosed0, doorOpen0);
		tilemapDoors1.SwapTile(doorClosed1, doorOpen1);
		tilemapDoors2.SwapTile(doorClosed2, doorOpen2);
		audioSource.clip = doorOpen;
		audioSource.Play();
		doorIsOpen = true;
	}

	//Closes the door
	private void CloseDoor() {
		tilemapDoors0.SwapTile(doorOpen0, doorClosed0);
		tilemapDoors1.SwapTile(doorOpen1, doorClosed1);
		tilemapDoors2.SwapTile(doorOpen2, doorClosed2);
		audioSource.clip = doorClose;
		audioSource.Play();
		doorIsOpen = false;
	}
}

[System.Serializable]
public class PassingDoorEvent : UnityEvent<bool> {
}