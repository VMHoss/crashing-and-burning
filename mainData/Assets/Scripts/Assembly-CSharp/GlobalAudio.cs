using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CD RID: 205
[RequireComponent(typeof(AudioListener))]
public class GlobalAudio : MonoBehaviour
{
	// Token: 0x06000626 RID: 1574 RVA: 0x0002CA64 File Offset: 0x0002AC64
	private void Awake()
	{
		this.pSFXAudioSources = new List<AudioSource>();
		this.pMusicAudioSources = new List<AudioSource>();
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x0002CA7C File Offset: 0x0002AC7C
	private void Update()
	{
		if (Input.GetKeyUp(KeyCode.M))
		{
			Data.muteAllSound = !Data.muteAllSound;
			if (Data.muteAllSound)
			{
				if (Data.sfx)
				{
					this.MuteSFX(true);
				}
				if (Data.music)
				{
					this.MuteMusic(true);
				}
			}
			else
			{
				if (Data.sfx)
				{
					this.MuteSFX(false);
				}
				if (Data.music)
				{
					this.MuteMusic(false);
				}
			}
		}
		this.pCleanLists--;
		if (this.pCleanLists <= 0)
		{
			this.pCleanLists = 60;
			this.CleanAudioSourcesList();
		}
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0002CB20 File Offset: 0x0002AD20
	public AudioSource PlaySFX(string aSFXName)
	{
		return this.PlaySFX(string.Empty, aSFXName, 1f, 0, null);
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0002CB38 File Offset: 0x0002AD38
	public AudioSource PlaySFX(string aSFXName, float aVolume)
	{
		return this.PlaySFX(string.Empty, aSFXName, aVolume, 0, null);
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x0002CB4C File Offset: 0x0002AD4C
	public AudioSource PlaySFX(string aSFXName, GameObject anObjectToBindTo)
	{
		return this.PlaySFX(string.Empty, aSFXName, 1f, 0, anObjectToBindTo);
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0002CB64 File Offset: 0x0002AD64
	public AudioSource PlaySFX(string aSFXName, float aVolume, int aLoop)
	{
		return this.PlaySFX(string.Empty, aSFXName, aVolume, aLoop, null);
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0002CB78 File Offset: 0x0002AD78
	public AudioSource PlaySFX(string aSFXName, float aVolume, int aLoop, GameObject anObjectToBindTo)
	{
		return this.PlaySFX(string.Empty, aSFXName, aVolume, aLoop, anObjectToBindTo);
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0002CB8C File Offset: 0x0002AD8C
	public AudioSource PlaySFX(string aBundle, string aSFXName)
	{
		return this.PlaySFX(aBundle, aSFXName, 1f, 0, null);
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0002CBA0 File Offset: 0x0002ADA0
	public AudioSource PlaySFX(string aBundle, string aSFXName, float aVolume)
	{
		return this.PlaySFX(aBundle, aSFXName, aVolume, 0, null);
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
	public AudioSource PlaySFX(string aBundle, string aSFXName, GameObject anObjectToBindTo)
	{
		return this.PlaySFX(aBundle, aSFXName, 1f, 0, anObjectToBindTo);
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0002CBC4 File Offset: 0x0002ADC4
	public AudioSource PlaySFX(string aBundle, string aSFXName, float aVolume, int aLoop)
	{
		return this.PlaySFX(aBundle, aSFXName, aVolume, aLoop, null);
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0002CBD4 File Offset: 0x0002ADD4
	public AudioSource PlaySFX(string aBundle, string aSFXName, float aVolume, int aLoop, GameObject anObjectToBindTo)
	{
		GameObject gameObject = anObjectToBindTo;
		if (gameObject == null)
		{
			gameObject = base.gameObject;
		}
		AudioClip audioClip;
		if (aBundle.Length == 0)
		{
			audioClip = (Resources.Load("Audio/SFX/" + aSFXName + "_SFX") as AudioClip);
			if (audioClip == null)
			{
				Debug.LogError("SFX sound not found: Audio/SFX/" + aSFXName + "_SFX");
				return null;
			}
		}
		else
		{
			audioClip = Loader.LoadAudio(aBundle, aSFXName + "_SFX");
			if (audioClip == null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"SFX sound not found in bundle ",
					aBundle,
					": Audio/",
					aSFXName,
					"_SFX"
				}));
				return null;
			}
		}
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.priority = 10;
		audioSource.clip = audioClip;
		audioSource.volume = aVolume;
		audioSource.minDistance = 50f;
		audioSource.dopplerLevel = 0f;
		audioSource.mute = (!Data.sfx || Data.muteAllSound);
		if (aLoop != 0)
		{
			audioSource.loop = true;
		}
		if (aLoop >= 0)
		{
			UnityEngine.Object.Destroy(audioSource, audioClip.length * (float)(aLoop + 1));
		}
		audioSource.Play();
		this.pSFXAudioSources.Add(audioSource);
		return audioSource;
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0002CD1C File Offset: 0x0002AF1C
	public AudioSource PlayMusic(string aMusicName)
	{
		return this.PlayMusic(string.Empty, aMusicName, 1f, 0, null);
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0002CD34 File Offset: 0x0002AF34
	public AudioSource PlayMusic(string aMusicName, float aVolume)
	{
		return this.PlayMusic(string.Empty, aMusicName, aVolume, 0, null);
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0002CD48 File Offset: 0x0002AF48
	public AudioSource PlayMusic(string aMusicName, GameObject anObjectToBindTo)
	{
		return this.PlayMusic(string.Empty, aMusicName, 1f, 0, anObjectToBindTo);
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x0002CD60 File Offset: 0x0002AF60
	public AudioSource PlayMusic(string aMusicName, float aVolume, int aLoop)
	{
		return this.PlayMusic(string.Empty, aMusicName, aVolume, aLoop, null);
	}

	// Token: 0x06000636 RID: 1590 RVA: 0x0002CD74 File Offset: 0x0002AF74
	public AudioSource PlayMusic(string aMusicName, float aVolume, int aLoop, GameObject anObjectToBindTo)
	{
		return this.PlayMusic(string.Empty, aMusicName, aVolume, aLoop, anObjectToBindTo);
	}

	// Token: 0x06000637 RID: 1591 RVA: 0x0002CD88 File Offset: 0x0002AF88
	public AudioSource PlayMusic(string aBundle, string aMusicName)
	{
		return this.PlayMusic(aBundle, aMusicName, 1f, 0, null);
	}

	// Token: 0x06000638 RID: 1592 RVA: 0x0002CD9C File Offset: 0x0002AF9C
	public AudioSource PlayMusic(string aBundle, string aMusicName, float aVolume)
	{
		return this.PlayMusic(aBundle, aMusicName, aVolume, 0, null);
	}

	// Token: 0x06000639 RID: 1593 RVA: 0x0002CDAC File Offset: 0x0002AFAC
	public AudioSource PlayMusic(string aBundle, string aMusicName, GameObject anObjectToBindTo)
	{
		return this.PlayMusic(aBundle, aMusicName, 1f, 0, anObjectToBindTo);
	}

	// Token: 0x0600063A RID: 1594 RVA: 0x0002CDC0 File Offset: 0x0002AFC0
	public AudioSource PlayMusic(string aBundle, string aMusicName, float aVolume, int aLoop)
	{
		return this.PlayMusic(aBundle, aMusicName, aVolume, aLoop, null);
	}

	// Token: 0x0600063B RID: 1595 RVA: 0x0002CDD0 File Offset: 0x0002AFD0
	public AudioSource PlayMusic(string aBundle, string aMusicName, float aVolume, int aLoop, GameObject anObjectToBindTo)
	{
		if (GameData.disableAudio)
		{
			return null;
		}
		GameObject gameObject = anObjectToBindTo;
		if (gameObject == null)
		{
			gameObject = base.gameObject;
		}
		AudioClip audioClip;
		if (aBundle.Length == 0)
		{
			audioClip = (Resources.Load("Audio/Music/" + aMusicName + "_MSC") as AudioClip);
			if (audioClip == null)
			{
				Debug.LogError("Music not found: Audio/Music/" + aMusicName + "_MSC");
				return null;
			}
		}
		else
		{
			audioClip = Loader.LoadAudio(aBundle, aMusicName + "_MSC");
			if (audioClip == null)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Music not found in bundle ",
					aBundle,
					": Audio/",
					aMusicName,
					"_MSC"
				}));
				return null;
			}
		}
		AudioSource audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.priority = 1;
		audioSource.clip = audioClip;
		audioSource.volume = aVolume;
		audioSource.mute = (!Data.music || Data.muteAllSound);
		if (aLoop != 0)
		{
			audioSource.loop = true;
		}
		if (aLoop >= 0)
		{
			UnityEngine.Object.Destroy(audioSource, audioClip.length * (float)(aLoop + 1));
		}
		audioSource.Play();
		this.pMusicAudioSources.Add(audioSource);
		return audioSource;
	}

	// Token: 0x0600063C RID: 1596 RVA: 0x0002CF0C File Offset: 0x0002B10C
	public void MuteSFX(bool aMuted)
	{
		int count = this.pSFXAudioSources.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			AudioSource audioSource = this.pSFXAudioSources[i];
			if (audioSource != null)
			{
				audioSource.mute = aMuted;
			}
			else
			{
				this.pSFXAudioSources.RemoveAt(i);
			}
		}
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0002CF6C File Offset: 0x0002B16C
	public void MuteMusic(bool aMuted)
	{
		int count = this.pMusicAudioSources.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			AudioSource audioSource = this.pMusicAudioSources[i];
			if (audioSource != null)
			{
				audioSource.mute = aMuted;
			}
			else
			{
				this.pMusicAudioSources.RemoveAt(i);
			}
		}
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0002CFCC File Offset: 0x0002B1CC
	public void StopSound(string aName)
	{
		string b = aName + "_SFX";
		int count = this.pSFXAudioSources.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			AudioSource audioSource = this.pSFXAudioSources[i];
			if (audioSource != null)
			{
				if (audioSource.clip.name == b)
				{
					UnityEngine.Object.Destroy(audioSource);
					this.pSFXAudioSources.RemoveAt(i);
					return;
				}
			}
			else
			{
				this.pSFXAudioSources.RemoveAt(i);
			}
		}
		string b2 = aName + "_MSC";
		count = this.pMusicAudioSources.Count;
		for (int j = count - 1; j >= 0; j--)
		{
			AudioSource audioSource = this.pMusicAudioSources[j];
			if (audioSource != null)
			{
				if (audioSource.clip.name == b2)
				{
					UnityEngine.Object.Destroy(audioSource);
					this.pMusicAudioSources.RemoveAt(j);
					return;
				}
			}
			else
			{
				this.pMusicAudioSources.RemoveAt(j);
			}
		}
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0002D0E0 File Offset: 0x0002B2E0
	public void StopAllSounds()
	{
		int count = this.pSFXAudioSources.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			AudioSource audioSource = this.pSFXAudioSources[i];
			if (audioSource != null)
			{
				UnityEngine.Object.Destroy(audioSource);
			}
			this.pSFXAudioSources.RemoveAt(i);
		}
		count = this.pMusicAudioSources.Count;
		for (int j = count - 1; j >= 0; j--)
		{
			AudioSource audioSource = this.pMusicAudioSources[j];
			if (audioSource != null)
			{
				UnityEngine.Object.Destroy(audioSource);
			}
			this.pMusicAudioSources.RemoveAt(j);
		}
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0002D184 File Offset: 0x0002B384
	public AudioSource GetSound(string aName)
	{
		string b = aName + "_SFX";
		int count = this.pSFXAudioSources.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			AudioSource audioSource = this.pSFXAudioSources[i];
			if (audioSource != null)
			{
				if (audioSource.clip.name == b)
				{
					return audioSource;
				}
			}
			else
			{
				this.pSFXAudioSources.RemoveAt(i);
			}
		}
		string b2 = aName + "_MSC";
		count = this.pMusicAudioSources.Count;
		for (int j = count - 1; j >= 0; j--)
		{
			AudioSource audioSource = this.pMusicAudioSources[j];
			if (audioSource != null)
			{
				if (audioSource.clip.name == b2)
				{
					return audioSource;
				}
			}
			else
			{
				this.pMusicAudioSources.RemoveAt(j);
			}
		}
		return null;
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0002D278 File Offset: 0x0002B478
	private void CleanAudioSourcesList()
	{
		int count = this.pSFXAudioSources.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			AudioSource x = this.pSFXAudioSources[i];
			if (x == null)
			{
				this.pSFXAudioSources.RemoveAt(i);
			}
		}
		count = this.pMusicAudioSources.Count;
		for (int j = count - 1; j >= 0; j--)
		{
			AudioSource x = this.pMusicAudioSources[j];
			if (x == null)
			{
				this.pMusicAudioSources.RemoveAt(j);
			}
		}
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0002D310 File Offset: 0x0002B510
	public void SetMusicVolume(float aVolume)
	{
		int count = this.pMusicAudioSources.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			AudioSource audioSource = this.pMusicAudioSources[i];
			if (audioSource != null)
			{
				audioSource.volume = aVolume;
			}
		}
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0002D360 File Offset: 0x0002B560
	public void StopAllMusic()
	{
		int count = this.pMusicAudioSources.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			AudioSource audioSource = this.pMusicAudioSources[i];
			if (audioSource != null)
			{
				UnityEngine.Object.Destroy(audioSource);
			}
			this.pMusicAudioSources.RemoveAt(i);
		}
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0002D3B8 File Offset: 0x0002B5B8
	public AudioSource LogAllSounds()
	{
		Debug.Log("LogAllSounds:");
		Debug.Log("SFX:");
		int count = this.pSFXAudioSources.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			AudioSource audioSource = this.pSFXAudioSources[i];
			if (audioSource != null)
			{
				Debug.Log(audioSource.name);
			}
		}
		Debug.Log("Music:");
		count = this.pMusicAudioSources.Count;
		for (int j = count - 1; j >= 0; j--)
		{
			AudioSource audioSource = this.pMusicAudioSources[j];
			if (audioSource != null)
			{
				Debug.Log(audioSource.name);
			}
		}
		return null;
	}

	// Token: 0x040006AE RID: 1710
	private const int LIST_CLEAN_TIME = 60;

	// Token: 0x040006AF RID: 1711
	private List<AudioSource> pSFXAudioSources;

	// Token: 0x040006B0 RID: 1712
	private List<AudioSource> pMusicAudioSources;

	// Token: 0x040006B1 RID: 1713
	private int pCleanLists = 60;
}
