//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

public /*static*/ class DB
{
    private /*const*/ string _fileName /*= "mainDB.bytes"*/;
    private /*static*/ string _DBPath;
    private /*static*/ SqliteConnection _connection;
    private /*static*/ SqliteCommand _command;

    /*static*/public DB(string fileName)
    {
        _fileName = fileName;
        _DBPath = GetDatabasePath();
    }

    // Возвращает путь к БД. Если её нет в нужной папке на Андроиде, то копирует её с исходного apk файла.
    private /*static*/ string GetDatabasePath()
    {
        string filePath;
#if UNITY_EDITOR
        filePath = Path.Combine(Application.streamingAssetsPath, _fileName);
      // filePath = Path.Combine("C:/_projects/DB", fileName);
        return filePath;
#endif
#if UNITY_STANDALONE
        filePath = Path.Combine(Application.dataPath, _fileName);
        if (File.Exists(filePath) == false)
            UnpackDatabase(filePath);
        return filePath;
#endif
#if UNITY_ANDROID
         filePath = Path.Combine(Application.persistentDataPath, _fileName);
        if (File.Exists(filePath)==false) 
            UnpackDatabase(filePath);
        return filePath;
#endif
    }

    // Распаковывает базу данных в указанный путь.
    /// <param name="toPath"> Путь в который нужно распаковать базу данных. </param>
    private /*static*/ void UnpackDatabase(string toPath)
    {
        string fromPath = Path.Combine(Application.streamingAssetsPath, _fileName);

        WWW reader = new WWW(fromPath);
        while (reader.isDone == false)
        { }

        File.WriteAllBytes(toPath, reader.bytes);
    }

    /// <summary> Этот метод открывает подключение к БД. </summary>
    private /*static*/ void OpenConnection()
    {
        _connection = new SqliteConnection("Data Source=" + _DBPath);
        _command = new SqliteCommand(_connection);
        _connection.Open();
    }

    /// <summary> Этот метод закрывает подключение к БД. </summary>
    public /*static*/ void CloseConnection()
    {
        _connection.Close();
        _command.Dispose();
    }

    /// <summary> Этот метод выполняет запрос query. </summary>
    /// <param name="query"> Собственно запрос. </param>
    public /*static*/ void ExecuteQueryWithoutAnswer(string query)
    {
        OpenConnection();
        _command.CommandText = query;
        _command.ExecuteNonQuery();
        CloseConnection();
    }

    /// <summary> Этот метод выполняет запрос query и возвращает ответ запроса. </summary>
    /// <param name="query"> Собственно запрос. </param>
    /// <returns> Возвращает значение 1 строки 1 столбца, если оно имеется. </returns>
    public /*static*/ string ExecuteQueryWithAnswer(string query)
    {
        OpenConnection();
        _command.CommandText = query;
        var answer = _command.ExecuteScalar();
        CloseConnection();

        if (answer != null)
            return answer.ToString();
        else
            return null;
    }

    /// <summary> Этот метод возвращает таблицу, которая является результатом выборки запроса query. </summary>
    /// <param name="query"> Собственно запрос. </param>
    //public static DataTable GetTable(string query)
    //{
    //    OpenConnection();

    //    SqliteDataAdapter adapter = new SqliteDataAdapter(query, connection);
    //    DataSet DS = new DataSet();

    //    adapter.Fill(DS);
    //    adapter.Dispose();

    //    CloseConnection();

    //    return DS.Tables[0];
    //}

    public /*static*/ DataTable ExecuteQueryWithAnswerAsDataTable(string query)
    {
        OpenConnection();

        SqliteDataAdapter adapter = new SqliteDataAdapter(query, _connection);
        DataSet DS = new DataSet();

        adapter.Fill(DS);
        adapter.Dispose();

        CloseConnection();

        return DS.Tables[0];
    }
}