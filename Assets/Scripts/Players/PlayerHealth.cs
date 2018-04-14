using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : GeneralPlayer, IPlayer {

    public int MaxSanity = 100;
    public int Sanity = 50;
    [HideInInspector]
    public bool IsInsane = false;
    [HideInInspector]
    public bool InsaneConversionAnimPlaying;
    public float BreakingAnimTimeStart = 1f, BreakingAnimTimeEnd = 1f;
    public float BreakDuration = 10f;
	public GameObject BreakParticles;
    public int InsanityDamage = 120;
    public Animator anim;

    public float InsanityStartTime;

    private PlayerManager _playerManager;
    public PlayerManager PlayerManager
    {
        get
        {
            if (_playerManager == null)
            {
                _playerManager = GameObject.FindGameObjectWithTag("Playermanager").GetComponent<PlayerManager>();
            }
            return _playerManager;
        }
    }

	public AudioManager audioManager;
	public AudioClip insanitySound;

	private AudioSource audioSource;

	void Awake() 
	{
		audioSource = gameObject.GetComponent<AudioSource> ();
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManager> ();
	} 
	
    public void RewardSanity(int amount)
    {
        Sanity += amount;
    }

    public void TakeDamage(int amount)
	{
        if (Sanity - amount <= 0)
        {
            StartBreakingSequence();
            Sanity = 0;
        }
        else
            Sanity -= amount;
    }

    void Start() {
        StartCoroutine(DepleteSanity());
    }

    private IEnumerator DepleteSanity() {
        while (true) {
            yield return new WaitForSeconds(PlayerManager.DepleteInterval);
            TakeDamage(PlayerManager.DepletePerTick);
        }
    }

	public void TakeDamage(int amount, IPlayer player)
	{
		TakeDamage(amount);
		player.TakeDamage(amount);
	}

    private void StartBreakingSequence() {
        if (IsInsane)
            return;
        IsInsane = true;
        Debug.Log("IsInsane");
		audioManager.PlaySound (audioSource, insanitySound);
		StartCoroutine(audioManager.ChangeBackgroundPitch(1.08f, 1.4f, 2f));
        StartCoroutine(BreakingSequence());
    }

    private IEnumerator BreakingSequence() {
		Instantiate(BreakParticles, transform.position, Quaternion.identity);
        anim.SetTrigger("PlayerBreaking");
        InsaneConversionAnimPlaying = true;
        yield return new WaitForSeconds(BreakingAnimTimeStart);
        InsaneConversionAnimPlaying = false;
        InsanityStartTime = Time.timeSinceLevelLoad;
        yield return new WaitForSeconds(BreakDuration);
        InsaneConversionAnimPlaying = true;
        anim.SetTrigger("PlayerNormal");
        yield return new WaitForSeconds(BreakingAnimTimeEnd);
        InsaneConversionAnimPlaying = false;
		StartCoroutine(audioManager.ChangeBackgroundPitch(1.4f, 1.08f, 3f));
        IsInsane = false;
        Sanity = (int)(MaxSanity * 0.2f);
    }

    public bool GetIsInsane()
    {
        return IsInsane;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var otherPlayer = collision.gameObject.GetComponent<IPlayer>();
            if (otherPlayer.GetIsInsane()) {
                this.TakeDamage(InsanityDamage);
            }
        }
    }

    public void OnDestroy()
    {
        StopAllCoroutines();
    }
}
