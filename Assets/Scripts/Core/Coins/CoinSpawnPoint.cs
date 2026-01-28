using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoinSpawnPoint : MonoBehaviour, ISpawnPoint<Coin>
{
    public Coin Current { get; private set; }

    public void Assign(Coin coin)
    {
        Current = coin;
        coin.transform.SetPositionAndRotation(
            transform.position,
            transform.rotation
        );
    }

    public void Clear()
    {
        Current = null;
    }
}

