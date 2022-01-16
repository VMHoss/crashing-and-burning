using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class CameraScript : MonoBehaviour
{
	// Token: 0x060006EE RID: 1774 RVA: 0x0003228C File Offset: 0x0003048C
	public void DecCamfar()
	{
		if (this.pCamera.farClipPlane > 99f)
		{
			this.pCamera.farClipPlane -= 50f;
		}
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x000322C8 File Offset: 0x000304C8
	public void IncCamfar()
	{
		if (this.pCamera.farClipPlane < 2200f)
		{
			this.pCamera.farClipPlane += 50f;
		}
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x00032304 File Offset: 0x00030504
	private void Awake()
	{
		this.pCamParent = new GameObject();
		this.pCamParent.name = "CameraParent";
		this.pGenericIntroCameras = new Transform[10];
		for (int i = 0; i < 10; i++)
		{
			this.pGenericIntroCameras[i] = null;
		}
		this.pCarIntroCameras = new Dictionary<string, Transform[]>();
		this.pActualIntroCameras = new Transform[10];
		for (int j = 0; j < 10; j++)
		{
			this.pActualIntroCameras[j] = null;
		}
		this.pOutroCameras = new Transform[10];
		for (int k = 0; k < 10; k++)
		{
			this.pOutroCameras[k] = null;
		}
		GameObject track = Scripts.trackScript.track;
		string key = string.Empty;
		foreach (object obj in track.transform)
		{
			Transform transform = (Transform)obj;
			GameObject gameObject = transform.gameObject;
			if (gameObject.name.Contains("Camera_Intro") && gameObject.name.Contains("Animation"))
			{
				int num = gameObject.name.IndexOf("Animation");
				if (num == 12)
				{
					int num2 = int.Parse(gameObject.transform.GetChild(0).gameObject.name.Substring(21, 2));
					this.pGenericIntroCameras[num2] = gameObject.transform.GetChild(0);
				}
				else
				{
					key = gameObject.name.Substring(12, num - 12);
					int num2 = int.Parse(gameObject.name.Substring(num + 9, 2));
					if (!this.pCarIntroCameras.ContainsKey(key))
					{
						Transform[] array = new Transform[10];
						for (int l = 0; l < 10; l++)
						{
							array[l] = null;
						}
						this.pCarIntroCameras.Add(key, array);
					}
					this.pCarIntroCameras[key][num2] = gameObject.transform;
				}
			}
			else if (gameObject.name.Contains("Camera_Outro") && gameObject.name.Contains("Animation"))
			{
				int num2 = int.Parse(gameObject.name.Substring(21, 2));
				this.pOutroCameras[num2] = gameObject.transform;
			}
		}
		this.pCamera = base.camera;
		Dictionary<string, DicEntry> d = Data.Shared["GridSystem"].d;
		this.pCamera.farClipPlane = (float)(d["BlockSize"].i * ((!Data.highDetails) ? 1 : d["BlockShowDist"].i));
		this.pCamera.backgroundColor = Color.black;
		this.pCamera.cullingMask = ~(1 << GameData.interfaceLayer | 1 << GameData.skyboxLayer | 1 << GameData.minimapLayer | 1 << GameData.invisibleWallLayer);
		this.pCamera.clearFlags = CameraClearFlags.Depth;
		this.pCameraShakes = new List<CameraShake>();
		this.ResetCam();
		base.gameObject.AddComponent("FlareLayer");
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x00032678 File Offset: 0x00030878
	public void AddCameraShake(CameraShake aCameraShake)
	{
		this.pCameraShakes.Add(aCameraShake);
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x00032688 File Offset: 0x00030888
	public void ResetCam()
	{
		this.pState = CameraScript.pCamState.STILL;
		base.transform.position = new Vector3(-902f, 70.6f, -835.6f);
		base.transform.rotation = Quaternion.Euler(2.2f, 217f, 0f);
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x000326DC File Offset: 0x000308DC
	public void DoIntroCamera(string aVehicle)
	{
		if (aVehicle == "ChevyNova")
		{
			aVehicle = "Mustang";
		}
		for (int i = 0; i < 10; i++)
		{
			this.pActualIntroCameras[i] = null;
		}
		int num = 0;
		if (this.pCarIntroCameras.ContainsKey(aVehicle))
		{
			foreach (Transform transform in this.pCarIntroCameras[aVehicle])
			{
				if (transform != null)
				{
					this.pActualIntroCameras[num] = transform;
					num++;
				}
			}
		}
		foreach (Transform transform2 in this.pGenericIntroCameras)
		{
			if (transform2 != null)
			{
				this.pActualIntroCameras[num] = transform2;
				num++;
			}
		}
		this.pActualIntroCamNumber = 0;
		if (this.pActualIntroCameras[this.pActualIntroCamNumber] == null)
		{
			Debug.LogWarning("There are no intro animations!");
			return;
		}
		this.pState = CameraScript.pCamState.INTRO;
		base.transform.parent = this.pActualIntroCameras[this.pActualIntroCamNumber];
		base.transform.localPosition = new Vector3(0f, 0f, 0f);
		base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x00032844 File Offset: 0x00030A44
	public void DoCarCamera()
	{
		this.pState = CameraScript.pCamState.CAR;
		base.transform.parent = this.pCamParent.transform;
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x00032864 File Offset: 0x00030A64
	public void DoNukeCamera()
	{
		this.pState = CameraScript.pCamState.NUKE;
		this.pNukeDummy = new GameObject("NukeCameraDummy");
		this.pNukeDummy.transform.position = this.followedCar.transform.position;
		base.transform.parent = null;
		base.transform.position = this.followedCar.transform.position + new Vector3(0f, 30f, -200f);
		base.transform.LookAt(this.followedCar.transform.position + new Vector3(0f, 15f, 0f));
		this.pNukeRotation = base.transform.rotation;
		base.transform.parent = this.pNukeDummy.transform;
		this.pCamera.fieldOfView = Data.Shared["Misc"].d["BuildingCamFoV"].f;
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x00032974 File Offset: 0x00030B74
	public void PauseGame()
	{
		this.LateUpdate();
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0003297C File Offset: 0x00030B7C
	public void LockCamera()
	{
		this.pLockCamera = true;
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x00032988 File Offset: 0x00030B88
	public float GetCurrentYAngle()
	{
		return this.pCurCamYAngle;
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x00032990 File Offset: 0x00030B90
	private void LateUpdate()
	{
		if (Data.debug && (Input.GetKeyDown(KeyCode.BackQuote) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && Input.GetTouch(0).position.sqrMagnitude < 4900f)))
		{
			FPSScript component = base.gameObject.GetComponent<FPSScript>();
			if (component != null)
			{
				UnityEngine.Object.Destroy(component);
			}
			else
			{
				base.gameObject.AddComponent<FPSScript>();
			}
		}
		if (Data.pause)
		{
			return;
		}
		switch (this.pState)
		{
		case CameraScript.pCamState.INTRO:
			this.IntroCameraUpdate();
			break;
		case CameraScript.pCamState.CAR:
			this.CarCameraUpdate();
			break;
		case CameraScript.pCamState.NUKE:
			this.NukeCameraUpdate();
			break;
		case CameraScript.pCamState.OUTRO:
			this.OutroCameraUpdate();
			break;
		}
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x00032A84 File Offset: 0x00030C84
	private void IntroCameraUpdate()
	{
		if ((double)this.pWaitIntroTime > 0.0)
		{
			this.pWaitIntroTime -= Time.deltaTime;
			if ((double)this.pWaitIntroTime > 0.0)
			{
				return;
			}
			this.pAnimationTime = 0f;
		}
		if (this.pAnimationTime >= 2f)
		{
			this.pActualIntroCamNumber++;
			if (this.pActualIntroCameras[this.pActualIntroCamNumber] == null)
			{
				this.pActualIntroCamNumber = 0;
			}
			this.pAnimationTime = 0f;
			base.transform.parent = this.pActualIntroCameras[this.pActualIntroCamNumber];
			base.transform.localPosition = new Vector3(0f, 0f, 0f);
			base.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		}
		else
		{
			this.pAnimationTime += Time.deltaTime;
		}
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x00032B98 File Offset: 0x00030D98
	private void CarCameraUpdate()
	{
		float num = 6.5f * Time.deltaTime;
		this.pCamParent.transform.position = this.followedCar.transform.position;
		Vector3 vector = -this.followedCar.gameObject.transform.forward * (float)((!Input.GetKey("c") && !this.reverseCarCamera) ? 1 : -1);
		vector.y = 0f;
		float num2 = Vector3.Angle(vector, Vector3.forward);
		float num3 = Vector3.Dot(vector, Vector3.right);
		if (num3 < 0f)
		{
			num2 = -num2;
		}
		float num4 = num2;
		if (num4 < -90f && this.pCurCamYAngle > 90f)
		{
			this.pCurCamYAngle -= 360f;
		}
		if (num4 > 90f && this.pCurCamYAngle < -90f)
		{
			this.pCurCamYAngle += 360f;
		}
		float num5 = this.followedCar.GetDriftParam();
		num5 = Mathf.Clamp(1f - num5, 0.6f, 1f);
		if (Mathf.Abs(num4 - this.pCurCamYAngle) > 0.25f && !this.pLockCamera)
		{
			this.pCurCamYAngle += (num4 - this.pCurCamYAngle) * num * num5;
		}
		if (!this.reverseCarCamera && (Input.GetKeyDown("c") || Input.GetKeyUp("c")))
		{
			this.pCurCamYAngle = num4;
		}
		float num6 = 0f;
		Vector3 forward = this.followedCar.transform.forward;
		if (forward.y < -0.1f && Vector3.Dot(this.followedCar.transform.up, Vector3.up) >= 0.3f)
		{
			forward.y = 0f;
			num6 = -Vector3.Angle(forward, this.followedCar.transform.forward);
		}
		float num7 = this.followedCar.GetAirTime() - 1f;
		num7 = Mathf.Clamp01(num7 * 60f);
		this.pCurCamXAngle += (num6 - num7 * 15f - this.pCurCamXAngle) * num;
		this.pCamParent.transform.rotation = Quaternion.Euler((!this.pLockCamera) ? this.pCurCamXAngle : 0f, this.pCurCamYAngle, 0f);
		float speed = this.followedCar.GetSpeed();
		float num8;
		if (this.dashEffectTime > 0f)
		{
			num8 = this.followedCar.carData.camPosZ + 15f * this.dashEffectTime;
			num *= 2f;
		}
		else
		{
			num8 = (1f + num7) * Mathf.Lerp(this.followedCar.carData.camPosZ, this.followedCar.carData.nitroCamPosZ, this.followedCar.carData.nitroIntensity) + Mathf.Max(speed * 0.045f, -1f);
		}
		this.pZoomValue += (num8 - this.pZoomValue) * num;
		Vector3 localPosition = new Vector3(0f, Mathf.Lerp(this.followedCar.carData.camPosY, this.followedCar.carData.nitroCamPosY, this.followedCar.carData.nitroIntensity), this.pZoomValue);
		float num9 = 0f;
		if (this.pLockCamera)
		{
			this.pTimeInLock += Time.deltaTime / Time.timeScale;
			float num10 = Mathf.Min(this.pTimeInLock, 2f) * 0.5f;
			float num11 = 1f;
			if (num10 < 0.99f)
			{
				float num12 = 1f - num10 * num10;
				num11 = 1f - num12 * num12;
			}
			localPosition.y += num11 * 40f;
			localPosition.z += num11 * 20f;
			num9 += num11 * 15f;
		}
		base.transform.localPosition = localPosition;
		Vector3 position = this.pCamParent.transform.position;
		position.y += this.followedCar.carData.camAimY;
		position.y += Mathf.Abs(speed) * 0.0125f;
		base.transform.LookAt(position);
		num9 += GameData.carFoV - Mathf.Abs(speed * 0.1f) + this.followedCar.carData.nitroIntensity * 20f;
		this.pCamera.fieldOfView = num9;
		float num13 = this.followedCar.GetWheelTurn() * 2f;
		num13 *= Mathf.Clamp01((speed - 15f) * 0.0222f);
		this.pBankAngle += (num13 - this.pBankAngle) * num;
		base.transform.localRotation *= Quaternion.Euler(0f, 0f, this.pBankAngle * (float)((!Input.GetKey("c") && !this.reverseCarCamera) ? -1 : 1));
		this.HandleCameraShakes();
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x00033108 File Offset: 0x00031308
	private void NukeCameraUpdate()
	{
		this.pNukeDummy.transform.rotation *= Quaternion.Euler(0f, Time.deltaTime * 2f, 0f);
		base.transform.localRotation = this.pNukeRotation;
		this.HandleCameraShakes();
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x00033164 File Offset: 0x00031364
	private void OutroCameraUpdate()
	{
		if ((double)this.pWaitOutroTime > 0.0)
		{
			this.pWaitOutroTime -= Time.deltaTime;
			if ((double)this.pWaitOutroTime > 0.0)
			{
				return;
			}
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x000331B4 File Offset: 0x000313B4
	private void HandleCameraShakes()
	{
		float num = Time.deltaTime / Time.timeScale;
		for (int i = this.pCameraShakes.Count - 1; i >= 0; i--)
		{
			CameraShake cameraShake = this.pCameraShakes[i];
			cameraShake.duration -= num;
			if (cameraShake.duration <= 0f)
			{
				this.pCameraShakes.RemoveAt(i);
			}
			else if (cameraShake.duration > cameraShake.fallOff)
			{
				base.transform.localRotation *= Quaternion.Euler(UnityEngine.Random.insideUnitSphere * cameraShake.intensity);
			}
			else
			{
				base.transform.localRotation *= Quaternion.Euler(UnityEngine.Random.insideUnitSphere * cameraShake.intensity * cameraShake.duration * cameraShake.fallOffInv);
			}
		}
		if (this.driveCamShakeIntensity > 0f)
		{
			if (this.pDriveCamShakeProgress > 1f)
			{
				this.pDriveCamShakeProgress = 0f;
				this.pLastDriveCamShakePos = this.pNewDriveCamShakePos;
				this.pNewDriveCamShakePos = UnityEngine.Random.insideUnitSphere;
			}
			else
			{
				this.pDriveCamShakeProgress += 0.20001f;
			}
			base.transform.localRotation *= Quaternion.Euler(Vector3.Slerp(this.pLastDriveCamShakePos, this.pNewDriveCamShakePos, this.pDriveCamShakeProgress) * this.driveCamShakeIntensity);
		}
	}

	// Token: 0x04000706 RID: 1798
	private const float CAMERA_INTERPOLATION_SPEED = 6.5f;

	// Token: 0x04000707 RID: 1799
	public CarScript followedCar;

	// Token: 0x04000708 RID: 1800
	public bool reverseCarCamera;

	// Token: 0x04000709 RID: 1801
	public float dashEffectTime = -1f;

	// Token: 0x0400070A RID: 1802
	public float driveCamShakeIntensity;

	// Token: 0x0400070B RID: 1803
	private float pCurCamYAngle;

	// Token: 0x0400070C RID: 1804
	private float pZoomValue;

	// Token: 0x0400070D RID: 1805
	private float pCurCamXAngle;

	// Token: 0x0400070E RID: 1806
	private Camera pCamera;

	// Token: 0x0400070F RID: 1807
	private CameraScript.pCamState pState;

	// Token: 0x04000710 RID: 1808
	private Transform[] pGenericIntroCameras;

	// Token: 0x04000711 RID: 1809
	private Dictionary<string, Transform[]> pCarIntroCameras;

	// Token: 0x04000712 RID: 1810
	private Transform[] pActualIntroCameras;

	// Token: 0x04000713 RID: 1811
	private Transform[] pOutroCameras;

	// Token: 0x04000714 RID: 1812
	private int pActualIntroCamNumber = 1;

	// Token: 0x04000715 RID: 1813
	private GameObject pCamParent;

	// Token: 0x04000716 RID: 1814
	private float pWaitIntroTime = 1f;

	// Token: 0x04000717 RID: 1815
	private float pWaitOutroTime = 0.5f;

	// Token: 0x04000718 RID: 1816
	private float pAnimationTime;

	// Token: 0x04000719 RID: 1817
	private float pBankAngle;

	// Token: 0x0400071A RID: 1818
	private List<CameraShake> pCameraShakes;

	// Token: 0x0400071B RID: 1819
	private GameObject pNukeDummy;

	// Token: 0x0400071C RID: 1820
	private bool pLockCamera;

	// Token: 0x0400071D RID: 1821
	private float pTimeInLock;

	// Token: 0x0400071E RID: 1822
	private Quaternion pNukeRotation;

	// Token: 0x0400071F RID: 1823
	private Vector3 pLastDriveCamShakePos = new Vector3(0f, 0f, 0f);

	// Token: 0x04000720 RID: 1824
	private Vector3 pNewDriveCamShakePos = new Vector3(0f, 0f, 0f);

	// Token: 0x04000721 RID: 1825
	private float pDriveCamShakeProgress = 1.1f;

	// Token: 0x020000E5 RID: 229
	private enum pCamState
	{
		// Token: 0x04000723 RID: 1827
		STILL,
		// Token: 0x04000724 RID: 1828
		INTRO,
		// Token: 0x04000725 RID: 1829
		CAR,
		// Token: 0x04000726 RID: 1830
		NUKE,
		// Token: 0x04000727 RID: 1831
		OUTRO
	}
}
