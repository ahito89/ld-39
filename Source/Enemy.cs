using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public enum Direction {Up, Down, Right, Left, None};
	public enum Rotation {Once,Twice,Thrice};
	public Direction direction;
	public Rotation rotation;

	public bool flagged = false;

	private void Update() {
		int i = (int)transform.position.x;
		int j = (int)transform.position.z;
		switch (direction) {
			case Direction.Up:
				j++;
				break;
			case Direction.Down:
				j--;
				break;
			case Direction.Right:
				i++;
				break;
			case Direction.Left:
				i--;
				break;
		}
		if (!flagged) {
			InitiateMove (i, j);
		}
	}

	private void InitiateMove (int i, int j) {
		int x = (int)transform.position.x;
		int z = (int)transform.position.z;
		Stage.Enable (x, z);
		bool legal = (Stage.counter > 0);
		bool valid = Stage.Check (i, j);
		if (valid && legal) {
			StartCoroutine (Move (i, j));
		} else if (!valid) {
			direction = Reverse (direction);
		}
	}

	private IEnumerator Move (int i, int j) {
		flagged = true;
		Vector3 target = new Vector3 (i, 1, j);
		Vector3 position = transform.position;
		Vector3 velocity = Vector3.zero;
	     while (position != target) {
			position = transform.position;
			transform.position = Vector3.SmoothDamp (position, target, ref velocity, 0.03f);
	         yield return null;
	     }
		 Stage.Disable (i, j);
		transform.position = target;
		Stage.counter--;
		flagged = false;
	}

	private Direction Reverse(Direction direction) {
		switch(rotation){
		case(Rotation.Once):
			switch (direction) {
			case Direction.Up:
				return Direction.Right;
			case Direction.Down:
				return Direction.Left;
			case Direction.Right:
				return Direction.Down;
			case Direction.Left:
				return Direction.Up;
			default:
				return Direction.None;
			}
		case(Rotation.Twice):
			switch (direction) {
			case Direction.Up:
				return Direction.Down;
			case Direction.Down:
				return Direction.Up;
			case Direction.Right:
				return Direction.Left;
			case Direction.Left:
				return Direction.Right;
			default:
				return Direction.None;
			}
		case(Rotation.Thrice):
			switch (direction) {
			case Direction.Up:
				return Direction.Left;
			case Direction.Down:
				return Direction.Right;
			case Direction.Right:
				return Direction.Up;
			case Direction.Left:
				return Direction.Down;
			default:
				return Direction.None;
			}
		default:
			return Direction.None;
		}
	}
}
