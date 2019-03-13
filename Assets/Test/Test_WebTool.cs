using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_WebTool : MonoBehaviour {

    public WebTool webTool;
    public QRCodeGenerator qrCodeGenerator;

    Texture2D tex2d_screenshot;
    Texture2D tex2d_qrCode;

    public RawImage rImg_qrCode;


    void Start () {

        rImg_qrCode.gameObject.SetActive(false);

        StartCoroutine(E_TakeScreenshot());

    }
	
	void Update () {
		
	}

    void OnEnable()
    {
        WebTool.OnUploadPhotoReqFinish += HandleUploadReqFinish;
        QRCodeGenerator.OnQRCodeGenerated += HandleQRCodeGenerated;
    }

    void OnDisable()
    {
        WebTool.OnUploadPhotoReqFinish -= HandleUploadReqFinish;
        QRCodeGenerator.OnQRCodeGenerated -= HandleQRCodeGenerated;
    }

    IEnumerator E_TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();

        tex2d_screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        tex2d_screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex2d_screenshot.Apply();

        Debug.Log("Photo taken!");

        // Note that if the file name doesn't contain an extension, with the URL you'll directly download a file without extension.
        // If you rename it with .png extension then the file becomes able to be opened.
        webTool.UploadPhoto(tex2d_screenshot, "MyScreenshot.png");
    }

    void HandleUploadReqFinish(bool _success, string _photoUrl)
    {
        if (_success)
        {
            if (_photoUrl != null)
            {
                // Generate QR Code
                qrCodeGenerator.GenerateQRCode(_photoUrl);
            }
            else
                Debug.LogError("photoURL == null");
        }
        else
        {
            Debug.LogError("Failed to upload");
        }
    }

    void HandleQRCodeGenerated(Texture2D _generatedQRCode)
    {
        tex2d_qrCode = _generatedQRCode;
        rImg_qrCode.texture = tex2d_qrCode;
        rImg_qrCode.gameObject.SetActive(true);
    }

}
