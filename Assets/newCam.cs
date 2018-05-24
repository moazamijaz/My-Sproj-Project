using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class newCam : MonoBehaviour {


	private bool camAvailable;
	private WebCamTexture backCam;
	private Texture defaultBackground;

	public RawImage background;
	public AspectRatioFitter fit;


	static readonly float MaxRecordingTime = 5.0f;
	
	//UnityEngine.XR.WSA.WebCam.VideoCapture m_VideoCapture = null;
	float m_stopRecordingTimer = float.MaxValue;
	// Use this for initialization
	void Start () {

		//StartVideoCaptureTest();

		defaultBackground = background.texture;
		WebCamDevice[] devices = WebCamTexture.devices;

		if (devices.Length == 0) {
			Debug.Log ("Cam is Missing");

			camAvailable = false;
			return;
		}

		for (int i = 0; i < devices.Length; i++) {
		
			if (devices[i].isFrontFacing){

				backCam = new WebCamTexture (devices [i].name, Screen.width, Screen.height);
		
		}
			if (backCam == null) {
			
				Debug.Log ("NO back Cam");
				return;
			}
			backCam.Play ();
			background.texture = backCam;

			camAvailable = true;

	}
	}


	// Update is called once per frame
	void Update () {
		if (!camAvailable) {
			return;
		}

		float ratio = (float)backCam.width / (float)backCam.height;
		fit.aspectRatio = ratio;

		float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
		background.rectTransform.localScale = new Vector3 (1f, scaleY, 1f);

		int orientation = -backCam.videoRotationAngle;
		background.rectTransform.localEulerAngles = new Vector3 (0, 0, orientation);
		/*
		if (m_VideoCapture == null || !m_VideoCapture.IsRecording)
		{
			return;
		}

		if (Time.time > m_stopRecordingTimer)
		{
			m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
		}*/
	}

	/*
	//public void StopRecordingAsync(VR.WSA.WebCam.VideoCapture.OnStoppedRecordingVideoCallback onStoppedRecordingVideoCallback);
	void StartVideoCaptureTest()
	{
		Resolution cameraResolution = UnityEngine.XR.WSA.WebCam.VideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
		Debug.Log(cameraResolution);

		float cameraFramerate = UnityEngine.XR.WSA.WebCam.VideoCapture.GetSupportedFrameRatesForResolution(cameraResolution).OrderByDescending((fps) => fps).First();
		Debug.Log(cameraFramerate);

		UnityEngine.XR.WSA.WebCam.VideoCapture.CreateAsync(false, delegate(UnityEngine.XR.WSA.WebCam.VideoCapture videoCapture){
				if (videoCapture != null)
				{
					m_VideoCapture = videoCapture;
					Debug.Log("Created VideoCapture Instance!");

					UnityEngine.XR.WSA.WebCam.CameraParameters cameraParameters = new UnityEngine.XR.WSA.WebCam.CameraParameters();
					cameraParameters.hologramOpacity = 0.0f;
					cameraParameters.frameRate = cameraFramerate;
					cameraParameters.cameraResolutionWidth = cameraResolution.width;
					cameraParameters.cameraResolutionHeight = cameraResolution.height;
					cameraParameters.pixelFormat = UnityEngine.XR.WSA.WebCam.CapturePixelFormat.BGRA32;

					m_VideoCapture.StartVideoModeAsync(cameraParameters,
						UnityEngine.XR.WSA.WebCam.VideoCapture.AudioState.ApplicationAndMicAudio,
						OnStartedVideoCaptureMode);
				}
				else
				{
					Debug.LogError("Failed to create VideoCapture Instance!");
				}
			});
	}

	void OnStartedVideoCaptureMode(UnityEngine.XR.WSA.WebCam.VideoCapture.VideoCaptureResult result)
	{
		Debug.Log("Started Video Capture Mode!");
		string timeStamp = Time.time.ToString().Replace(".", "").Replace(":", "");
		string filename = string.Format("TestVideo_{0}.mp4", timeStamp);
		string filepath = System.IO.Path.Combine(Application.persistentDataPath, filename);
		filepath = filepath.Replace("/", @"\");
		m_VideoCapture.StartRecordingAsync(filepath, OnStartedRecordingVideo);
	}

	void OnStoppedVideoCaptureMode(UnityEngine.XR.WSA.WebCam.VideoCapture.VideoCaptureResult result)
	{
		Debug.Log("Stopped Video Capture Mode!");
	}

	void OnStartedRecordingVideo(UnityEngine.XR.WSA.WebCam.VideoCapture.VideoCaptureResult result)
	{
		Debug.Log("Started Recording Video!");
		m_stopRecordingTimer = Time.time + MaxRecordingTime;
	}

	void OnStoppedRecordingVideo(UnityEngine.XR.WSA.WebCam.VideoCapture.VideoCaptureResult result)
	{
		Debug.Log("Stopped Recording Video!");
		m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
	}
	*/

}
