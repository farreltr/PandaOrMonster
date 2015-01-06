using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour
{

		public int score = 0;
		public Rigidbody2D panda;
		public Rigidbody2D monster;
		public float pandaScore = 1f;
		public float monsterScore = 1f;
		public bool gameOver = false;

		void Start ()
		{
				InvokeRepeating ("SpawnPanda", Random.Range (1, 3), Random.Range (2, 4));
				InvokeRepeating ("SpawnMonster", Random.Range (1, 3), Random.Range (2, 4));
		}

		void OnGUI ()
		{
				string scoreText = "Score : " + score.ToString ();
				GUILayout.Label (scoreText);
				if (gameOver) {
						CancelInvoke ("SpawnPanda");
						CancelInvoke ("SpawnMonster");
						foreach (Controller controller in GameObject.FindObjectsOfType<Controller>()) {
								Destroy (controller.gameObject);
						}
						EnableRestarts ();
				}

		}

		void SpawnMonster ()
		{
				Rigidbody2D instance = Instantiate (monster) as Rigidbody2D;
				instance.velocity = new Vector2 (0f, -1f * monsterScore);
				Vector3 position = instance.gameObject.transform.position;
				position.x = Random.Range (-6, 6);
				position.y = Random.Range (27, 32);
				instance.gameObject.transform.position = position;
		}

		void SpawnPanda ()
		{
				Rigidbody2D instance = Instantiate (panda) as Rigidbody2D;
				instance.velocity = new Vector2 (0f, -1f * pandaScore);
				Vector3 position = instance.gameObject.transform.position;
				position.x = Random.Range (-6, 6);
				position.y = Random.Range (27, 32);
				instance.gameObject.transform.position = position;
		}

		public void Restart ()
		{
				Application.LoadLevel (Application.loadedLevel);
		}

		void EnableRestarts ()
		{
				GameObject[] restarts = GameObject.FindGameObjectsWithTag ("restart");
				foreach (GameObject go in restarts) {
						UnityEngine.UI.Button button = go.GetComponent<UnityEngine.UI.Button> ();
						UnityEngine.UI.Text text = go.GetComponent<UnityEngine.UI.Text> ();
						UnityEngine.UI.Image image = go.GetComponent<UnityEngine.UI.Image> ();
						if (button != null)
								button.enabled = true;
						if (text != null)
								text.enabled = true;
						if (image != null)
								image.enabled = true;
				}
		}
}
