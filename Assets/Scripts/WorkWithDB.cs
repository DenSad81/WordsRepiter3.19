using System;
//using System.Collections;
using System.Collections.Generic;
using System.Data;
//using Unity.VisualScripting;
//using UnityEditor.Search;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEngine.UIElements.UxmlAttributeDescription;
//using UnityEngine.Assertions;

public class WorkWithDB : MonoBehaviour
{
    private DB _db = new DB("mainDB.bytes");

    private void Start()
    {
        //List<Word> words = GetAllWordsFromDB();

        ////foreach (var word in words)
        ////    word.ShowData();

        //Debug.Log("#######################");

        //GetWordFromDB(1960).ShowData();

        //IncreaseCorrectAnswersInTableWords(1960);

        //GetWordFromDB(1960).ShowData();

        //DeleteWordIntoDB(1965);
        //DeleteWordIntoDB(1968);
        //DeleteWordIntoDB(1969);
        //DeleteWordIntoDB(1970);

        //SetCorrectAnswersInTableWords(1960, 333);
        //GetWordFromDB(1960).ShowData();
        //SetAllCorrectAnswersInTableWords(1);
        //GetWordFromDB(1960).ShowData();

        //GetWordFromDB().ShowData();

        //Debug.Log(GetWordFromDB(55555555));//null
        //// Debug.Log(GetWordFromDB(null));

        //AddWordWithoutIdToDB(new Word(1999,"rrr","ttt","fff",1,4));



        //SetModeAutoInTableUsers(true);
        //Debug.Log(ChekIfModeAutoFromTableUsers());

        //SetModeAutoInTableUsers(false);
        //Debug.Log(ChekIfModeAutoFromTableUsers());



        //SetDirectionEnRuInTableUsers(true);
        //Debug.Log(ChekIfDirectionEnRuFromTableUsers());


        //SetDirectionEnRuInTableUsers(false);
        //Debug.Log(ChekIfDirectionEnRuFromTableUsers());


        //Debug.Log(GetQuantityRepitFromTableUsers());
        //SetQuantityRepitInTableUsers(3);
        //Debug.Log(GetQuantityRepitFromTableUsers());
        //SetQuantityRepitFromTableUsers(33);
        //Debug.Log(GetQuantityRepitFromTableUsers());
        //SetQuantityRepitFromTableUsers();
        //Debug.Log(GetQuantityRepitFromTableUsers());

        //доделать-запуск только на сцене слов!!!


        if (SceneManager.GetActiveScene().name == "WordsScene")
            MainProcces.Start(/*this*/);

        //GetDictionaryFromTableDictionaris(3).ShowData();
        //SetStateActivInTableDictionaris(true,3);
        //GetDictionaryFromTableDictionaris(3).ShowData();
    }

    public void RepackDataBase() =>
        _db.RepackDataBase();



    public int GetQuantityRowsFromTableDictionaris() =>
             int.Parse(_db.ExecuteQueryWithAnswer($"SELECT COUNT(*) AS total_rows FROM Dictionaris;"));
    public Dictionary GetDictionaryFromTableDictionaris(int id = 1)
    {
        if (id < 1)
            id = 1;


        DataTable table = _db.ExecuteQueryWithAnswerAsDataTable($"SELECT* FROM Dictionaris WHERE id={id};");
        //table - содержит всего одну строку
        //Debug.Log(table.Rows.Count);
        //Debug.Log(table.Columns.Count);
        if (table.Rows.Count != 1)
            return null;

        return new Dictionary(int.Parse(table.Rows[0][0].ToString()),
                              int.Parse(table.Rows[0][1].ToString()),
                                        table.Rows[0][2].ToString(),
                                        table.Rows[0][3].ToString());
    }
    public bool ChekIfStateActivFromTableDictionaris(int id = 1)
    {
        string temp = _db.ExecuteQueryWithAnswer($"SELECT state FROM Dictionaris WHERE id={id};");

        if (temp == "Activ")
            return true;
        else
            return false;
    }
    public void SetStateActivInTableDictionaris(bool isActiv = true, int id = 1)
    {
        string tt;

        if (isActiv == true)
            tt = "Activ";
        else
            tt = "Passiv";

        _db.ExecuteQueryWithoutAnswer($"UPDATE Dictionaris SET state ='{tt}' WHERE id={id};");
    }


