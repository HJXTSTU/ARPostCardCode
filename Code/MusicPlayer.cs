using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class MusicPlayer : MonoBehaviour {

	//private static object mutex = new object();
	private static MusicPlayer _Instance;
	public static MusicPlayer Insatance{
		get{
			return _Instance;
		}
	}

	private AudioSource m_audioSource;
	//private AudioClip m_currentMusic;
	private MusicState m_state=MusicState.IDLE;

	public void SetAudioClip(AudioClip clip){
		this.m_audioSource.clip = clip;
	}

	void Awake(){
		_Instance = this;
		m_audioSource = GetComponent<AudioSource> ();
	}

	public void ClearAudioClip(){
		if (m_audioSource.isPlaying)
			m_audioSource.Stop ();
		m_audioSource.clip = null;
		m_state = MusicState.IDLE;
	}

	public void Play(){
		if (m_audioSource.clip != null && !m_audioSource.isPlaying) {
			if (m_state == MusicState.IDLE)
				m_audioSource.Play ();
			else if (m_state == MusicState.PAUSE)
				m_audioSource.UnPause ();
			m_state = MusicState.PLAYING;
		}
	}

	public void Pause(){
		if (m_audioSource.clip != null && m_audioSource.isPlaying) {
			m_audioSource.Pause ();
			m_state = MusicState.PAUSE;
		}
	}

	public void Stop(){
		if (m_audioSource.clip != null && (m_audioSource.isPlaying||m_state==MusicState.PAUSE)) {
			m_audioSource.Stop ();
			m_state = MusicState.IDLE;
		}
	}


	public bool HasAudio(){
		return m_audioSource.clip != null;
	}

}
