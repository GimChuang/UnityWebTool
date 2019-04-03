# UnityWebTool
<img src="https://github.com/GimChuang/UnityWebTool/blob/master/readme_information/WebTool_Component.png">

Simple scripts featuring uploading picture to your server and generate QR codes from the pictures' url.
There is a **Test_WebTool** sample scene showing how I use the scripts.

Dependency
---
This project use LitJson to parse Json files and ZXing to generate QR codes.
- [LitJson.dll](https://github.com/GimChuang/UnityWebTool/blob/master/Assets/Plugins/LitJson.dll)
- [zxing.unity.dll](https://github.com/GimChuang/UnityWebTool/blob/master/Assets/Plugins/zxing.unity.dll)

Scripting Reference
---
**WebTool.cs**
```csharp
UploadPhoto(Texture2D _textureToUpload, string _fileName)
```
uploads `_textureToUpload` as a png with `_fileName` as its filename. Internally this function uses [UnityWebRequest](https://docs.unity3d.com/2018.2/Documentation/ScriptReference/Networking.UnityWebRequest.html) to upload the picture to `urlForUploading` with `bodyKey` and `mimeType`. 
If the uploading is successful, it parses downloaded json string to get the picture's url for **QRCodeGenerator.cs** to generate a QR code. Note that it's for my use case and it may not fit your need ;-)
```csharp
OnUploadPhotoReqFinish(bool success, string photoUrl)
```
is called when the WebRequest finishes.

**QRCodeGenerator.cs**
```csharp
GenerateQRCode(string _textToEncode)
```
encodes `_textToEncode` to a Texture2D.
```csharp
OnQRCodeGenerated(Texture2D _texture)
```
is called when the QR code is generated.

Note
---
This project use Git submodule [gm_WebcamTool](https://github.com/GimChuang/gm_WebcamTool). You need to call
```
git clone --recurse-submodules <URL>
``` 
when you clone this reposiory.
