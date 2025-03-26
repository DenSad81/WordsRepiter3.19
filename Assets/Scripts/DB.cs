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

    // ���������� ���� � ��. ���� � ��� � ������ ����� �� ��������, �� �������� � � ��������� apk �����.
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

    // ������������� ���� ������ � ��������� ����.
    /// <param name="toPath"> ���� � ������� ����� ����������� ���� ������. </param>
    private /*static*/ void UnpackDatabase(string toPath)
    {
        string fromPath = Path.Combine(Application.streamingAssetsPath, _fileName);

        WWW reader = new WWW(fromPath);
        while (reader.isDone == false)
        { }

        File.WriteAllBytes(toPath, reader.bytes);
    }

    /// <summary> ���� ����� ��������� ����������� � ��. </summary>
    private /*static*/ void OpenConnection()
    {
        _connection = new SqliteConnection("Data Source=" + _DBPath);
        _command = new SqliteCommand(_connection);
        _connection.Open();
    }

    /// <summary> ���� ����� ��������� ����������� � ��. </summary>
    public /*static*/ void CloseConnection()
    {
        _connection.Close();
        _command.Dispose();
    }

    /// <summary> ���� ����� ��������� ������ query. </summary>
    /// <param name="query"> ���������� ������. </param>
    public /*static*/ void ExecuteQueryWithoutAnswer(string query)
    {
        OpenConnection();
        _command.CommandText = query;
        _command.ExecuteNonQuery();
        CloseConnection();
    }

    /// <summary> ���� ����� ��������� ������ query � ���������� ����� �������. </summary>
    /// <param name="query"> ���������� ������. </param>
    /// <returns> ���������� �������� 1 ������ 1 �������, ���� ��� �������. </returns>
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

    /// <summary> ���� ����� ���������� �������, ������� �������� ����������� ������� ������� query. </summary>
    /// <param name="query"> ���������� ������. </param>
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