using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{

    public MiniEnemyController miniEnemy;
    public BombEnemyController bombEnemy;
    public LazerEnemyController lazerEnemy;

    private ObjectPool<BombEnemyController> _bombPool;
    private ObjectPool<MiniEnemyController> _miniPool;
    private ObjectPool<LazerEnemyController> _lazerPool;

    public ObjectPool<BombEnemyController> BombPool { get { return _bombPool; } }
    public ObjectPool<MiniEnemyController> MiniPool { get { return _miniPool; } }
    public ObjectPool<LazerEnemyController> LazerPool { get { return _lazerPool; } }

    // Start is called before the first frame update
    void Start()
    {

        _bombPool = new ObjectPool<BombEnemyController>(() =>
        {
            return Instantiate(bombEnemy.Clone() as BombEnemyController);
        }, bomb =>
        {
            bomb.gameObject.SetActive(true);
        }, bomb =>
        {
            bomb.gameObject.SetActive(false);
        }, bomb =>
        {
            Destroy(bomb);
        }, true, defaultCapacity: 5, maxSize: 10);

        _miniPool = new ObjectPool<MiniEnemyController>(() =>
        {
            return Instantiate(miniEnemy.Clone() as MiniEnemyController);
        }, mini =>
        {
            mini.gameObject.SetActive(true);
        }, mini =>
        {
            mini.gameObject.SetActive(false);
        }, mini =>
        {
            Destroy(mini);
        }, true, defaultCapacity: 5, maxSize: 10);

        _lazerPool = new ObjectPool<LazerEnemyController>(() =>
        {
            return Instantiate(lazerEnemy.Clone() as LazerEnemyController);
        }, lazer =>
        {
            lazer.gameObject.SetActive(true);
        }, lazer =>
        {
            lazer.gameObject.SetActive(false);
        }, lazer =>
        {
            Destroy(lazer);
        }, true, defaultCapacity: 5, maxSize: 10);

    }

}
