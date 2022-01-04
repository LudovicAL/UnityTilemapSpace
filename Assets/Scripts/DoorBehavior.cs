using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorBehavior : MonoBehaviour {

	public TileBase doorClosed2;
	public TileBase doorClosed1;
	public TileBase doorClosed0;
	public TileBase doorOpen2;
	public TileBase doorOpen1;
	public TileBase doorOpen0;
	public AudioClip doorOpen;
	public AudioClip doorClose;
	private bool doorIsOpen = false;
	private Tilemap tilemapDoors0;
	private Tilemap tilemapDoors1;
	private Tilemap tilemapDoors2;
	private Transform characterTransform;
	private AudioSource audioSource;

	// Start is called before the first frame update
	void Start() {
		tilemapDoors0 = this.transform.Find("Tilemap Doors 0").GetComponent<Tilemap>();
		tilemapDoors1 = this.transform.Find("Tilemap Doors 1").GetComponent<Tilemap>();
		tilemapDoors2 = this.transform.Find("Tilemap Doors 2").GetComponent<Tilemap>();
		audioSource = this.GetComponent<AudioSource>();
		characterTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
