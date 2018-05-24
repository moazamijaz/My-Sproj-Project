#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class filepanel : MonoBehaviour {

	// Use this for initialization
	public static string newpath;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	[MenuItem("Example/Overwrite Texture")]
	static void Apply()
	{
		
		Texture2D texture = Selection.activeObject as Texture2D;
		if (texture == null)
		{
			EditorUtility.DisplayDialog("Select Texture", "You must select a texture first!", "OK");
			return;
		}
		string path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
		if (path.Length != 0)
		{
			var fileContent = File.ReadAllBytes(path);
			texture.LoadImage(fileContent);
		}
	}


	public void filepath(){
		newpath = EditorUtility.OpenFilePanel("Overwrite with png", "", "mp4");
		Debug.Log (newpath);

	}

	public void StartVideo(){
		SceneManager.LoadScene("cameraScene");
	}

}
#endif