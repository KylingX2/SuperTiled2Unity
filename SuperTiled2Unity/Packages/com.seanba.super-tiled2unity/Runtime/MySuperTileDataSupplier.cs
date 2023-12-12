using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SuperTiled2Unity
{ 
    [System.Serializable]
    public class SuperTileTypeData
    {
        public int tileId;
        public float animMinSpeed;
        public float animMaxSpeed;
    }

    public class MySuperTileDataSupplier : MonoBehaviour
    {
        [SerializeField] protected List<SuperTileTypeData> _superTileTypeDatas;

        protected static bool _isCheckedForInstance;
        protected static MySuperTileDataSupplier _instance;
        public static MySuperTileDataSupplier Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                if (!_isCheckedForInstance)
                {
                    _isCheckedForInstance = true;
                    _instance = FindAnyObjectByType<MySuperTileDataSupplier>();
                }
                return _instance;
            }
        }

        protected void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogError("MySuperTileDataSupplier.Instance already exists!");
            }
            _instance = this;
        }

        public float GetSuperTileAnimationSpeed(int tileId, ITilemap itilemap)
        {
            if (_superTileTypeDatas != null)
            {
                var tilemap = itilemap.GetComponent<Tilemap>();
                for (var i = 0; i < _superTileTypeDatas.Count; i++)
                {
                    var data = _superTileTypeDatas[i];
                    if (data != null && data.tileId == tileId)
                    {
                        var animSpeed = Random.Range(data.animMinSpeed, data.animMaxSpeed);
                        return animSpeed;
                    }
                }
            }
            return 1.0f;
        }
    }
}
