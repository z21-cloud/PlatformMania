using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlatfromMania.Helpers;
using PlatfromMania.Core;

public class CoinPool : MonoBehaviour, IPool<Coin>
{
    [SerializeField] private Coin coin;
    [SerializeField] private Transform coinsParent;
    [SerializeField] private int poolSize = 10;

    private ObjectPooling<Coin> pool;

    private void Awake()
    {
        pool = new ObjectPooling<Coin>(
            coin,
            poolSize,
            coinsParent);
    }

    public Coin Get() => pool.Get();

    public void Release(Coin coin) => pool.Release(coin);
}

