using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SQLite4Unity3d;
using UnityEngine;
public class DBManager : IManager
{
    private SQLiteConnection _connection;
    private const string DBName = "GameDB.db";
    async UniTask IManager.Init()
    {
        var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DBName);
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        this._connection.CreateTable<Account>();
    }
    public async UniTask<int> AsyncInsert<T>(T data) where T : IDBData
    {
        var result = await UniTask.RunOnThreadPool(() => _connection.Insert(data));
        return result;
    }
    public async UniTask<int> AsyncUpdate<T>(T data) where T : IDBData
    {
        var result = await UniTask.RunOnThreadPool(() => _connection.Update(data));
        return result;
    }
    public async UniTask<int> AsyncDelete<T>(T data) where T : IDBData
    {
        var result = await UniTask.RunOnThreadPool(() => _connection.Delete(data));
        return result;
    }
    public async UniTask<List<T>> AsyncQuery<T>(string sql, params object[] args) where T : IDBData, new()
    {
        var result = await UniTask.RunOnThreadPool(() => _connection.Query<T>(sql, args));
        return result;
    }

    public void Dispose()
    {
        _connection.Close();
    }
}