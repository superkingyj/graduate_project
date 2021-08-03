using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	// This must be assigned value from the Inspector
	public Texture2D image;

	private GUIStyle buttonStyle;
	private int optionsUnlocked;

	private void OnGUI()
	{
		if (buttonStyle == null)
			buttonStyle = new GUIStyle(GUI.skin.button) { fontSize = Screen.height / 40, wordWrap = true };

		float width = Screen.width;
		float height = Screen.height;
		if (optionsUnlocked == 0)
		{
			if (GUI.Button(new Rect(0f, 0f, width, height * 0.2f), "See Buttons", buttonStyle))
			{
				optionsUnlocked = 1;
				Invoke("UnlockOptions", 0.1f);
				GUIUtility.ExitGUI();
			}
		}
		else if (optionsUnlocked == 2)
		{
			bool interacted = false;

			//if (GUI.Button(new Rect(0f, 0f, width / 3, height * 0.2f), "Permission Free Mode: " + NativeGallery.PermissionFreeMode, buttonStyle))
			//	NativeGallery.PermissionFreeMode = !NativeGallery.PermissionFreeMode;
			if (GUI.Button(new Rect(width / 3, 0f, width / 3, height * 0.2f), "Check Read Permission", buttonStyle))
				print(NativeGallery.CheckPermission(NativeGallery.PermissionType.Read));
			if (GUI.Button(new Rect(width * 2 / 3, 0f, width / 3, height * 0.2f), "Check Write Permission", buttonStyle))
				print(NativeGallery.CheckPermission(NativeGallery.PermissionType.Write));

			if (GUI.Button(new Rect(0f, height * 0.2f, width / 3, height * 0.2f), "Request Read Permission", buttonStyle))
			{
				interacted = true;
				print(NativeGallery.RequestPermission(NativeGallery.PermissionType.Read));
			}
			if (GUI.Button(new Rect(width / 3, height * 0.2f, width / 3, height * 0.2f), "Request Write Permission", buttonStyle))
			{
				interacted = true;
				print(NativeGallery.RequestPermission(NativeGallery.PermissionType.Write));
			}
			if (GUI.Button(new Rect(width * 2 / 3, height * 0.2f, width / 3, height * 0.2f), "Extend Limited Permission", buttonStyle))
			{
				interacted = true;
				//NativeGallery.TryExtendLimitedAccessPermission();
			}

			if (GUI.Button(new Rect(0f, height * 0.4f, width / 3, height * 0.2f), "Get Image", buttonStyle))
			{
				interacted = true;
				print(NativeGallery.GetImageFromGallery((path) =>
				{
					print(path);
					DisplayImage(path);
				}));
			}
			if (GUI.Button(new Rect(width / 3, height * 0.4f, width / 3, height * 0.2f), "Get Video", buttonStyle))
			{
				interacted = true;
				print(NativeGallery.GetVideoFromGallery((path) =>
				{
					print(path);
					DisplayVideo(path);
				}));
			}
			if (GUI.Button(new Rect(width * 2 / 3, height * 0.4f, width / 3, height * 0.2f), "Get Image/Video", buttonStyle))
			{
				interacted = true;
				print(NativeGallery.GetMixedMediaFromGallery((path) =>
				{
					print(path);
					if (path != null)
					{
						// Determine if user has picked an image, video or neither of these
						switch (NativeGallery.GetMediaTypeOfFile(path))
						{
							case NativeGallery.MediaType.Image: Debug.Log("Picked image"); break;
							case NativeGallery.MediaType.Video: Debug.Log("Picked video"); break;
							default: Debug.Log("Probably picked something else"); break;
						}
					}
				}, NativeGallery.MediaType.Image | NativeGallery.MediaType.Video));
			}

			if (GUI.Button(new Rect(0f, height * 0.6f, width / 3, height * 0.2f), "Get Images", buttonStyle))
			{
				interacted = true;
				print(NativeGallery.GetImagesFromGallery((paths) =>
				{
					if (paths != null)
					{
						for (int i = 0; i < paths.Length; i++)
						{
							print(paths[i]);

							if (i == paths.Length - 1)
								DisplayImage(paths[i]);
						}
					}
				}));
			}
			if (GUI.Button(new Rect(width / 3, height * 0.6f, width / 3, height * 0.2f), "Get Videos", buttonStyle))
			{
				interacted = true;
				print(NativeGallery.GetVideosFromGallery((paths) =>
				{
					if (paths != null)
					{
						for (int i = 0; i < paths.Length; i++)
						{
							print(paths[i]);

							if (i == paths.Length - 1)
								DisplayVideo(paths[i]);
						}
					}
				}));
			}
			if (GUI.Button(new Rect(width * 2 / 3, height * 0.6f, width / 3, height * 0.2f), "Get Images/Videos", buttonStyle))
			{
				interacted = true;
				print(NativeGallery.GetMixedMediasFromGallery((paths) =>
				{
					if (paths != null)
					{
						for (int i = 0; i < paths.Length; i++)
						{
							print(paths[i]);

							// Determine if user has picked an image, video or neither of these
							switch (NativeGallery.GetMediaTypeOfFile(paths[i]))
							{
								case NativeGallery.MediaType.Image: Debug.Log("Picked image"); break;
								case NativeGallery.MediaType.Video: Debug.Log("Picked video"); break;
								default: Debug.Log("Probably picked something else"); break;
							}
						}
					}
				}, NativeGallery.MediaType.Image | NativeGallery.MediaType.Video));
			}

			if (GUI.Button(new Rect(0f, height * 0.8f, width, height * 0.2f), "Save Image", buttonStyle))
			{
				interacted = true;
				print(NativeGallery.SaveImageToGallery(image, "Test Album", "Hello world.png", (bool success, string path) =>
				{
					print("Save result: " + success + " " + path);
				}));
			}

			if (interacted)
			{
				optionsUnlocked = 1;
				Invoke("LockOptions", 0.1f);
				GUIUtility.ExitGUI();
			}
		}
	}

	private void DisplayImage(string path)
	{
		if (path != null)
		{
			// Create Texture from selected image
			Texture2D texture = NativeGallery.LoadImageAtPath(path, 512);
			if (texture == null)
			{
				Debug.Log("Couldn't load texture from " + path);
				return;
			}

			// Assign texture to a temporary quad and destroy it after 5 seconds
			GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
			quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
			quad.transform.forward = Camera.main.transform.forward;
			quad.transform.localScale = new Vector3(1f, texture.height / (float)texture.width, 1f);

			Material material = quad.GetComponent<Renderer>().material;
			if (!material.shader.isSupported) // happens when Standard shader is not included in the build
				material.shader = Shader.Find("Legacy Shaders/Diffuse");

			material.mainTexture = texture;

			Destroy(quad, 5f);
			Destroy(texture, 5f);
		}
	}

	private void DisplayVideo(string path)
	{
		if (path != null)
			Handheld.PlayFullScreenMovie("file://" + path); // It is OK if this function sometimes fails due to "Couldn't attach movie player to UIImagePickerController"-like warnings
	}

	private void UnlockOptions()
	{
		optionsUnlocked = 2;
	}

	private void LockOptions()
	{
		optionsUnlocked = 0;
	}

}

