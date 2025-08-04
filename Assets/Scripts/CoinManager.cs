


using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Collidable mainCharacter;
    List<GameObject> coins = new List<GameObject>();

    private AudioSource coinSoundPlayer;
    [SerializeField] private AudioClip coinSound;

    public void Start()
    {
        for (float x = -5.0f; x <= 5.0f; x += 1.0f)
        {
            AddThreeCoins(x * 10.0f);
        }
        SpawnCoin(new Vector3(0, 8, 0));
        SpawnCoin(new Vector3(-2, 6, 0));
        SpawnCoin(new Vector3(2, 6, 0));
        coinSoundPlayer = gameObject.AddComponent<AudioSource>();
        if (coinSound != null)
        {
            coinSoundPlayer.clip = coinSound;
            coinSoundPlayer.playOnAwake = false;
            coinSoundPlayer.loop = false;
            coinSoundPlayer.volume = 1.0f; // Adjust volume as needed
        }
        else
        {
            Debug.LogWarning("Coin sound not assigned in CoinManager.");
        }
    }

    private void AddThreeCoins(float x)
    {

        SpawnCoin(new Vector3(x, 8, 0));
        SpawnCoin(new Vector3(x-2, 6, 0));
        SpawnCoin(new Vector3(x+2, 6, 0));
    }

    public void Update()
    {
        CheckCollisions();
    }

    public void SpawnCoin(Vector3 position)
    {
        GameObject coin = Instantiate(coinPrefab, transform);
        coin.transform.localPosition = position;
        coins.Add(coin);
    }

    public void RemoveCoin(GameObject coin)
    {
        if (coins.Contains(coin))
        {
            coins.Remove(coin);
            Destroy(coin);
        }
    }


    public void CheckCollisions()
    {
        List<GameObject> collidingCoins = new List<GameObject>();
        foreach (GameObject coin in coins)
        {
            Collidable coinCollidable = coin.GetComponent<Collidable>();
            if (coinCollidable != null)
            {
                if (BoxCollider3DLike.Intersects(coinCollidable.BoxCollider, mainCharacter.BoxCollider))
                {
                    collidingCoins.Add(coin);
                    PlayCoinSound();
                }
            }
        }
        foreach (GameObject coin in collidingCoins)
        {
            RemoveCoin(coin);
            // Optionally, you can add logic here to update the score or play a sound effect
        }
    }

    public void PlayCoinSound()
    {
        if (coinSoundPlayer != null)
        {
            coinSoundPlayer.Play();
        }
    }
}