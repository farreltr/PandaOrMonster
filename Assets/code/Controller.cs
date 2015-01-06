using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{

		private Vector3 screenPoint;
		private Vector3 offset;
		//	private Vector2 velocity = new Vector2 (0f, -1f);
		private float yBound = -7f;
		private float collisionRadius = 0.1f;
		private GameControl gameControl;
		private static string PANDA_BOX = "Panda Box";
		private static string MONSTER_BOX = "Monster Box";

		void Start ()
		{
				gameControl = GameObject.FindObjectOfType<GameControl> ();
		}

		void OnMouseDown ()
		{
				screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);		
				offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
		
		}
	
		void OnMouseDrag ()
		{
				Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);		
				Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
				transform.position = curPosition;
		
		}

		void OnMouseUp ()
		{
				float radius = this.transform.localScale.x / 2;
				Vector2 point = new Vector2 ();
				point.x = this.transform.position.x + radius;
				point.y = this.transform.position.y + radius;
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point, radius, Physics.AllLayers, -Mathf.Infinity, Mathf.Infinity);
				foreach (Collider2D collider in colliders) {
						string colliderName = collider.gameObject.name;
						if (isMonster ()) {
								if (colliderName == MONSTER_BOX) {
										gameControl.score++;
										gameControl.monsterScore++;
										Destroy (this.gameObject);
								}
								if (colliderName == PANDA_BOX) {
										gameControl.gameOver = true;
										SetEnabledText ("WrongBoxMonster");		
								}	
						} else {
								if (colliderName == MONSTER_BOX) {
										gameControl.gameOver = true;
										SetEnabledText ("WrongBoxPanda");
								}
								if (colliderName == PANDA_BOX) {
										gameControl.score++;
										gameControl.pandaScore++;
										Destroy (this.gameObject);
								}	
						}
				}
		}
		string GetName ()
		{
				return this.name.Replace ("(Clone)", "");
		}

		bool isMonster ()
		{
				return GetName () == "monster";
		}
	

		void Update ()
		{
				if (transform.position.y < yBound) {
						if (isMonster ()) {
								gameControl.gameOver = true;
								SetEnabledText ("GameOverMonster");
						} else {
								gameControl.gameOver = true;
								SetEnabledText ("GameOverPanda");
						}

				} else {
						//			rigidbody2D.velocity = new Vector2 (0f, -1f * (gameControl.score + 1));
				}
		}

		void SetEnabledText (string tag)
		{
				GameObject.FindGameObjectWithTag (tag).GetComponent<UnityEngine.UI.Text> ().enabled = true;

		}
	
}