    public int GetQuantityRowsFromTableUsers() =>
           int.Parse(_db.ExecuteQueryWithAnswer($"SELECT COUNT(*) AS total_rows FROM Users;"));
    public bool ChekIfModeAutoFromTableUsers(int id = 1)
    {
        string temp = _db.ExecuteQueryWithAnswer($"SELECT modeChange FROM Users WHERE id={id};");

        if (temp == "Auto")
            return true;
        else
            return false;
    }
    public void SetModeAutoInTableUsers(bool isAutoModeChange = true, int id = 1)
    {
        string tt;

        if (isAutoModeChange == true)
            tt = "Auto";
        else
            tt = "Manual";

        _db.ExecuteQueryWithoutAnswer($"UPDATE Users SET modeChange ='{tt}' WHERE id={id};");
    }
    public bool ChekIfDirectionEnRuFromTableUsers(int id = 1)
    {
        string temp = _db.ExecuteQueryWithAnswer($"SELECT direction FROM Users WHERE id={id};");

        if (temp == "EnRu")
            return true;
        else
            return false;
    }
    public void SetDirectionEnRuInTableUsers(bool directionEnRu = true, int id = 1)
    {
        string tt;

        if (directionEnRu == true)
            tt = "EnRu";
        else
            tt = "RuEn";

        _db.ExecuteQueryWithoutAnswer($"UPDATE Users SET direction ='{tt}' WHERE id={id};");
    }
    public int GetQuantityRepitFromTableUsers(int id = 1)
    {
        string temp = _db.ExecuteQueryWithAnswer($"SELECT quantityRepit FROM Users WHERE id={id};");
        return int.Parse(temp);
    }
    public void SetQuantityRepitInTableUsers(int quantityRepit = 1, int id = 1)
    {
        _db.ExecuteQueryWithoutAnswer($"UPDATE Users SET quantityRepit ={quantityRepit} WHERE id={id};");
    }


    public int GetQuantityRowsFromTableWords() =>
              int.Parse(_db.ExecuteQueryWithAnswer($"SELECT COUNT(*) AS total_rows FROM Words;"));
    public void IncreaseCorrectAnswersInTableWords(int idWord)
    {
        string temp = _db.ExecuteQueryWithAnswer($"SELECT correctAnswers FROM Words WHERE id={idWord};");
        int t = int.Parse(temp);
        t++;
        _db.ExecuteQueryWithoutAnswer($"UPDATE Words SET correctAnswers={t} WHERE id={idWord};");
    }
    public void DecreaseCorrectAnswersInTableWords(int idWord)
    {
        string temp = _db.ExecuteQueryWithAnswer($"SELECT correctAnswers FROM Words WHERE id={idWord};");

        if (temp == null)
            return;

        int value = int.Parse(temp);

        if (value > 0)
            value--;

        _db.ExecuteQueryWithoutAnswer($"UPDATE Words SET correctAnswers = {value} WHERE id={idWord};");
    }
    public void SetCorrectAnswersInTableWords(int idWord, int quantityCorrectAnswers = 1)
    {
        _db.ExecuteQueryWithoutAnswer($"UPDATE Words SET correctAnswers={quantityCorrectAnswers} WHERE id={idWord};");
    }
    public void SetAllCorrectAnswersInTableWords(int quantityCorrectAnswers = 1)
    {
        _db.ExecuteQueryWithoutAnswer($"UPDATE Words SET correctAnswers={quantityCorrectAnswers};");
    }
    public List<Word> GetAllWordsFromTableWords()
    {
        DataTable table = _db.ExecuteQueryWithAnswerAsDataTable("SELECT* FROM Words;");
        // Debug.Log(table.Rows.Count);
        // Debug.Log(table.Columns.Count);
        List<Word> words = new List<Word>();

        for (int i = 0; i < table.Rows.Count; i++)
            words.Add(new Word(int.Parse(table.Rows[i][0].ToString()),
                                         table.Rows[i][1].ToString(),
                                         table.Rows[i][2].ToString(),
                                         table.Rows[i][3].ToString(),
                               int.Parse(table.Rows[i][4].ToString()),
                               int.Parse(table.Rows[i][5].ToString())));
        return words;
    }
    public Word GetWordFromTableWords(int idWord = 1)
    {
        if (idWord < 1)
            idWord = 1;


        DataTable table = _db.ExecuteQueryWithAnswerAsDataTable($"SELECT* FROM Words WHERE id={idWord};");
        //table - содержит всего одну строку
        // Debug.Log(table.Rows.Count);
        // Debug.Log(table.Columns.Count);
        if (table.Rows.Count != 1)
            return null;

        return new Word(int.Parse(table.Rows[0][0].ToString()),
                                  table.Rows[0][1].ToString(),
                                  table.Rows[0][2].ToString(),
                                  table.Rows[0][3].ToString(),
                        int.Parse(table.Rows[0][4].ToString()),
                        int.Parse(table.Rows[0][5].ToString()));
    }
    public void AddWordWithIdToDB(Word word)//DANGER
    {
        if (_db.ExecuteQueryWithAnswer($"SELECT id FROM Words WHERE id={word.Id};") != null)
            throw new InvalidOperationException("This index is bisy");

        _db.ExecuteQueryWithoutAnswer($"INSERT INTO Words VALUES({word.Id}, '{word.WordEn}', '{word.Transcryption}', '{word.WordRu}', {word.Dict_id}, {word.CorrectAnswers});");
    }
    public void AddWordWithoutIdToDB(Word word)
    {
        _db.ExecuteQueryWithoutAnswer($"INSERT INTO Words (wordEn, transcryption, wordRu, dict_id, correctAnswers)" +
                   $"VALUES('{word.WordEn}', '{word.Transcryption}', '{word.WordRu}', {word.Dict_id}, {word.CorrectAnswers});");
    }
    public void DeleteWordIntoDB(int idWord)
    {
        _db.ExecuteQueryWithoutAnswer($"DELETE FROM Words WHERE id={idWord};");
    }
}